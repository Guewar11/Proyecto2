using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2_Api.Datos;
using Proyecto2_Api.Modelo;
using Proyecto2_Api.Modelo.NewFolder;

namespace Proyecto2_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly ILogger<ProyectoController> _logger;
        private readonly AppDbContext _appDbContext;
        public ProyectoController(ILogger<ProyectoController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
       public ActionResult<IEnumerable<ProyectoDto>> GetProyecto2()
        {
            _logger.LogInformation("Obtener los datos de GetProyecto2b");
            return Ok (_appDbContext.ProyectoModelos.ToList());
        }
        [HttpGet("id", Name = "GetProyecto2b")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProyectoDto> GetProyecto2b(int id)
        {   if(id == 0)
            {
                _logger.LogError("Error al traer Vista con Id");
                return BadRequest();
            }
            // var proyect = ProyectoStore.ProyectoList.FirstOrDefault(v => v.Id == id);
            var proyect = _appDbContext.ProyectoModelos.FirstOrDefault(v => v.Id == id);
           
            if(proyect == null)
            {
                return NotFound();
            }
            return Ok(proyect);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProyectoDto> CrearProyecto([FromBody]ProyectoDto proyectoDto)         
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_appDbContext.ProyectoModelos.FirstOrDefault(v => v.Nombre.ToLower() == proyectoDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "Ya existe esa vista!");
                return BadRequest(ModelState);
            }
            if (proyectoDto==null)
            {
                return BadRequest(proyectoDto);
            }
            
            if (proyectoDto.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            ProyectoModelo modelo = new()
            {
                Nombre = proyectoDto.Nombre,
                Detalle = proyectoDto.Detalle,
                ImagenUrl = proyectoDto.ImagenUrl,
                Cantidades = proyectoDto.Cantidades,
                Tarifa = proyectoDto.Tarifa,
                MetrosCuadrados = proyectoDto.MetrosCuadrados,
                Amenidad = proyectoDto.Amenidad
            };

            _appDbContext.ProyectoModelos.Add(modelo);
            _appDbContext.SaveChanges();

            //proyectoDto.Id = ProyectoStore.ProyectoList.OrderByDescending(v=>v.Id).FirstOrDefault().Id+1;
           // ProyectoStore.ProyectoList.Add(proyectoDto);
            return CreatedAtRoute("GetProyecto2b", new { id = proyectoDto.Id }, proyectoDto);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProyect(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
           // var proyect = ProyectoStore.ProyectoList.FirstOrDefault(v => v.Id == id);
            var proyect = _appDbContext.ProyectoModelos.FirstOrDefault(v => v.Id == id);


            if (proyect == null)
            {
                return NotFound();
            }
           // ProyectoStore.ProyectoList.Remove(proyect);
           _appDbContext.ProyectoModelos.Remove(proyect);
            _appDbContext.SaveChanges();

            return NoContent();

        }
        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProyect (int id,[FromBody]ProyectoDto proyectoDto)
        {
            if (proyectoDto == null || id != proyectoDto.Id)
            {
                return BadRequest();
            }
            /*
            var proyect = ProyectoStore.ProyectoList.FirstOrDefault(v => v.Id == id);
            proyect.Nombre=proyectoDto.Nombre;
            proyect.Cantidades=proyectoDto.Cantidades;
            proyect.MetrosCuadrados=proyectoDto.MetrosCuadrados;
            */

            ProyectoModelo modelo = new()
            {
                Id = proyectoDto.Id,
                Nombre = proyectoDto.Nombre,
                Detalle = proyectoDto.Detalle,
                ImagenUrl = proyectoDto.ImagenUrl,
                Cantidades = proyectoDto.Cantidades,
                Tarifa = proyectoDto.Tarifa,
                MetrosCuadrados = proyectoDto.MetrosCuadrados,
                Amenidad = proyectoDto.Amenidad


            };
            _appDbContext.ProyectoModelos.Update(modelo);
            _appDbContext.SaveChanges();

            return NoContent();
        }
        [HttpPatch("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialProyect(int id, JsonPatchDocument<ProyectoDto> patchdto)
        {
            if (patchdto == null || id == 0)
            {
                return BadRequest();
            }
            //var proyect = ProyectoStore.ProyectoList.FirstOrDefault(v => v.Id == id);
            var proyect = _appDbContext.ProyectoModelos.AsNoTracking().FirstOrDefault(v => v.Id == id);

            ProyectoDto proyectoDto = new()
            {
                Id = proyect.Id,
                Nombre = proyect.Nombre,
                Detalle = proyect.Detalle,
                ImagenUrl = proyect.ImagenUrl,
                Cantidades = proyect.Cantidades,
                Tarifa = proyect.Tarifa,
                MetrosCuadrados = proyect.MetrosCuadrados,
                Amenidad = proyect.Amenidad

            };
            if (proyect == null) return BadRequest();

            patchdto.ApplyTo(proyectoDto, ModelState);
            //  patchdto.ApplyTo (proyect, ModelState);


            if (!ModelState.IsValid)         
            {
                return BadRequest(ModelState);

            }

            ProyectoModelo modelo = new()
            {
                Id = proyectoDto.Id,
                Nombre = proyectoDto.Nombre,
                Detalle = proyectoDto.Detalle,
                ImagenUrl = proyectoDto.ImagenUrl,
                Cantidades = proyectoDto.Cantidades,
                Tarifa = proyectoDto.Tarifa,
                MetrosCuadrados = proyectoDto.MetrosCuadrados,
                Amenidad = proyectoDto.Amenidad
            };
            _appDbContext.ProyectoModelos.Update(modelo);
            _appDbContext.SaveChanges();
            return NoContent();
        }

    }
}
