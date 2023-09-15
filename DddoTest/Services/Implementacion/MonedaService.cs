using Microsoft.EntityFrameworkCore;
using DddoTest.Models;
using DddoTest.Services.Contrato;

namespace DddoTest.Services.Implementacion
{
    public class MonedaService : IMonedaServices
    {
        private TestDbContext _context;

        public MonedaService(TestDbContext testDbContext)
        {
            _context = testDbContext;
        }

        public async Task<List<MonedaDdo>> GetList()
        {
            try
            {
                List<MonedaDdo> lista = new List<MonedaDdo>();
                lista = await _context.MonedaDdos.ToListAsync();
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
