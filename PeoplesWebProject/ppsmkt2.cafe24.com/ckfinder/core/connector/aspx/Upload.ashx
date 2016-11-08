<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;

public class Upload : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        string CKEditor = context.Request["CKEditor"];
        string langCode = context.Request["langCode"];
        string message = string.Empty;
        string url = string.Empty;
        try
        {
            if (context.Request.Files.Count == 0)
            {
                context.Response.Write("<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ",\"\",\"No\");</script></body></html>");
                context.Response.End();                
            }

            HttpPostedFile upload = context.Request.Files[0];
            string path = upload.FileName;
            string fileName = path.Substring(path.LastIndexOf(@"\") + 1);
            url = context.Request.Url.GetLeftPart(UriPartial.Authority) + "/ScreenShot/images/" + fileName;
            var savePath = context.Server.MapPath(string.Format("~{0}", "/ScreenShot/images/")) + fileName;
            upload.SaveAs(savePath);
            
            //UcGuideLogger.FileLogTextWrite("upload savePath:", savePath);
            //UcGuideLogger.FileLogTextWrite("upload url:", url);         
            
            message = "이미지가 서버에 저장되었습니다.";
            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ",\"" + url + "\",\"" + message + "\");</script></body></html>";
            //UcGuideLogger.FileLogTextWrite("upload savePath:", savePath);
            //UcGuideLogger.FileLogTextWrite("upload output:", output);
            context.Response.Write(output);
        }
        catch
        {
            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ",\"" + url + "\",\" 서버 업로드중 에러가 발생했습니다. 업로드가 취소되었습니다.\");</script></body></html>";
            context.Response.Write(output);
        }       
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}