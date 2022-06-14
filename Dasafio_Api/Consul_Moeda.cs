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
            int cuenta_monedas = 0;
            string mensaje = "";
            string validacion;
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://api.exchangerate.host/latest") };
            var response = await client.GetAsync("Cambios");
            var content = await response.Content.ReadAsStringAsync();
            var Moeda = JsonConvert.DeserializeObject<Moeda>(content);
            if (moeda == "1")
            {
                foreach (var item in Moeda.Rates)
                {
                    if (cuenta_monedas == 0)
                    {
                        mensaje += "\n\t\t" + item.Key + " ";
                        cuenta_monedas++;
                    }
                    else
                    {
                        mensaje += "" + item.Key + " ";
                        cuenta_monedas++;
                        if (cuenta_monedas > 10)
                        {
                            cuenta_monedas = 0;
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(mensaje);
            }

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
