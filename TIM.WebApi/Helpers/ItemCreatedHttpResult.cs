using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace TIM.WebApi.Helpers
{
    public class ItemCreatedHttpResult<T> : IHttpActionResult
    {
        private T _item;
        private HttpRequestMessage _request;
        private decimal _id;
        private string _uri;

        public ItemCreatedHttpResult(T item, HttpRequestMessage request, decimal itemId = 0, string uri = "")
        {
            _request = request;
            _item = item;
            _id = itemId;
            _uri = uri;
        }
        
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse<T>(HttpStatusCode.Created, _item);
            
            UrlHelper urlHelper = new UrlHelper(_request);
            Uri uri = new Uri(urlHelper.Link(_uri, new { id = _id }));
            response.Headers.Location = uri;

            return Task.FromResult(response);
        }
    }
}