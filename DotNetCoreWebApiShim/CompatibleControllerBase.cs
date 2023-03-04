using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;

namespace DotNetCoreWebApiShim
{
    /// <summary>
    /// ASP.NET Web API との互換性のため <see cref="HttpRequestMessage"/> を保持するコントローラ
    /// </summary>
    public abstract class CompatibleControllerBase : ControllerBase
    {
        private HttpRequestMessage? cachedRequestMessage;

        /// <summary>
        /// <see cref="HttpRequestMessage"/>
        /// </summary>
        protected HttpRequestMessage RequestMessage
        {
            get
            {
                if (cachedRequestMessage != null)
                {
                    return cachedRequestMessage;
                }

                // NOTE 同期処理による例外が発生するためオプションを変更
                var controlFeature = Request.HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (controlFeature != null)
                {
                    controlFeature.AllowSynchronousIO = true;
                }

                cachedRequestMessage = new HttpRequestMessageFeature(Request.HttpContext)
                    .HttpRequestMessage;

                return cachedRequestMessage;
            }
        }
    }
}
