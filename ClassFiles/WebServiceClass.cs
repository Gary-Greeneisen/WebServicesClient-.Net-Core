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
    public class WebServiceClass
    {

        //Create Class Properties
        //public string baseURL { get; set; }
        //public string urlParameters { get; set; }
        //public string headerType { get; set; }

        //Change to class vars, abel to view the variable content
        public string baseURL;
        public string urlParameters;
        public string headerType;

        /// <summary>
        /// Change hard coding parameters to use Class Properties
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
            //var URL = "https://postman-echo.com";
            //var urlParameters = "/get";

            var URL = baseURL;
            var parameters = urlParameters;

            var client = WebServiceClientClass.HttpInstance;
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerType));

            // List data response.
            HttpResponseMessage response = client.GetAsync(parameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
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
