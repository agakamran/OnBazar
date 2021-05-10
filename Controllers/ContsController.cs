using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBazar.Data;
using OnBazar.Models;
using OnBazar.Services;

namespace OnBazar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Cont> _con = null;
        string format = "yyyy-MM-dd HH:mm:ss";
        string format1 = "yyyy-MM-dd";
        public ContsController(ApplicationDbContext context, IRepository<Cont> con)
        {
            _context = context;
            _con = con;
        }

        // GET: api/Conts
        [HttpGet]
        public IEnumerable<Cont> Getconts()
        {
            return _con.GetAll();
        }

        // GET: api/Conts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCont([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cont = await _context.conts.FindAsync(id);

            if (cont == null)
            {
                return NotFound();
            }

            return Ok(cont);
        }

        // PUT: api/Conts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCont([FromRoute] string id, [FromBody] Cont cont)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cont.cid)
            {
                return BadRequest();
            }

            _context.Entry(cont).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContExists(id))
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

        // POST: api/Conts
        [HttpPost]
        [Route("PostCont")]
        public async Task<IActionResult> PostCont([FromBody] Cont c)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _c = _con.GetAll().FirstOrDefault(x => x.userId == c.userId && x.tarix.ToString(format1) == DateTime.Now.Date.ToString(format1));
            if (_c != null)
            {
              
                _c.cid = _c.cid;
                _c.userId = c.userId;
                _c.yourname = c.yourname;
                _c.subject = c.subject;
                _c.message = c.message;
                _c.tarix = DateTime.Now.Date;
                _c.isdelete = c.isdelete;
                await _con.EditAsync(_c);
                return Ok();
            }
            else
            {
                var cc = new Cont();
                cc.cid = Guid.NewGuid().ToString();
                cc.userId = c.userId; ;
                cc.yourname = c.yourname;
                cc.subject = c.subject;
                cc.message = c.message;
                cc.tarix = DateTime.Now.Date;
                cc.isdelete = false;
                await _con.InsertAsync(cc);
                return Ok();
            }
            // _context.conts.Add(cont);
            //  await _context.SaveChangesAsync();
            //  return CreatedAtAction("GetCont", new { id = cont.cId }, cont);
        }

        // DELETE: api/Conts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCont([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cont = await _context.conts.FindAsync(id);
            if (cont == null)
            {
                return NotFound();
            }

            _context.conts.Remove(cont);
            await _context.SaveChangesAsync();

            return Ok(cont);
        }

        private bool ContExists(string id)
        {
            return _context.conts.Any(e => e.cid == id);
        }
    }
}