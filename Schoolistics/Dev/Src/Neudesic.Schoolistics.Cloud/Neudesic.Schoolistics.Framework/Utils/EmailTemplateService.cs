using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Neudesic.Schoolistics.Framework.Utils
{
    public static class EmailTemplateService
    {
        /// <param name="applicationPath">application path, should start with "~/"</param>
        private static string LoadText(string applicationPath)
        {
            string text = string.Empty;
            string serverPath = HttpContext.Current.Server.MapPath(applicationPath);
            using (StreamReader reader = new StreamReader(serverPath))
            {
                text = reader.ReadToEnd();
                reader.Close();
            }
            return text;
        }

        public static string HtmlMessageBody(string htmlTemplatePath, object contentReplacements)
        {
            return HtmlMessageBody(htmlTemplatePath, new RouteValueDictionary(contentReplacements));
        }

        private static string HtmlMessageBody(string htmlTemplatePath, IDictionary<string, object> contentReplacements)
        {
            if (htmlTemplatePath == null) return null;
            var body = LoadText(htmlTemplatePath);
            foreach (var replacement in contentReplacements)
                body = body.Replace(replacement.Key.ToReplaceable(), (replacement.Value ?? string.Empty).ToString());
            return body;
        }

        private static string ToReplaceable(this string s)
        {
            return string.IsNullOrWhiteSpace(s) || (s.StartsWith("[[") && s.EndsWith("]]")) ? s : string.Format("[[{0}]]", s);
        }
    }
}
