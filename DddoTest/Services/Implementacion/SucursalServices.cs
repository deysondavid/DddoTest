using Microsoft.EntityFrameworkCore;
using DddoTest.Models;
using DddoTest.Services.Contrato;

namespace DddoTest.Services.Implementacion
{
    public class SucursalServices : ISucursalServices
    {
        private TestDbContext _context;

        public SucursalServices(TestDbContext testDbContext)
        {
            _context = testDbContext;
        }
        public async Task<SucursalesDdo> Get(int idSucursal)
        {
            try
            {
                SucursalesDdo? encontrado = new SucursalesDdo();
                
                encontrado = await _context.SucursalesDdos.Include(suc => suc.IdMonedaNavigation)
                    .Where(m => m.IdSucursal == idSucursal).FirstOrDefaultAsync();
                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<SucursalesDdo>> GetList()
        {
            try
            {
                List<SucursalesDdo> lista = new List<SucursalesDdo>();
                lista = await _context.SucursalesDdos.
                    Include(obj => obj.IdMonedaNavigation)
                    .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SucursalesDdo> Add(SucursalesDdo modelo)
        {
            try
            {
                _context.SucursalesDdos.Add(modelo);
                    await _context.SaveChangesAsync();
                return modelo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(SucursalesDdo modelo)
        {
            try
            {
                _context.SucursalesDdos.Update(modelo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(SucursalesDdo sucursalesDdo)
        {
            try
            {
                _context.SucursalesDdos.Remove(sucursalesDdo);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
       
    }
}
