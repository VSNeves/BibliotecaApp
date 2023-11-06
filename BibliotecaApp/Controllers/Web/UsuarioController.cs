using BibliotecaApp.Data;
using BibliotecaApp.Extensions;
using BibliotecaApp.Models;
using BibliotecaApp.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Controllers.Web
{
   
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsuariosController : Controller
    {
        private readonly BibliotecaAppContext _context;

        public UsuariosController(BibliotecaAppContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToArrayAsync());
            
        }


        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            return _context.Usuario != null ?
                        View(await _context.Usuario.ToListAsync()) :
                        Problem("Entity set 'BibliotecaAppContext.Usuario' is null.");
        }

        
        [HttpGet("Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,Senha")] Usuario usuario)
        {
            if (!ModelState.IsValid) return View(usuario);

            ModelState.AddModelErrorIfNotEmpty("Nome", usuario.Nome.ValidarNome());
            ModelState.AddModelErrorIfNotEmpty("Email", usuario.Email.ValidarEmail());
            ModelState.AddModelErrorIfNotEmpty("Senha", usuario.Senha.ValidarSenha());
            if (EmailExists(usuario.Email))
            {
                ModelState.AddModelErrorIfNotEmpty("Email", "E-mail já existe");
            }

            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

       
        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }


        [HttpPost("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Email,Senha")] Usuario usuario)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(usuario);

            ModelState.AddModelErrorIfNotEmpty("Nome", usuario.Nome.ValidarNome());
            ModelState.AddModelErrorIfNotEmpty("Email", usuario.Email.ValidarEmail());
            ModelState.AddModelErrorIfNotEmpty("Senha", usuario.Senha.ValidarSenha());
            if (EmailExists(usuario.Email))
            {
                ModelState.AddModelErrorIfNotEmpty("Email", "E-mail já existe");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Usuarios");
            }

            return View(usuario);
        }

        
        [HttpGet("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        
        [HttpPost("Delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (!IsAdministrador())
            {
                return Unauthorized();
            }

            if (_context.Usuario == null)
            {
                return Problem("Entity set 'BibliotecaAppContext.Usuario' is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Usuarios");
            
        }

        private bool IsAdministrador()
        {
            return true;
        }

        private bool UsuarioExists(Guid id)
        {
            return (_context.Usuario?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool EmailExists(string email)
        {
            return (_context.Usuario?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}



