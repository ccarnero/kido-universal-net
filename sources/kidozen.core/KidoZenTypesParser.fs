module KidoZenTypesParser
open Protocol
open FSharp.Net
open FSharp.Data
open FSharp.Data.Json
open FSharp.Data.Json.Extensions

exception ApplicationParseException of string

type TypesParserResult =
    | Application of Application
    | KidoUser of KidoUser
    | Fail of System.Exception 

let parseApplicationSettings value =
    try
        let config = JsonValue.Parse value
        let authconfig = config.[0]?authConfig
        let providers = authconfig?identityProviders
        let app = { 
            Url = config.[0]?url.AsString()
            Scope = authconfig?applicationScope.AsString(); 
            ServiceScope = authconfig?authServiceScope.AsString(); 
            AuthEndpoint = authconfig?authServiceEndpoint.AsString(); 
            Providers = providers.Properties 
                |> Seq.toList 
                |> List.map (
                    fun(a,b) -> 
                        { 
                            Name = a; 
                            Protocol= b.GetProperty("protocol").InnerText
                            Endpoint= b.GetProperty("endpoint").InnerText
                        }
                )
            Services = config.[0].Properties 
                |> Seq.filter(fun (k,v) -> (["storage";"queue";"service"] |> List.exists( fun x-> x=k )) )
                |> Seq.map(fun (x,y) -> {Service =x; Endpoint = y.InnerText}) |> Seq.toList             
        }
        Application app
    with | :?System.Exception as ex ->
        Fail ex

let parseUserToken value = 
    try
        let config = JsonValue.Parse value 
        let user = { 
            Token = config?rawToken.AsString() ; 
            Expires = config?expirationTime.AsString()
        }
        KidoUser user
    with | :?System.Exception as ex ->
        Fail ex

        
type EApiServiceResult =
    | EApiResponse of EApiResponse
    | ServiceFail of System.Exception 

let internal parseServiceResponse response =
    try
        let value = JsonValue.Parse response 
        let r = {
            Status = value?data?status.AsInteger();
            Body = value?data?body;
        }
        EApiResponse r
    with | :?System.Exception as ex ->
        ServiceFail ex

