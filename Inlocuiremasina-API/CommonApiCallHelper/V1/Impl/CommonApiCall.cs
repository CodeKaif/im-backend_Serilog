using CommonApiCallHelper.V1.Interface;
using CommonApiCallHelper.V1.Model;
using ConfigurationModel.ServicesEndpoint;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace CommonApiCallHelper.V1.Impl
{
    public class CommonApiCall : ICommonApiCall
    {
        private readonly ServicesEndpoint _servicesEndpoint;
        public CommonApiCall(IOptions<ServicesEndpoint> servicesEndpoint) 
        {
            _servicesEndpoint = servicesEndpoint?.Value;
        }

        public async Task FireForgotPost(CommonPostApiModel commonPostApiModel, string requestEndpoint ,string path)
        {
            string service = GetRequiredEndpointUrl(requestEndpoint);

            //Passing service base url  
            using (var client = new HttpClient())
            {
                string model = JsonConvert.SerializeObject(commonPostApiModel);
                StringContent sc = new StringContent(model, Encoding.UTF8, "application/json");
                try
                {
                    client.BaseAddress = new Uri(service);
                    var response = client.PostAsync(path, sc).Result.Content.ReadAsStringAsync().Result;                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<T> PostDataAsync<T>(CommonPostApiModel commonPostApiModel, string requestEndpoint, string path)
        {
            string service = GetRequiredEndpointUrl(requestEndpoint);

            //Passing service base url  
            using (var client = new HttpClient())
            {
                string model = JsonConvert.SerializeObject(commonPostApiModel);
                StringContent sc = new StringContent(model, Encoding.UTF8, "application/json");
                try
                {
                    client.BaseAddress = new Uri(service);
                    var response = await client.PostAsync(path, sc);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"API call failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    }

                    string responseData = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string GetRequiredEndpointUrl(string requestEndpoint)
        {
            return requestEndpoint switch
            {
                "ContentApi" => _servicesEndpoint.ContentApi,
                "AuthApi" => _servicesEndpoint.AuthApi,
                "NotificationApi" => _servicesEndpoint.NotificationApi,                
                _ => _servicesEndpoint.NotificationApi
            };
        }
    }
}
