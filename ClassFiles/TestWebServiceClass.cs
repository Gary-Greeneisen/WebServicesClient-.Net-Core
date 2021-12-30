using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WebServicesClient_.Net_Core.ClassFiles;
using System.Net;
using System.IO;

namespace WebServicesClient_.Net_Core.ClassFiles
{
    public class TestWebServiceClass
    {

        //Create Class Properties
        //public string baseURL { get; set; }
        //public string urlParameters { get; set; }
        //public string headerType { get; set; }

        //Change to class vars, abel to view the variable content
        public string baseURL;
        public string urlParameters;
        public string headerType;



        public void TestWebServiceDriver()
        {
            //Set the class properties before calling the methods
            baseURL = "https://postman-echo.com";
            urlParameters = "/get";
            headerType = "application/json";     //set return Json response

            //Call the method to call the web service
            TestWebService4();

            headerType = "text/plain";     //set return text response
            //Call the method to call the web service
            TestWebService4();

        }



        /// <summary>
        /// Change hard coding parameters to use Class Properties
        /// 
        /// Example API Get request executed from a browser
        /// https://postman-echo.com/get
        /// 
        /// XML response format
        /// {"args":{},"headers":{"x-forwarded-proto":"https","x-forwarded-
        /// port":"443","host":"postman-echo.com","x-amzn-trace-id":"Root=1-61c8b31b-
        /// 0769b55346835ceb5c4f6c39","content-length":"0","user-
        /// agent":"PostmanRuntime/7.28.4","accept":"*/*","cache-control":"no-cache","postman-
        /// token":"92cd8e60-50dc-4d68-b084-f0c43af08c29","accept
        /// encoding":"gzip, deflate, br"},"url":"https://postman-echo.com/get"}
        /// 
        /// </summary>
        public void TestWebService1()
         {
            try
            {
                var client = WebServiceClientClass.HttpInstance;
                //client.BaseAddress = new Uri("https://postman-echo.com/get");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerType));

                //GET Method
                //var stringTask = client.GetStringAsync("https://postman-echo.com/get");
                //var msg =  await stringTask;
                //Console.Write(msg);

                //HttpResponseMessage response = await client.GetAsync("https://postman-echo.com/get");
                //var task = Task.Run(() => client.GetAsync("https://postman-echo.com/get"));
                var task = Task.Run(() => client.GetAsync(baseURL + urlParameters));
                task.Wait();
                var response = task.Result;

                response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                var responseBody = response.Content.ReadAsStringAsync().Result;
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

         }

        /// <summary>
        /// Change hard coding parameters to use Class Properties
        /// </summary>
        /// This is from a previous HTTP project
        public void TestWebService2()
        {
            //This returns data in XML format identified by content type = XML
            //http://maps.googleapis.com/maps/api/geocode/xml?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&sensor=false

            //var url = "http://maps.googleapis.com/maps/api/geocode/xml?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&sensor=false";

            //var url = "https://postman-echo.com/get";
            var url = baseURL + urlParameters;

            //Pass request to Postman api with orgin and destination details
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create(url);

            //Consume web service synchronous
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                //XML format
                var result = streamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(result))
                {
                    var text = result.ToString();
                }
            }
        }

        /// <summary>
        /// Change hard coding parameters to use Class Properties
        /// </summary>
        public void TestWebService3()
        {
            var client = WebServiceClientClass.HttpInstance;
            //client.BaseAddress = new Uri("https://postman-echo.com/get");
            //var endpoint = "https://postman-echo.com/get";
            var endpoint = baseURL + urlParameters;

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerType));

            var response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        }

        /// <summary>
        /// Change hard coding parameters to use Class Properties
        /// </summary>
        public void TestWebService4()
        {
            var client = WebServiceClientClass.HttpInstance;
            //client.BaseAddress = new Uri("https://postman-echo.com/get");
            //var endpoint = "https://postman-echo.com/get";
            var endpoint = baseURL + urlParameters;

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerType));

            // List data response.
            HttpResponseMessage response = client.GetAsync(endpoint).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                var dataObjects = response.Content.ReadAsStringAsync().Result;


                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}", d.ToString());
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

        }





    }//End  public class WebServiceClass
}//End namespace WebServicesClient_.Net_Core.ClassFiles
