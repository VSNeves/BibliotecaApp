using BibliotecaApp.Data;
using BibliotecaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BibliotecaAppContext _context;
    
        private List<Usuario> usuarios;


        public UsuariosController(BibliotecaAppContext context)
        {
            _context = context;
        }

        [HttpGet("{Usuarios}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return _context.Usuario == null ? (ActionResult<IEnumerable<Usuario>>)NotFound() : await _context.Usuario.ToListAsync();
        }

       //Listar usuário específico
        [HttpGet("Usuario/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario([FromRoute] int id)
        {
            if (_context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }



        [HttpPost("Usuario/{cliente}")]
        public ActionResult<Usuario> AddCliente([FromRoute] Usuario cliente)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            // Lógica de validação das propriedades do cliente
            usuarios.Add(cliente);

            return CreatedAtAction(nameof(GetUsuario), new { id = cliente.Id }, cliente);
            
           
        }

        [HttpPost("Usuario/{administrador}")]
        public ActionResult<Usuario> AddAdministrador([FromRoute] Usuario administrador)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            // Lógica de validação das propriedades do administrador
            usuarios.Add(administrador);

            return CreatedAtAction(nameof(GetUsuario), new { id = administrador.Id }, administrador);
            
            

        }

        //Atualizar
        [HttpPut("Usuario/{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] Guid id, [FromBody] Usuario usuario)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            if (id.ToString().Equals(usuario.Id.ToString()))
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

                
        [HttpDelete("Usuario/{id}")]
        public async Task<ActionResult> DeleteUsuarioAsync(int id)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            if (_context.Usuario == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IsAdministrador() 
        {
            return true;
        }

        private bool UsuarioExists(Guid id)
        {
            return (_context.Usuario?.Any(e => e.Id.ToString().Equals(id.ToString()))).GetValueOrDefault();
        }
        private bool EmailExists(string email)
        {
            return (_context.Usuario?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}



