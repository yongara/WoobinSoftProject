using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = Request["filename"] != null ? Request["filename"].ToString() : string.Empty;
        if (string.IsNullOrEmpty(fileName) == false)
        {
            Response.Redirect(string.Format("/download/{0}.apk", fileName));
        }

    }
}