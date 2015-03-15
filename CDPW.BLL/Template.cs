using System;
using System.IO;

namespace CDPW.BLL
{
    public class Template
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Reading a template file (txt, html).
        /// </summary>
        /// <param name="paramsToReplace">Parameters to replace with text - using "\\" for breaking parameters</param>
        /// <param name="textToReplaceWith">Text to replace parameters - using "\\" for breaking parameters</param>
        /// <param name="templatePath">The file path on the server's disc.</param>
        public static string readTemplate(string paramsToReplace, string textToReplaceWith, string templatePath)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            string message = "";
            string[] paramsToReplace2 = paramsToReplace.Split('\\');
            string[] textToReplaceWith2 = textToReplaceWith.Split('\\');

            bool fileExists = System.IO.File.Exists(templatePath);

            if (fileExists)
            {
                message = File.ReadAllText(templatePath);
                message = message.Replace(paramsToReplace2[0], textToReplaceWith2[0]);
                message = message.Replace(paramsToReplace2[1], textToReplaceWith2[1]);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return message;
        }
    }
}
