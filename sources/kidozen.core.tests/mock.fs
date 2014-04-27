module MockTests

open NUnit.Framework
open KidoZen

open Microsoft.Owin.Testing
open Owin
open System.Web
open System.Web.Http 

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
        owinServiceMock "application/json" UnitTests.appconfigasstring 

    let initializeWithMock = initialize mockedconfig
    //
    let settings = initializeWithMock "armonia" "tasks"
    Assert.AreEqual("http://tasks.armonia.kidocloud.com/", settings.Scope)         
    Assert.AreEqual("https://armonia.kidocloud.com/auth/v1/WRAPv0.9", settings.AuthEndpoint)
    Assert.AreEqual("https://kido-armonia.accesscontrol.windows.net/", settings.ServiceScope)


let [<Test>] ``should get token for MarketPlace`` () = 
    //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials
    let mockedtokenrequest tokenrequest =
        owinServiceMock "application/json" UnitTests.wrapv9tokenasstring 

    let authenticateWithMock = authenticate mockedtokenrequest
    // 
    let token = authenticateWithMock tokenRequest
    Assert.AreEqual(token.Expires,"2000")

let [<Test>] ``should get token for Application`` () = 
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks"
    let mockedtokenrequest tokenrequest =
        owinServiceMock "application/json" UnitTests.wrapv9tokenasstring 

    let authenticateWithMock = authenticate mockedtokenrequest
    // 
    let token = authenticateWithMock tokenRequest
    Assert.AreEqual(token.Expires,"2000")

let [<Test>] ``should get token for Application using Provider`` () = 
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks" |> usingProvider "x"
    let mockedtokenrequest tokenrequest =
        owinServiceMock "application/json" UnitTests.wrapv9tokenasstring 

    let authenticateWithMock = authenticate mockedtokenrequest
    
    // 
    let token = authenticateWithMock tokenRequest
    Assert.AreEqual(token.Expires,"2000")


let [<Test>] `` should invoke service`` () =
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks" |> usingProvider "x"
    let getmockedtoken tokenRequest =
        wrapv9tokenasstring 
    let authenticateWithMock = authenticate getmockedtoken
    // 
    let user = authenticateWithMock tokenRequest
    let apiRequest = createEapiRequest user  "my_rest_service" 
    let invokeSvcMock  apiRequest =
         owinServiceMock "application/json" UnitTests.serviceresponse_asstring
    let invokeWithMock =  invokeEApi invokeSvcMock
    // =>
    let apiResponse = invokeWithMock apiRequest
    Assert.AreEqual(200,apiResponse.Status);

