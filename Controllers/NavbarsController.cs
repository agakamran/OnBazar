using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBazar.Data;
using OnBazar.Models;
using OnBazar.Services;

namespace Webschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavbarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Navbar> _nav = null;
        private readonly IRepository<NavbarRole> _navrol = null;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<Role> _rol = null;
        private readonly IRepository<UserRole> _userol = null; 
        public NavbarsController(ApplicationDbContext context, IRepository<Navbar> nav,
            IRepository<Role> rol, IRepository<UserRole> userol,
            IRepository<NavbarRole> navrol, RoleManager<IdentityRole> roleMgr)
        {
            _context = context;
            _nav = nav;
            _navrol = navrol;
            _roleManager = roleMgr;
            _rol = rol;
            _userol = userol;
        }
        // GET: api/Navbars
        [HttpGet]
        [Route("_getnav")]
        public IQueryable<Navbar> _getnav()
        {           
            return _nav.GetAll().OrderBy(o => o.ink);
        }
        /*  public void menu()
          {
              #region MyRegion         

              if (_nav.GetAll().Count() == 0)
              {
                  var MenuItems = new List<Navbar>();
                  var ii = Guid.NewGuid().ToString();
                  MenuItems.Add(new Navbar()
                  {
                      nid = ii,
                      pid = null,
                      ntitle = " Administrator",
                      npath = "admins/",
                      nicon = null,
                      nlan = "Az",
                      ncsay = 1,
                      nrol = "Administrator",
                      ink = 1,
                      nisparent = true
                  });
                  MenuItems.Add(new Navbar()
                  {
                      nid = Guid.NewGuid().ToString(),
                      pid = ii,
                      ntitle = "ADD MENU",
                      npath = "admins/addmen",
                      nicon = null,
                      nlan = "Az",
                      ncsay = 1,
                      nrol = "Administrator",
                      ink = 3,
                      nisparent = false
                  });
                  MenuItems.Add(new Navbar()
                  {
                      nid = Guid.NewGuid().ToString(),
                      pid = ii,
                      ntitle = "ADD ROLE",
                      npath = " admins/role",
                      nicon = null,
                      nlan = "Az",
                      ncsay = 1,
                      nrol = "Administrator",
                      ink = 2,
                      nisparent = false
                  });
                   //MenuItems.Add(new Navbar() {nid = Guid.NewGuid().ToString(), pid = null, ntitle = "Ana Səhvə",
                   //                           npath = "/", nicon = null, nlan = " Az", ncsay = 1, nrol = "User", ink = 4, nisparent = false
                   //});
                   //MenuItems.Add(new Navbar(){nid = Guid.NewGuid().ToString(), pid = null, ntitle = "Haqqımda",
                   //                          npath = "lessin/blog", nicon = null, nlan = "Az",ncsay = 1, nrol = "User", ink = 5, nisparent = false
                   //});
                  var nr = new NavbarRole();
                  foreach (var m in MenuItems)
                  {
                      _nav.InsertAsync(m);

                      nr.nrid = Guid.NewGuid().ToString();
                      nr.nid = m.nid;
                      var role = _roleManager.FindByNameAsync(m.nrol).Result;
                      nr.RoleId = role.Id;
                      _navrol.InsertAsync(nr);
                  }
              }
              #endregion
          }*/

        //  [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("_getnavbar")]  //esas
        public IActionResult _getnavbar([FromBody] string[] rol)
        {
            //  if (rol.Contains("User") == false) { rol[0] = "User"; }    
            var res = (from a in _nav.GetAll()
                       join b in _navrol.GetAll() on a.nid equals b.nid
                       join c in _context.Roles on b.RoleId equals c.Id
                       where rol.Any(id => c.NormalizedName == id)
                       select new
                       {
                           a.nid,
                           a.pid,
                           a.ntitle,
                           a.npath,
                           a.nlan,
                           a.nicon,
                           a.ink,
                           a.nisparent,
                           a.ncsay,
                           a.nrol,
                           c.Id,
                           c.Name,
                           isChecked=false
                       });
           
            int dd = res.Count();
            
            return Ok( res.OrderBy(o => o.ink).ToList());
        }
        [HttpGet]
        [Route("_getRoles")]
        public IActionResult _getRoles(string use)
        {            
            var res = (from ro in _roleManager.Roles.ToList()
            join usr in _context.UserRoles on ro.Id equals usr.RoleId                       
            where usr.UserId == use
                       select new
                       {
                           ro.Id,
                           ro.Name,
                           ro.NormalizedName,
                           usr.UserId
                       });
            return Ok(res.ToList());
        }
        // select ro.id,ro.name,usro.RoleId, case when usro.RoleId is null then 'false' when usro.RoleId is not null then 'true' end as checked from[dbo].[AspNetRoles] ro
        //left join[dbo].[AspNetUserRoles] usro on usro.RoleId = ro.Id

        //var res = (from ro in _roleManager.Roles.ToList()
        //           join usro in _context.UserRoles on ro.Id equals usro.RoleId into ur
        //          // join usro in _userol.GetAll() on ro.Id equals usro.RoleId into ur
        //           from _ur in ur.DefaultIfEmpty()
        //           where _ur.UserId == use
        //           select new
        //           {
        //               ro.Id,
        //               ro.Name,
        //               ro.NormalizedName,
        //               _ur.RoleId,
        //               _ur.UserId
        //           })
        //.GroupBy(t => t.RoleId).Select(p => new {
        //    Count = p.Count(),
        //    RoleId = p.Key == "" ? "false" : p.Key != "" ? "true" : "checked"
        //});
        //return Ok(res.ToList());
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postmenu")]
        public async Task<IActionResult> postmenu([FromBody] Navbar p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (p.nid != "")
            {
                var _p = _nav.GetAll().FirstOrDefault(x => x.nid == p.nid);
                _p.nid = _p.nid;
                if (p.pid != null)
                {
                    var nnid = _nav.GetAll().FirstOrDefault(f => f.nid == p.pid && f.nlan == p.nlan);
                    _p.pid = nnid.nid;
                }
                else
                {
                    _p.pid = null;
                }
                _p.ntitle = p.ntitle;
                _p.npath = p.npath;
                _p.nlan = p.nlan;
                _p.ncsay = p.ncsay;
                _p.nisparent = p.nisparent;
                _p.nicon = p.nicon;
                _p.nrol = p.nrol;
                await _nav.EditAsync(_p);
                //=============================
                if (p.nrol != "true")
                {
                    var nr = _navrol.GetAll().FirstOrDefault(y => y.nid == p.nid);
                    IdentityRole role = await _roleManager.FindByNameAsync(p.nrol);//"User"
                    nr.RoleId = role.Id;
                    // nr.RoleId = nr.nid;               
                    await _navrol.EditAsync(nr);
                }
               
                return Ok();
            }
            else
            {
                if (_nav.GetAll().FirstOrDefault(f => f.ntitle == p.ntitle && f.nlan == p.nlan) == null)
                {
                    int Idt = 0;
                    if (_nav.GetAll().Count() > 0)
                    {
                        Idt = _nav.GetAll().Max(u => u.ink);
                    }
                    var pp = new Navbar();
                    pp.nid = Guid.NewGuid().ToString();
                    if (p.pid != null)
                    {
                        var nnid = _nav.GetAll().FirstOrDefault(f => f.ntitle.Trim() == p.pid.Trim() && f.nlan == p.nlan);
                        if (nnid != null) { pp.pid = nnid.nid; }
                        else { pp.pid = p.pid; }
                    }

                    pp.ntitle = p.ntitle.Trim();
                    pp.npath = p.npath.Trim();
                    pp.nlan = p.nlan;
                    pp.nlan = p.nlan;
                    pp.ncsay = p.ncsay;
                    pp.nisparent = p.nisparent;
                    pp.nicon = p.nicon;
                    pp.nrol = p.nrol;
                    if (p.ink != 0) { pp.ink = p.ink; }
                    else { pp.ink = Idt + 1; }
                    await _nav.InsertAsync(pp);
                    //=============================
                    if (p.nrol != "")
                    {
                        var nr = new NavbarRole();
                        nr.nrid = Guid.NewGuid().ToString();
                        nr.nid = pp.nid;
                        IdentityRole role = await _roleManager.FindByNameAsync(p.nrol);//"User"
                        nr.RoleId = role.Id;
                        await _navrol.InsertAsync(nr);
                    }
                    
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delmenu")]
        public async Task<IActionResult> delmenu([FromBody] Navbar p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _nav.GetAll().FirstOrDefault(x => x.nid == p.nid);
            await _nav.DeleteAsync(_p);
            var vr = _navrol.GetAll().FirstOrDefault(x => x.nid == _p.nid);
            await _navrol.DeleteAsync(vr);
            return Ok();
        }
        //-------------------------------------------------------------------------------
       // [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("_getnrol")]
        public IEnumerable _getnrol(string rol)
        {
           // string xx = "";
            //if(rol!="Op"){  xx = "where c.Name.Contains(" + rol + ")";  }

            var res = (from a in _nav.GetAll()
                       join b in _navrol.GetAll() on a.nid equals b.nid into nro                       
                       from _b in nro.DefaultIfEmpty()
                       join c in _roleManager.Roles on _b.RoleId equals c.Id 
                      // from _c in tt.DefaultIfEmpty()
                      // + xx                     
                       where c.Name.Contains(rol)
                       select new
                       {
                           _b.nid ,
                           id= _b.RoleId ,
                           c.Name,
                           a.ntitle                           
                       });
            int n = res.Count();
            return res.ToList();
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("_getnavrol")]
        public IEnumerable _getnavrol()
        {
            var res = (from a in _nav.GetAll()
                       join b in _navrol.GetAll() on a.nid equals b.nid
                       join c in _context.Roles on b.RoleId equals c.Id
                     //  where c.Name.Contains(rol)
                       select new
                       {
                           a.nid,
                           a.pid,
                           a.ntitle,
                           a.npath,
                           a.nlan,
                           a.nicon,
                           c.Id,
                           c.Name
                       });
            return res.ToList();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("_addnavrol")]
        public async Task<IActionResult> _addnavrol([FromBody] NavbarRole nr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (nr.nid != "" && nr.RoleId != null)
            {
                var xx = _navrol.GetAll().FirstOrDefault(p => p.nid == nr.nid && p.RoleId == nr.RoleId);
                var _nr = new NavbarRole();
                if (xx ==null)
                {
                    _nr.nrid = Guid.NewGuid().ToString();
                    _nr.nid = nr.nid;
                    _nr.RoleId = nr.RoleId;
                    await _navrol.InsertAsync(_nr);
                }
            }
               
                return Ok();
            //}
        }
        
        // POST: api/Menu
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("_delnavrol")]
        public async Task<IActionResult> _delnavrol([FromBody] NavbarRole nr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var _nr = _navrol.GetAll().FirstOrDefault(x => x.RoleId == nr.RoleId && x.nid == nr.nid);
                //        //foreach(var f in _nr){
                            await _navrol.DeleteAsync(_nr);
                //        //}      
                return Ok();
            }
        }
        // GET: api/Navbars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNavbar([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var navbar = await _context.navbars.FindAsync(id);

            if (navbar == null)
            {
                return NotFound();
            }

            return Ok(navbar);
        }

        // PUT: api/Navbars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavbar([FromRoute] string id, [FromBody] Navbar navbar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != navbar.nid)
            {
                return BadRequest();
            }

            _context.Entry(navbar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavbarExists(id))
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

        // POST: api/Navbars
        [HttpPost]
        public async Task<IActionResult> PostNavbar([FromBody] Navbar navbar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.navbars.Add(navbar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNavbar", new { id = navbar.nid }, navbar);
        }

        // DELETE: api/Navbars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNavbar([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var navbar = await _context.navbars.FindAsync(id);
            if (navbar == null)
            {
                return NotFound();
            }

            _context.navbars.Remove(navbar);
            await _context.SaveChangesAsync();

            return Ok(navbar);
        }

        private bool NavbarExists(string id)
        {
            return _context.navbars.Any(e => e.nid == id);
        }
    }
}