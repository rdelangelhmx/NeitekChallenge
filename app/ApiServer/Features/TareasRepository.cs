using Server.Entities;
using Server.Interfaces;
using Server.Persistence;

namespace Server.Features;

public class TareasRepository : ITareasRepository
{
    private readonly NeitekContext context;

    public TareasRepository(NeitekContext _context)
    { context = _context; }

    public IEnumerable<TblTareas> GetAll(int metaId)
    {
        try
        {
            return context.TblTareas.Where(w => w.MetaId == metaId).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public TblTareas GetById(int id, int metaId)
    {
        try
        {
            return context.TblTareas.FirstOrDefault(w => w.MetaId == metaId && w.TareaId == id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool DeleteById(int id, int metaId)
    {
        try
        {
            var data = context.TblTareas.FirstOrDefault(w => w.MetaId == metaId && w.TareaId == id);
            context.Remove(data);
            context.SaveChanges();  
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Put(TblTareas data, int? id)
    {
        try
        {
            if (id.HasValue)
                context.TblTareas.Update(data);
            else
                context.TblTareas.Add(data);
            context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
