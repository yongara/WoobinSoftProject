using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBHelper.Base;
using DBHelper;
using System.Data.Common;
using DBHelper.SQLServer;
using System.Data;
using System.IO;
using System.Text;

/// <summary>
/// WebBase의 요약 설명입니다.
/// </summary>
public class WebBase : System.Web.UI.Page
{
    protected string connectionString = string.Format("server={0};Initial Catalog={1};Integrated Security=false;UID={2};PWD={3};Network Library=DBMSSOCN;", "woobinsoft.cafe24.com", "woobinsoft", "woobinsoft", "tkagowjs1!");
	public WebBase()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}

    public  string GettingIpChangeFlag(string userid)
    {
        string result = "N";
       
        DataAccess da = new SQLServerDB();
        if (!da.IsConnected) Connect(ref da);
        
        try
        {
            ParameterEngine param = ParameterEngine.New(da);
            param.Add("@USERID", userid);
            string procedureName = "SP_SELECT_IPCHANGE_FLAG";
            DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
            result = da.GetSingleValue(command).ToString();

            WriteTextLog("result", result);

            if (result.ToUpper() == "Y")
            {
                param = ParameterEngine.New(da);
                param.Add("@USERID", userid);
                param.Add("@FLAG", "N");
                procedureName = "SP_UPDATE_IPCHANGE_FLAG";
                command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                da.ExecuteNonQuery(command);
            }

        }
        catch (Exception ex)
        {
            WriteTextLog("GetIPChangeFlag", ex.Message.ToString());
        }
        finally
        {
            DisConnect(ref da);
        }

        return result.ToUpper();
    }

    public DataTable MacAuthCheck(string mac)
    {
        DataTable resultTable = new DataTable();
        DataAccess da = new SQLServerDB();
        if (!da.IsConnected) Connect(ref da);

        try
        {
            ParameterEngine param = ParameterEngine.New(da);          
            param.Add("@MAC_ADDRESS", mac);
            string procedureName = "SP_INSTAGRAM_AUTH_CHECK";
            DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
            resultTable = da.GetData(command);           
        }
        catch (Exception ex)
        {
            WriteTextLog("MacAuthCheck", ex.Message.ToString());
        }
        finally
        {
            DisConnect(ref da);
        }

        return resultTable;
    }

    public string MacAddressInfoInsert(string mac,string userName,string telNo)
    {
        string result = "001";

        DataAccess da = new SQLServerDB();
        if (!da.IsConnected) Connect(ref da);

        try
        {
            ParameterEngine param = ParameterEngine.New(da);
            param.Add("@MAC_ADDRESS", mac);
            param.Add("@USER_NAME", userName);
            param.Add("@TEL_NO", telNo);
            string procedureName = "SP_INSTA_PRODUCTINFO_INSERT";
            DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
            result = da.GetSingleValue(command).ToString();          
        }
        catch (Exception ex)
        {
            result = "003";
            WriteTextLog("MacAddressInfoInsert", ex.Message.ToString());
        }
        finally
        {
            DisConnect(ref da);
        }

        return result;
    }

    public string RandomValue(int start, int end)
    {
        int rtn = 0;
        char rtnval;

        string val = "abcdefghijklmnopqrstuvwxyz1234567890";

        Random random = new Random();
        rtn = random.Next(start, end);

        rtnval = val[rtn - 1];

        return rtnval.ToString();
    }       

     #region WriteTextLog - 로그를 작성합니다.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public static void WriteTextLog(string type, string message)
        {
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\";

                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }
                 
                 //string filePath =  @"F:￦HOME￦woobinsoft\www\Log\";
               

                filePath += string.Format("[{0}].txt", DateTime.UtcNow.AddHours(9).ToLongDateString());

                StreamWriter output = new StreamWriter(filePath, true, Encoding.Default);
                output.WriteLine(string.Format("[ {0} : {1} ]" + type, DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond.ToString()));
                output.WriteLine(message);
                output.Close();
                output = null;

            }
            catch { }
        }
        #endregion

    #region Connect,DisConnect
    private void Connect(ref DataAccess da)
    {
        da.Connect(connectionString, true);
    }

    private void DisConnect(ref DataAccess da)
    {
        da.Disconnect();
        da.Dispose();
        da = null;
    }
    #endregion
}