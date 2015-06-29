using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromium
{
    public class LocalSchemeHandler : ISchemeHandler
    {
        public bool ProcessRequestAsync(IRequest request, ISchemeHandlerResponse response, OnRequestCompletedHandler requestCompletedCallback)
        {
            var uri = new Uri(request.Url);
            var file = uri.Authority + uri.AbsolutePath;
            file = file.Replace("/", "");
            
            if(File.Exists(file))
            {
                var bytes = File.ReadAllBytes(file);
                response.ResponseStream = new MemoryStream(bytes);
                switch(Path.GetExtension(file))
                {
                    case ".html":
                        response.MimeType = "text/html";
                        break;
                }
                requestCompletedCallback();
                return true;
            }
            return false;
        }

        public static string SchemeName { get { return "local"; } }
    }
}
