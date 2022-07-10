using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyURLApplication.Entities;
using TinyURLApplication.Repositories;


namespace TinyURLApplication
{
    public partial class Form1 : Form
    {

        cache CacheTable = new cache();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            label4.Visible = false;
            System.Threading.Thread.Sleep(1000);
            string oldurl = textBox1.Text;
            bool checkstring = CheckURL(oldurl);

                string CheckwhereURL = CheckIfInCache(oldurl);
                string NewURL;

                if (CheckwhereURL == "L" && oldurl.Length < 31&&!checkstring)
                {
                    NewURL = ShrinkURL(oldurl);
                    URLObject newobject = new URLObject(oldurl, NewURL);
                    textBox2.Text = NewURL;
                    CacheTable.AddToCache(newobject);
                }
                else if (CheckwhereURL == "S" && oldurl.Length > 31)

                {
                    NewURL = ExpendUrl(oldurl);
                    URLObject newobject = new URLObject(oldurl, NewURL);
                    textBox2.Text = NewURL;
                    CacheTable.AddToCache(newobject);
                }

                else if (CheckwhereURL == "F")
                {

                    NewURL = DeciedIfShrinkOrExpend(oldurl);
                    textBox2.Text = NewURL;
                    Task.Delay(300).Wait();
                    textBox1.Text = "";
                    URLObject newobject = new URLObject(oldurl, NewURL);
                    CacheTable.AddToCache(newobject);
                }

        }
   


        private void Copy_Click(object sender, EventArgs e)
        {
            bool visiblelabel = false;
            string Copyurl = textBox2.Text;
            label3.Visible = true;
            Clipboard.SetText(Copyurl);
            Task.Delay(1000).Wait();
            label3.Visible = visiblelabel;
            
        }


        //approaching to tinyurl website to get the url
        private string ShrinkURL(string UrlToConvert)
        {

            string URL;
            URL = "http://tinyurl.com/api-create.php?url=" +
               UrlToConvert.ToLower();

            System.Net.HttpWebRequest objWebRequest;
            System.Net.HttpWebResponse objWebResponse;

            System.IO.StreamReader srReader;

            string strHTML;

            objWebRequest = (System.Net.HttpWebRequest)System.Net
               .WebRequest.Create(URL);
            objWebRequest.Method = "GET";

            objWebResponse = (System.Net.HttpWebResponse)objWebRequest
               .GetResponse();
            srReader = new System.IO.StreamReader(objWebResponse
               .GetResponseStream());

            strHTML = srReader.ReadToEnd();

            srReader.Close();
            objWebResponse.Close();
            objWebRequest.Abort();

            return (strHTML);

        }

        //expending an url by an api expender
        private string ExpendUrl(string UrlToConvert)
        {

            string URL;
            URL = "https://onesimpleapi.com/api/unshorten?token=A5Mbn73ZGqLTP2o6SuGwzARxuD5ssLSG65NesPwd&url=" +
               UrlToConvert.ToLower();

            System.Net.HttpWebRequest objWebRequest;
            System.Net.HttpWebResponse objWebResponse;

            System.IO.StreamReader srReader;

            string strHTML;

            objWebRequest = (System.Net.HttpWebRequest)System.Net
               .WebRequest.Create(URL);
            objWebRequest.Method = "GET";

            objWebResponse = (System.Net.HttpWebResponse)objWebRequest
               .GetResponse();
            srReader = new System.IO.StreamReader(objWebResponse
               .GetResponseStream());

            strHTML = srReader.ReadToEnd();

            srReader.Close();
            objWebResponse.Close();
            objWebRequest.Abort();

            return (strHTML);

        }


            //method checking if the url entered is tiny
            private bool CheckURL(string EnteredURL)
        {
            if (EnteredURL.StartsWith("https://tinyurl.com/"))
                return true;
            else
                return false;

        }


        //method to check if the requested url is already in cache - and in which paramater - long/short
        private string CheckIfInCache(string oldurl)
        {
            //if the cache table is ampty - the url id defently not in cache
            if (CacheTable.CacheRows==0)
                return "F";
            //going throw the cache before reaching to web
            foreach (URLObject ROW in CacheTable.cachelist)
                {
                if (ROW.Longurl == oldurl)
                    return "L";
                else if (ROW.Shorturl == oldurl)
                    return "T";
                }

            return "F";
        }


        //method that expend or shrink an url by its lenght
        private string DeciedIfShrinkOrExpend(string oldurl)
        {
            string NewURL;
            if (oldurl.Length > 31)
                NewURL = ShrinkURL(oldurl);
            else
                NewURL = ExpendUrl(oldurl);
            return NewURL;
    }


        //opening the converted url on web
        private void button1_Click_1(object sender, EventArgs e)
        {
            string URL;
            URL = textBox2.Text;
            System.Diagnostics.Process.Start(URL);

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
