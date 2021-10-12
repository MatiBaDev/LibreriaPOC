using AutoMapper;
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
    public class EditorialesController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public EditorialesController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editorial>>> Get()
        {
            return await _applicationDbContext.Editoriales.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Editorial>> Get(int id)
        {
            var editorial = await _applicationDbContext.Editoriales.FirstOrDefaultAsync(c => c.Id == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return editorial;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Editorial editorial)
        {
            await _applicationDbContext.Editoriales.AddAsync(editorial);
            await _applicationDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var editorial = await _applicationDbContext.Editoriales.FirstOrDefaultAsync(c => c.Id == id);
            if (editorial == null)
            {
                return NotFound();
            }

            _applicationDbContext.Entry(editorial).State = EntityState.Deleted;
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }


    }
}
