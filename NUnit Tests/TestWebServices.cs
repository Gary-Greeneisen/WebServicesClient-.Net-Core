using NUnit.Framework;
using WebServicesClient_.Net_Core.ClassFiles;


namespace WebServicesClient_.Net_Core.NUnit_Tests
{
    class TestWebServices
    {
        [Test]
        public void Test1()
        {
            //Set the class properties before calling the methods
            WebServiceClass webService = new WebServiceClass();
            webService.baseURL = "https://postman-echo.com";
            webService.urlParameters = "/get";
            webService.headerType = "application/json";     //set return Json response

            //Call the method to call the web service
            webService.TestWebService1();

            webService.headerType = "text/plain";     //set return text response
            //Call the method to call the web service
            webService.TestWebService1();

        }

        [Test]
        public void Test2()
        {

        }

        [Test]
        public void Test3()
        {

        }

        [Test]

        public void Test4()
        {

        }




    }

}
