using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dasafio_Api
{
    class Consul_Moeda
    {
        public async Task<string> ValidaMDAAsync(string moeda)
        {
            string validacion;
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://api.exchangerate.host/latest") };
            var response = await client.GetAsync("Cambios");
            var content = await response.Content.ReadAsStringAsync();
            var Moeda = JsonConvert.DeserializeObject<Moeda>(content);
            if (Moeda.Rates.ContainsKey(moeda))
            {
                validacion = "encontro";
                return validacion;
            }
            else
            {
                validacion = "no encontro";
                return validacion;
            }
        }
    }
    
}
