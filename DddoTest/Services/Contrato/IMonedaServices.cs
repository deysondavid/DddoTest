using DddoTest.Models;


namespace DddoTest.Services.Contrato
{
    public interface IMonedaServices
    {
        Task<List<MonedaDdo>> GetList();
    }
}
