using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper;
using System.Data.Common;
using System.Data;
using DBHelper.SQLServer;
using DBHelper.Base;
using System.IO;
using System.Net;
using System.Xml;

namespace MobiClickManager
{
    class Common
    {
        #region 전역변수
        public static string connectionString = string.Format("server={0};Initial Catalog={1};Integrated Security=false;UID={2};PWD={3};Network Library=DBMSSOCN;", "woobinsoft.cafe24.com", "woobinsoft", "woobinsoft", "tkagowjs1!");
       
        #endregion

        #region Enum 타입입니다.
        /// <summary>
        /// 레지스트리 모드 enum 설정값입니다 
        /// </summary>
        public enum RegistryMode
        {
            WoW64,
            Normal
        }

        public enum SiteType
        {
            Blog,
            Cafe,
            CafeArticle,
            News,
            Webkr
        }

        public enum BrowserType
        {
            InternerExplorer,
            Chrome,
            Safari,
            Opera
        }


        public enum NetWorkConnectionType
        {
            Mobile,
            Proxy
        }

        public enum SearchType
        {
            Total,
            UserId,
            KeyCode
        }

        public enum SaleType
        {
            Total,
            Sale,
            Test,
            Etc
        }
        #endregion

        #region Common - 생성자 입니다.
        /// <summary>
        /// 생성자입니다.
        /// </summary>
        static Common()
        {            
        }
        #endregion

        #region GetUserList - 사용자리스트를 가져옵니다.
        /// <summary>
        /// 사용자 정보를 가져옵니다.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetUserList()
        {
            DataTable dt = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);               
                string procedureName = "SP_GET_USER_LIST";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                dt = da.GetData(command);

            }
            catch (Exception ex)
            {
                WriteTextLog("GetUserList", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            return dt;
        }
        #endregion      
       
        #region UpdateUserInfo - 사용자정보를 수정 합니다.
        /// <summary>
        /// 사용자정보를 추가합니다.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="macAddress"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public static string UpdateUserInfo(string userid, string password, string authCode)
        {
            string result = "001";
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USERID", userid);
                param.Add("@PASSWORD", password);              
                param.Add("@KEY_CODE", authCode);

                string procedureName = "SP_USERINFO_UPDATE";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.GetSingleValue(command).ToString();
            }
            catch (Exception ex)
            {
                WriteTextLog("UpdateUserInfo", ex.Message.ToString());
                result = "005";
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion
        
        #region DeleteUserInfo - 사용자정보를 삭제 합니다.
        /// <summary>
        /// 사용자정보를 추가합니다.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="macAddress"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public static int DeleteUserInfo(string userid,string keyCode)
        {
            int result = -1;
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USERID", userid);
                param.Add("@KEY_CODE", keyCode);                

                string procedureName = "SP_USERINFO_DELETE";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                WriteTextLog("DeleteUserInfo", ex.Message.ToString());
                result = -1;
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion

        #region GetProductList - 제품리스트를 가져옵니다.
        /// <summary>
        /// 사용자 정보를 가져옵니다.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetProductList(SaleType saleType,SearchType searchType,string keyword,string searchCount)
        {
            DataTable dt = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                string procedureName = "SP_GET_PRODUCT_LIST";
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@SEARCH_TYPE", searchType.ToString().ToUpper());
                param.Add("@SALE_TYPE", saleType.ToString().ToUpper());
                param.Add("@KEY_WORD", keyword);
                param.Add("@SEARCH_COUNT", searchCount);               
                

                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                dt = da.GetData(command);

            }
            catch (Exception ex)
            {
                WriteTextLog("GetUserList", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            return dt;
        }
        #endregion      

        #region UpdateProductInfo - 상품정보를 업데이트 합니다.
        /// <summary>
        /// 상품정보를 업데이트 합니다.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="userID"></param>
        /// <param name="macAddress"></param>
        /// <param name="allowKeyCount"></param>
        /// <returns></returns>
        public static string UpdateProductInfo(string userID, string password, string keyCode, string macAddress, int allowKeyCount, string fromDt, string toDt, string saleCode, string saleDate, string dealCode)
        {
            string result = "001";
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@KEY_CODE", keyCode);  
                param.Add("@USERID", userID);
                param.Add("@PWD", password);                            
                param.Add("@MACADDRESS", macAddress);
                param.Add("@ALLOW_KEYWORD_CNT", allowKeyCount);
                param.Add("@FROM_DT", fromDt);
                param.Add("@TO_DT", toDt);
                param.Add("@SALE_CODE", saleCode);
                param.Add("@SALE_DATE", saleDate);
                param.Add("@DEAL_CODE", dealCode);

                string procedureName = "SP_PRODUCT_INFO_ALL_UPDATE";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.GetSingleValue(command).ToString();
            }
            catch (Exception ex)
            {
                WriteTextLog("UpdateProductInfo", ex.Message.ToString());
                result = "005";
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion 

        #region DeleteProductInfo - 상품정보를 삭제 합니다.
        /// <summary>
        /// 상품정보를 업데이트 합니다.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="userID"></param>
        /// <param name="macAddress"></param>
        /// <param name="allowKeyCount"></param>
        /// <returns></returns>
        public static string DeleteProductInfo(string keyCode)
        {
            string result = "001";
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@KEY_CODE", keyCode);               

                string procedureName = "SP_PRODUCT_INFO_DELETE";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.GetSingleValue(command).ToString();
            }
            catch (Exception ex)
            {
                WriteTextLog("DeleteProductInfo", ex.Message.ToString());
                result = "005";
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion 
        
        #region UserAgentList
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable UserAgentList()
        {           
            DataTable dt = new DataTable();
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected) Connect(ref da);

            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                string procedureName = "SP_USER_AGENT_LIST";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                dt = da.GetData(command); 
            }
            catch (Exception ex)
            {
            }
            finally
            {
                DisConnect(ref da);
            }

            return dt;
        }
        #endregion

        #region InsertUserAgent -user agent를 추가합니다.
        /// <summary>
        /// KeyWord 정보를 추가합니다.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="macAddress"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public static string InsertUserAgent(string userAgent)
        {
            string result = "001";
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USER_AGENT", userAgent);

                string procedureName = "SP_USER_AGENT_INSERT";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.GetSingleValue(command).ToString();
            }
            catch (Exception ex)
            {
                WriteTextLog("InsertUserAgent", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion      

        #region DeleteUserAgent -user agent를 삭제 합니다.
        /// <summary>
        /// KeyWord 정보를 추가합니다.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="macAddress"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public static string DeleteUserAgent(string userAgent)
        {
            string result = "001";
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@USER_AGENT", userAgent);

                string procedureName = "SP_USER_AGENT_DELETE";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                result = da.GetSingleValue(command).ToString();
            }
            catch (Exception ex)
            {
                WriteTextLog("DeleteUserAgent", ex.Message.ToString());
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
        #endregion

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
        private static void Connect(ref DataAccess da)
        {
            da.Connect(connectionString, true);
        }

        private static void DisConnect(ref DataAccess da)
        {
            da.Disconnect();
            da.Dispose();
            da = null;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createKeyCodeCount">생성할 건수</param>
        /// <param name="saleType">판매타입</param>
        public static int CreateProductINfo(int createKeyCodeCount, SaleType saleType)
        {
            int result = createKeyCodeCount;
            DataAccess da = new SQLServerDB();
            if (!da.IsConnected)
            {
                Connect(ref da);
            }
            try
            {
                ParameterEngine param = ParameterEngine.New(da);
                param.Add("@SALE_TYPE", saleType.ToString().ToUpper());
                param.Add("@KEYCODE_COUNT", createKeyCodeCount);

                string procedureName = "SP_KEY_CODE_CREATE";
                DbCommand command = da.GenerateCommand(procedureName, CommandType.StoredProcedure, param);
                da.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                WriteTextLog("CreateProductINfo", ex.Message.ToString());
                result = 0;
            }
            finally
            {
                DisConnect(ref da);
            }

            return result;
        }
    }
}
