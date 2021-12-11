using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Namek.Core
{
    public class ScriptBlock : IDisposable
    {
        private const string GlobalScriptKey = "__SCRIPTBLOCKS";

        public ScriptBlock(WebViewPage html, string key = null)
        {
            Html = html;
            ScriptKey = key;
             Html.OutputStack.Push(new StringWriter());
        }

        public static IDictionary<string, string> PageScripts
        {
            get
            {
                if (HttpContext.Current.Items[GlobalScriptKey] == null)
                    HttpContext.Current.Items[GlobalScriptKey] = new Dictionary<string, string>();

                return (IDictionary<string, string>) HttpContext.Current.Items[GlobalScriptKey];
            }
        }

        private WebViewPage Html { get; }

        private string ScriptKey { get; }

        public void Dispose()
        {
            var script = Html.OutputStack.Pop().ToString();

            if (Html.IsAjax)
                Html.Write(new HtmlString(script));
            else
                Register(script, ScriptKey);
        }

        public static void Register(string script, string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                key = Guid.NewGuid().ToString();

            if (!PageScripts.ContainsKey(key))
                PageScripts.Add(key, script);
        }
    }
}