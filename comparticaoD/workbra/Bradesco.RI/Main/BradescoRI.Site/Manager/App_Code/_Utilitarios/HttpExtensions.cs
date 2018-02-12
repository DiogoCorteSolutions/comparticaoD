using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HttpExtensions
/// </summary>
public static class HttpExtensions
{
    public static void ForceDownloadRealPath(HttpResponse Response, string realPath, string fileName)
    {
        if (System.IO.File.Exists(realPath))
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.WriteFile(realPath);
            Response.ContentType = "application/octet-stream";
            Response.End();
        }
    }
}