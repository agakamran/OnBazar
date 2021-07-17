using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnBazar.Data;
using OnBazar.Models;
using OnBazar.Models.AccountViewModels;
using OnBazar.Services;

namespace OnBazar.Controllers
{
    //https://odetocode.com/blogs/scott/archive/2017/02/06/anti-forgery-tokens-and-asp-net-core-apis.aspx
    //Oyan 0031685045716    
    // [Route("api/Account")]   
    //[Authorize(Roles = "Administrator")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    // [EnableCors("AllowOrigin")]
    public class AccountController : Controller
    {
        private readonly IRepository<store> _firma = null;
        private readonly ApplicationDbContext db;
        // private readonly IAntiforgery _antiforgery;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly IEmailSender _emailSender;
        //private readonly ILogger _logger;
        //  private readonly IRepository<payment> _pa = null; //imkani
        ///  private readonly IRepository<Price> _pr = null;  //qiymet
        ////  private readonly IRepository<invoice> _inv = null;  //qiymet
        //  private readonly IRepository<Customer> _cu = null;
        //  private readonly IHostingEnvironment _hostingEnvironment;
        //-----------------------
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;
        // static int _issueCount = 0;
        //-----------------------


        public AccountController(
            IRepository<store> firma,
            ApplicationDbContext _db,
            //  IAntiforgery antiforgery,
            //  IRepository<payment> pa,  IRepository<Customer> cu, IRepository<Price> pr,  IRepository<invoice> inv,     IHostingEnvironment hostingEnvironment,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, IConfiguration config, RoleManager<IdentityRole> roleMgr,
            //ILogger<AccountController> logger, 
            IUserValidator<ApplicationUser> userValid,
          IPasswordValidator<ApplicationUser> passValid,
        IPasswordHasher<ApplicationUser> passwordHash)
        {
            _firma = firma;
            db = _db;
            // _pa = pa; _cu = cu;  _pr = pr;  _inv = inv;    _hostingEnvironment = hostingEnvironment;
            // _antiforgery = antiforgery;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            // _logger = logger;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            //-------------
            _config = config;
            _roleManager = roleMgr;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "kamran", "Salam555" };
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            //Request.HttpContext.Response.Headers.Add("X-Total-Count", "20");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
               // Logout();
                var user1 = await _userManager.FindByNameAsync(model.Email);
                if (user1 == null || !(await _userManager.IsEmailConfirmedAsync(user1)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Ok(new { mesage = "400" });
                }
                else
                {
                    IActionResult response = Unauthorized();
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        //  _logger.LogInformation("User logged in.");
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        var tokenString = BuildToken(user);
                        var use = new
                        {
                            uid = user.Id,
                            displayName = user.UserName,
                            email = user.Email,
                            providerId = user.providerId,
                            photoUrl = user.photoUrl,
                            isEmailConfirmed = user.EmailConfirmed,
                            PhoneNumber = user.PhoneNumber,
                            percent=user.percent,
                            token = tokenString,
                            mesage = ""
                        };
                        return response = Ok(use //token = tokenString
                        );
                    }
                    if (result.RequiresTwoFactor)
                    {
                        // var x = result.RequiresTwoFactor;
                        // return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                        // return Ok(result.RequiresTwoFactor);
                        return Ok(new { RequiresTwoFactor = "true" });
                    }
                    if (result.IsLockedOut)
                    {
                        // _logger.LogWarning("User account locked out.");
                        //  return RedirectToAction(nameof(Lockout));
                    }
                    else
                    {
                        StatusMessage = "Invalid login attempt.";
                        // return View(model);
                        // return Ok(new { StatusMessage = "Sizin şifrə uygun deyil." });
                        return BadRequest(new { mesage = StatusMessage });

                    }

                }
            }
            // If we got this far, something failed, redisplay form
            return Ok();
        }
        //public string Haqq="read";
        /* private async Task<string> _Role(ApplicationUser user)
         {      
             var Ro=  await _userManager.GetRolesAsync(user);
             for(int f=0;Ro.Count()>f;f++)
             {
                 if (Ro[f].ToString() == "Administrator") { Haqq = "writer"; }
             }
             return String.Join(",", await _userManager.GetRolesAsync(user));
         */
        private string BuildToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.NameId,user.Id)
            };
            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach (String role in userRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                //IsExpired: DateTime.Now,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // _logger.LogInformation("User logged out.");
            return Ok(new { mesage = "User logged out." });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var msg = "Bad Request";
            if (!ModelState.IsValid)
            {
                #region
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                #endregion
            }
            else // (ModelState.IsValid)
            {
                //-----------------------------
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    IP = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                    // Mebleg =1
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // _logger.LogInformation("User created a new account with password.");
                    var applicationRole = await _roleManager.FindByNameAsync("User");
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);

                        /*  var MaxDate = (from d in _pr.GetAll().Where(k => k._pname == "Imtahan") select d._pdate).Max();
                          var p = _pr.GetAll().SingleOrDefault(k => k._pname == "Imtahan" && k._pdate == MaxDate)._price;
                          payment pp = new payment();
                          pp._pid = Guid.NewGuid().ToString(); pp._userId = user.Id; pp._Sum = p * 4; pp._date = DateTime.Now;
                          await _pa.InsertAsync(pp);

                          Customer cca = new Customer();
                          cca.IdentityId = user.Id; cca.Locale = ""; cca.Location = "";
                          await _cu.InsertAsync(cca);*/
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    if (_firma.GetAll().FirstOrDefault(f => f.userId == user.Id) == null)
                    {
                        var pp = new store();
                        pp.storId = Guid.NewGuid().ToString();
                        pp.storname = user.UserName;
                        pp.storphone = "";
                        pp.storadress = "";
                        pp.storemail = user.Email;
                        pp.userId = user.Id;
                        await _firma.InsertAsync(pp);

                    }
                    //   Url = _config["ClientDomain"]; 
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl.Replace("https://localhost:44336", _config["ClientDomain"]), "E");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //  _logger.LogInformation("User created a new account with password.");
                    msg = "User created a new account with password.";
                    return Ok(msg);

                }
                return Error(result.ToString(), 1);
                //AddErrors(result);
            }
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("checkUser")]
        public async Task<IActionResult> checkUser(string ema)
        {
            string mes = "Error";
            if (ema == null)
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(new { mesage = ModelState });
            }
            var result = await _userManager.FindByEmailAsync(ema);
            if (result != null) { mes = ema; }
            return Ok(mes);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                // return RedirectToAction(nameof(HomeController.Index), "Home");
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(new { mesage = ModelState });
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // var result1 = await _userManager.ConfirmEmailAsync(user, code1);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            // return Ok(result.Succeeded ? "ConfirmEmail" : "Error");          
            return Ok(new { mesage = result.Succeeded ? "ConfirmEmail" : "Error" });//new { mesage = "Təşəkker Emailiniz təsdiq olundu." }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    //  return RedirectToAction(nameof(ForgotPasswordConfirmation));
                    return Ok(new { mesage = "Siz qeydiyyatdan keçməmisiniz." });
                }
                else
                {
                    // For more information on how to enable account confirmation and password reset please
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);//Request.Scheme
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl.Replace("https://localhost:44378", _config["ClientDomain"]), "Rp");
                    // return emailSender.SendEmailAsync(email, "Confirm your email",
                    // $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
                    // await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                    // $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                    return Ok(new { mesage = "Sizin email ünvana link göndərildi." });
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok(model);
        }

        [HttpGet]
        [Route("_getConfirmation")]
        public IActionResult _getConfirmation(string id)
        {
            var us = _userManager.Users.FirstOrDefault(x => x.Id == id);
            return Ok(us);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mesage = ModelState });
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                //return RedirectToAction(nameof(ResetPasswordConfirmation));
                return BadRequest(new { mesage = "Don't ResetPasswordConfirmation." });
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
            if (result.Succeeded)
            {
                //return RedirectToAction(nameof(ResetPasswordConfirmation));
                return Ok(new { mesage = "ResetPasswordConfirmation." });
            }
            AddErrors(result);
            return Ok();
        }
        //-------------------------------------------------------------------------------------------------
        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mesage = ModelState });
            }
            var user = await _userManager.FindByEmailAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (user == null)
            {
                StatusMessage = "Sizin şifrə uygun deyil.";
                return BadRequest(new { mesage = StatusMessage });
            }
            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (hasPassword)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    AddErrors(changePasswordResult);
                    return View(model);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                //  _logger.LogInformation("User changed their password successfully.");               

                return Ok(new { StatusMessage = "Your password has been changed." });
            }
            else { return Ok(new { StatusMessage = "Sizin şifrə uygun deyil." }); }
        }



        //------Account son--------------------


        private JsonResult Errors(IdentityResult result)
        {
            var items = result.Errors.Select(x => x.Description)
                .ToArray();
            return new JsonResult(items) { StatusCode = 402 };
        }
        private JsonResult Error(string message, int L)
        {
            // http://www.restapitutorial.com/httpstatuscodes.html
            int kod = 0;
            switch (kod)
            {
                case 1: { kod = 100; } break;
                case 2: { kod = 200; } break;
                case 3: { kod = 300; } break;
                case 4: { kod = 400; } break;
                case 5: { kod = 500; } break;
                default: { kod = 0; } break;
            }
            return new JsonResult(message) { StatusCode = kod };
        }
        [TempData]
        public string ErrorMessage { get; set; }
        [HttpGet]
        [AllowAnonymous]
        [Route("_LoginWith2fa")]
        public async Task<IActionResult> _LoginWith2fa(bool rememberMe)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                //throw new ApplicationException($"Unable to load two-factor authentication user.");
                return BadRequest(new { twoFactorCode = $"Unable to load two-factor authentication user." });
            }
            var model = new LoginWith2faViewModel { RememberMe = rememberMe };//bool.Parse(
            // ViewData["ReturnUrl"] = returnUrl;
            return Ok(model);
        }
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Route("LoginWith2fa")]
        public async Task<IActionResult> LoginWith2fa([FromBody] LoginWith2faViewModel model, bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
                return Ok(new { twoFactorCode = ModelState.IsValid });
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                // throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return Ok(new { twoFactorCode = $"Unable to load user with ID '{_userManager.GetUserId(User)}'." });
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                // _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                //  return RedirectToLocal(returnUrl);
                return Ok(new { twoFactorCode = "User with ID {UserId} logged in with 2fa." });
            }
            else if (result.IsLockedOut)
            {
                // _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                // return RedirectToAction(nameof(Lockout));
                return Ok(new { twoFactorCode = "User with ID {UserId} account locked out." });
            }
            else
            {
                //  _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Ok(new { twoFactorCode = "Invalid authenticator code." });
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("LoginWithRecoveryCode")]
        public async Task<IActionResult> LoginWithRecoveryCode()
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                // throw new ApplicationException($"Unable to load two-factor authentication user.");
                return BadRequest(new { recoveryCode = $"Unable to load two-factor authentication user." });
            }
            //  ViewData["ReturnUrl"] = returnUrl;
            return Ok(user);
        }
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [Route("LoginWithRecoveryCode")]
        public async Task<IActionResult> LoginWithRecoveryCode([FromBody] LoginWithRecoveryCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
                return BadRequest(new { recoveryCode = ModelState.IsValid });
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                //throw new ApplicationException($"Unable to load two-factor authentication user.");
                return BadRequest(new { recoveryCode = $"Unable to load two-factor authentication user." });
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                //_logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                //return RedirectToLocal(returnUrl);
                return Ok();
            }
            if (result.IsLockedOut)
            {
                // _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                // return RedirectToAction(nameof(Lockout));
                return Ok(new { recoveryCode = "User with ID {UserId} account locked out." });
            }
            else
            {
                // _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return Ok(new { recoveryCode = "Invalid recovery code entered." });
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("Lockout")]
        public IActionResult Lockout()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                // _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ExternalLoginConfirmation")]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //  _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        //  return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }
        //----------------------------------------------------------------------
        [HttpGet]
        [Route("profil")]
        public async Task<IActionResult> profil()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string nameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByEmailAsync(nameIdentifier);
            // Retrieve this user from the database
            //  User user = await _context.User.SingleOrDefaultAsync(u => u.Username == nameIdentifier);

            //var xz = identity.Claims.;
            // var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage,
                photoUrl = user.photoUrl
            };

            return Ok(model);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("profil")]
        public async Task<IActionResult> profil([FromBody] IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //  string nameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByEmailAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            // var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }
            var d = _firma.GetAll().FirstOrDefault(f => f.userId == user.Id && f.storemail == user.Email);
            var phoneNumber = user.PhoneNumber;
            if (d == null)
            {
                var pp = new store();
                pp.storId = Guid.NewGuid().ToString();
                pp.storname = user.UserName;
                pp.storphone = model.PhoneNumber;
                pp.storadress = "Owner";
                pp.storemail = user.Email;
              //  pp.storpercent = model.percent;
                pp.userId = user.Id;
                await _firma.InsertAsync(pp);
                //return Ok();
            }
            else if (model.PhoneNumber != d.storphone )//|| d.storpercent != model.percent
            {
                d.storId = d.storId;
                d.storphone = model.PhoneNumber;
               // if (d.storpercent != model.percent) { d.storpercent = model.percent; }
                await _firma.EditAsync(d);
                phoneNumber = model.PhoneNumber;
               // return Ok();
            }
            if (model.photoUrl != user.photoUrl || model.PhoneNumber != phoneNumber || user.percent != model.percent)
            {
                //var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                user.PhoneNumber = model.PhoneNumber;
                user.photoUrl = model.photoUrl;
                user.percent = model.percent;
               
                var setphotoUrl = await _userManager.UpdateAsync(user);
                if (!setphotoUrl.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }
            StatusMessage = "Your profile has been updated";
            return Ok(new { StatusMessage = "Your profile has been updated" });
        }
        //-------Administrator bash
        /*  [HttpGet]
     [Route("_getprice")]
     public IActionResult _getprice(string ll)
     {
         if (ll != null)
         {
             var MaxDate = (from d in _pr.GetAll().Where(k => k._pname == ll) select d._pdate).Max();
             var pr = _pr.GetAll().FirstOrDefault(x => x._pname == ll && x._pdate == MaxDate)._price;
             return Ok(pr);
         }
         else
         {
             var MaxDate = (from d in _pr.GetAll() select d._pdate).Max();
             var pr = _pr.GetAll().OrderByDescending(x => x._pdate);
             return Ok(pr);
         }
     }
    [HttpPost]
     [Route("_addprise")]
     [Authorize(Roles = "Administrator")]
     public async Task<IActionResult> _addprise([FromBody]Price p)
     {
         if (p != null && p._price > 0 && p._pname != "")
         {
             Price pp = new Price();

             pp._pid = Guid.NewGuid().ToString();
             pp._pname = p._pname;
             pp._price = p._price;
             pp._pdate = DateTime.Now;
             pp.is_actived = true;
             pp.Percent = 0;
             await _pr.InsertAsync(pp);
             return Ok();
         }
         else { return BadRequest("0"); }
     }
     */
        //---------------payment--------------------------
        /*  [HttpGet("{id}")]
          [Route("_getpayment")]
          public IActionResult _getpayment(string email)
          {
              var pay = from us in _userManager.Users
                        join pa in _pa.GetAll() on us.Id equals pa._userId
                        select new { pa._pid, us.Email, pa._Sum, pa._date };
              if (email != null)
              {
                  var pay1 = from us in _userManager.Users
                             join pa in _pa.GetAll() on us.Id equals pa._userId
                             where us.Email == email
                             select new { pa._pid, us.Email, pa._Sum, pa._date };
                  return Ok(pay1);
              }
              return Ok(pay);
          }*/
        /*  [HttpPost]
          [Route("_addpayment")]
          [Authorize(Roles = "Administrator")]
          public async Task<IActionResult> _addpayment([FromBody]payment p)
          {
              if (p != null && p._Sum > 0 && p._userId != null)
              {
                  var us = _userManager.FindByEmailAsync(p._userId).Result;
                  if (p._pid == "")
                  {
                      payment pp = new payment();
                      pp._pid = Guid.NewGuid().ToString();
                      pp._userId = us.Id.ToString();
                      pp._Sum = p._Sum;
                      pp._date = DateTime.Now;
                      pp.imag = null;
                      await _pa.InsertAsync(pp);
                  }
                  else
                  {
                      var pp = _pa.GetAll().FirstOrDefault(x => x._userId == us.Id);
                      pp._pid = pp._pid;
                      pp._userId = us.Id.ToString();
                      pp._Sum = p._Sum;
                      pp._date = DateTime.Now;
                      await _pa.EditAsync(pp);
                  }
                  return Ok();
              }
              else { return BadRequest("0"); }
          }
          [HttpPost]
          [Route("_addpayment1")]
          public async Task<IActionResult> _addpayment1()
          {
              try
              {
                  byte[] imageData = null;
                  if (Request.Form.Files.Count > 0)
                  {
                      var file = Request.Form.Files["file"];
                      using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                      {
                          imageData = binaryReader.ReadBytes((int)file.Length);
                      }
                      string _userId = Request.Form["_userId"];
                      var us = _userManager.FindByEmailAsync(_userId).Result;
                      payment pp = new payment();
                      pp._pid = Guid.NewGuid().ToString();
                      pp._userId = us.Id.ToString();
                      pp._Sum = 0;
                      pp._date = DateTime.Now;
                      pp.imag = imageData;
                      await _pa.InsertAsync(pp);
                  }
              }
              catch (Exception ex)
              {
                  return BadRequest(ex);
              }
              return Ok();
          }

          [Authorize]
          [HttpGet]
          [Route("_getus")]
          public IActionResult _getmeb(string email)
          {
              var meb = from us in _userManager.Users
                        join pa in _pa.GetAll() on us.Id equals pa._userId
                        where us.Email == email
                        group pa by pa._Sum into playerGroup
                        select new
                        {
                            Team = playerGroup.Key,
                            _Sum = playerGroup.Sum(x => x._Sum)
                        }._Sum;
              return Ok(meb.Sum());
          }*/
        [Authorize]
        [HttpGet]
        [Route("_getUsers")]
        public IActionResult _getUsers()
        {
            var us = _userManager.Users.ToList();
            return Ok(us);
        }
        [Authorize]
        [HttpPost]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] RegisterViewModel model)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
            IdentityResult result = new IdentityResult();
            if (user != null)
            {
                #region MyRegion                
                user.Email = model.Email;
                user.UserName = model.Email;
                user.AccessFailedCount = model.AccessFailedCount;
                user.PhoneNumber = model.PhoneNumber;
                // user.Hal = model.Hal;
                user.IP = model.IP;
                //  user.Mebleg = model.Mebleg;
                //  user.MacAdd = model.MacAdd;                
                IdentityResult validEmail = await userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrors(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(_userManager,
                    user, model.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                    }
                    else
                    {
                        AddErrors(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                         || (validEmail.Succeeded
                         && model.Password != string.Empty && validPass.Succeeded))
                {
                    result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new { mesage = "Məlumatlar yeniləndi." });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }

                #endregion
            }

            return Ok();
        }
        /* [Authorize]
         [HttpPost]
         [Route("_Edmeb")]
         public async Task<IActionResult> _Edmeb([FromBody] invoice model)
         {
             //[Payments]
             //[invoices]
             if (model.MovId != null || model.BId != null)
             {
                 var MaxDate = (from d in _pr.GetAll().Where(k => k._pname == model._pid) select d._pdate).Max();

                 var pra = _pr.GetAll().SingleOrDefault(f => f._pname == model._pid && f._pdate == MaxDate);
                 var us = await _userManager.FindByEmailAsync(model._UserId);
                 invoice ini = new invoice();
                 ini.Inid = Guid.NewGuid().ToString();
                 ini._UserId = us.Email;
                 ini.MovId = model.MovId;
                 ini.BId = model.BId;
                 ini._pid = pra._pid;
                 ini.Tarix = DateTime.Now;
                 await _inv.InsertAsync(ini);
                 payment pp = new payment();
                 pp._pid = Guid.NewGuid().ToString();
                 pp._userId = us.Id;
                 pp._Sum = -pra._price;
                 pp._date = DateTime.Now;
                 await _pa.InsertAsync(pp);
             }
             return Ok();
         }*/

        [HttpGet]
        [Route("_delUser")]
        public async Task<IActionResult> _delUser(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    var ema = applicationUser.Email;
                    IdentityResult result = await _userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        //_fu(id);
                        return Ok(new { mesage = ema + " məlumatlar silindi." });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            return Ok();
        }


        //-------Administrator Son
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}