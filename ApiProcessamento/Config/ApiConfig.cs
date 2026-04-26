namespace ApiProcessamento.Config
{
    /// <summary>
    /// Classe de configuração para os parâmetros globais da API.
    /// Define os limites aceitáveis para os sinais industriais.
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// Limite máximo de temperatura permitida (°C).
        /// </summary>
        public double MaxTemperatura { get; set; }

        /// <summary>
        /// Limite máximo de umidade permitida (%).
        /// </summary>
        public double MaxUmidade { get; set; }
    }
}