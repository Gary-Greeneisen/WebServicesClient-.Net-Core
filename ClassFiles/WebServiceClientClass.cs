using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


namespace WebServicesClient_.Net_Core.ClassFiles
{
    /// <summary>
    /// HttpClient Class Descripes how to use the Class
    /// https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0
    /// 
    /// HttpClient is intended to be instantiated once and re-used throughout the life of an application
    /// Instantiating an HttpClient class for every request will exhaust the number of sockets available 
    /// under heavy loads.This will result in SocketException errors. 
    /// </summary>
    public static class WebServiceClientClass
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        //Define a global class var
        private static HttpClient client = new HttpClient();

        //Define a get method to return client instance
        public static HttpClient HttpInstance   
        {
            get { return client; }      // get method
            //set { client = value; }     //There is no set operation

        }

    }


}
