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

- `HttpResponseMessage` の生成は、ヘルパーメソッドを利用します。
```cs
public static HttpResponseMessage CreateResponse<T>(this HttpRequest request, HttpStatusCode statusCode, T value)
{
    SetAllowSynchronous(request);
    return new HttpRequestMessageFeature(request.HttpContext)
        .HttpRequestMessage
        .CreateResponse(statusCode, value);
}
```