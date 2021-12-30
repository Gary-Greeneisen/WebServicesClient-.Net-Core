using NUnit.Framework;
using WebServicesClient_.Net_Core.ClassFiles;


namespace WebServicesClient_.Net_Core.NUnit_Tests
{
    class TestWebServices
    {
        [Test]
        public void Test1()
        {

            //TestWebServiceClass webService = new TestWebServiceClass();
            //webService.TestWebServiceDriver();

            //*****************************************************************************************
            // This is from Soap UI Web Site - https://www.soapui.org/docs/rest-testing/
            //Use this REST url to return 590 XML pet records
            //https://petstore.swagger.io/v2/pet/findByStatus?status=available
            //*****************************************************************************************

             WebServiceClass webService = new WebServiceClass();

            //Set the class properties before calling the methods
            webService.baseURL = "https://petstore.swagger.io/v2/pet";
            webService.urlParameters = "/findByStatus?status=available";
            //webService.headerType = "application/xml";       //set return Json response
            webService.headerType = "application/json";     //set return Json response

            webService.TestGetAsync();
 

        }

        [Test]
        public void Test2()
        {
            WebServiceClass webService = new WebServiceClass();

            //Set the class properties before calling the methods
            webService.TestPostAsync1(123);
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
