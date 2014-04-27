module UnitTests

open NUnit.Framework
open KidoZen

let appconfigasstring = "{\"_id\":\"50ca314e3ccb54000a000001\",\"androidUrl\":\"\",\"categories\":[\"productivity\"],\"customUrl\":\"\",\"description\":\"Use this tasks management application to keep track of the pending tasks that your team has.\n\n - Create Tasks\n - Complete Tasks\n - Delete Tasks\n - Send Tasks by Email\",\"displayName\":\"tasks\",\"domain\":\"armonia.kidocloud.com\",\"gitPath\":\"c:/jazz-data/repositories/50ca314e3ccb54000a000001.git\",\"html5Url\":\"https://tasks.armonia.kidocloud.com/\",\"iosUrl\":\"https://tasks.armonia.kidocloud.com\",\"name\":\"tasks\",\"path\":\"applications/armonia/50ca314e3ccb54000a000001\",\"port\":5236,\"published\":true,\"ratingCount\":1,\"ratingSum\":5,\"shortDescription\":\"\",\"tile\":\"/uploads/apps/50ca314e3ccb54000a000001-1384877462743.png\",\"uploads\":\"applications/armonia/50ca314e3ccb54000a000001.uploads\",\"version\":\"0.0.1\",\"voters\":[\"50b625dfef330f2c0d000001\"],\"win8Url\":\"\",\"wp7Url\":\"\",\"url\":\"https://tasks.armonia.kidocloud.com/\",\"gitUrl\":\"http://git.armonia.kidocloud.com/50ca314e3ccb54000a000001.git/\",\"ftp\":\"ftp://armonia.kidocloud.com/50ca314e3ccb54000a000001\",\"ws\":\"ws://ws.tasks.armonia.kidocloud.com/\",\"notification\":\"https://tasks.armonia.kidocloud.com/notifications\",\"storage\":\"https://tasks.armonia.kidocloud.com/storage/local\",\"queue\":\"https://tasks.armonia.kidocloud.com/queue/local\",\"pubsub\":\"https://tasks.armonia.kidocloud.com/pubsub/local\",\"config\":\"https://tasks.armonia.kidocloud.com/config\",\"logging\":\"https://tasks.armonia.kidocloud.com/logging\",\"email\":\"https://tasks.armonia.kidocloud.com/email\",\"sms\":\"https://tasks.armonia.kidocloud.com/sms\",\"service\":\"https://tasks.armonia.kidocloud.com/api/services\",\"files\":\"https://tasks.armonia.kidocloud.com/uploads\",\"img\":\"https://armonia.kidocloud.com/uploads/apps/50ca314e3ccb54000a000001-1384877462743.png\",\"rating\":5,\"authConfig\":{\"applicationScope\":\"http://tasks.armonia.kidocloud.com/\",\"authServiceScope\":\"https://kido-armonia.accesscontrol.windows.net/\",\"authServiceEndpoint\":\"https://armonia.kidocloud.com/auth/v1/WRAPv0.9\",\"identityProviders\":[{\"name\" : \"Kidozen\", \"protocol\":\"wrapv0.9\",\"endpoint\":\"https://identity.dev.kidozen.com/WRAPv0.9\"}]}}"

let wrapv9tokenasstring = "{\"rawToken\":\"http%3A%2F%2Fschemas.kidozen.com%2Fusersource%3DI-Mobility%20(i-Mobility)%26http%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fclaims%2Fauthenticationinstant%3D2014-01-08T15%3A32%3A49.418Z%26http%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fclaims%2Fauthenticationmethod%3Dhttp%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fauthenticationmethod%2Fpassword%26http%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fclaims%2Fdateofbirth%3D1%2F1%2F1989%2012%3A00%3A00%20AM%26http%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fclaims%2Ffirstname%3DAdmin%26http%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fclaims%2Flastname%3DAdmin%26http%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fclaims%2Frole%3DIdentityServerAdministrators%2CIdentityServerUsers%26http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fname%3Dadmin%26http%3A%2F%2Fschemas.kidozen.com%2Frole%3DApplication%20Admin%26http%3A%2F%2Fschemas.kidozen.com%2Faction%3Dallow%20all%20*%26http%3A%2F%2Fschemas.microsoft.com%2Faccesscontrolservice%2F2010%2F07%2Fclaims%2Fidentityprovider%3Dhttp%3A%2F%2Fidentityserver.v2.thinktecture.com%2Fsamples%26Audience%3Dhttp%3A%2F%2Ftooltracker.happiestminds.dev.kidozen.com%2F%26ExpiresOn%3D1389198778%26Issuer%3Dhttps%3A%2F%2Fkido-hm.accesscontrol.windows.net%2F%26HMACSHA256%3DQ4g%2B4QDnV%2Fn2orQRAGzIaIenA6W0tA87n6%2BfiWhBlfo%3D%0A\", \"expirationTime\" : \"2000\"}"

let serviceresponse_array_asstring =   "{\"data\": {\"status\": 200,\"headers\": {\"cache-control\": \"no-cache\",\"pragma\": \"no-cache\",\"content-type\": \"application/json; charset=utf-8\",\"expires\": \"-1\",\"server\": \"Microsoft-IIS/7.5\",\"x-aspnet-version\": \"4.0.30319\",\"x-powered-by\": \"ASP.NET\",\"date\": \"Sat, 08 Feb 2014 20:29:48 GMT\",\"content-length\": \"18161\"},\"body\": [{\"Id\": \"60966963\",\"Status\": \"Pending\",\"SubStatus\": \"Wai\"}]}}";

let serviceresponse_asstring =   "{\"data\": {\"status\": 200,\"headers\": {\"cache-control\": \"no-cache\",\"pragma\": \"no-cache\",\"content-type\": \"application/json; charset=utf-8\",\"expires\": \"-1\",\"server\": \"Microsoft-IIS/7.5\",\"x-aspnet-version\": \"4.0.30319\",\"x-powered-by\": \"ASP.NET\",\"date\": \"Sat, 08 Feb 2014 20:29:48 GMT\",\"content-length\": \"18161\"},\"body\": {\"Id\": \"60966963\",\"Status\": \"Pending\",\"SubStatus\": \"Wai\"}}}";

let [<Test>] ``should parse configuration as Application`` () = 
    //mock
    let mockedconfig appcenter appname =
        appconfigasstring
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
    let getmockedtoken tokenRequest =
        wrapv9tokenasstring 
    let authenticateWithMock = authenticate getmockedtoken
    // 
    let token = authenticateWithMock tokenRequest
    Assert.AreEqual(token.Expires,"2000")

let [<Test>] ``should get token for Application`` () = 
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks"
    let getmockedtoken tokenRequest =
        wrapv9tokenasstring 
    let authenticateWithMock = authenticate getmockedtoken
    // 
    let token = authenticateWithMock tokenRequest
    Assert.AreEqual(token.Expires,"2000")

let [<Test>] ``should get token for Application using Provider`` () = 
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks" |> usingProvider "x"
    let getmockedtoken tokenRequest =
        wrapv9tokenasstring 
    let authenticateWithMock = authenticate getmockedtoken
    // =>
    let token = authenticateWithMock tokenRequest
    Assert.AreEqual(token.Expires,"2000")

let [<Test>] `` should invoke service as array`` () =
     //mock
    let credentials =  { name = "christian"; secret = "pass"}
    let tokenRequest = createTokenRequest  "armonia" credentials |> forApplication "tasks" |> usingProvider "x"
    let getmockedtoken tokenRequest =
        wrapv9tokenasstring 
    let authenticateWithMock = authenticate getmockedtoken
    // 
    let user = authenticateWithMock tokenRequest
    let apiRequest = createEapiRequest user  "my_rest_service" |> withParameters ""
    let invokeSvcMock  apiRequest =
        serviceresponse_array_asstring
    let invokeWithMock =  invokeEApi invokeSvcMock
    // =>
    let apiResponse = invokeWithMock apiRequest
    Assert.AreEqual(200,apiResponse.Status);

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
        serviceresponse_asstring
    let invokeWithMock =  invokeEApi invokeSvcMock
    // =>
    let apiResponse = invokeWithMock apiRequest
    Assert.AreEqual(200,apiResponse.Status);
