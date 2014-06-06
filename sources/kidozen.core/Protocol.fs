module Protocol

open HttpClient
open FSharp.Net
open FSharp.Data
open FSharp.Data.Json
open FSharp.Data.Json.Extensions

// Get Application Endpoints and provider configuration
type ServiceConfiguration = {
    Service: string 
    Endpoint: string
}

type ProviderConfiguration = {
    Name: string
    Protocol: string 
    Endpoint: string
}

type Application = {
    Url : string
    Scope : string
    ServiceScope: string 
    AuthEndpoint: string 
    Providers : List<ProviderConfiguration>
    Services : List<ServiceConfiguration> 
}

// "initialize" debe ser invocado cuando se especifica una aplicacion.
let initialize fnGetSettings appcenter application =  async {
    let app = fnGetSettings appcenter application
    return app
}
// Authentication
type KidoCredentials = { 
    name:string; secret:string 
}
type TokenRequest = {
    Credentials : KidoCredentials
    Marketplace : string
    Application : string option
    Provider : string option 
}
// token request creation
let createTokenRequest marketplace credentials = {
    Marketplace = marketplace;
    Credentials = credentials;
    Provider = None;
    Application = None
}

let forApplication appname request  = { 
    request with Application = Some(appname) 
}

let usingProvider name request = {
    request with Provider = Some (name)
}
// kidozen
type KidoUser = {
    Token: string;      
    Expires: string
}

let authenticate fnGetToken tokenRequest = async {
    return fnGetToken tokenRequest 
}

// Enterprise API  
type EApiResponse =  {
    Status : int;
    Body : JsonValue
}

type EapiRequest = {
    User : KidoUser
    Name : string
    Method : string
    Application : Application
    Body : string option
    Options : string option
}

let createEapiRequest application user apiname apimethod = {
    User = user;
    Application = application;
    Name = apiname;
    Method = apimethod;
    Body = None;
    Options = None
}

let withJsonBody body (request:EapiRequest) = {
    request with Body = Some (body)
}

let andOptions options request = {
    request with Options = Some (options)
}

let invokeEApi fnServiceInvoke request = async {
    return fnServiceInvoke request 
 }

//DataSources