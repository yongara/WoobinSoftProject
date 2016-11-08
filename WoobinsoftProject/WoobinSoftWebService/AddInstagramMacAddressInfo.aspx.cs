using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Xml;
using MobiClick;

public partial class AddInstagramMacAddressInfo : WebBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string authKey = Request["uk"] != null ? Request["uk"].ToString() : string.Empty;
        try
        {
            string returnValue = string.Empty;            
            if (string.IsNullOrEmpty(authKey) == false)
            {
                //uk 값으로 사용자 인증을 처리한다.
                //사용자 인증 성공 후 암호화 키를 발행한다
                //암호화 키는 32자로 발행한다.
                authKey = EncriptUtils.AES256_decrypt(authKey);
                string[] pParams = authKey.Split(new string[] { "||" }, StringSplitOptions.None);
                if (pParams.Length == 3)
                {
                    returnValue = MacAddressInfoInsert(pParams[0], pParams[1], pParams[2]);
                }
            }

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement nodes = xmlDoc.CreateElement("DATA");
            xmlDoc.AppendChild(nodes);

            XmlElement node = xmlDoc.CreateElement("KEY");
            nodes.AppendChild(node);

            XmlCDataSection cdataSection = xmlDoc.CreateCDataSection(returnValue);
            node.AppendChild(cdataSection);

            xmlDoc.Save(Response.OutputStream);
        }
        catch (Exception ex)
        {
           // Response.Write(ex.Message.ToString());
            WriteTextLog("AddInstagramMacAddressInfo", ex.Message.ToString());
        }

    }
}