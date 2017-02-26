using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using iStore.Models;

namespace iStore.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StoreItemsController : Controller
    {
        private StoreContext _context;
        public StoreItemsController(StoreContext context)
        {
            _context = context;
        }


        // api/StoreItems
        [HttpGet("")]
        public async Task<IActionResult> All()
        {
            return this.Ok(await _context.StoreItems.ToListAsync());
        }


        // api/StoreItems/[id]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var item = _context.StoreItems.Find(id);
            if (item == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Json(item);
            }
        }

        // api/StoreItems
        [HttpPost("")]
        public IActionResult Post([FromBody] StoreItem item)
        {
             if (item == null)
             {
                 return BadRequest();
             }

             _context.Add(item);
             _context.SaveChangesAsync();        
            return this.Ok(item); // enhancement - should really set the headers location url to the new resource.
        }

    }
}