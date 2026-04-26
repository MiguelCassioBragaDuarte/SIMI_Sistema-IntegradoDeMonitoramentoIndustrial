using ApiProcessamento.Data;
using ApiProcessamento.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProcessamento.Services
{
    /// <summary>
    /// Interface para padronizar o serviço de sensores.
    /// </summary>
    public interface ISensorService
    {
        Task<IEnumerable<Medicoes>> ObterTodasMedicoes();
        Task SalvarMedicao(Medicoes medicao);
    }

    /// <summary>
    /// Classe que gerencia a lógica de persistência e regras dos sensores.
    /// </summary>
    public class SensorService : ISensorService
    {
        private readonly AppDbContext _context;

        public SensorService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recupera todas as medições do banco SQLite ordenadas pela mais recente.
        /// </summary>
        public async Task<IEnumerable<Medicoes>> ObterTodasMedicoes()
        {
            return await _context.Medicoes
                .OrderByDescending(m => m.DataHora)
                .ToListAsync();
        }

        /// <summary>
        /// Salva a medição no banco de dados e garante a integridade da data.
        /// </summary>
        public async Task SalvarMedicao(Medicoes medicao)
        {
            // Regra: Sempre registra o horário do servidor no momento da persistência
            medicao.DataHora = DateTime.Now;

            _context.Medicoes.Add(medicao);
            await _context.SaveChangesAsync();
        }
    }
}