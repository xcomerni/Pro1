using Microsoft.AspNetCore.Http;

namespace Pro1.Helpers
{
    public static class HttpContextAccessorHelper
    {
        public static HttpContext GetHttpContext()
        {
            var httpContextAccessor = new HttpContextAccessor();
            return httpContextAccessor.HttpContext;
        }
    }
}
