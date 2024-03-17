using Magic_VilllaAPI.Data;
using Magic_VilllaAPI.Models;
using Magic_VilllaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Magic_VilllaAPI.Controllers;

//[Route("api/[controller]")]

[Route("api/VillaAPI")]
[ApiController]
public class VillaApiControllers : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult<VillaDTO> GetVillas(int id)
    {
        if(id == 0)
        {
            return BadRequest();
        }

        if((VillaStore.villaList.FirstOrDefault(u => u.Id == id)) == null)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpPost]
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO) { 
        if(villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
        if(villaDTO .Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        villaDTO.Id = VillaStore.villaList.OrderDescending(u=>u.Id).FirstOrDefault().Id + 1;

        VillaStore.villaList.Add(villaDTO );

        return Ok(villaDTO);
    }
}
