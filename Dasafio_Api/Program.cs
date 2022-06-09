using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Dasafio_Api
{
    internal class Program
    {
        public static object MessageBox { get; private set; }

        static void Main(string[] args)
        {
            string mensaje = " ";
            Console.Write("Moneda Origen: ");
            string m_from = Console.ReadLine();
            if (m_from == null || m_from.Length  != 3)
            {
                mensaje += "\nerror en la moneda origen, Verifique";
            }
            Console.Write("Moneda Destino: ");
            string m_to = Console.ReadLine();
            if (m_to == null || m_to.Length != 3)
            {
                mensaje += "\nerror en la moneda origen, Verifique";
            }
            if (m_from == m_to)
            {
                mensaje += "\nMoneda de origen y destino deben ser diferentes";
            } 
            Console.Write("Valor a cambiar: ");
            double m_valor = Convert.ToDouble(Console.ReadLine());
            if (m_valor < 1)
            {
                mensaje += "\nMonto debe ser mayor a 0";
            }
            if (mensaje != " ")
            {
                Console.WriteLine(mensaje);
                Console.ReadLine();
            }
           
            string buscar = "from=" + m_from + "&to=" + m_to + "&amount=" + m_valor;
            string strURL = "https://api.exchangerate.host/convert?" + buscar;


            //string strURL = "https://api.exchangerate.host/convert?from=USD&to=BRL&amount=100.0";
            if (mensaje == " ")
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {

                        var response = client.GetAsync(strURL).Result;
                        if (response.IsSuccessStatusCode)
                        {

                            var result = response.Content.ReadAsStringAsync().Result;
                            var cambios = JsonConvert.DeserializeObject<Cambios>(result);

                            Console.WriteLine("Moeda Origen: " + cambios.Query.From);
                            Console.WriteLine("Moeda Destino: " + cambios.Query.To);
                            Console.WriteLine("Valor: " + cambios.Query.Amount + "\n");
                            Console.WriteLine(cambios.Query.From + " " + cambios.Query.Amount + " => " + cambios.Query.To + " " + cambios.Result);
                            Console.WriteLine("Taxa: " + cambios.Info.Rate);
                            Console.ReadLine();

                        }
                        else
                        {
                            Console.WriteLine("No fue posible ejecutar su solicitud");
                            Console.ReadLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("No fue posible ejecutar su solicitud");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
