using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Xyngr.helper
{
    public enum ActionType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class ApiClient
    {
        public string ServiceURL { get; set; }

        public ApiClient(string url)
        {
            this.ServiceURL = url;
        }

        public List<T> GetAll<T>(string controller, string action, string parameters)
        {
            string endPoint = "api/" + controller + "/" + action + "?" + parameters;
            //string jsonInput = JsonConvert.SerializeObject(input);
            string uri = (this.ServiceURL);
            string jsonData = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(endPoint).Result;  // Blocking call!  

                if (response.IsSuccessStatusCode)
                {
                    // Get the response
                    var customerJsonString = response.Content.ReadAsStringAsync();
                    jsonData = custome‌​rJsonString.Result;
                }
            }
            var data = JsonConvert.DeserializeObject<List<T>>(jsonData);
            if (data != null)
            {
                return data;
            }
            else
            {
                return new List<T>();
            }



        }

        public T Get<T>(string controller, string action, string parameters)
        {
            string endPoint = "api/" + controller + "/" + action + "?" + parameters;
            string uri = (this.ServiceURL);
            string jsonData = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(endPoint).Result;  // Blocking call!  

                if (response.IsSuccessStatusCode)
                {
                    // Get the response
                    var customerJsonString = response.Content.ReadAsStringAsync();
                    jsonData = custome‌​rJsonString.Result;
                }
            }
            var data = JsonConvert.DeserializeObject<T>(jsonData);
            if (data != null)
            {
                return data;
            }
            else
            {
                return default(T);
            }
        }

        public string Post<T>(string controller, string action, T input)
        {
            string endPoint = "api/" + controller + "/" + action;
            string jsonInput = JsonConvert.SerializeObject(input);
            string uri = (this.ServiceURL);

            string jsonData = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync(endPoint,input).Result;  // Blocking call!  
                //if (response.IsSuccessStatusCode)
                //{
                //    // Get the response
                //    var customerJsonString = response.Content.ReadAsStringAsync();
                //    jsonData = custome‌​rJsonString.Result;
                //}
                //else
                //{
                //    jsonData = response.Content.ReadAsStringAsync().ToString();
                //}

                var customerJsonString = response.Content.ReadAsStringAsync();
                jsonData = custome‌​rJsonString.Result;
            }

            return jsonData;
        }

        public string PUT<T>(string controller, string action, T input)
        {
            string endPoint = "api/" + controller + "/" + action;
            string jsonInput = JsonConvert.SerializeObject(input);
            string uri = (this.ServiceURL);

            string jsonData = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PutAsJsonAsync(endPoint, input).Result;  // Blocking call!  

                //if (response.IsSuccessStatusCode)
                //{
                //    // Get the response
                //    var customerJsonString = response.Content.ReadAsStringAsync();
                //    jsonData = custome‌​rJsonString.Result;
                //}

                var customerJsonString = response.Content.ReadAsStringAsync();
                jsonData = custome‌​rJsonString.Result;
            }

            return jsonData;

        }

        public string Delete<T>(string controller, string action, string parameters)
        {
            string endPoint = "api/" + controller + "/" + action + "?" + parameters;
            string uri = (this.ServiceURL);
            string jsonData = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync(endPoint).Result;  // Blocking call!  

                if (response.IsSuccessStatusCode)
                {
                    // Get the response
                    var customerJsonString = response.Content.ReadAsStringAsync();
                    jsonData = custome‌​rJsonString.Result;
                }
            }
            return jsonData;
        }

        
    }
}