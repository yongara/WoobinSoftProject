using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class GetOpenURLKey : WebBase
{
    public string key = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            string iniPath = Server.MapPath("ConnectionInfo.ini");
            MobiClick.IniFileHandler ini = new MobiClick.IniFileHandler(iniPath);

          

            string dbinfo = string.Empty;
            string ftpinfo = string.Empty;
            /*
             * 
             [DBINFO]
DOMAIN=www.woobinsoft.kr
PORT = 1433
USERID = woobinsoft
PASSWORD = tkagowjs1!

[FTPINFO]
DOMAIN=www.woobinsoft.kr
PORT = 5721
USERID = woobinsoft
PASSWORD = tkagowjs1!* 
             */

            string dbDomain = ini.GetIniValue("DBINFO", "DOMAIN");
            string dbPort =ini.GetIniValue("DBINFO", "PORT");
            string dbUserId=ini.GetIniValue("DBINFO", "USERID");
            string dbPassword = ini.GetIniValue("DBINFO", "PASSWORD");
            string ftpDomain = ini.GetIniValue("FTPINFO", "DOMAIN");
            string ftpPort = ini.GetIniValue("FTPINFO", "PORT");
            string ftpUserId = ini.GetIniValue("FTPINFO", "USERID");
            string ftpPassword = ini.GetIniValue("FTPINFO", "PASSWORD");

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement nodes = xmlDoc.CreateElement("DATA");
            xmlDoc.AppendChild(nodes);

            XmlElement node = xmlDoc.CreateElement("DBINFO");
            nodes.AppendChild(node);

            XmlElement subNode;
            XmlCDataSection cdataSection;
            
            subNode= xmlDoc.CreateElement("DOMAIN");
            node.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(dbDomain);
            subNode.AppendChild(cdataSection);

            subNode = xmlDoc.CreateElement("PORT");
            node.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(dbPort);
            subNode.AppendChild(cdataSection);

            subNode = xmlDoc.CreateElement("USERID");
            node.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(dbUserId);
            subNode.AppendChild(cdataSection);

            subNode = xmlDoc.CreateElement("PASSWORD");
            node.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(dbPassword);
            subNode.AppendChild(cdataSection);
            
            XmlElement node1 = xmlDoc.CreateElement("FTPINFO");
            nodes.AppendChild(node1);

            subNode = xmlDoc.CreateElement("DOMAIN");
            node1.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(ftpDomain);
            subNode.AppendChild(cdataSection);

            subNode = xmlDoc.CreateElement("PORT");
            node1.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(ftpPort);
            subNode.AppendChild(cdataSection);

            subNode = xmlDoc.CreateElement("USERID");
            node1.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(ftpUserId);
            subNode.AppendChild(cdataSection);

            subNode = xmlDoc.CreateElement("PASSWORD");
            node1.AppendChild(subNode);
            cdataSection = xmlDoc.CreateCDataSection(ftpPassword);
            subNode.AppendChild(cdataSection);

            xmlDoc.Save(Response.OutputStream);
        }
        catch (Exception ex)
        {
            WriteTextLog("GetOpenURLKey", ex.Message.ToString());
        }

    }
}