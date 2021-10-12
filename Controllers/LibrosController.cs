using BibliotecaAPI.Entidades;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public LibrosController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> Get()
        {
            var libros = await _applicationDbContext.Libros.ToListAsync();
            return Ok(libros);

            //var libros = await (from libro in _applicationDbContext.Libros
            //                    where libro.reservado == false
            //                    select new
            //                    {
            //                        Id = libro.Id,
            //                        Nombre = libro.Nombre,
            //                        Descripcion = libro.Descripcion,
            //                        Autor = libro.Autor,
            //                        Editorial = libro.Editorial
            //                    }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await _applicationDbContext.Libros.FirstOrDefaultAsync(c => c.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            
            return Ok(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Libro libro)
        {
            await _applicationDbContext.Libros.AddAsync(libro);
            libro.reservado = false;
            libro.fechaSubida = DateTime.Now;
            await _applicationDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var libro = await _applicationDbContext.Libros.FirstOrDefaultAsync(c => c.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            _applicationDbContext.Entry(libro).State = EntityState.Deleted;
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("reservar-libro/{id}")]
        public async Task<ActionResult> ReservarLibro(int id, [FromBody]Libro libro)
        {
            var _libro = _applicationDbContext.Libros.FirstOrDefault(l => l.Id == id);
            _libro.Nombre = _libro.Nombre;
            _libro.Descripcion = _libro.Descripcion;
            _libro.Autor = _libro.Autor;
            _libro.Editorial = _libro.Editorial;
            _libro.fechaSubida = _libro.fechaSubida;
            _libro.reservado = true;

            await _applicationDbContext.SaveChangesAsync();
            return Ok("Libro reservado");
        }

        [HttpPut("liberar-libro/{id}")]
        public async Task<ActionResult> LiberarLibro(int id, [FromBody] Libro libro)
        {
            var _libro = _applicationDbContext.Libros.FirstOrDefault(l => l.Id == id);
            _libro.Nombre = _libro.Nombre;
            _libro.Descripcion = _libro.Descripcion;
            _libro.Autor = _libro.Autor;
            _libro.Editorial = _libro.Editorial;
            _libro.fechaSubida = _libro.fechaSubida;
            _libro.reservado = false;

            await _applicationDbContext.SaveChangesAsync();
            return Ok("Libro devuelto");
        }

    }
}
