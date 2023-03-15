using Blossom_API.Data;
using Blossom_API.Models;
using Blossom_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blossom_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlossomController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BlossomDto>> GetBlossom()
        {
            return Ok(BlossomStore.blossomList);           
        }

        [HttpGet("id", Name ="GetBlossomProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BlossomDto> GetBlossom(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var blossom = BlossomStore.blossomList.FirstOrDefault(v => v.Id == id);

            if (blossom == null)
            {
                return NotFound();
            }

            return Ok(blossom);
          
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult<BlossomDto> PostProduct([FromBody] BlossomDto blossomDto) 
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }
            if(BlossomStore.blossomList.FirstOrDefault(v=>v.Name.ToLower() == blossomDto.Name.ToLower()) !=null)
            {
                ModelState.AddModelError("NameExist", "The product with that name already exists");
                return BadRequest(ModelState);
            }
            if(blossomDto == null) 
            {
                return BadRequest(blossomDto);
            }
            if (blossomDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            blossomDto.Id =BlossomStore.blossomList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            BlossomStore.blossomList.Add(blossomDto);

            return CreatedAtRoute("GetBlossomProduct", new {id= blossomDto.Id}, blossomDto);
        }

        [HttpDelete ("{id}")]
        [ProducesResponseType (StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            if(id== 0)
            {
                return BadRequest();
            }
            var blossom = BlossomStore.blossomList.FirstOrDefault(v=>v.Id ==id);
            if (blossom == null)
            {
                return NotFound();
            }
            BlossomStore.blossomList.Remove(blossom);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, [FromBody] BlossomDto blossomDto)
        {
            if(blossomDto == null || id!=blossomDto.Id) 
            {
                return BadRequest();
            }
            var blossom = BlossomStore.blossomList.FirstOrDefault(v => v.Id == id);
        }
    }
}
