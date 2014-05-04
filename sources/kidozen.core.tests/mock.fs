module MockTests

open NUnit.Framework
open KidoZenCloudClient
open KidoZenTypesParser
open Protocol
open TestValues
open Microsoft.Owin.Testing
open Owin
open UnitTests

open  System.Collections.Generic

//por el momento no consegui hacer que owin me devuelva status codes distintos de 200 desde f# ya que no esta marcado como mutable ese atributo
let owinServiceMock expectedContent (expectedBody:string) = 
    use server = TestServer.Create (fun app ->  
        (
            app.UseHandlerAsync(fun req (res: Types.OwinResponse) -> 
                (
                    res.SetHeader("Content-Type", expectedContent ) |> ignore
                    res.WriteAsync(expectedBody) 
                )
        ) |> ignore
    ))
    server.HttpClient.GetAsync("/api/test").Result.Content.ReadAsStringAsync().Result

let [<Test>] ``should parse configuration as Application`` () = 
    //mock
    let mockedconfig appcenter appname =
        owinServiceMock "application/json" TestValues.appconfigasstring |> parseApplicationSettings

    let initializeWithMock = initialize mockedconfig
    //
    let settings = initializeWithMock "armonia" "tasks" |> Async.RunSynchronously
    match settings with 
        | Application app -> 
            Assert.AreEqual("http://tasks.armonia.kidocloud.com/", app.Scope)         
            Assert.AreEqual("https://armonia.kidocloud.com/auth/v1/WRAPv0.9", app.AuthEndpoint)
            Assert.AreEqual("https://kido-armonia.accesscontrol.windows.net/", app.ServiceScope)
        | _ -> Assert.Fail()


let [<Test>] ``should get token for MarketPlace`` () = 
    //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials
    let mockedtokenrequest tokenrequest =
        owinServiceMock "application/json" TestValues.wrapv9tokenasstring 

    let authenticateWithMock = authenticate mockedtokenrequest
    // 
    let token = authenticateWithMock tokenRequest |> Async.RunSynchronously
    match token with
    |  t -> Assert.NotNull(t)
    | _ -> Assert.Fail()

let [<Test>] ``should get token for Application`` () = 
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks"
    let mockedtokenrequest tokenrequest =
        owinServiceMock "application/json" TestValues.wrapv9tokenasstring 

    let authenticateWithMock = authenticate mockedtokenrequest
    // 
    let token = authenticateWithMock tokenRequest |> Async.RunSynchronously
    match token with
    |  t -> Assert.NotNull(t)
    | _ -> Assert.Fail()

let [<Test>] ``should get token for Application using Provider`` () = 
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks" |> usingProvider "x"
    let mockedtokenrequest tokenrequest =
        owinServiceMock "application/json" TestValues.wrapv9tokenasstring 

    let authenticateWithMock = authenticate mockedtokenrequest
    
    // 
    let token = authenticateWithMock tokenRequest |> Async.RunSynchronously
    match token with
    |  t -> Assert.NotNull(t)
    | _ -> Assert.Fail()


