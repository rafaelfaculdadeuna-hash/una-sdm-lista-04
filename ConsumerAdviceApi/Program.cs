using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumerAdviceApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://api.adviceslip.com/advice";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    using (JsonDocument document = JsonDocument.Parse(json))
                    {
                        JsonElement root = document.RootElement;
                        JsonElement slip = root.GetProperty("slip");
                        string advice = slip.GetProperty("advice").GetString();

                        Console.WriteLine("Conselho de Hoje:");
                        Console.WriteLine(advice);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao consumir a API:");
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadLine();
        }
    }
}