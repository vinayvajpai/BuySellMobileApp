using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model.RestResponse;
using Newtonsoft.Json;

namespace BuySell.WebServices
{
    public class WebService
    {
        public static string encoding = "application/json";
        public static HttpClient Client;

        #region PostData WebServices
        public static async Task<RestResponseModel> PostData(object body, string methodUrl, bool Istoken)
        {
            RestResponseModel resp = new RestResponseModel();
            try
            {

                var serialized_body = " ";

                //if (Client == null)
                   var Client = new HttpClient() { Timeout = new TimeSpan(0, 0, 45) };
                
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(Constant.BaseUrl + methodUrl),
                    Method = HttpMethod.Post

                };
                if (body != null)
                 {
                    serialized_body = JsonConvert.SerializeObject(body);
                    request.Content = new StringContent(serialized_body, Encoding.UTF8, encoding);
                }
                
                HttpResponseMessage response = null;
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(encoding));
                if (Istoken)
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", Global.AccessToken);
                    Client.DefaultRequestHeaders.Authorization = authHeader;
                }
                //request.Headers.Add("Bearer", Global.AccessToken);
                var task = Client.SendAsync(request)
                    .ContinueWith((taskwithmsg) =>
                    {
                        response = taskwithmsg.Result;

                    });
                task.Wait();
                //response = await Client.SendAsync(request);

                var response_text = await response.Content.ReadAsStringAsync();

                #region Build-Response-Object

                if (!string.IsNullOrEmpty(response_text))
                {
                    resp.content_length = response_text.Length;
                }
                else
                {
                    resp.content_length = 0;
                }

                resp.content_type = encoding;
                resp.response_body = response_text;
                resp.status_code = (int)response.StatusCode;
                #endregion

                #region Enumerate-Response
                bool rest_enumerate = true;
                if (rest_enumerate)
                {
                    Console.WriteLine("APIURL:-" + request.RequestUri.AbsolutePath);
                    Console.WriteLine("Request:-" + serialized_body);
                    Debug.WriteLine("Api response status_code " + resp.status_code + ": " + "for " + "POST " + " " + request.RequestUri.ToString());
                    Debug.WriteLine(resp.response_body);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            return resp;
        }
        #endregion


        #region PostData WebServices
        public static async Task<RestResponseModel> PostData(object body, string methodUrl, bool Istoken,int timeout)
        {
            RestResponseModel resp = new RestResponseModel();
            try
            {

                var serialized_body = " ";

                //if (Client == null)
                var Client = new HttpClient() { Timeout = new TimeSpan(0, 0, timeout) };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(Constant.BaseUrl + methodUrl),
                    Method = HttpMethod.Post

                };
                if (body != null)
                {
                    serialized_body = JsonConvert.SerializeObject(body);
                    request.Content = new StringContent(serialized_body, Encoding.UTF8, encoding);
                }
                HttpResponseMessage response = null;
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(encoding));
                if (Istoken)
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", Global.AccessToken);
                    Client.DefaultRequestHeaders.Authorization = authHeader;
                }
                //request.Headers.Add("Bearer", Global.AccessToken);

                var task = Client.SendAsync(request)
                    .ContinueWith((taskwithmsg) =>
                    {
                        response = taskwithmsg.Result;

                    });
                task.Wait();

                //response = await Client.SendAsync(request);

                var response_text = await response.Content.ReadAsStringAsync();

                #region Build-Response-Object

                if (!string.IsNullOrEmpty(response_text))
                {
                    resp.content_length = response_text.Length;
                }
                else
                {
                    resp.content_length = 0;
                }

                resp.content_type = encoding;
                resp.response_body = response_text;
                resp.status_code = (int)response.StatusCode;
                #endregion

                #region Enumerate-Response
                bool rest_enumerate = true;
                if (rest_enumerate)
                {
                    Console.WriteLine("APIURL:-" + request.RequestUri.AbsolutePath);
                    Console.WriteLine("Request:-" + serialized_body);
                    Debug.WriteLine("Api response status_code " + resp.status_code + ": " + "for " + "POST " + " " + request.RequestUri.ToString());
                    Debug.WriteLine(resp.response_body);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            return resp;
        }
        #endregion

        #region GetData WebServices
        public static async Task<RestResponseModel> GetData(string methodUrl, bool Istoken)
        {
            RestResponseModel resp = new RestResponseModel();
            try
            {
                string url = string.Empty;
                HttpResponseMessage response = null;
                url = Constant.BaseUrl + methodUrl;
                //if(Client == null)
                var Client = new HttpClient() { Timeout = new TimeSpan(0, 0, 120) };
                //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (Istoken)
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", Global.AccessToken);
                    Client.DefaultRequestHeaders.Authorization = authHeader;
                }
                response = await Client.GetAsync(url);
                var response_text = await response.Content.ReadAsStringAsync();

                #region Build-Response-Object

                if (!string.IsNullOrEmpty(response_text))
                {
                    resp.content_length = response_text.Length;
                }
                else
                {
                    resp.content_length = 0;
                }

                resp.content_type = encoding;
                resp.response_body = response_text;
                resp.status_code = (int)response.StatusCode;

                #endregion

                #region Enumerate-Response
                bool rest_enumerate = true;
                if (rest_enumerate)
                {
                    Console.WriteLine("APIURL:-" + url);
                    Debug.WriteLine("Api response status_code " + resp.status_code + ": " + "for " + "GET " + " " + url);
                    Debug.WriteLine(resp.response_body);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            return resp;
        }
        #endregion

        #region DeleteData WebServices
        public static async Task<RestResponseModel> DeleteData(string methodUrl, bool Istoken)
        {
            RestResponseModel resp = new RestResponseModel();
            try
            {
                string url = string.Empty;
                HttpResponseMessage response = null;
                url = Constant.BaseUrl + methodUrl;
                //if (Client == null)
                 var Client = new HttpClient() { Timeout = new TimeSpan(0, 0, 120) };
                //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (Istoken)
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", Global.AccessToken);
                    Client.DefaultRequestHeaders.Authorization = authHeader;
                }

                response = await Client.DeleteAsync(url);
                var response_text = await response.Content.ReadAsStringAsync();

                #region Build-Response-Object

                if (!string.IsNullOrEmpty(response_text))
                {
                    resp.content_length = response_text.Length;
                }
                else
                {
                    resp.content_length = 0;
                }

                resp.content_type = encoding;
                resp.response_body = response_text;
                resp.status_code = (int)response.StatusCode;

                #endregion

                #region Enumerate-Response
                bool rest_enumerate = true;
                if (rest_enumerate)
                {
                    Console.WriteLine("APIURL:-" + url);
                    Debug.WriteLine("Api response status_code " + resp.status_code + ": " + "for " + "GET " + " " + url);
                    Debug.WriteLine(resp.response_body);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            return resp;
        }
        #endregion

        #region Put Data WebServices
        public static async Task<RestResponseModel> PutData(object body, string methodUrl, bool Istoken)
        {
            RestResponseModel resp = new RestResponseModel();
            try
            {

                var serialized_body = " ";

                //if (Client == null)
                var Client = new HttpClient() { Timeout = new TimeSpan(0, 0, 300) };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(Constant.BaseUrl + methodUrl),
                    Method = HttpMethod.Put

                };
                if (body != null)
                {
                    serialized_body = JsonConvert.SerializeObject(body);
                    request.Content = new StringContent(serialized_body, Encoding.UTF8, encoding);
                }
                HttpResponseMessage response = null;
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(encoding));
                if (Istoken)
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", Global.AccessToken);
                    Client.DefaultRequestHeaders.Authorization = authHeader;
                }
                //request.Headers.Add("Bearer", Global.AccessToken);

                var task = Client.SendAsync(request)
                    .ContinueWith((taskwithmsg) =>
                    {
                        response = taskwithmsg.Result;

                    });
                task.Wait();

                //response = await Client.SendAsync(request);

                var response_text = await response.Content.ReadAsStringAsync();

                #region Build-Response-Object

                if (!string.IsNullOrEmpty(response_text))
                {
                    resp.content_length = response_text.Length;
                }
                else
                {
                    resp.content_length = 0;
                }

                resp.content_type = encoding;
                resp.response_body = response_text;
                resp.status_code = (int)response.StatusCode;
                #endregion

                #region Enumerate-Response
                bool rest_enumerate = true;
                if (rest_enumerate)
                {
                    Console.WriteLine("APIURL:-" + request.RequestUri.AbsolutePath);
                    Console.WriteLine("Request:-" + serialized_body);
                    Debug.WriteLine("Api response status_code " + resp.status_code + ": " + "for " + "POST " + " " + request.RequestUri.ToString());
                    Debug.WriteLine(resp.response_body);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            return resp;
        }

        public static async Task<RestResponseModel> PutData(object body, string methodUrl, bool Istoken,int timeout)
        {
            RestResponseModel resp = new RestResponseModel();
            try
            {

                var serialized_body = " ";

                //if (Client == null)
                var Client = new HttpClient() { Timeout = new TimeSpan(0, 0, timeout) };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(Constant.BaseUrl + methodUrl),
                    Method = HttpMethod.Put

                };
                if (body != null)
                {
                    serialized_body = JsonConvert.SerializeObject(body);
                    request.Content = new StringContent(serialized_body, Encoding.UTF8, encoding);
                }
                HttpResponseMessage response = null;
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(encoding));
                if (Istoken)
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", Global.AccessToken);
                    Client.DefaultRequestHeaders.Authorization = authHeader;
                }
                //request.Headers.Add("Bearer", Global.AccessToken);

                var task = Client.SendAsync(request)
                    .ContinueWith((taskwithmsg) =>
                    {
                        response = taskwithmsg.Result;

                    });
                task.Wait();

                //response = await Client.SendAsync(request);

                var response_text = await response.Content.ReadAsStringAsync();

                #region Build-Response-Object

                if (!string.IsNullOrEmpty(response_text))
                {
                    resp.content_length = response_text.Length;
                }
                else
                {
                    resp.content_length = 0;
                }

                resp.content_type = encoding;
                resp.response_body = response_text;
                resp.status_code = (int)response.StatusCode;
                #endregion

                #region Enumerate-Response
                bool rest_enumerate = true;
                if (rest_enumerate)
                {
                    Console.WriteLine("APIURL:-" + request.RequestUri.AbsolutePath);
                    Console.WriteLine("Request:-" + serialized_body);
                    Debug.WriteLine("Api response status_code " + resp.status_code + ": " + "for " + "POST " + " " + request.RequestUri.ToString());
                    Debug.WriteLine(resp.response_body);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            return resp;
        }
        #endregion

    }
}
