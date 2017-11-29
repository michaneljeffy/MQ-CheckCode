using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using WebSocket4Net;

namespace ConsoleApplication2
{
    public class HttpHelper
    {
        //protected static  HttpClient _httpClient;

        //private HttpHelper ()
        //{
        //    _httpClient = new HttpClient() { BaseAddress = new Uri("http://box.in66.co/V6/init?DEVICE_CODE=252248326A85FC3E") };        
        //}

        //public static  HttpClient HttpClientInstance()
        //{

        //    if(_httpClient == null)
        //    { 
        //        HttpHelper helper = new HttpHelper();
        //    }
        //    return _httpClient;
     
        //}

    }

    public class WebRequestHelper
    {
        protected static HttpWebRequest _request;
         private WebRequestHelper ()
        {
            _request = (HttpWebRequest )HttpWebRequest.Create("http://box.in66.co/V6/init?DEVICE_CODE=252248326A85FC3E");
        }

        public static HttpWebRequest WebRequestInstance()
        {
            if (_request == null)
            {
                WebRequestHelper helper = new WebRequestHelper();
            }

            return _request;
        }
    }
}
