using KnowledgeBase.Core.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace KnowledgeBase.Api.Utils
{
    public class HttpRequest : IHttpRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequest(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                return Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Properties.ElementAt(0).Value == "sub").Value);
            }
        }
    }
}
