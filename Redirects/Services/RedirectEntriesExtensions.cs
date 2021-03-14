﻿using OrchardCore.ContentManagement.Routing;

namespace Etch.OrchardCore.SEO.Redirects.Services
{
    public static class RedirectEntriesExtensions
    {
        public static void AddEntry(this IRedirectEntries entries, string contentItemId, string path)
        {
            entries.AddEntry( contentItemId, path);
        }

        public static void RemoveEntry(this IRedirectEntries entries, string contentItemId, string path)
        {
            entries.RemoveEntry(contentItemId, path);
        }
    }
}
