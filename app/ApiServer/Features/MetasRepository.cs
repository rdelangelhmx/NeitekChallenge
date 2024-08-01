using Microsoft.EntityFrameworkCore;
using Server.Entities;
using Server.Interfaces;
using Server.Persistence;

namespace Server.Features;

public class MetasRepository : IMetasRepository
{
    private readonly NeitekContext context;

    public MetasRepository(NeitekContext _context)
    { context = _context; }

    public IEnumerable<TblMetas> GetAll()
    {
        try
        {
            return context.TblMetas.Include(i => i.TblTareas).ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public TblMetas GetById(int id)
    {
        try
        {
            return context.TblMetas.Include(i => i.TblTareas).FirstOrDefault(w => w.MetaId == id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool DeleteById(int id)
    {
        try
        {
            var data = context.TblMetas.FirstOrDefault(w => w.MetaId == id);
            context.Remove(data);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Put(TblMetas data, int? id)
    {
        try
        {
            if (id.HasValue)
                context.TblMetas.Update(data);
            else
                context.TblMetas.Add(data);
            context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
