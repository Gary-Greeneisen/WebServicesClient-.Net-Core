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
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace WebServicesClient_.Net_Core.ClassFiles
{
    public partial class WebServiceClass
    {
        //Create Class Properties
        //public string baseURL { get; set; }
        //public string urlParameters { get; set; }
        //public string headerType { get; set; }

        //Change to class vars, able to view the variable content
        public string baseURL;
        public string urlParameters;
        public string headerType;

        /// <summary>
        /// 
        /// </summary>
        public void TestGetAsync()
        {
            try
            {
                //Update requst headers with global class vars
                var client = WebServiceClientClass.HttpInstance;
                var endpoint = baseURL + urlParameters;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerType));

                //Blocking call! Program will wait here until a response is received or a timeout occurs.
                HttpResponseMessage response = client.GetAsync(endpoint).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    //var data = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var data = response.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    throw new Exception(response.ReasonPhrase);
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }


        }

        /// <summary>
        /// The Number Conversion Web Service, implemented with Visual DataFlex, provides functions that convert 
        /// numbers into words or dollar amounts.
        /// 
        /// Number to Dollars
        /// Returns the word corresponding to the positive number passed as parameter. Limited to quadrillions.
        /// </summary>
        /// <param name="number"></param>
        public void TestPostAsync1(int number)
        {
            try
            {
                // Converting byte[] into System.Net.Http.HttpContent
                //create byte array
                byte[] data = new byte[1];
                data[0] = 123;                  //Initialize with passed in value
                //Now use the byteContent in the HTTP.PostAsync
                ByteArrayContent byteContent = new ByteArrayContent(data);


                //Update requst headers with global class vars
                var client = WebServiceClientClass.HttpInstance;
                //var endpoint = baseURL + urlParameters;
                //Call the Number to Dollars web service
                var endpoint = "https://www.dataaccess.com/webservicesserver/numberconversion.wso";
                //var endpoint = "https://www.dataaccess.com/webservicesserver/numberconversion.wso?op=NumberToDollars";

                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerType));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

                //Blocking call! Program will wait here until a response is received or a timeout occurs.
                //HttpContent number = 123;
                //HttpResponseMessage response = client.PostAsync(endpoint, 123, new JsonMediaTypeFormatter()).Result;

                //JsonContent content = JsonContent.Create(123);

                //Use a Dictionary/List to add values pairs to the server call
                var values = new Dictionary<string, string>();
                values.Add("dNum", "123");

                //********************************************************************************
                //All these server calls return either 415/500 error ???
                //var content = new StringContent(values.ToString(), System.Text.Encoding.UTF8, "application/xml");
                var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/soap+xml");
                //var content = new FormUrlEncodedContent(values);

                HttpResponseMessage response = client.PostAsync(endpoint, content).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    //var data = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var responseData = response.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    throw new Exception(response.ReasonPhrase);
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }


        }

        /// <summary>
        /// The Number Conversion Web Service, implemented with Visual DataFlex, provides functions that convert 
        /// numbers into words or dollar amounts.
        /// 
        /// Number to Words
        /// Returns the non-zero dollar amount of the passed number.
        /// </summary>
        /// <param name="number"></param>
        public void TestPostAsync2(int number)
        {
            try
            {

                //Update requst headers with global class vars
                var client = WebServiceClientClass.HttpInstance;
                //var endpoint = baseURL + urlParameters;
                //Call the Number to Dollars web service
                var endpoint = "https://www.dataaccess.com/webservicesserver/numberconversion.wso";

                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerType));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                //Blocking call! Program will wait here until a response is received or a timeout occurs.
                //HttpContent number = 123;

                //********************************************************************************
                //These server calls return either 415/500 error ???
                HttpResponseMessage response = client.PostAsync(endpoint, 123, new JsonMediaTypeFormatter()).Result;
                //HttpResponseMessage response = client.PostAsync(endpoint, byteContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    //var data = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var responseData = response.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    throw new Exception(response.ReasonPhrase);
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }


        }






    }
}
