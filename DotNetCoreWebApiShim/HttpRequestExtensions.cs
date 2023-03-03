using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;

namespace DotNetCoreWebApiShim
{
    /// <summary>
    /// <see cref="HttpRequest"/> の拡張メソッドクラス
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// <see cref="HttpResponseMessage"/> によるHTTPレスポンスを生成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="statusCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpResponseMessage CreateResponse<T>(this HttpRequest request, HttpStatusCode statusCode, T value)
        {
            SetAllowSynchronous(request);
            return new HttpRequestMessageFeature(request.HttpContext)
                .HttpRequestMessage
                .CreateResponse(statusCode, value);
        }

        /// <summary>
        /// <see cref="HttpResponseMessage"/> によるHTTPエラーレスポンスを生成します。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="statusCode"></param>
        /// <param name="httpError"></param>
        /// <returns></returns>
        public static HttpResponseMessage CreateErrorResponse(this HttpRequest request, HttpStatusCode statusCode, HttpError httpError)
        {
            SetAllowSynchronous(request);
            return new HttpRequestMessageFeature(request.HttpContext)
                .HttpRequestMessage
                .CreateErrorResponse(statusCode, httpError);
        }

        /// <summary>
        /// 同期処理オプションを変更。
        /// </summary>
        /// <param name="request"></param>
        private static void SetAllowSynchronous(HttpRequest request)
        {
            // NOTE 同期処理による例外が発生するためオプションを変更
            var controlFeature = request.HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (controlFeature != null)
            {
                controlFeature.AllowSynchronousIO = true;
            }
        }
    }
}
