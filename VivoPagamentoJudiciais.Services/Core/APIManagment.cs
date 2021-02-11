using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VivoPagamentoJudiciais.Services.Core
{
    public class Document
    {
        public string BASE64 { get; set; }

    }
    public class Filters
    {
        public string title { get; set; }
        public string textToFind { get; set; }
        public int findSimilarity { get; set; }
        public string textFound { get; set; }
        public int foundSimilarity { get; set; }

    }

    public class APIManagment
    {
        public APIManagment()
        {

        }
        public async  Task<string> CreateNewDocument(string fileBase64)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Config.API.URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            var doc = new Document();
            doc.BASE64 = fileBase64;

            var filter = new Filters();
            filter.title = "";
            filter.textToFind = "";
            filter.findSimilarity = 0;
            filter.textFound = "";
            filter.foundSimilarity = 0;


            List<Document> lstDocuments = new List<Document>();
            lstDocuments.Add(doc);

            List<Filters> lstFilter = new List<Filters>();
            lstFilter.Add(filter);


            var parametro = new
            {
                files = lstDocuments.ToArray(),
                filters = lstFilter.ToArray()
            };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");
            // List data response.
            HttpResponseMessage response = await client.PostAsync("/api/OCR/UploadDocOCR", contentString);// Blocking call! Program will wait here until a response is received or a timeout occurs.


            if (response.IsSuccessStatusCode)
            {
                var bodyContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(bodyContent);
                // Parse the response body.
                //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d.Name);
                //}
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            return "";
        }
    }
}
