# dotnet-core-webapi-shim
ASP.NET Core の WebAPI で 互換性パッケージによる実装サンプル

## Feature
- .NET6
- ASP.NET Core
- Microsoft.AspNetCore.Mvc.WebApiCompatShim

## Note
- ASP.NET Web API で利用していた `HttpResponseMessage` をコントローラのレスポンスで利用するサンプルです。

- アプリケーション起動時にて `AddWebApiConventions` を設定します。
```cs
builder.Services.AddControllers()
    .AddWebApiConventions();
```

- `HttpResponseMessage` は、`CompatibleControllerBase` の `RequestMessage` を利用します。

```cs
[HttpGet("GetWeatherForecastLegacy")]
public HttpResponseMessage GetLegacy()
{
    return RequestMessage.CreateResponse(HttpStatusCode.OK, GetWeatherForecast());
}
```