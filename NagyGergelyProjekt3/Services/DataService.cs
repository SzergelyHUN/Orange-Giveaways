using NagyGergelyProjekt3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NagyGergelyProjekt3.Services
{
    public class DataService
    {
        static string url = "https://www.gamerpower.com";

        public static async Task<IEnumerable<Giveaway>> GetGiveaways()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var uri = "/api/giveaways";
            var result = await client.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<List<Giveaway>>(result);

        }

        public static async Task<IEnumerable<Giveaway>> GetGiveawaysByPlatform(string platform)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var uri = $"/api/filter?platform={platform}";
            var result = await client.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<List<Giveaway>>(result);

        }

        public static async Task<IEnumerable<Giveaway>> GetGiveawaysByType(string type)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var uri = $"/api/giveaways?type={type}";
            var result = await client.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<List<Giveaway>>(result);
        }


    }
}
