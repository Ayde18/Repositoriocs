using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiProyecto.Models; // Asegúrate de que este sea el espacio correcto para tu modelo Item

namespace MiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            // Busca el elemento por ID
            var item = await _context.Items.FindAsync(id);

            // Si no se encuentra el elemento, devuelve un error 404
            if (item == null)
            {
                return NotFound();
            }

            // Devuelve el elemento encontrado
            return Ok(item); // Utiliza Ok() para devolver un código 200
        }

        // Otras acciones (Get, Post, Put, Delete, etc.) pueden ir aquí

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items); // Devuelve la lista de items
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item); // Devuelve 201 Created
        }

        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest(); // Devuelve 400 Bad Request si el ID no coincide
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); /

