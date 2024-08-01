using Server.Entities;

namespace Server.Interfaces;

public interface ITareasRepository
{
    IEnumerable<TblTareas> GetAll(int metaId);
    TblTareas GetById(int id, int metaId);
    bool DeleteById(int id, int metaId);
    bool Put(TblTareas data, int? id);
}
