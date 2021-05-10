using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBazar.Data;
using OnBazar.Models;
using OnBazar.Services;

namespace OnBazar.Controllers
{
   // [Authorize(Roles = "Administrator")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
       // private readonly IHostingEnvironment _host;
        //private readonly IAntiforgery _antiforgery;
       // private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Navbar> _na = null;
        private readonly IRepository<Cart> _car = null;
        private readonly IRepository<vido> _vi = null;
       // private readonly ApplicationDbContext _context;

        public CartsController( //IHostingEnvironment host, IAntiforgery antiforgery, UserManager<ApplicationUser> userManager,
             IRepository<Navbar> na,  IRepository<Cart> car, IRepository<vido> vi)
        {
          //  _host = host;
            // _context = context;
           // _antiforgery = antiforgery;
            //_userManager = userManager;
            _na = na;
            _car = car;
            _vi = vi;
        }
      /*  [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("_getpages")]
        public IEnumerable<Navbar> _getpages()
        {
            return _pa.GetAll();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postpage")]
        public async Task<IActionResult> postpage([FromBody] Page p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _pa.GetAll().FirstOrDefault(x => x.Pid == p.Pid);
            if (_p!= null)
            {
                            
                _p.Pid = _p.Pid;
                _p.pagename = p.pagename;      
                await _pa.EditAsync(_p);
                return Ok();
            }
            else
            {
                var pp = new Page();
                pp.Pid = Guid.NewGuid().ToString();
                pp.pagename = p.pagename;
                await _pa.InsertAsync(pp);
                return Ok();
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delpage")]
        public async Task<IActionResult> delpage([FromBody] Page p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _pa.GetAll().FirstOrDefault(x => x.Pid == p.Pid);
            await _pa.DeleteAsync(_p);
            return Ok();           
        }*/

        [HttpPost]
        [Route("uplodeVidio")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> uplodeVidio()
        {
            try
            {
                byte[] imageData = null;
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files["file"];
                    string exten = Path.GetExtension(file.FileName);                   
                    string nid = Request.Form["nid"];
                    string lang = Request.Form["lan"];
                    string charda = Request.Form["harda"];
                    int sira =int.Parse( Request.Form["sira"])-1;
                   // sira = (int.Parse(sira) - 1).ToString();
                    var v = new vido();
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)file.Length);
                        v.vid = Guid.NewGuid().ToString();
                        v.vidio = imageData;
                        v.url= "Images/" + lang + "_" + nid + "_" + charda + "_" + sira + "_"+ file.FileName;
                      //  var root = _host.ContentRootPath + "\\Resources\\"+ lang+"_"+ pag +"_"+ harda +"_" + sira + "_" + file.FileName;
                        await _vi.InsertAsync(v);
                        if (file.Length > 0)
                        {//file varsa papkaya saxlayaq
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", lang + "_" + nid +"_"+ charda + "_" + sira + "_" + file.FileName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                    var _p = _na.GetAll().FirstOrDefault(x => x.nid == nid);
                    var _c = _car.GetAll().FirstOrDefault(x => x.nid ==_p.nid && x.clan==lang && x.charda==charda && x.csira==sira);                    
                    if (_c==null)
                    {
                        var cc = new Cart();
                        cc.cid = Guid.NewGuid().ToString();
                        cc.clan = lang;
                        cc.charda = charda;
                        cc.nid = _p.nid;
                        cc.vid = v.vid;
                        cc.csira = 1;
                        await _car.InsertAsync(cc);
                    }
                    else
                    {                       
                        _c.vid = v.vid;
                        await _car.EditAsync(_c);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
        // GET: api/Carts
        [HttpPost]
        [Route("getCarts")]
        public IEnumerable<Cart> getCarts([FromBody] Cart c)
        {
            if (c!=null){
               // int cc = _car.GetAll().Where(t=>t.harda==c.harda && t.pId == _pa.GetAll().LastOrDefault(h => h.pagename == c.pId).Pid).Count();
                return _car.GetAll().Where(t => t.charda == c.charda && t.clan == c.clan && t.nid == _na.GetAll().LastOrDefault(h => h.ntitle == c.nid).nid);
               
            }
            else {
                return  _car.GetAll();
            }            
        }
        [HttpGet]
        [Route("allCarts")]
        public IEnumerable allCarts()
        {
            var res = (from a in _car.GetAll()
                       join b in _na.GetAll() on a.nid equals b.nid
                      // join c in _vi.GetAll() on a.vid equals c.vid
                       //where a.Status == "Pending"
                       select new
                        {
                            a.cid,
                            a.csubject,
                            a.nid,
                            b.ntitle,
                            a.charda,
                           a.clan,
                            a.csira,
                            a.cheader,
                            a.ctex,
                           a.cpris,
                           a.cbuton,
                           a.vid,
                          //  url=c.url,
                          a.ctarix

                        });
            return res.ToList();            
        }
        [HttpPost]
        [Route("_getsay")]
        [Authorize(Roles = "Administrator")]
        public int _getsay([FromBody] Cart cart)
        {
           
            int Idt = 0;
            var px =new Navbar();
            px =_na.GetAll().FirstOrDefaultAsync(p => p.nid == cart.nid).Result;
            if (px != null)
            {
                var ca = _car.GetAll().Where(f => f.nid == px.nid && f.clan == cart.clan && f.charda == cart.charda);
                if (ca != null)
                {
                    if (ca.Count() != 0) { Idt = ca.Max(u => u.csira); }
                }
            }           
            return Idt+1;
        }
        // GET: api/Carts/5
        [HttpGet]
        [Route("GetCart")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetCart( string id)
        {
            var px =await _na.GetAll().FirstOrDefaultAsync(p => p.ntitle == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(px==null)
            {
               // var p = new Page();
               // p.Pid = Guid.NewGuid().ToString();
              //  p.pagename =id;
              // await _pa.InsertAsync(p);
            }
            int Idt = _car.GetAll().Max(u => u.csira);
            var cart = _car.GetAll().Where(c => c.nid == px.nid && c.csira== Idt);
          
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/Carts/5
       /* [HttpPut("{id}")]
        public async Task<IActionResult> PutCart([FromRoute] string id, [FromBody] Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cart.cid)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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
        */
        // POST: api/Carts
        [HttpPost]
        [Route("PostCart")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostCart([FromBody] Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cart.cid != "")
            {
                var _c = _car.GetAll().FirstOrDefault(x => x.cid == cart.cid);
                var pp = _na.GetAll().FirstOrDefault(x => x.nid == cart.nid);
                _c.nid = pp.nid;   
                _c.csubject=cart.csubject;
                _c.cid = cart.cid;
                _c.csira = cart.csira;
                _c.charda = cart.charda;
                _c.cheader = cart.cheader;
                _c.ctex = cart.ctex;
                _c.cpris = cart.cpris;
                _c.cbuton = cart.cbuton;              
                //_c.vid = cart.vid;
                _c.ctarix = cart.ctarix;
                _c.clan = cart.clan;
               
                await _car.EditAsync(_c);
                return Ok();
              //  return BadRequest();
            }
            else
            {
                try
                {
                    var pp = _na.GetAll().FirstOrDefault(x => x.nid == cart.nid);
                    cart.cid = Guid.NewGuid().ToString();
                    cart.ctarix = DateTime.Now;
                    cart.nid = pp.nid;
                    await _car.InsertAsync(cart);
                    return Ok();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee);
                    return BadRequest();
                }
            }
           
        }

        // DELETE: api/Carts/5
        [HttpPost]
        [Route("delcart")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> delcart([FromBody] Cart ca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var x = await _car.GetAll().FirstOrDefaultAsync(d => d.cid ==ca.cid);
            var cart = _car.DeleteAsync(x);
            if (ca.vid != "")
            {
                var v = await _vi.GetAll().FirstOrDefaultAsync(d => d.vid ==x.vid);
               await _vi.DeleteAsync(v);

                FileInfo file = new FileInfo(v.url);
                if (file.Exists)
                {
                    file.Delete();
                }               
            }
            if (cart == null)
            {
                return NotFound();
            }

            //  _car.Carts.Remove(cart);
            //  await _context.SaveChangesAsync();
           // return Ok();
            return Ok(cart);
        }

       // private bool CartExists(string id) { return _context.Carts.Any(e => e.cid == id); }
    }
}