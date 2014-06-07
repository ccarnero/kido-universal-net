module KidoZenCloudClient

open HttpClient
open Utilities
open Protocol
open KidoZenTypesParser

let getApplicationConfiguration appcenter appname =
    let url = match appcenter with
    | Prefix "https://" rest -> sprintf "%s/publicapi/apps?name=%s" appcenter appname
    | Prefix "http://" rest -> sprintf "%s/publicapi/apps?name=%s" appcenter appname
    | _ -> sprintf "https://%s.kidocloud.com/publicapi/apps?name=%s" appcenter appname 
    createRequest Get url |> getResponseBody |> parseApplicationSettings
    
// IP's tokens
let getWrapv9Token ipEndpoint scope (credentials:KidoCredentials) =
    let body = sprintf "wrap_name=%s&wrap_password=%s&wrap_scope=%s" credentials.name credentials.secret scope
    let content = createRequest Post ipEndpoint |> withBody body |> withHeader (ContentType "application/x-www-form-urlencoded") |> getResponseBody   
    let indexBegin = content.IndexOf("<Assertion ");
    let indexEnd = content.IndexOf("</Assertion>") + 12;
    content.Substring(indexBegin, indexEnd - indexBegin);

let getWSTrustToken ipEndpoint scope credentials = getWrapv9Token ipEndpoint scope credentials

//KidoZen token
let getTokenFromKidozenIdentity endpoint protocol scope token =
    let body = sprintf "wrap_assertion_format=%s&wrap_assertion=%s&wrap_scope=%s" protocol token scope
    createRequest Post endpoint |> withBody body |> withHeader (ContentType "application/x-www-form-urlencoded") |> getResponseBody   

let internal getApplicationToken tokenRequest settings = 
    let provider = match tokenRequest.Provider with
    | None -> settings.Providers.[0]
    | Some provider -> List.find( fun x -> x.Name = provider) settings.Providers  
    let iptoken = getWrapv9Token provider.Endpoint settings.ServiceScope tokenRequest.Credentials
    let encodedScope = System.Net.WebUtility.UrlEncode settings.Scope
    let encodedtoken = System.Net.WebUtility.UrlEncode iptoken            
    getTokenFromKidozenIdentity settings.AuthEndpoint "SAML" encodedScope encodedtoken                

let internal getMarketPlaceToken tokenRequest = 
    let scope = sprintf "https://kido-%s.accesscontrol.windows.net/" tokenRequest.Marketplace 
    let iptoken = getWrapv9Token "https://identity.kidozen.com/WRAPv0.9" scope  tokenRequest.Credentials
    let ipEndpoint = sprintf "https://%s.kidocloud.com/auth/v1/WRAPv0.9" tokenRequest.Marketplace
    let scope2 = sprintf "http://%s.kidocloud.com/" tokenRequest.Marketplace 
    let encodedScope = System.Net.WebUtility.UrlEncode scope2
    let encodedtoken = System.Net.WebUtility.UrlEncode iptoken            
    getTokenFromKidozenIdentity ipEndpoint "SAML" encodedScope encodedtoken                

type GetKidoZenTokenResult =
    | Success of string
    | Error of System.Exception 
                        
let getKidoZenToken (tokenRequest:TokenRequest) =
    try
        match tokenRequest.Application with
        | Some application ->
            let initializeApplication = initialize getApplicationConfiguration
            let settings = initializeApplication tokenRequest.Marketplace application |> Async.Catch |> Async.RunSynchronously
            match settings with
            | Choice1Of2 app ->
                match app with
                | Application a -> 
                    Success (getApplicationToken tokenRequest a)
                | Fail e-> Error e
            | Choice2Of2 ex -> failwith ex.Message
        | None ->
            Success (getMarketPlaceToken tokenRequest)
    with | :?System.Exception as ex ->
        Error ex    

type CallServiceResult =
    | StatusCode of int
    | Error of System.Exception 
            
let callService (apiRequest:EapiRequest) =
    let url = apiRequest.BaseEndpoint + "/" + apiRequest.Name + "/invoke/" + apiRequest.Method
    createRequest Post url 
                |> withHeader (Authorization apiRequest.Token) 
                |> withBody apiRequest.Body.Value
                |> getResponseBody 
                |> parseServiceResponse
    
