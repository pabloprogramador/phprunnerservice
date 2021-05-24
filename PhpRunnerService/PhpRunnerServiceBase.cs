﻿using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PhpRunnerService.Models;
using PhpRunnerService.Enum;
using System.Linq;
using System.Text.Json;

namespace PhpRunnerService
{
    public class PhpRunnerServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUrl = new Uri(Settings.ServerApi);

        public PhpRunnerServiceBase()
        {
            _httpClient = new HttpClient(new HttpClientHandler()) { BaseAddress = _baseUrl };
        }
        
        public async Task<T> Get<T>(int id)
        {
            IPhpRunnerApi<DataPhpRunnerOne<T>> service = RestService.For<IPhpRunnerApi<DataPhpRunnerOne<T>>>(_httpClient);
            var json = await service.GetItem(typeof(T).Name.ToLower(), id).ConfigureAwait(false);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <GET> JSON: " + json);
#endif
            DataPhpRunnerOne<T> result = JsonSerializer.Deserialize<DataPhpRunnerOne<T>>(json);
            var teste = result;
            var value = result.Data;
            return result.Data;
        }

        public async Task<List<T>> List<T>()
        {
            IPhpRunnerApi<DataPhpRunnerMany<T>> service = RestService.For<IPhpRunnerApi<DataPhpRunnerMany<T>>>(_httpClient);
            var json = await service.ListItems(typeof(T).Name.ToLower()).ConfigureAwait(false);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <LIST> JSON: " + json);
#endif
            DataPhpRunnerMany<T> result = JsonSerializer.Deserialize<DataPhpRunnerMany<T>>(json);
            return result.Data;
        }

        /// <summary>
        /// Search PhpRunnerService
        /// </summary>
        /// <param name="value">Example: ({id}~{equals}~{1})</param>
        /// <returns>List<T></returns>
        public async Task<List<T>> Search<T>(string value)
        {
            IPhpRunnerApi<DataPhpRunnerMany<T>> service = RestService.For<IPhpRunnerApi<DataPhpRunnerMany<T>>>(_httpClient);
            var json = await service.SearchItems(typeof(T).Name.ToLower(), value).ConfigureAwait(false);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <LIST search > JSON: " + json);
#endif
            DataPhpRunnerMany<T> result = JsonSerializer.Deserialize<DataPhpRunnerMany<T>>(json);
            return result.Data;
        }

        public async Task<List<T>> Search<T>(string field, Enum.PhpRunnerFilter filter, string value)
        {
            string comp = $"({field}~{filter.Value}~{value})";
            return await Search<T>(comp);
        }

        /// <summary>
        /// Search By Ids PhpRunnerService
        /// </summary>
        /// <param name="value">Ex.: 2,3,6</param>
        /// <returns>List<T></returns>
        public async Task<List<T>> SearchByIds<T>(string value)
        {
            string[] list = value.Split(',');
            string comp = "";
            foreach (var item in list)
            {
                comp += $"(id~equals~{item})";
            }
            return await Search<T>(comp);
        }

        //public async Task<List<T>> SearchByIds<T>(string value)
        //{
        //    string comp = $"({field}~{filter.Value}~{value})";
        //    return await Search<T>(comp);
        //}

        public async Task<bool> Update<T>(int id, T obj)
        {
            string objJson = JsonSerializer.Serialize(obj);
            Dictionary<string, object> dic = JsonSerializer.Deserialize<Dictionary<string, object>>(objJson);
            dic.Remove("id");

            IPhpRunnerApi<DataPhpRunnerOne<T>> service = RestService.For<IPhpRunnerApi<DataPhpRunnerOne<T>>>(_httpClient);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <UPDATE> OBJ: " + JsonSerializer.Serialize(dic));
#endif
            var json = await service.UpdateItem(typeof(T).Name.ToLower(), id, dic).ConfigureAwait(false);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <UPDATE> JSON: " + json);
#endif
            DataPhpRunnerOne<T> result = JsonSerializer.Deserialize<DataPhpRunnerOne<T>>(json);
            return result.Success;
        }

        public async Task<T> Insert<T>(T obj)
        {
            string objJson = JsonSerializer.Serialize(obj);
            Dictionary<string, object> dic = JsonSerializer.Deserialize<Dictionary<string, object>>(objJson);
            dic.Remove("id");

            IPhpRunnerApi<DataPhpRunnerOne<T>> service = RestService.For<IPhpRunnerApi<DataPhpRunnerOne<T>>>(_httpClient);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <INSERT> OBJ: " + JsonSerializer.Serialize(dic));
#endif
            var json = await service.InsertItem(typeof(T).Name.ToLower(), dic).ConfigureAwait(false);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <INSERT> JSON: " + json);
#endif
            DataPhpRunnerOne<T> result = JsonSerializer.Deserialize<DataPhpRunnerOne<T>>(json);
            return result.Data;
        }

        public async Task<bool> Delete<T>(int id)
        {
            IPhpRunnerApi<DataPhpRunnerOne<T>> service = RestService.For<IPhpRunnerApi<DataPhpRunnerOne<T>>>(_httpClient);
            var json = await service.DeleteItem(typeof(T).Name.ToLower(), id).ConfigureAwait(false);
#if DEBUG
            Console.WriteLine("::::> table: " + typeof(T).Name.ToLower() + " <LIST> JSON: " + json);
#endif
            DataPhpRunnerOne<T> result = JsonSerializer.Deserialize<DataPhpRunnerOne<T>>(json);
            return result.Success;
        }

        // CUSTOM BEGIN HERE //////////////////////////

        //public async Task<User> Login(string email)
        //{
        //    var result = await Search<User>("email", PhpRunnerFilter.Equals, email);
        //    return result.FirstOrDefault();
        //}

    }
}