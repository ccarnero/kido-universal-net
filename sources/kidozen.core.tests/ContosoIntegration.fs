module ContosoIntegration

open NUnit.Framework
open Protocol
open KidoZenCloudClient
open System.Web
open System.Web.Http 
open System.Net
open KidoZenTypesParser
open System.Collections.Generic
open TestValues

// kidozen , should I added to core?
type UserForApplication = {
    Usr: KidoUser;      
    App: Application
}

//
// need settings before getting token, thats why async.RunSync....
// need token before call any service, thats why async.RunSync....
//
let getUserAndApp = 
    let credentials =  { name = "contoso@kidozen.com"; secret = "pass"}
    let tokenRequest = createTokenRequest "https://contoso.local.kidozen.com" credentials |> forApplication "tasks"
    let settings = initialize getApplicationConfiguration "https://contoso.local.kidozen.com" "tasks" |> Async.RunSynchronously
    match settings with
    | Application a ->
        let token = authenticate getKidoZenToken tokenRequest |> Async.RunSynchronously
        match token with
            | Success t -> 
                let user = t |> parseUserToken;
                match user with
                    | KidoUser u -> { Usr = u ; App=a }
                    | _ -> failwith "invalid user"        
            | _ -> failwith "invalid token"
    | _ -> failwith "invalid settings"

let [<Test>] ``should invoke service with user and application`` () =
    ServicePointManager.ServerCertificateValidationCallback  <- (fun _ _ _ _  -> true);
    let userforapp = getUserAndApp
    let apiRequest = createEapiRequest userforapp.App userforapp.Usr "Weather" "get" |> withJsonBody "{\"path\":\"?q=buenos aires,ar\"}"
    let apiResponse = invokeEApi callService apiRequest |> Async.RunSynchronously
    match apiResponse with
    | EApiResponse r ->
        Assert.AreEqual(200,r.Status);
    | ServiceFail f-> Assert.Fail()

    
//Para la llamada a los servicios, en el tokenRequest deberia incluirse el parametro "url q viene en la aplicacion