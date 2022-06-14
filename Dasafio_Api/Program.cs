using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Dasafio_Api
{
    class Program
    {
        static void Main(string[] args)
        {
            Consul_Moeda consul_Moeda = new Consul_Moeda();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n\t\t▒▒▒▒▒▒▒▒▒▒▒▒ SERVIÇO DE CÂMBIO ▒▒▒▒▒▒▒▒▒▒▒▒" +
                            "\n\t\t              SEJA BEM-VINDO");
            Console.ForegroundColor = ConsoleColor.Green;
            string mensaje = " ";
            Console.Write("\n\n\t\tVerifique as moedas disponíveis digitando 1: ");
            string m_from = Console.ReadLine();
            if (m_from == "1")
            {
                var valido = consul_Moeda.ValidaMDAAsync(m_from);
                Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\t\t\tMoneda Origen:    ");
            m_from = Console.ReadLine();
            if (m_from == null || m_from.Length  != 3)
            {
                mensaje += "\n\t\tErro na moeda origen, verifique";
            }
            else
            {
                var valido = consul_Moeda.ValidaMDAAsync(m_from);
                if (valido.Result != "encontro")
                {
                    mensaje += "\n\t\t" + m_from + " não está registrado";
                }
            }
            Console.Write("\t\t\tMoneda Destino:   "); 
            string m_to = Console.ReadLine();
            if (m_to == null || m_to.Length != 3)
            {
                mensaje += "\n\t\terro na moeda de destino, verifique";
            }
            else
            {
                var valido = consul_Moeda.ValidaMDAAsync(m_to);
                if (valido.Result != "encontro")
                {
                    mensaje += "\n\t\t" + m_to + " não está registrado";
                }
            }
            if (m_from == m_to)
            {
                mensaje += "\n\t\tOrigem e Destino devem ser diferentes";
            } 
            Console.Write("\t\t\tValor a cambiar:  ");
            int m_valor;
            while (!int.TryParse(Console.ReadLine(), out m_valor))
            {
                Console.WriteLine("\t\tInsira apenas números inteiros");
                Console.Write("\t\t\tValor a cambiar: ");
            }
            if (m_valor < 1)
            {
                mensaje += "\n\t\tO valor deve ser maior que 0";
            }
            if (mensaje != " ")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t\t\t" + mensaje);
                Console.ReadLine();
            }
            string buscar = "from=" + m_from + "&to=" + m_to + "&amount=" + m_valor;
            string strURL = "https://api.exchangerate.host/convert?" + buscar;
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
                            Console.WriteLine("\n\t\t" + cambios.Query.From + " " + cambios.Query.Amount + " => " + cambios.Query.To + " " + decimalValue);
                            Console.ForegroundColor = ConsoleColor.Green;
                            decimalValue = decimal.Round((decimal)cambios.Info.Rate, 6);
                            Console.WriteLine("\t\tTaxa: " + decimalValue);
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("\t\tSua solicitação não pôde ser executada");
                            Console.ReadLine();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\t\tSua solicitação não pôde ser executada");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
