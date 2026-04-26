using System;
using System.Text.Json.Serialization;

namespace Shared
{
    public class SensorData
    {
        public int Id { get; set; }

        [JsonPropertyName("valor")]
        public double ValorOriginal { get; set; }

        // Temperatura exibe o valor real enviado (ex: 25°C)
        [JsonIgnore]
        public double Temperatura => ValorOriginal;

        // Humidade gera um valor diferente baseado no ID ou no tempo 
        // para não ser igual à temperatura na sua SA
        [JsonIgnore]
        public double Umidade
        {
            get
            {
                // Simula uma humidade lógica: se a temp é 25, a umid é ~50%
                // Usamos uma conta simples para os valores variarem de forma independente
                var calculoSimulado = (ValorOriginal * 1.5) + (Id % 10);
                return calculoSimulado > 100 ? 95 : Math.Round(calculoSimulado, 1);
            }
        }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = "Industrial";

        [JsonPropertyName("unidade")]
        public string Unidade { get; set; } = "Múltipla";

        [JsonPropertyName("dataHora")]
        public DateTime Timestamp { get; set; }

        // Propriedade de Status para a SA (Regra de Negócio)
        [JsonIgnore]
        public string Status => Temperatura > 35 ? "ALERTA" : "OK";

        [JsonIgnore]
        public string CorStatus => Temperatura > 35 ? "Red" : "Green";
    }
}