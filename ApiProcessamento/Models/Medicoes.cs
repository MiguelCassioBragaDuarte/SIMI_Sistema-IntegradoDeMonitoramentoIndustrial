using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProcessamento.Models
{
    /// <summary>
    /// Representa a entidade de uma medição industrial no banco de dados.
    /// </summary>
    public class Medicoes
    {
        /// <summary>
        /// Identificador único da medição (Chave Primária).
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// O tipo do sinal enviado (ex: Temperatura, Pressão, Umidade).
        /// </summary>
        [Required]
        public string Tipo { get; set; } = string.Empty;

        /// <summary>
        /// O valor lido pelo sensor.
        /// </summary>
        [Required]
        public double Valor { get; set; }

        /// <summary>
        /// A unidade de medida (ex: °C, %, bar).
        /// </summary>
        [Required]
        public string Unidade { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora exata em que a medição foi processada.
        /// </summary>
        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}