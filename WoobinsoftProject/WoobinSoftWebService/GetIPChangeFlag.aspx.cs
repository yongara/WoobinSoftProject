using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class GetIPChangeFlag : WebBase
{
    public string IPCHANGE = "N";
    protected void Page_Load(object sender, EventArgs e)
    {       
        string userid = Request["userid"] != null ? Request["userid"] : string.Empty;
        if (string.IsNullOrEmpty(userid) == false)
        {
            string flag = GettingIpChangeFlag(userid);
            if (flag == "Y") IPCHANGE = "Y";
        }

        Response.Write(IPCHANGE);          
    }
   
}
