using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.DTOs
{
    public class CategoriaCreationDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}
