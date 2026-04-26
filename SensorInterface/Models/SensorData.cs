using System.Text.Json.Serialization;

namespace SensorInterface.Models // ou Shared, dependendo de onde está seu arquivo
{
    public class SensorData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("temperatura")]
        public double Temperatura { get; set; }

        [JsonPropertyName("umidade")]
        public double Umidade { get; set; }

        [JsonPropertyName("pressao")]
        public double Pressao { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}