using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Interfaces;

namespace Server.Controller;

[Route("[controller]")]
[ApiController]
public class MetasController : ControllerBase
{
    private readonly IMetasRepository metasRepository;

    public MetasController(IMetasRepository _metasRepository)
    {
        metasRepository = _metasRepository;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(metasRepository.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return Ok(metasRepository.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Add(TblMetas data)
    {
        try
        {
            if (metasRepository.Put(data, null))
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
    public async Task<IActionResult> Update(TblMetas data)
    {
        try
        {
            if(metasRepository.Put(data, data.MetaId))
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
    public async Task<IActionResult> Delete(TblMetas data)
    {
        try
        {
            if(metasRepository.DeleteById(data.MetaId))
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
