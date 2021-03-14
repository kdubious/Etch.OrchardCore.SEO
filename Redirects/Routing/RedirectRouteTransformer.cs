using Etch.OrchardCore.SEO.Redirects.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using OrchardCore.ContentManagement.Routing;
using System.Threading.Tasks;

namespace Etch.OrchardCore.SEO.Redirects.Routing
{
    public class RedirectRouteTransformer : DynamicRouteValueTransformer
    {
        private readonly IRedirectEntries _entries;

        public RedirectRouteTransformer(IRedirectEntries entries)
        {
            _entries = entries;
        }

        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {

            var contentItemId = GetContentItemId(httpContext).Result;

            if (!string.IsNullOrEmpty(contentItemId))
            {
                return new ValueTask<RouteValueDictionary>(new RouteValueDictionary {
                    {"Area", "Etch.OrchardCore.SEO"},
                    {"Controller", "Redirect"},
                    {"Action", "Index"},
                    {"ContentItemId", contentItemId}
                });
            }

            return new ValueTask<RouteValueDictionary>((RouteValueDictionary)null);
        }

        private async Task<string> GetContentItemId(HttpContext httpContext)
        {
            AutorouteEntry entry;
            var url = httpContext.Request.Path.ToString().TrimEnd('/');

            if (string.IsNullOrEmpty(url) )
            {
                var rsltSlash = await _entries.TryGetEntryByPathAsync("/");
                if(rsltSlash.Item1)
                {
                    return rsltSlash.Item2.ContentItemId;
                }
            }

            var rsltUrl = await _entries.TryGetEntryByPathAsync(url);
            if (rsltUrl.Item1)
            {
                return rsltUrl.Item2.ContentItemId;
            }

            return null;
        }
    }
}