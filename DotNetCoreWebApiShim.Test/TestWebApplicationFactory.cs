using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNetCoreWebApiShim.Test
{
    /// <summary>
    /// ユニットテスト用の<see cref="WebApplicationFactory{TEntryPoint}"/>
    /// </summary>
    internal class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
    }
}
