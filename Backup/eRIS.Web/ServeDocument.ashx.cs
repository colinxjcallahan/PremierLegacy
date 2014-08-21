using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace eRIS.Web
{
    /// <summary>
    /// Summary description for ServeDocument
    /// </summary>
    public class ServeDocument : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string f = "";
            context.Response.Clear();
            context.Response.ClearHeaders();
            //
            switch (context.Request.QueryString["doc"])
            {
                case "BioImagingReport":
                    f = "\\\\PMR-FP01\\BioImagingReports\\" + context.Request.QueryString["id"] + ".pdf";
                    if (File.Exists(f))
                    {
                        context.Response.ContentType = "application/pdf";
                        context.Response.AddHeader("content-disposition", "inline; filename=Report.PDF");
                        context.Response.WriteFile(f);
                    }
                    else
                    {
                        context.Response.Write("The requested file [" + context.Request.QueryString["id"] + "] does not exist.");
                    }
                    break;
                case "SynapseDocument":
                    f = "\\\\NOLWEB-FTP\\FtpRoot\\Scans\\" + context.Request.QueryString["id"];
                    switch(Path.GetExtension(f).ToUpper()) {
                        case ".PDF":
                            context.Response.ContentType = "application/pdf";
                            break;
                        default:
                            context.Response.ContentType = "application/octet-stream";
                            break;
                    }
                    context.Response.AddHeader("content-disposition", "inline; filename=" + context.Request.QueryString["id"]);
                    context.Response.WriteFile(f);
                    break;
            }
            //
            context.Response.End();
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