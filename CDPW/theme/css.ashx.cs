using System;
using System.Web;
using System.IO;

namespace CDPW.theme.clasic
{
    /// <summary>
    /// Summary description for css
    /// </summary>
    public class css : IHttpHandler {

        public void ProcessRequest (HttpContext context) {
            context.Response.ContentType = "text/css";
            context.Response.Expires = 30;

            string cssFile = string.Empty;
            string[] cssFiles;
            string cssFolder;

            cssFolder = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"].ToString();
            cssFiles = Directory.GetFiles(context.Server.MapPath(cssFolder));

            cssFile = File.ReadAllText(context.Server.MapPath(cssFolder + "reset.css"));
            cssFile += Environment.NewLine.ToString();
            cssFile += File.ReadAllText(context.Server.MapPath(cssFolder + "main.css"));
            cssFile += "\n\r";

            foreach (string fileName in cssFiles)
            {
                if (((fileName.IndexOf("reset.css") < 0)))
                {
                    if (fileName.IndexOf("ie.css") < 0)
                    {
                        if (fileName.IndexOf("main.css") < 0)
                        {
                            cssFile += System.IO.File.ReadAllText(fileName);
                            cssFile += "\n\r";
                        }
                    }
                }
            }

            context.Response.Write(cssFile);
        }
     
        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}