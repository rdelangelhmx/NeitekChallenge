using Server.Entities;

namespace Server.Interfaces;

public interface IMetasRepository
{
    IEnumerable<TblMetas> GetAll();
    TblMetas GetById(int id);
    bool DeleteById(int id);
    bool Put(TblMetas data, int? id);
}
