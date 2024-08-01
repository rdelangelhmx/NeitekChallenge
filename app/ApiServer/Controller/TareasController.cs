using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Interfaces;

namespace Server.Controller;

[Route("[controller]")]
[ApiController]
public class TareasController : ControllerBase
{
    private readonly ITareasRepository tareasRepository;

    public TareasController(ITareasRepository _tareasRepository)
    {
        tareasRepository = _tareasRepository;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll(int metaId)
    {
        try
        {
            return Ok(tareasRepository.GetAll(metaId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id, int metaId)
    {
        try
        {
            return Ok(tareasRepository.GetById(id, metaId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Add(TblTareas data)
    {
        try
        {
            if (tareasRepository.Put(data, null))
                return Ok(data);
            else
                throw new Exception("No se puede agregar el registro");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(TblTareas data)
    {
        try
        {
            if(tareasRepository.Put(data, data.TareaId))
                return Ok(data);
            else
                throw new Exception("No se puede actualizar el registro");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(TblTareas data)
    {
        try
        {
            if(tareasRepository.DeleteById(data.TareaId, data.MetaId))
                return Ok(data);
            else
                throw new Exception("No se puede borrar el registro");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
