using System;
using System.Web;

namespace CDPW.theme
{
    /// <summary>
    /// Summary description for js
    /// </summary>
    public class js : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            context.Response.Expires = 30;

            string jsFile = string.Empty;
            string[] jsFiles;
            string jsFolder;

            jsFolder = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"].ToString() +"js/";
            jsFiles = System.IO.Directory.GetFiles(context.Server.MapPath(jsFolder));

            jsFile = System.IO.File.ReadAllText(context.Server.MapPath(jsFolder + "jquery.js"));
            jsFile += "\n\r";

            foreach (string fileName in jsFiles)
            {
                if (fileName.IndexOf(".js") > 0)
                {
                    if (((fileName.IndexOf("jquery.js") < 0)))
                    {
                        jsFile += System.IO.File.ReadAllText(fileName);
                        jsFile += "\n\r";
                    }
                }
            }

            context.Response.Write(jsFile);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}