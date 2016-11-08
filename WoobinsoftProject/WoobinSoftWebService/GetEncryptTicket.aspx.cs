using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Xml;
using MobiClick;
using System.Data;

public partial class GetEncryptTicket : WebBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string authKey = Request["uk"] != null ? Request["uk"].ToString() : string.Empty;
        try
        {
            string returnValue = string.Empty;
            string macAddress = string.Empty;
            string allowCnt = "0";
            string fromDt = string.Empty;
            string toDt = string.Empty;


            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(authKey) == false)
            {
                //uk 값으로 사용자 인증을 처리한다.
                //사용자 인증 성공 후 암호화 키를 발행한다
                //암호화 키는 32자로 발행한다.
                authKey = EncriptUtils.AES256_decrypt(authKey);
                //Response.Write(authKey);
                dt = MacAuthCheck(authKey);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        macAddress = dr["MACADDRESS"].ToString();
                        allowCnt = dr["ALLOW_APP_CNT"].ToString();
                        fromDt = dr["FROM_DT"].ToString();
                        toDt = dr["TO_DT"].ToString();
                    }
                }               
            }

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement nodes = xmlDoc.CreateElement("DATA");
            xmlDoc.AppendChild(nodes);

            XmlElement node = null;
            XmlCDataSection cdataSection = null;
            node = xmlDoc.CreateElement("MACADDRESS");
            nodes.AppendChild(node);
            cdataSection = xmlDoc.CreateCDataSection(macAddress);
            node.AppendChild(cdataSection);

            node = xmlDoc.CreateElement("ALLOW_APP_CNT");
            nodes.AppendChild(node);
            cdataSection = xmlDoc.CreateCDataSection(allowCnt);
            node.AppendChild(cdataSection);

            node = xmlDoc.CreateElement("FROM_DT");
            nodes.AppendChild(node);
            cdataSection = xmlDoc.CreateCDataSection(fromDt);
            node.AppendChild(cdataSection);

            node = xmlDoc.CreateElement("TO_DT");
            nodes.AppendChild(node);
            cdataSection = xmlDoc.CreateCDataSection(toDt);
            node.AppendChild(cdataSection);
            
            xmlDoc.Save(Response.OutputStream);
        }
        catch (Exception ex)
        {
           // Response.Write(ex.Message.ToString());
            WriteTextLog("GetEncryptTicket", ex.Message.ToString());
        }

    }
}