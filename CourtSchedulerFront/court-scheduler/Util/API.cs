using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ADX.DAL
{
    public static class API
    {
        #region API.GET

        /// <summary>
        /// Does an HTTP GET for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public static async Task<T> Get<T>(HttpClient client, Uri uri)
        {
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Does an HTTP GET for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public static async Task<T> Get<T>(Uri uri)
        {
            using var client = new HttpClient();
            return await Get<T>(client, uri);
        }

        /// <summary>
        /// Does an HTTP GET for the API endpoint provided
        /// </summary>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Get(HttpClient client, Uri uri) { return await Get<object>(client, uri); }

        /// <summary>
        /// Does an HTTP GET for the API endpoint provided
        /// </summary>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Get(Uri uri) { return await Get<object>(uri); }

        #endregion

        #region API.POST

        /// <summary>
        /// Does an HTTP POST for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public async static Task<T> Post<T>(HttpClient client, Uri uri, object body = null)
        {
            var json = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json-patch+json"
            );

            using var response = await client.PostAsync(uri, json);

            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Does an HTTP POST for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public static async Task<T> Post<T>(Uri uri, object body = null)
        {
            using var client = new HttpClient();
            return await Post<T>(client, uri, body);
        }

        /// <summary>
        /// Does an HTTP POST for the API endpoint provided
        /// </summary>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Post(HttpClient client, Uri uri, object body) { return await Post<object>(client, uri, body); }

        /// <summary>
        /// Does an HTTP POST for the API endpoint provided
        /// </summary>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Post(Uri uri, object body) { return await Post<object>(uri, body); }

        #endregion

        #region API.PUT

        /// <summary>
        /// Does an HTTP PUT for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public static async Task<T> Put<T>(HttpClient client, Uri uri, object body)
        {
            var json = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json-patch+json"
            );

            using var response = await client.PutAsync(uri, json);

            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        }

        /// <summary>
        /// Does an HTTP PUT for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public static async Task<T> Put<T>(Uri uri, object body)
        {
            using var client = new HttpClient();
            return await Put<T>(client, uri, body);
        }

        /// <summary>
        /// Does an HTTP PUT for the API endpoint provided
        /// </summary>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Put(HttpClient client, Uri uri, object body) { return await Put<object>(client, uri, body); }

        /// <summary>
        /// Does an HTTP PUT for the API endpoint provided
        /// </summary>
        /// <param name="uri">The API endpoint</param>
        /// <param name="body">The request body</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Put(Uri uri, object body) { return await Put<object>(uri, body); }

        #endregion

        #region API.DELETE

        /// <summary>
        /// Does an HTTP DELETE for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public static async Task<T> Delete<T>(HttpClient client, Uri uri)
        {
            using var response = await client.DeleteAsync(uri);

            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Does an HTTP DELETE for the API endpoint provided
        /// </summary>
        /// <typeparam name="T">The data type under the Task</typeparam>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The typed data and status result from the API endpoint</returns>
        public static async Task<T> Delete<T>(Uri uri)
        {
            using var client = new HttpClient();
            return await Delete<T>(client, uri);
        }

        /// <summary>
        /// Does an HTTP DELETE for the API endpoint provided
        /// </summary>
        /// <param name="client">Externally initialized httpclient, mainly for defining token / auth header stuff </param>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Delete(HttpClient client, Uri uri) { return await Delete<object>(client, uri); }

        /// <summary>
        /// Does an HTTP DELETE for the API endpoint provided
        /// </summary>
        /// <param name="uri">The API endpoint</param>
        /// <returns>The data and status result from the API endpoint</returns>
        public static async Task<object> Delete(Uri uri) { return await Delete<object>(uri); }

        #endregion
    }
}