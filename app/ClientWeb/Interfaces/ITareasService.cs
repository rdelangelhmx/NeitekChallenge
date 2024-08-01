using Client.Models;

namespace Client.Interfaces;

public interface ITareasService
{
    Task<IEnumerable<TareasModel>> GetAllAsync(int metaId);
    Task<TareasModel> GetByIdAsync(int id, int metaId);
    Task<TareasModel> AddAsync(TareasModel data);
    Task<TareasModel> UpdateAsync(TareasModel data);
    Task<TareasModel> DeleteAsync(TareasModel data);
}
