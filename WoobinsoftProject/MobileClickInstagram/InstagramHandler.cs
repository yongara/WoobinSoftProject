using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Windows.Forms;

namespace MobileClickInstagram
{
    class InstagramHandler
    {
        // Fields
        private string ArtBody;
        private string ArtCl;
        private ChromeOptions COptions = new ChromeOptions();
        private IWebDriver driver;
        private long Follows;
        private long Likess;
        private Proxy Proxys = new Proxy();
        private long Reply;
        private ChromeDriverService service = ChromeDriverService.CreateDefaultService();
        private bool Swit;

        public List<string> boardURLList = new List<string>();
        private List<string> KeyWordList = new List<string>();
        private List<string> SpamList = new List<string>();
        private List<string> ReplyList = new List<string>();
        private List<string> FollowList = new List<string>();
        private List<string> LikeList = new List<string>();

        public InstagramHandler()
        {
            SpamList = InstagramCommon.GetSpamList();
            ReplyList = InstagramCommon.GetReplyList();
            FollowList = InstagramCommon.GetFollowList();
            LikeList = InstagramCommon.GetLikeList();
        }

        //인스타그램 본문에 스팸문자가 있는지 확인합니다.
        public bool IsCheckBodyBySpamList()
        {
            bool value = true;
            foreach (string word in SpamList)
            {
                if (this.ArtBody.Contains(word))
                {
                    value = false;
                }
            }
            return value;
        }

        //팔로우를 클릭합니다.
        public bool FollowClick()
        {
            bool flag = true; 
            try
            {
                //아티클의  css 값으로 검색하여 버튼을 클릭
                this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/header/span/button")).Click();
            }
            catch (Exception ex)
            {
                flag = false;
            }          

            return flag;
        }

        //아티클의 class(css) 값을 가져옵니다.
        public bool GetArticleCssName()
        {
            bool flag = true;
            try
            {
                string pageSource = this.driver.PageSource;
                string tmpSource = pageSource.Split(new string[] { "<article class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                tmpSource = tmpSource.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];
                this.ArtCl = tmpSource;
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        //아티클의 본문을 가져옵니다.
        public bool GetArticleBody()
        {
            bool flag = true;
            try
            {
                string pageSource = this.driver.PageSource;
                string tmpSource = pageSource.Split(new string[] { "<title>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                tmpSource = tmpSource.Split(new string[] { "</title>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                this.ArtBody = tmpSource;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        ////언팔로우를 가져옵니다
        //public bool Getunfollow()
        //{
        //    bool flag;
        //    //사인
        //    this.driver.FindElement(By.XPath("//a[@class='instagram signin-button']")).Click();
        //    Common.Dela2(1);
        //    //언팔로우 클릭
        //    this.driver.FindElement(By.XPath("//ul[@class='uf-navbar-nav nav']/li[2]/a/span[2]")).Click();
        //    Common.Dela2(1);

        //    //60초동안 delay
        //    for (int i = 0; i < 60; i++)
        //    {
        //        this.ScrollDown();
        //        Common.Dela2(1);
        //    }

        //    Common.Dela2(5);

        //    //string[] array = Strings.Split(this.driver.PageSource, "<span class=\"username\"", -1, CompareMethod.Binary);
        //    //MyProject.Forms.INSTA.ListBox4.Items.Clear();
        //    //int num2 = Information.UBound(array, 1);
        //    // for (index = 1; index <= num2; index++)
        //    //{
        //    //MyProject.Forms.INSTA.ListBox4.Items.Add(Strings.Split(Strings.Split(array[index], "@", -1, CompareMethod.Binary)[1], "<", -1, CompareMethod.Binary)[0]);
        //    //Application.DoEvents();
        //    // }
        //    return flag;
        //}

        //크롬에서 웹사이트를 오픈합니다.
        public void WebSiteOpenURL(string Str)
        {
            this.driver.Navigate().GoToUrl(Str);
        }
        
        //언팔로우 작업
        public bool ExecuteUnFollow(string ID)
        {
            bool flag = true;
            try
            {
                //ID 에 해당하는 페이지를 찾아가서 팔로잉 이있을경우 팔로잉을 클릭하여 업팔로우 처리를 합니다.
                this.driver.Navigate().GoToUrl("https://www.instagram.com/" + ID + "/");
                InstagramCommon.Dela2(2);
                string tmpSource = this.driver.PageSource.Split(new string[] { "<button class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                tmpSource = tmpSource.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];

                //팔로잉이 있을경우는 이미 팔로우 하고 있는경우이므로 다시 클릭하여 언팔로우 처리를합니다.
                if (this.driver.FindElement(By.XPath("//button[@class='" + tmpSource + "']")).GetAttribute("outerText").IndexOf("팔로잉") >= 0)
                {
                    this.driver.FindElement(By.XPath("//button[@class='" + tmpSource + "']")).Click();
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }

        //좋아요 클릭
        public bool LikeClick()
        {
            bool flag = true;
            try
            {
                InstagramCommon.Dela2(5);
                this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/a")).Click();              
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }

        //로그인 버튼을 클릭합니다.
        public bool LoginClick(string ID, string PW)
        {
            bool flag = true;
            try
            {
                InstagramCommon.Dela2(5);
                this.driver.FindElement(By.Name("username")).Click();
                this.driver.FindElement(By.Name("username")).SendKeys(ID);
                this.driver.FindElement(By.XPath("//input[@name='password' and @placeholder='비밀번호']")).SendKeys(PW);
                string str = this.driver.PageSource.Split(new string[] { "<button class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                str = str.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];

                this.driver.FindElement(By.XPath("//button[@class='" + str + "']")).Click();
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }

        //댓글을 작성합니다.
        public bool WriteReply(string reply)
        {
            bool flag=true;

            try
            {
                InstagramCommon.Dela2(5);
                this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/form/input")).Click();
                this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/form/input")).SendKeys(reply);
                InstagramCommon.Dela2(5);
                this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/form/input")).SendKeys(OpenQA.Selenium.Keys.Enter);
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        //스크롤을 실행합니다.
        public void ScrollDown()
        {
            IJavaScriptExecutor driver = (IJavaScriptExecutor)this.driver;
            object[] args = new object[] { "" };
            driver.ExecuteScript("window.scrollBy(0,9999999)", args);
        }


        //해시 태그로 검색
        public bool ExecuteSearch(string searchText)
        {
            bool flag=true;
            try
            {
                InstagramCommon.Dela2(5);
                this.driver.Navigate().GoToUrl("https://instagram.com/explore/tags/" + searchText + "/");
                InstagramCommon.Dela2(5);
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;

        }

        //검색된 거 선택
        public bool SelectPictuer()
        {
            bool flag = true;
            try
            {
                InstagramCommon.Dela2(5);
                string[] array = this.driver.PageSource.Split(new string[] { "href=\"/p" }, StringSplitOptions.RemoveEmptyEntries);
                boardURLList.Clear();

                foreach (string value in array)
                {
                    string tempItem = value.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    tempItem = "/p/"+tempItem.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    boardURLList.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }

        //웹을 오픈합니다.
        public bool WebOpen(string openURL, string IP)
        {
            bool flag=true;

            try
            {
                //시크릿모드로 실행
                this.COptions.AddArgument("-incognito");
                //프록시 IP 가 있는경우 프록시 IP 로 실행
                if (String.Compare(IP, "1:1", false) > 0)
                {
                    this.Proxys.HttpProxy = IP;
                    this.COptions.Proxy = this.Proxys;
                }
                this.driver = new ChromeDriver(this.service, this.COptions);
                this.driver.Navigate().GoToUrl(openURL);
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;

        }

        // Methods
        public void ChangeSwit(bool Swi)
        {
            this.Swit = Swi;
        }

        //스팸문자가 있는지 확인
        public bool CheckBody()
        {
            /*
            int num2 = MyProject.Forms.INSTA.ListView1.Items.Count - 1;
            for (int i = 0; i <= num2; i++)
            {
                if (Strings.InStr(this.ArtBody, MyProject.Forms.INSTA.ListView1.Items[i].SubItems[1].Text, CompareMethod.Binary) > 0)
                {
                    return false;
                }
            }
             * */
            return true;
        }

        public bool CheckID(string ID)
        {
            bool flag;
            InstagramCommon.Dela2(5);

            if (this.driver.PageSource.IndexOf("href=\"/" + ID + "/\"") >= 0)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool CheckPage()
        {
            bool flag;
            if (this.driver.PageSource.IndexOf("페이지가 삭제") >= 0)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public void ClearRate()
        {
            InstagramCommon.FallowRate.Clear();
            InstagramCommon.LikeRate.Clear();
            InstagramCommon.ReplyRate.Clear();
        }

        public void EndWeb()
        {           
            this.driver.Quit();
        }

        public bool Follow()
        {
            bool flag;
         

            this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/header/span/button")).Click();
            flag = true;

            return flag;
        }

        public bool GetArt()
        {
            bool flag;
            string pageSource = this.driver.PageSource;
            string tmpSource = pageSource.Split(new string[] { "<article class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
            tmpSource = tmpSource.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];
            this.ArtCl = tmpSource;
            flag = true;

            return flag;
        }

        public bool GetBody()
        {
            bool flag;

            string pageSource = this.driver.PageSource;
            string tmpSource = pageSource.Split(new string[] { "<title>" }, StringSplitOptions.RemoveEmptyEntries)[1];
            tmpSource = tmpSource.Split(new string[] { "</title>" }, StringSplitOptions.RemoveEmptyEntries)[0];
            this.ArtBody = tmpSource;

            flag = true;

            return flag;
        }

        public long GetFollow()
        {
            return this.Follows;
        }

        public long GetLike()
        {
            return this.Likess;
        }

        public long GetReply()
        {
            return this.Reply;
        }

        //public bool Getunfollow()
        //{
        //    bool flag;
        //    this.driver.FindElement(By.XPath("//a[@class='instagram signin-button']")).Click();
        //    Common.Dela2(1);
        //    this.driver.FindElement(By.XPath("//ul[@class='uf-navbar-nav nav']/li[2]/a/span[2]")).Click();
        //    Common.Dela2(1);
        //    int index = 1;
        //    do
        //    {
        //        this.ScrollDown();
        //        Common.Dela2(1);
        //        index++;
        //    }
        //    while (index <= 60);
        //    Common.Dela2(5);
        //    //string[] array = Strings.Split(this.driver.PageSource, "<span class=\"username\"", -1, CompareMethod.Binary);
        //    //MyProject.Forms.INSTA.ListBox4.Items.Clear();
        //    //int num2 = Information.UBound(array, 1);
        //    // for (index = 1; index <= num2; index++)
        //    //{
        //    //MyProject.Forms.INSTA.ListBox4.Items.Add(Strings.Split(Strings.Split(array[index], "@", -1, CompareMethod.Binary)[1], "<", -1, CompareMethod.Binary)[0]);
        //    //Application.DoEvents();
        //    // }
        //    return flag;
        //}

        //public bool GOTOURL(string Str)
        //{
        //    bool flag;

        //    this.driver.Navigate().GoToUrl(Str);

        //    return flag;
        //}

        ////언팔로우
        //public bool HateFollow(string ID)
        //{
        //    bool flag;

        //    //ID 에 해당하는 페이지를 찾아가서 팔로잉 이있을경우 팔로잉을 클릭하여 업팔로우 처리를 합니다.
        //    this.driver.Navigate().GoToUrl("https://www.instagram.com/" + ID + "/");
        //    Common.Dela2(2);
        //    string tmpSource = this.driver.PageSource.Split(new string[] { "<button class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
        //    tmpSource = tmpSource.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];

        //    //팔로잉이 있을경우는 이미 팔로우 하고 있는경우이므로 다시 클릭하여 언팔로우 처리를합니다.
        //    if (this.driver.FindElement(By.XPath("//button[@class='" + tmpSource + "']")).GetAttribute("outerText").IndexOf("팔로잉") >= 0)
        //    {
        //        this.driver.FindElement(By.XPath("//button[@class='" + tmpSource + "']")).Click();
        //    }

        //    return flag;
        //}

        //public bool IDCheck(ListView LV)
        //{
        //    bool flag;

        //    int num4 = LV.Items.Count - 1;
        //    for (int i = 0; i <= num4; i++)
        //    {
        //        if (this.driver.PageSource.IndexOf("\">" + LV.Items[i].Text + "</a><span") >= 0)
        //        {
        //            flag = true;
        //            break;
        //        }
        //        flag = false;
        //    }

        //    return flag;
        //}

        //사용안함
        public bool InLike()
        {
            bool flag;

            InstagramCommon.Dela2(3);
            this.driver.FindElement(By.XPath("//a[@class='-cx-PRIVATE-PostInfo__likeButton -cx-PRIVATE-LikeButton__root -cx-PRIVATE-Util__hideText coreSpriteHeartOpen']")).Click();
            flag = true;
            return flag;
        }

        //좋아요 실행
        public bool Likes()
        {
            bool flag;

            InstagramCommon.Dela2(5);
            this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/a")).Click();
            flag = true;

            return flag;
        }

        //로그인
        public bool login_IN(string ID, string PW)
        {
            InstagramCommon.Dela2(5);
            this.driver.FindElement(By.Name("username")).Click();
            this.driver.FindElement(By.Name("username")).SendKeys(ID);
            this.driver.FindElement(By.XPath("//input[@name='password' and @placeholder='비밀번호']")).SendKeys(PW);
            //string str = Strings.Split(Strings.Split(this.driver.PageSource, "<button class=\"", -1, CompareMethod.Binary)[1], "\"", -1, CompareMethod.Binary)[0];
            string str = this.driver.PageSource.Split(new string[] { "<button class=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
            str = str.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];
            
            this.driver.FindElement(By.XPath("//button[@class='" + str + "']")).Click();
            return true;
        }

        public void rateSet()
        {
            /*
            int num2;
            VBMath.Randomize();
            if (Conversion.Int(Conversions.ToDouble(Operators.DivideObject(Conversion.Int(MyProject.Forms.INSTA.TextBox8.Text), 100))) == 1.0)
            {
                int num5 = Conversions.ToInteger(Conversion.Int(MyProject.Forms.INSTA.TextBox2.Text));
                int num6 = Conversions.ToInteger(Conversion.Int(MyProject.Forms.INSTA.TextBox3.Text));
                for (num2 = num5; num2 <= num6; num2++)
                {
                    Common.LikeRate[num2] = 1;
                }
            }
            else
            {
                int num7 = Conversions.ToInteger(Operators.DivideObject(Operators.MultiplyObject(Conversions.ToInteger(Operators.SubtractObject(Conversion.Int(MyProject.Forms.INSTA.TextBox3.Text), Conversion.Int(MyProject.Forms.INSTA.TextBox2.Text))), Conversion.Int(MyProject.Forms.INSTA.TextBox8.Text)), 100)) - 1;
                for (num2 = 0; num2 <= num7; num2++)
                {
                    int num4;
                    do
                    {
                        num4 = Common.RandomVal(Conversions.ToInteger(MyProject.Forms.INSTA.TextBox2.Text), Conversions.ToInteger(MyProject.Forms.INSTA.TextBox3.Text));
                    }
                    while (Common.LikeRate[num4] == 1);
                    Common.LikeRate[num4] = 1;
                }
            }
             * */
        }

        public void rateSet2()
        {
            /*
            int num2;
            VBMath.Randomize();
            if (Conversion.Int(Conversions.ToDouble(Operators.DivideObject(Conversion.Int(MyProject.Forms.INSTA.TextBox9.Text), 100))) == 1.0)
            {
                int num5 = Conversions.ToInteger(Conversion.Int(MyProject.Forms.INSTA.TextBox2.Text));
                int num6 = Conversions.ToInteger(Conversion.Int(MyProject.Forms.INSTA.TextBox3.Text));
                for (num2 = num5; num2 <= num6; num2++)
                {
                    Common.FollowRate[num2] = 1;
                }
            }
            else
            {
                int num7 = Conversions.ToInteger(Operators.DivideObject(Operators.MultiplyObject(Conversions.ToInteger(Operators.SubtractObject(Conversion.Int(MyProject.Forms.INSTA.TextBox3.Text), Conversion.Int(MyProject.Forms.INSTA.TextBox2.Text))), Conversion.Int(MyProject.Forms.INSTA.TextBox9.Text)), 100)) - 1;
                for (num2 = 0; num2 <= num7; num2++)
                {
                    int num4;
                    do
                    {
                        num4 = Common.RandomVal(Conversions.ToInteger(MyProject.Forms.INSTA.TextBox2.Text), Conversions.ToInteger(MyProject.Forms.INSTA.TextBox3.Text));
                    }
                    while (Common.FollowRate[num4] == 1);
                    Common.FollowRate[num4] = 1;
                }
            }
             * 
             * */
        }

        public void rateSet3()
        {
            /*
            int num2;
            VBMath.Randomize();
            if (Conversion.Int(Conversions.ToDouble(Operators.DivideObject(Conversion.Int(MyProject.Forms.INSTA.TextBox7.Text), 100))) == 1.0)
            {
                int num5 = Conversions.ToInteger(Conversion.Int(MyProject.Forms.INSTA.TextBox2.Text));
                int num6 = Conversions.ToInteger(Conversion.Int(MyProject.Forms.INSTA.TextBox3.Text));
                for (num2 = num5; num2 <= num6; num2++)
                {
                    Common.ReplyRate[num2] = 1;
                }
            }
            else
            {
                int num7 = Conversions.ToInteger(Operators.DivideObject(Operators.MultiplyObject(Conversions.ToInteger(Operators.SubtractObject(Conversion.Int(MyProject.Forms.INSTA.TextBox3.Text), Conversion.Int(MyProject.Forms.INSTA.TextBox2.Text))), Conversion.Int(MyProject.Forms.INSTA.TextBox7.Text)), 100)) - 1;
                for (num2 = 0; num2 <= num7; num2++)
                {
                    int num4;
                    do
                    {
                        num4 = Common.RandomVal(Conversions.ToInteger(MyProject.Forms.INSTA.TextBox2.Text), Conversions.ToInteger(MyProject.Forms.INSTA.TextBox3.Text));
                    }
                    while (Common.ReplyRate[num4] == 1);
                    Common.ReplyRate[num4] = 1;
                }
            }
             * 
             * */
        }

        //답글작성
        public bool Replys(string Str)
        {
            bool flag;

            InstagramCommon.Dela2(5);
            this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/form/input")).Click();
            this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/form/input")).SendKeys(Str);
            InstagramCommon.Dela2(5);
            this.driver.FindElement(By.XPath("//article[@class='" + this.ArtCl + "']/div[2]/section[2]/form/input")).SendKeys(OpenQA.Selenium.Keys.Enter);
            flag = true;

            return flag;
        }
        
        ////해시 태그로 검색
        //public bool Seach_IN(string Str)
        //{
        //    bool flag;

        //    Common.Dela2(5);
        //    this.driver.Navigate().GoToUrl("https://instagram.com/explore/tags/" + Str + "/");
        //    Common.Dela2(5);

        //    return flag;

        //}

        //검색된 거 선택
        public bool SelectPic()
        {
            bool flag;
            int num = 2;
            InstagramCommon.Dela2(5);
            string[] array = this.driver.PageSource.Split(new string[] { "href=\"/p" }, StringSplitOptions.RemoveEmptyEntries);
            //MyProject.Forms.INSTA.ListBox1.Items.Clear();
            //int num4 = Information.UBound(array, 1);
            //for (int i = 1; i <= num4; i++)
            //{
            //    string item = "/p/" + Strings.Split(Strings.Split(array[i], "/", -1, CompareMethod.Binary)[1], "\"", -1, CompareMethod.Binary)[0];
            //    MyProject.Forms.INSTA.ListBox1.Items.Add(item);
            //}
            flag = true;

            return flag;
        }      

        public void SetLF()
        {
            this.Likess = 0L;
            this.Follows = 0L;
            this.Reply = 0L;
        }

        public void SetFollow()
        {
            this.Follows += 1L;
        }

        public void SetLike()
        {
            this.Likess += 1L;
        }

        public void SetReply()
        {
            this.Reply += 1L;
        }
               
    }
}
