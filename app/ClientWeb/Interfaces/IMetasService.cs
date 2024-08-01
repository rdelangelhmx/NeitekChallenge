using Client.Models;

namespace Client.Interfaces;

public interface IMetasService
{
    Task<IEnumerable<MetasModel>> GetAllAsync();
    Task<MetasModel> GetByIdAsync(int id);
    Task<MetasModel> AddAsync(MetasModel data);
    Task<MetasModel> UpdateAsync(MetasModel data);
    Task<MetasModel> DeleteAsync(MetasModel data);
}
