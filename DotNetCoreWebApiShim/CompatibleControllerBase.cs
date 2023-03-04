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
        private HttpRequestMessage? _cachedRequestMessage;

        /// <summary>
        /// <see cref="HttpRequestMessage"/>
        /// </summary>
        protected HttpRequestMessage RequestMessage
        {
            get
            {
                if (_cachedRequestMessage != null)
                {
                    return _cachedRequestMessage;
                }

                // NOTE 同期処理による例外が発生するためオプションを変更
                var controlFeature = Request.HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (controlFeature != null)
                {
                    controlFeature.AllowSynchronousIO = true;
                }

                _cachedRequestMessage = new HttpRequestMessageFeature(Request.HttpContext)
                    .HttpRequestMessage;

                return _cachedRequestMessage;
            }
        }
    }
}
