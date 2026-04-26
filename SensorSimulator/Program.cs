using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared;

// Configuração inicial
var http = new HttpClient();
var random = new Random();

Console.WriteLine("=== SIMULADOR INDUSTRIAL (SA) ===");
Console.WriteLine("Conectado em: https://localhost:7179/api/v1/sensores");
Console.WriteLine("--------------------------------------------------\n");

while (true)
{
    var medicao = new
    {
        Tipo = "Industrial",
        Unidade = "Celsius/Percentual",
        Valor = Math.Round(random.NextDouble() * (40 - 15) + 15, 2), // Gera a Temperatura (ex: 25.5)
        DataHora = DateTime.Now
    };

    try
    {
        var response = await http.PostAsJsonAsync("https://localhost:7179/api/v1/sensores", medicao);

        if (response.IsSuccessStatusCode)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCESSO] {DateTime.Now:HH:mm:ss} -> Valor Enviado: {medicao.Valor}%");
        }
        else
        {
            string erroDetalhado = await response.Content.ReadAsStringAsync();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERRO API] Status: {response.StatusCode} - {erroDetalhado}");
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ERRO CONEXÃO] A API está ligada? Detalhes: {ex.Message}");
    }

    Console.ResetColor();
    // Espera 3 segundos para a próxima leitura
    await Task.Delay(3000);
}