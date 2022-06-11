using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Dasafio_Api
{
    class Program
    {
        public static object MessageBox { get; private set; }

        static void Main(string[] args)
        {
            Consul_Moeda consul_Moeda = new Consul_Moeda();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("\n▒▒▒▒▒▒▒▒▒▒▒▒ SERVIÇO DE CÂMBIO ▒▒▒▒▒▒▒▒▒▒▒" +
                            "\n              SEJA BEM-VINDO");
            Console.ForegroundColor = ConsoleColor.Green;

            string mensaje = " ";
            Console.Write("\n\n\tMoneda Origen:    ");
            string m_from = Console.ReadLine();
            if (m_from == null || m_from.Length  != 3)
            {
                mensaje += "\nErro na moeda origen, verifique";
            }
            else
            {
                var valid = consul_Moeda.ValidaMDAAsync(m_from);
                if (valid.Result != "encontro")
                {
                    mensaje += "\n" + m_from + " não está registrado";
                }
            }
            Console.Write("\tMoneda Destino:   "); 
            string m_to = Console.ReadLine();
            if (m_to == null || m_to.Length != 3)
            {
                mensaje += "\nerro na moeda de destino, verifique";
            }
            else
            {
                var valid = consul_Moeda.ValidaMDAAsync(m_to);
                if (valid.Result != "encontro")
                {
                    mensaje += "\n" + m_to + " não está registrado";
                }
            }
            if (m_from == m_to)
            {
                mensaje += "\nOrigem e Destino devem ser diferentes";
            } 
            Console.Write("\tValor a cambiar:  ");
            int m_valor = Convert.ToInt32(Console.ReadLine());
            if (m_valor < 1)
            {
                mensaje += "\nO valor deve ser maior que 0";
            }
            if (mensaje != " ")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t" + mensaje);
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
                            decimal decimalValue = decimal.Round((decimal)cambios.Result, 2);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n" + cambios.Query.From + " " + cambios.Query.Amount + " => " + cambios.Query.To + " " + decimalValue);
                            Console.ForegroundColor = ConsoleColor.Green;
                            decimalValue = decimal.Round((decimal)cambios.Info.Rate, 6);
                            Console.WriteLine("Taxa: " + decimalValue);
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Sua solicitação não pôde ser executada");
                            Console.ReadLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Sua solicitação não pôde ser executada");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
