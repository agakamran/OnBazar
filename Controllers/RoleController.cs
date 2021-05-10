using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnBazar.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace OnBazar.Controllers.Adminstrator
{
   // [Route("api/Role")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class RoleController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> userManager;
        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            _roleManager = roleMgr;
            userManager = userMrg;
        }
        [HttpGet]
        [Route("_getRoles")]
        public IActionResult _getRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_roleManager.Roles);
        }
        [HttpGet]//("{id}")
        [Route("_getUser")]
        public IActionResult _getUser(string id)
        {
            RoleEditModel Mod = new RoleEditModel();
            if (id != null)
            {
                IdentityRole role = _roleManager.FindByIdAsync(id).Result;
                var members = new List<ApplicationUser>();
                var nonMembers = new List<ApplicationUser>();
                foreach (ApplicationUser user in userManager.Users)
                {
                    var list = userManager.IsInRoleAsync(user, role.Name).Result ? members : nonMembers;
                    list.Add(user);
                }
                Mod.Role = role; Mod.Members = members; Mod.NonMembers = nonMembers;
            }
            return Ok(Mod);
        }
        [HttpPost]
        [Route("_EditRoles")]
        public IActionResult _EditRoles([FromBody] RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    ApplicationUser user = userManager.FindByIdAsync(userId).Result;
                    if (user != null)
                    {
                        result = userManager.AddToRoleAsync(user, model.RoleName).Result;
                        if (!result.Succeeded)
                        {
                            AddErrors(result);
                        }
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    ApplicationUser user = userManager.FindByIdAsync(userId).Result;
                    if (user != null)
                    {
                        result = userManager.RemoveFromRoleAsync(user, model.RoleName).Result;
                        if (!result.Succeeded)
                        {
                            AddErrors(result);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {

                var Rol = new RoleModificationModel();
                Rol.RoleId = model.RoleId;
                return _getUser(Rol.RoleId);
            }
        }

        [HttpPost]
        [Route("_CreateRole")]
        public async Task<IActionResult> _CreateRole([FromBody] Role rol)
        {
            if (ModelState.IsValid)
            {
                if (rol.id == "")
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(rol.name));
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        AddErrors(result);
                        return BadRequest();
                    }
                }
                /*  else
                  {

                      IdentityResult result = await _roleManager.UpdateAsync();
                      if (result.Succeeded)
                      {
                          return Ok();
                      }
                      else
                      {
                          AddErrors(result);
                          return BadRequest();
                      }

                  }*/

            }
            return Ok(rol.name);
        }
        [HttpPost]
        [Route("_DeleteRole")]
        public async Task<IActionResult> _DeleteRole([FromBody] Role rol)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(rol.id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok(_roleManager.Roles);
                }
                else
                {
                    AddErrors(result);
                    return BadRequest(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
                return Ok();
            }
            // return View("Index", _roleManager.Roles);
        }


        #region Helpers       
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        //[HttpGet]
        //private IActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        #endregion
        //------------------------------------

    }
}