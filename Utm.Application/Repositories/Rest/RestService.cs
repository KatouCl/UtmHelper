using System;
using System.Net;
using RestSharp;

namespace Utm.Application.Repositories.Rest
{
    public class RestService : IRestRepository
    {
        public string PostJsonRequest(string body, string url, string resource)
        {
            var client = new RestClient(url);
            var request = new RestRequest(resource, Method.POST);
            request.AddParameter("application/json; charset=utf-8", body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);


            if (response.StatusCode != HttpStatusCode.OK)
                throw new NotImplementedException($"Произошла ошибка. \r\nСтатус код {response.StatusCode}");

            return response.Content;
        }

        public string GetJsonRequest(string url, string resource)
        {
            var client = new RestClient(url);
            var request = new RestRequest(resource, Method.GET);
            var queryResult = client.Execute(request).Content;

            return queryResult;
        }
    }
}