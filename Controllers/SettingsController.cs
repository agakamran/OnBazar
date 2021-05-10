using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnBazar.Models;
using OnBazar.Services;

namespace OnBazar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IHostingEnvironment _host;
        private readonly IRepository<_firma> _firma = null;
        private readonly IRepository<_beden> _beden = null;
        private readonly IRepository<_gender> _gender = null;
        private readonly IRepository<_item_categoriy> _item_categ = null;
        private readonly IRepository<_item_color> _item_color = null;
        private readonly IRepository<_item_desen> _item_desen = null;
        private readonly IRepository<_item_marka> _item_marka = null;
        private readonly IRepository<_item_materal> _item_materal = null;
        private readonly IRepository<_item_stil> _item_stil = null;
        private readonly IRepository<_kullanimAlani> _kullanimAlani = null;
        private readonly IRepository<_kumashtipi> _kumashtipi = null;
        private readonly IRepository<_qelip> _qelip = null;
        private readonly IRepository<_qoltipi> _qoltipi = null;
        private readonly IRepository<_yaka> _yaka = null;
        private readonly IRepository<_items_qaime> _items_qaime = null;
        private readonly IRepository<_itemdetail> _itemdetail = null;
        private readonly IRepository<_items_photo> _items_photo = null;
        private readonly IRepository<_item_sales> _item_sales = null;
        private readonly IRepository<_ShippingDetail> _ShippingDetail = null;
        private readonly UserManager<ApplicationUser> _userManager;
        public SettingsController(IHostingEnvironment host, IRepository<_firma> firma, IRepository<_beden> beden, IRepository<_gender> gender, IRepository<_item_categoriy> item_categ,

            IRepository<_item_color> item_color, IRepository<_item_desen> item_desen, IRepository<_item_marka> item_marka, IRepository<_item_materal> item_materal,
            IRepository<_item_stil> item_stil, IRepository<_kullanimAlani> kullanimAlani, IRepository<_kumashtipi> kumashtipi,
            IRepository<_qelip> qelip, IRepository<_qoltipi> qoltipi, IRepository<_yaka> yaka, UserManager<ApplicationUser> userManager,
            IRepository<_items_qaime> items_qaime, IRepository<_itemdetail> itemdetail, IRepository<_items_photo> items_photo,
            IRepository<_item_sales> item_sales, IRepository<_ShippingDetail> ShippingDetail)
        {
            _host = host;
            _userManager = userManager;
            _firma = firma;
            _beden = beden;
            _gender = gender;
            _item_categ = item_categ;
            _item_color = item_color;
            _item_desen = item_desen;
            _item_marka = item_marka;
            _item_materal = item_materal;
            _item_sales = item_sales;
            _item_stil = item_stil;
            _itemdetail = itemdetail;
            _items_photo = items_photo;
            _items_qaime = items_qaime;
            _kullanimAlani = kullanimAlani;
            _kumashtipi = kumashtipi;
            _qelip = qelip;
            _qoltipi = qoltipi;
            _yaka = yaka;
            _ShippingDetail = ShippingDetail;
        }
        #region ------------ firma-
        [HttpGet]
        [Route("firma")]
        public IEnumerable<_firma> firma()
        {
            // int c= _firma.GetAll().OrderBy(o => o.firma_Id).Count();
            return _firma.GetAll().OrderBy(o => o.firma_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postfirma")]
        public async Task<IActionResult> postfirma([FromBody] _firma p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.firma_Id != "")
            {
                var _p = _firma.GetAll().FirstOrDefault(x => x.firma_Id == p.firma_Id);
                _p.firma_Id = _p.firma_Id;
                _p.firma_name = p.firma_name;
                _p.firma_telefon = p.firma_telefon;
                _p.firma_unvan = p.firma_unvan;
                _p.firma_email = p.firma_email;
                _p.userId = p.userId;
                _p.voen = p.voen;
                await _firma.EditAsync(_p);
                var user = await _userManager.FindByEmailAsync(p.firma_email);
                var phoneNumber = user.PhoneNumber;
                if (p.firma_telefon != phoneNumber)
                {
                    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, p.firma_telefon);
                    if (!setPhoneResult.Succeeded)
                    {
                        throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                    }
                }
                return Ok();
            }
            else { return BadRequest(); }
            /* else
             {
                 if (_firma.GetAll().FirstOrDefault(f => f.firma_name == p.firma_name && f.firma_email == p.firma_email) == null)
                 {
                     var pp = new _firma();
                     pp.firma_Id = Guid.NewGuid().ToString();                    
                     pp.firma_name = p.firma_name;
                     pp.firma_telefon = p.firma_telefon;
                     pp.firma_unvan = p.firma_unvan;
                     pp.firma_email = p.firma_email;
                     pp.userId = p.userId;                   
                     await _firma.InsertAsync(pp);
                     return Ok();
                 }
                 else { return BadRequest(); }
             }*/
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delfirma")]
        public async Task<IActionResult> delfirma([FromBody] _firma p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _firma.GetAll().FirstOrDefault(f => f.firma_Id == p.firma_Id);
            await _firma.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ------------- beden
        [HttpGet]
        [Route("beden")]
        public IEnumerable beden()
        {
            var res = (from a in _beden.GetAll()
                       join c in _gender.GetAll() on a.gender_Id equals c.gender_Id
                       join b in _item_categ.GetAll() on a.item_categoriy_Id equals b.item_categoriy_Id
                       select new
                       {
                           a.beden_Id,
                           a.beden,
                           a.trEu,
                           a.uk,
                           a.us,
                           a.it,
                           a.koks,
                           a.bel,
                           a.ayakUz,
                           a.ichBacakBoyu,
                           a.yaka,
                           a.kot,
                           // a.kanvas,
                           a.uzunluk,
                           a.gender_Id,
                           c.gender_name,
                           a.item_categoriy_Id,
                           b.item_categoriy_name
                       });
            return res.OrderBy(o => o.trEu).OrderBy(k => k.ayakUz).ToList();
            // return _beden.GetAll().OrderBy(o => o.beden_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postbeden")]
        public async Task<IActionResult> postbeden([FromBody] _beden p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.beden_Id != "")
            {

                var _p = _beden.GetAll().FirstOrDefault(x => x.beden_Id == p.beden_Id);
                _p.beden_Id = _p.beden_Id;
                _p.beden = p.beden;
                _p.trEu = p.trEu;
                _p.uk = p.uk;
                _p.us = p.us;
                _p.it = p.it;
                _p.koks = p.koks;
                _p.bel = p.bel;
                _p.ayakUz = p.ayakUz;
                _p.ichBacakBoyu = p.ichBacakBoyu;
                _p.yaka = p.yaka;
                _p.kot = p.kot;
                // _p.kanvas = p.kanvas;
                _p.uzunluk = p.uzunluk;
                _p.item_categoriy_Id = p.item_categoriy_Id;
                _p.gender_Id = p.gender_Id;

                await _beden.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_beden.GetAll().FirstOrDefault(x => x.beden_Id == p.beden_Id && x.gender_Id == p.gender_Id && x.item_categoriy_Id == p.item_categoriy_Id) == null)
                {
                    var pp = new _beden();
                    pp.beden_Id = Guid.NewGuid().ToString();
                    pp.beden = p.beden;
                    pp.trEu = p.trEu;
                    pp.uk = p.uk;
                    pp.us = p.us;
                    pp.it = p.it;
                    pp.koks = p.koks;
                    pp.bel = p.bel;
                    pp.ayakUz = p.ayakUz;
                    pp.ichBacakBoyu = p.ichBacakBoyu;
                    pp.yaka = p.yaka;
                    pp.kot = p.kot;
                    //  pp.kanvas = p.kanvas;
                    pp.uzunluk = p.uzunluk;
                    pp.item_categoriy_Id = p.item_categoriy_Id;
                    pp.gender_Id = p.gender_Id;
                    await _beden.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delbeden")]
        public async Task<IActionResult> delbeden([FromBody] _beden p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _beden.GetAll().FirstOrDefault(x => x.beden_Id == p.beden_Id && x.gender_Id == p.gender_Id && x.item_categoriy_Id == p.item_categoriy_Id);
            await _beden.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ------------- gender
        [HttpGet]
        [Route("gender")]
        public IEnumerable<_gender> gender()
        {
            return _gender.GetAll().OrderBy(o => o.gender_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postgender")]
        public async Task<IActionResult> postgender([FromBody] _gender p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.gender_Id != "")
            {
                var _p = _gender.GetAll().FirstOrDefault(x => x.gender_Id == p.gender_Id);
                _p.gender_Id = _p.gender_Id;
                _p.gender_name = p.gender_name;
                await _gender.EditAsync(_p);
                return Ok();
            }
            else
            {
                if (_gender.GetAll().FirstOrDefault(x => x.gender_name == p.gender_name && x.gender_Id == p.gender_Id) == null)
                {
                    var pp = new _gender();
                    pp.gender_Id = Guid.NewGuid().ToString();
                    pp.gender_name = p.gender_name;
                    await _gender.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delgender")]
        public async Task<IActionResult> delgender([FromBody] _gender p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _gender.GetAll().FirstOrDefault(x => x.gender_name == p.gender_name && x.gender_Id == p.gender_Id);
            await _gender.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ------------- categiry
        /* [HttpGet]
         [Route("_getcat")]
         public IEnumerable<_item_categoriy> _getcat()
         {
             return _item_categ.GetAll().Where(r => r.parid=="");
         }*/
        [HttpGet]
        [Route("categoriy")]
        public IEnumerable icategoriy()
        {

            // if (_item_categ.GetAll().Count() == 0) {
            //  _addCate();
            //  }

            // if (_item_categ.GetAll().Count() == 0) {
            //  _addCate();
            //  }
            var res = (from a in _item_categ.GetAll()
                       join b in _gender.GetAll() on a.gender_Id equals b.gender_Id
                       select new
                       {
                           a.item_categoriy_Id,
                           a.item_categoriy_name,
                           a.parid,
                           a.gender_Id,
                           b.gender_name
                       });

            return res.OrderBy(o => o.gender_name).ToList();
        }
        /* public async void _addCate()
         {
             using (StreamReader r = new StreamReader(_host.ContentRootPath + "\\Data\\" + "categoriy.json"))
             {
                 string json = r.ReadToEnd();
                 List<_item_categoriy> items = JsonConvert.DeserializeObject<List<_item_categoriy>>(json);
                 foreach(var p in items)
                 {
                     var _it = _item_categ.GetAll();
                    // if (_it.FirstOrDefault(x => x.item_categoriy_name == p.item_categoriy_name && x.gender_Id == p.gender_Id) == null)
                   //  {
                         var pp = new _item_categoriy();
                         pp.item_categoriy_Id = Guid.NewGuid().ToString();
                         pp.item_categoriy_name = p.item_categoriy_name;   
                         if (_it.FirstOrDefault(x => x.item_categoriy_name == p.parid && x.gender_Id == p.gender_Id) != null) {
                             pp.parid = _it.FirstOrDefault(x => x.item_categoriy_name == p.parid && x.gender_Id == p.gender_Id).item_categoriy_Id;
                         }                      
                         pp.gender_Id = p.gender_Id;
                        await _item_categ.InsertAsync(pp);
                     //}
                 }
             }
         }*/
        /*
            Eşofman,
            Spor Ayakkabı,
            T-shirt,
            Tıraş Makinesi,
            Sweatshirt,
            Şort,
            Eşofman,
            İç Giyim & Pijama,
            Spor Ayakkabı,
            Günlük Ayakkabı,
            Yatak Odası,
            Banyo,
            Tayt,
            Sweatshirt
            */
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postcategoriy")]
        public async Task<IActionResult> postcategoriy([FromBody] _item_categoriy p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.item_categoriy_Id != "")
            {
                var _p = _item_categ.GetAll().FirstOrDefault(x => x.item_categoriy_Id == p.item_categoriy_Id);
                _p.item_categoriy_Id = _p.item_categoriy_Id;
                _p.item_categoriy_name = p.item_categoriy_name;
                _p.parid = p.parid;
                _p.gender_Id = p.gender_Id;
                await _item_categ.EditAsync(_p);
                return Ok();
            }
            else
            {
                try
                {
                    if (_item_categ.GetAll().FirstOrDefault(x => x.item_categoriy_name == p.item_categoriy_name && x.parid == p.parid && x.gender_Id == p.gender_Id) == null)
                    {
                        var pp = new _item_categoriy();
                        pp.item_categoriy_Id = Guid.NewGuid().ToString();
                        pp.item_categoriy_name = p.item_categoriy_name;

                        // var pparid = _item_categ.GetAll().FirstOrDefault(f => f.item_categoriy_name.Trim().Contains(p.parid.Trim()) && f.gender_Id == p.gender_Id);
                        //  e.Notes.Contains(plan.Notes)
                        // if (pparid != null) { pp.parid = pparid.parid; }
                        // else { pp.parid = p.parid; }

                        pp.parid = p.parid;
                        pp.gender_Id = p.gender_Id;
                        await _item_categ.InsertAsync(pp);
                        return Ok();
                    }
                    else { return BadRequest(); }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sehv" + ex);
                    return BadRequest("Sehv" + ex);
                }

            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delcategoriy")]
        public async Task<IActionResult> delcategoriy([FromBody] _item_categoriy p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _item_categ.GetAll().FirstOrDefault(x => x.item_categoriy_Id == p.item_categoriy_Id);
            await _item_categ.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------color
        [HttpGet]
        [Route("itemcolor")]
        public IEnumerable<_item_color> itemcolor()
        {
            return _item_color.GetAll().OrderBy(o => o.item_color_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemcolor")]
        public async Task<IActionResult> postitemcolor([FromBody] _item_color p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.item_color_Id != "")
            {
                var _p = _item_color.GetAll().FirstOrDefault(x => x.item_color_Id == p.item_color_Id);
                _p.item_color_Id = _p.item_color_Id;
                _p.item_color = p.item_color;
                _p.url_color = p.url_color;
                await _item_color.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_item_color.GetAll().FirstOrDefault(f => f.item_color == p.item_color) == null)
                {
                    var pp = new _item_color();
                    pp.item_color_Id = Guid.NewGuid().ToString();
                    pp.item_color = p.item_color;
                    pp.url_color = p.url_color;
                    await _item_color.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemcolor")]
        public async Task<IActionResult> delitemcolor([FromBody] _item_color p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _item_color.GetAll().FirstOrDefault(f => f.item_color_Id == p.item_color_Id);
            await _item_color.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------_desen
        [HttpGet]
        [Route("itemdesen")]
        public IEnumerable<_item_desen> itemdesen()
        {
            return _item_desen.GetAll().OrderBy(o => o.item_desen_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemdesen")]
        public async Task<IActionResult> postitemdesen([FromBody] _item_desen p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.item_desen_Id != "")
            {
                var _p = _item_desen.GetAll().FirstOrDefault(x => x.item_desen_Id == p.item_desen_Id);
                _p.item_desen_Id = _p.item_desen_Id;
                _p.item_desen_name = p.item_desen_name;
                await _item_desen.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_item_desen.GetAll().FirstOrDefault(f => f.item_desen_Id == p.item_desen_Id) == null)
                {
                    var pp = new _item_desen();
                    pp.item_desen_Id = Guid.NewGuid().ToString();
                    pp.item_desen_name = p.item_desen_name;
                    await _item_desen.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemdesen")]
        public async Task<IActionResult> delitemdesen([FromBody] _item_desen p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _item_desen.GetAll().FirstOrDefault(f => f.item_desen_Id == p.item_desen_Id);
            await _item_desen.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------marka
        [HttpGet]
        [Route("itemmarka")]
        public IEnumerable<_item_marka> itemmarka()
        {
            return _item_marka.GetAll().OrderBy(o => o.item_marka_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemmarka")]
        public async Task<IActionResult> postitemmarka([FromBody] _item_marka p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.item_marka_Id != "")
            {
                var _p = _item_marka.GetAll().FirstOrDefault(x => x.item_marka_Id == p.item_marka_Id);
                _p.item_marka_Id = _p.item_marka_Id;
                _p.item_marka_name = p.item_marka_name;
                await _item_marka.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_item_marka.GetAll().FirstOrDefault(f => f.item_marka_name == p.item_marka_name) == null)
                {
                    var pp = new _item_marka();
                    pp.item_marka_Id = Guid.NewGuid().ToString();
                    pp.item_marka_name = p.item_marka_name;
                    await _item_marka.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemmarka")]
        public async Task<IActionResult> delitemmarka([FromBody] _item_marka p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _item_marka.GetAll().FirstOrDefault(f => f.item_marka_Id == p.item_marka_Id);
            await _item_marka.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------materal
        [HttpGet]
        [Route("itemmateral")]
        public IEnumerable<_item_materal> itemmateral()
        {
            return _item_materal.GetAll().OrderBy(o => o.item_materal_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemmateral")]
        public async Task<IActionResult> postitemmateral([FromBody] _item_materal p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.item_materal_Id != "")
            {
                var _p = _item_materal.GetAll().FirstOrDefault(x => x.item_materal_Id == p.item_materal_Id);
                _p.item_materal_Id = _p.item_materal_Id;
                _p.item_materal_name = p.item_materal_name;
                await _item_materal.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_item_materal.GetAll().FirstOrDefault(f => f.item_materal_name == p.item_materal_name) == null)
                {
                    var pp = new _item_materal();
                    pp.item_materal_Id = Guid.NewGuid().ToString();
                    pp.item_materal_name = p.item_materal_name;
                    await _item_materal.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemmateral")]
        public async Task<IActionResult> delitemmateral([FromBody] _item_materal p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _item_materal.GetAll().FirstOrDefault(f => f.item_materal_Id == p.item_materal_Id);
            await _item_materal.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------item_stil
        [HttpGet]
        [Route("itemstil")]
        public IEnumerable<_item_stil> itemstil()
        {
            return _item_stil.GetAll().OrderBy(o => o.item_stil_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemstil")]
        public async Task<IActionResult> postitemstil([FromBody] _item_stil p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.item_stil_Id != "")
            {
                var _p = _item_stil.GetAll().FirstOrDefault(x => x.item_stil_Id == p.item_stil_Id);
                _p.item_stil_Id = _p.item_stil_Id;
                _p.item_stil_name = p.item_stil_name;
                await _item_stil.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_item_stil.GetAll().FirstOrDefault(f => f.item_stil_name == p.item_stil_name) == null)
                {
                    var pp = new _item_stil();
                    pp.item_stil_Id = Guid.NewGuid().ToString();
                    pp.item_stil_name = p.item_stil_name;
                    await _item_stil.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemstil")]
        public async Task<IActionResult> delitemstil([FromBody] _item_stil p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _item_stil.GetAll().FirstOrDefault(f => f.item_stil_Id == p.item_stil_Id);
            await _item_stil.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------kullanimAlani
        [HttpGet]
        [Route("kullanimAlani")]
        public IEnumerable<_kullanimAlani> kullanimAlani()
        {
            return _kullanimAlani.GetAll().OrderBy(o => o.kulalan_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postkullanimAlani")]
        public async Task<IActionResult> postkullanimAlani([FromBody] _kullanimAlani p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.kulalan_Id != "")
            {
                var _p = _kullanimAlani.GetAll().FirstOrDefault(x => x.kulalan_Id == p.kulalan_Id);
                _p.kulalan_Id = _p.kulalan_Id;
                _p.kullanim_name = p.kullanim_name;
                await _kullanimAlani.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_kullanimAlani.GetAll().FirstOrDefault(f => f.kullanim_name == p.kullanim_name) == null)
                {
                    var pp = new _kullanimAlani();
                    pp.kulalan_Id = Guid.NewGuid().ToString();
                    pp.kullanim_name = p.kullanim_name;
                    await _kullanimAlani.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delkullanimAlani")]
        public async Task<IActionResult> delkullanimAlani([FromBody] _kullanimAlani p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _kullanimAlani.GetAll().FirstOrDefault(f => f.kulalan_Id == p.kulalan_Id);
            await _kullanimAlani.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------kumashtipi
        [HttpGet]
        [Route("kumashtipi")]
        public IEnumerable<_kumashtipi> kumashtipi()
        {
            return _kumashtipi.GetAll().OrderBy(o => o.kumash_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postkumashtipi")]
        public async Task<IActionResult> postkumashtipi([FromBody] _kumashtipi p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.kumash_Id != "")
            {
                var _p = _kumashtipi.GetAll().FirstOrDefault(x => x.kumash_Id == p.kumash_Id);
                _p.kumash_Id = _p.kumash_Id;
                _p.kumash_name = p.kumash_name;
                await _kumashtipi.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_kumashtipi.GetAll().FirstOrDefault(f => f.kumash_name == p.kumash_name) == null)
                {
                    var pp = new _kumashtipi();
                    pp.kumash_Id = Guid.NewGuid().ToString();
                    pp.kumash_name = p.kumash_name;
                    await _kumashtipi.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delkumashtipi")]
        public async Task<IActionResult> delkumashtipi([FromBody] _kumashtipi p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _kumashtipi.GetAll().FirstOrDefault(f => f.kumash_Id == p.kumash_Id);
            await _kumashtipi.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------qelip
        [HttpGet]
        [Route("qelip")]
        public IEnumerable<_qelip> qelip()
        {
            return _qelip.GetAll().OrderBy(o => o.qelip_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postqelip")]
        public async Task<IActionResult> postqelip([FromBody] _qelip p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.qelip_Id != "")
            {
                var _p = _qelip.GetAll().FirstOrDefault(x => x.qelip_Id == p.qelip_Id);
                _p.qelip_Id = _p.qelip_Id;
                _p.qelip_name = p.qelip_name;
                await _qelip.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_qelip.GetAll().FirstOrDefault(f => f.qelip_name == p.qelip_name) == null)
                {
                    var pp = new _qelip();
                    pp.qelip_Id = Guid.NewGuid().ToString();
                    pp.qelip_name = p.qelip_name;
                    await _qelip.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delqelip")]
        public async Task<IActionResult> delqelip([FromBody] _qelip p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _qelip.GetAll().FirstOrDefault(f => f.qelip_Id == p.qelip_Id);
            await _qelip.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------qoltipi
        [HttpGet]
        [Route("qoltipi")]
        public IEnumerable qoltipi()
        {
            var res = (from a in _qoltipi.GetAll()
                       join b in _gender.GetAll() on a.gender_Id equals b.gender_Id
                       select new
                       {
                           a.qol_Id,
                           a.qoltipi_name,
                           a.gender_Id,
                           b.gender_name
                       });
            return res.OrderBy(o => o.qol_Id).ToList();
            // return _qoltipi.GetAll().OrderBy(o => o.qol_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postqoltipi")]
        public async Task<IActionResult> postqoltipi([FromBody] _qoltipi p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.qol_Id != "")
            {
                var _p = _qoltipi.GetAll().FirstOrDefault(x => x.qol_Id == p.qol_Id);
                _p.qol_Id = _p.qol_Id;
                _p.qoltipi_name = p.qoltipi_name;
                _p.gender_Id = p.gender_Id;
                await _qoltipi.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_qoltipi.GetAll().FirstOrDefault(f => f.qoltipi_name == p.qoltipi_name && f.gender_Id == p.gender_Id) == null)
                {
                    var pp = new _qoltipi();
                    pp.qol_Id = Guid.NewGuid().ToString();
                    pp.qoltipi_name = p.qoltipi_name;
                    pp.gender_Id = p.gender_Id;
                    await _qoltipi.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delqoltipi")]
        public async Task<IActionResult> delqoltipi([FromBody] _qoltipi p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _qoltipi.GetAll().FirstOrDefault(f => f.qol_Id == p.qol_Id);
            await _qoltipi.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        #region ----------yaka
        [HttpGet]
        [Route("yaka")]
        public IEnumerable yaka()
        {
            var res = (from a in _yaka.GetAll()
                       join b in _gender.GetAll() on a.gender_Id equals b.gender_Id
                       select new
                       {
                           a.yaka_Id,
                           a.yaka_name,
                           a.gender_Id,
                           b.gender_name
                       });
            return res.OrderBy(o => o.yaka_Id).ToList();
            //return _yaka.GetAll().OrderBy(o => o.yaka_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postyaka")]
        public async Task<IActionResult> postyaka([FromBody] _yaka p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.yaka_Id != "")
            {
                var _p = _yaka.GetAll().FirstOrDefault(x => x.yaka_Id == p.yaka_Id && x.gender_Id == p.gender_Id);
                _p.yaka_Id = _p.yaka_Id;
                _p.yaka_name = p.yaka_name;
                _p.gender_Id = p.gender_Id;
                await _yaka.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_yaka.GetAll().FirstOrDefault(f => f.yaka_name == p.yaka_name && f.gender_Id == p.gender_Id) == null)
                {
                    var pp = new _yaka();
                    pp.yaka_Id = Guid.NewGuid().ToString();
                    pp.yaka_name = p.yaka_name;
                    pp.gender_Id = p.gender_Id;
                    await _yaka.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delyaka")]
        public async Task<IActionResult> delyaka([FromBody] _yaka p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _yaka.GetAll().FirstOrDefault(f => f.yaka_Id == p.yaka_Id);
            await _yaka.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        //----------------------
        #region ----------items_qaime
        [HttpGet]
        [Route("itemsqaime")]
        public IEnumerable<_items_qaime> itemsqaime()
        {
            return _items_qaime.GetAll().OrderBy(o => o.qaime_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemsqaime")]
        public async Task<IActionResult> postitemsqaime([FromBody] _items_qaime p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.qaime_Id != "")
            {
                var _p = _items_qaime.GetAll().FirstOrDefault(x => x.qaime_Id == p.qaime_Id && x.firma_Id == p.firma_Id);
                _p.qaime_Id = _p.qaime_Id;
                _p.qaime_name = p.qaime_name;
                _p.firma_Id = p.firma_Id;
                await _items_qaime.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_items_qaime.GetAll().FirstOrDefault(f => f.qaime_name == p.qaime_name && f.firma_Id == p.firma_Id) == null)
                {
                    var pp = new _items_qaime();
                    pp.qaime_Id = Guid.NewGuid().ToString();
                    pp.qaime_name = p.qaime_name;
                    pp.firma_Id = p.firma_Id;
                    await _items_qaime.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemsqaime")]
        public async Task<IActionResult> delitemsqaime([FromBody] _items_qaime p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _items_qaime.GetAll().FirstOrDefault(f => f.qaime_Id == p.qaime_Id && f.firma_Id == p.firma_Id);
            await _items_qaime.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        #region ----------itemdetail
        [HttpGet]
        [Route("itemdetail")]
        public IEnumerable itemdetail(string userId)
        {
            var _itemd= _itemdetail.GetAll();
            if (_itemd.Count() == 0) { return _itemd; }
            if (userId!=null && userId != "") { _itemd = _itemdetail.GetAll().Where(fv => fv.firma_Id == _firma.GetAll().FirstOrDefault(f => f.userId == userId).firma_Id); }
            // var ffir= _firma.GetAll().FirstOrDefault(f=>f.userId== userId).firma_Id
            var res = (from a in _itemd
                       join p in _items_photo.GetAll() on a.item_Id equals p.item_Id
                       join b in _firma.GetAll() on a.firma_Id equals b.firma_Id
                       join c in _gender.GetAll() on a.gender_Id equals c.gender_Id
                       join d in _item_categ.GetAll() on a.item_categoriy_Id equals d.item_categoriy_Id
                       join e in _item_marka.GetAll() on a.item_marka_Id equals e.item_marka_Id
                       join f in _beden.GetAll() on a.beden_Id equals f.beden_Id into be
                       join q in _item_color.GetAll() on a.item_color_Id equals q.item_color_Id
                       join w in _qelip.GetAll() on a.qelip_Id equals w.qelip_Id into qel
                       join r in _item_materal.GetAll() on a.item_materal_Id equals r.item_materal_Id into mat
                       join t in _yaka.GetAll() on a.yaka_Id equals t.yaka_Id into ya
                       join y in _qoltipi.GetAll() on a.qol_Id equals y.qol_Id into qol
                       join s in _item_stil.GetAll() on a.item_stil_Id equals s.item_stil_Id
                       join u in _item_desen.GetAll() on a.item_desen_Id equals u.item_desen_Id into des
                       join i in _kullanimAlani.GetAll() on a.kulalan_Id equals i.kulalan_Id
                       join o in _kumashtipi.GetAll() on a.kumash_Id equals o.kumash_Id into kum
                       from _be in be.DefaultIfEmpty()
                       from _qel in qel.DefaultIfEmpty()
                       from _mat in mat.DefaultIfEmpty()
                       from _ya in ya.DefaultIfEmpty()
                       from _qol in qol.DefaultIfEmpty()
                       from _des in des.DefaultIfEmpty()
                       from _kum in kum.DefaultIfEmpty()

                       select new
                       {
                           a.item_Id,
                           p.item_photo_Id,
                           p.item_photo_url,
                           a.firma_Id,
                           b.firma_name,
                           a.gender_Id,
                           c.gender_name,
                           a.item_categoriy_Id,
                           d.item_categoriy_name,
                           a.item_marka_Id,
                           e.item_marka_name,
                           a.beden_Id,
                           _be.beden,
                           _be.trEu,
                           a.item_color_Id,
                           q.item_color,
                           a.qelip_Id,
                           _qel.qelip_name,
                           a.item_materal_Id,
                           _mat.item_materal_name,
                           a.yaka_Id,
                           _ya.yaka_name,
                           //t.yaka_name,
                           a.qol_Id,
                           _qol.qoltipi_name,
                           //y.qoltipi_name,
                           a.item_stil_Id,
                           s.item_stil_name,
                           a.item_desen_Id,
                           _des.item_desen_name,
                           a.kulalan_Id,
                           i.kullanim_name,
                           a.kumash_Id,
                           _kum.kumash_name,
                           a.item_name,
                           a.item_code,
                           a.item_price,
                           a.item_sales_price,
                           a.item_quantity,
                           a.item_discount,
                           a.item_hidden,
                           a.item_delivery,
                           a.qaime_date
                       });

            int bv = res.Count();
            return res.OrderBy(o => o.item_Id).ToList();
            // return _itemdetail.GetAll().OrderBy(o => o.item_Id);
        }
        [HttpPost]
        [Route("postitemdetail")]
        // [Authorize]
        public async Task<IActionResult> postitemdetail([FromBody] _itemdetail p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (p.item_Id != "")
            {
                var _p = _itemdetail.GetAll().FirstOrDefault(x => x.item_Id == p.item_Id);
                _p.item_Id = _p.item_Id;
                //_p.qaime_Id = _p.qaime_Id;
                _p.firma_Id = _p.firma_Id;
                _p.gender_Id = p.gender_Id;
                _p.item_categoriy_Id = p.item_categoriy_Id;
                _p.item_marka_Id = p.item_marka_Id;
                _p.beden_Id = p.beden_Id;
                _p.item_color_Id = p.item_color_Id;
                _p.qelip_Id = p.qelip_Id;
                _p.item_materal_Id = p.item_materal_Id;
                _p.qol_Id = p.qol_Id;
                _p.item_stil_Id = p.item_stil_Id;
                _p.item_desen_Id = p.item_desen_Id;
                _p.kulalan_Id = p.kulalan_Id;
                _p.kumash_Id = p.kumash_Id;
                _p.item_name = p.item_name;
                _p.item_code = p.item_code;
                _p.item_price = p.item_price;
                _p.item_sales_price = p.item_sales_price;
                _p.item_quantity = p.item_quantity;
                _p.item_discount = p.item_discount;
                _p.item_hidden = p.item_hidden;
                _p.yaka_Id = p.yaka_Id;
                _p.qaime_date = p.qaime_date;
                _p.item_delivery = p.item_delivery;
                await _itemdetail.EditAsync(_p);
                return Ok(_p);
            }
            else
            {
                // if (_itemdetail.GetAll().FirstOrDefault(f => f.item_name == p.item_name && f.firma_Id == p.firma_Id) == null)
                // {

                var pp = new _itemdetail();
                pp.item_Id = Guid.NewGuid().ToString();
                // pp.qaime_Id = p.qaime_Id;
                var ff = _firma.GetAll().FirstOrDefault(f => f.firma_email == p.firma_Id);
                pp.firma_Id = ff.firma_Id;
                pp.gender_Id = p.gender_Id;
                pp.item_categoriy_Id = p.item_categoriy_Id;
                pp.item_marka_Id = p.item_marka_Id;
                pp.beden_Id = p.beden_Id;
                pp.item_color_Id = p.item_color_Id;
                pp.qelip_Id = p.qelip_Id;
                pp.item_materal_Id = p.item_materal_Id;
                pp.qol_Id = p.qol_Id;
                pp.item_stil_Id = p.item_stil_Id;
                pp.item_desen_Id = p.item_desen_Id;
                pp.kulalan_Id = p.kulalan_Id;
                pp.kumash_Id = p.kumash_Id;
                pp.item_name = p.item_name;
                pp.item_code = p.item_code;
                pp.item_price = p.item_price;
                pp.item_sales_price = p.item_sales_price;
                pp.item_quantity = p.item_quantity;
                pp.item_discount = p.item_discount;
                pp.item_hidden = p.item_hidden;
                pp.item_delivery = p.item_delivery;
                pp.qaime_date = p.qaime_date;
                pp.yaka_Id = p.yaka_Id;
                await _itemdetail.InsertAsync(pp);
                // var c = pp.item_Id;
                //  var ppp = _items_photo.GetAll().Where(k=>k.item_Id== "-111");
                //  foreach(var f in ppp)
                // {
                //     f.item_Id= pp.item_Id;
                //     await _items_photo.EditAsync(f);
                //  }                    
                return Ok(pp);
                //  }
                //  else { return BadRequest(); }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("delitemdetail")]
        public async Task<IActionResult> delitemdetail([FromBody] _itemdetail p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _itemdetail.GetAll().FirstOrDefault(f => f.item_Id == p.item_Id);
            await _itemdetail.DeleteAsync(_p);
            var _pp = _items_photo.GetAll().FirstOrDefault(f => f.item_Id == p.item_Id);
            await _items_photo.DeleteAsync(_pp);
            return Ok();
        }
        #endregion
        #region ----------items_photo--------------------------------------------

        [HttpGet]
        [Route("itemsphoto")]
        [Authorize]
        public IEnumerable<_items_photo> itemsphoto(string itemid)
        {
            //var f = _items_photo.GetAll().Where(k => k.item_Id == itemid);
            return _items_photo.GetAll().Where(k => k.item_Id == itemid);
        }
        [Authorize]
        [HttpPost]
        [Route("postitemsphoto")]
        public async Task<IActionResult> postitemsphoto()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            else
            {
                var pp = new _items_photo();
                pp.item_photo_Id = Guid.NewGuid().ToString();
                var file = Request.Form.Files["file"];
              //  var ft = Request.Form["gender_Id"];
              //  var fff = Request.Form["firma_Id"].ToString();
                var fi = Request.Form["firma_Id"].ToString();// _firma.GetAll().FirstOrDefault(f => f.firma_Id == Request.Form["firma_Id"]).firma_Id ;
                var gen = _gender.GetAll().FirstOrDefault(k => k.gender_Id == Request.Form["gender_Id"]).gender_name;
                string exten = Path.GetExtension(file.FileName);
                string url = "Images/" + fi + "/" + gen + "/" + pp.item_photo_Id + exten;
                string _path = _host.ContentRootPath + "\\Images\\" + fi + "\\" + gen + "\\";

                if (!(Directory.Exists(_path))) { Directory.CreateDirectory(_path); }
                if (file.Length > 0)
                {
                    var path = Path.Combine(_path, pp.item_photo_Id + exten);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                pp.item_Id = Request.Form["item_Id"];
                pp.item_photo_url = url;
                await _items_photo.InsertAsync(pp);
                return Ok();


            }
        }
        [Authorize]
        [HttpPost]
        [Route("delitemsphoto")]
        public async Task<IActionResult> delitemsphoto([FromBody] _items_photo p)//[FromBody] _items_photo p
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _items_photo.GetAll().FirstOrDefault(f => f.item_photo_Id == p.item_photo_Id);
            var fileSavePath = _host.ContentRootPath.Replace("\\", "/") + "/" + p.item_photo_url;
            if (System.IO.File.Exists(fileSavePath))
            {
                System.IO.File.Delete(fileSavePath);
            }
            await _items_photo.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        //--------------------
        #region ----------itemsales
        [HttpGet]
        [Route("itemsales")]
        public IEnumerable<_item_sales> itemsales()
        {
            return _item_sales.GetAll().OrderBy(o => o.item_sales_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemsales")]
        public async Task<IActionResult> postitemsales([FromBody] _item_sales p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.item_sales_Id != "")
            {
                var _p = _item_sales.GetAll().FirstOrDefault(x => x.item_sales_Id == p.item_sales_Id && x.item_Id == p.item_Id);
                _p.item_sales_Id = _p.item_sales_Id;
                _p.item_Id = p.item_Id;
                _p.shipdet_Id = p.shipdet_Id;
                _p.item_sale_date = p.item_sale_date;
                await _item_sales.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_item_sales.GetAll().FirstOrDefault(f => f.item_sales_Id == p.item_sales_Id && f.shipdet_Id == p.shipdet_Id) == null)
                {
                    var pp = new _item_sales();
                    pp.item_sales_Id = Guid.NewGuid().ToString();
                    pp.item_Id = p.item_Id;
                    pp.shipdet_Id = p.shipdet_Id;
                    pp.item_sale_date = DateTime.Now.Date;
                    await _item_sales.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemsales")]
        public async Task<IActionResult> delitemsales([FromBody] _item_sales p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _item_sales.GetAll().FirstOrDefault(f => f.item_sales_Id == p.item_sales_Id && f.shipdet_Id == p.shipdet_Id);
            await _item_sales.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        #region ---------ShippingDetail
        [HttpGet]
        [Route("ShippingDetail")]
        public IEnumerable<_ShippingDetail> ShippingDetail()
        {
            return _ShippingDetail.GetAll().OrderBy(o => o.shipdet_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postShippingDetail")]
        public async Task<IActionResult> postShippingDetail([FromBody] _ShippingDetail p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.shipdet_Id != "")
            {
                var _p = _ShippingDetail.GetAll().FirstOrDefault(x => x.shipdet_Id == p.shipdet_Id && x.userId == p.userId);
                _p.shipdet_Id = _p.shipdet_Id;
                _p.userId = p.userId;
                _p.client_name = p.client_name;
                _p.client_sity = p.client_sity;
                _p.client_strit = p.client_strit;
                _p.client_house = p.client_house;
                _p.client_flat = p.client_flat;
                _p.client_phone = p.client_phone;
                _p.client_email = p.client_email;
                await _ShippingDetail.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_ShippingDetail.GetAll().FirstOrDefault(x => x.shipdet_Id == p.shipdet_Id && x.userId == p.userId) == null)
                {
                    var pp = new _ShippingDetail();
                    pp.shipdet_Id = Guid.NewGuid().ToString();
                    pp.userId = p.userId;
                    pp.client_name = p.client_name;
                    pp.client_sity = p.client_sity;
                    pp.client_strit = p.client_strit;
                    pp.client_house = p.client_house;
                    pp.client_flat = p.client_flat;
                    pp.client_phone = p.client_phone;
                    pp.client_email = p.client_email;
                    await _ShippingDetail.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delShippingDetail")]
        public async Task<IActionResult> delShippingDetail([FromBody] _ShippingDetail p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _ShippingDetail.GetAll().FirstOrDefault(f => f.shipdet_Id == p.shipdet_Id && f.userId == p.userId);
            await _ShippingDetail.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        // GET: api/Settings
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Settings/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Settings
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Settings/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
