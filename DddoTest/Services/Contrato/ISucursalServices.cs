using DddoTest.Models;

namespace DddoTest.Services.Contrato
{
    public interface ISucursalServices
    {
        Task<List<SucursalesDdo>> GetList();
        Task<SucursalesDdo> Get(int idSucursal);
        Task<SucursalesDdo> Add(SucursalesDdo sucursalesDdo);
        Task<bool> Update(SucursalesDdo sucursalesDdo);
        Task<bool> Delete(SucursalesDdo sucursalesDdo);
    }
}
