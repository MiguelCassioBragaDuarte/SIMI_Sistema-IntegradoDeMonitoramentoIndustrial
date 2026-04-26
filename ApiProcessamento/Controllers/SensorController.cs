using ApiProcessamento.Config;
using ApiProcessamento.Models;
using ApiProcessamento.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations; // Adicione esta biblioteca via NuGet se quiser usar o SwaggerOperation

namespace ApiProcessamento.Controllers
{
    [ApiController]
    [Route("api/v1/sensores")]
    [Produces("application/json")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;
        private readonly ApiConfig _config;

        public SensorController(ISensorService sensorService, IOptions<ApiConfig> config)
        {
            _sensorService = sensorService;
            _config = config.Value;
        }

        /// <summary>
        /// Registra uma nova medição industrial.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/v1/sensores
        ///     {
        ///        "tipo": "Umidade",
        ///        "unidade": "%",
        ///        "valor": 45.5,
        ///        "dataHora": "2026-04-26T15:00:00"
        ///     }
        /// </remarks>
        /// <param name="medicao">Objeto de medição (Temperatura ou Umidade).</param>
        /// <returns>Mensagem de confirmação.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Receber([FromBody] Medicoes medicao)
        {
            if (medicao == null) return BadRequest("O corpo da medição não pode ser nulo.");

            // Normalização para evitar erros de digitação (case insensitive)
            string tipoNormalizado = medicao.Tipo.Trim().ToLower();

            // Validação de Regra de Negócio: Temperatura
            if (tipoNormalizado == "temperatura" && medicao.Valor > _config.MaxTemperatura)
            {
                return BadRequest(new { erro = "Limite Excedido", detalhe = $"Temperatura ({medicao.Valor}°C) acima do limite permitido." });
            }

            // Validação de Regra de Negócio: Umidade
            if (tipoNormalizado == "umidade" && medicao.Valor > _config.MaxUmidade)
            {
                return BadRequest(new { erro = "Limite Excedido", detalhe = $"Umidade ({medicao.Valor}%) acima do limite permitido." });
            }

            await _sensorService.SalvarMedicao(medicao);

            // Retornar 201 Created é semanticamente mais correto para POST
            return CreatedAtAction(nameof(Listar), new { id = medicao.Id }, new { mensagem = "Dado persistido com sucesso!", data = DateTime.Now });
        }

        /// <summary>
        /// Obtém o histórico completo de sinais industriais.
        /// </summary>
        /// <returns>Lista de medições ordenadas por data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Medicoes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var dados = await _sensorService.ObterTodasMedicoes();
            return Ok(dados);
        }
    }
}