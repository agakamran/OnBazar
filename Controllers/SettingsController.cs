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
   // [Authorize]
    public class SettingsController : ControllerBase
    {
       // private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly IHostingEnvironment _host;
        private readonly IRepository<store> _firma = null;
        private readonly IRepository<beden> _beden = null;
        private readonly IRepository<gender> _gender = null;
        private readonly IRepository<categoriy> _categ = null;
        private readonly IRepository<color> _color = null;
        private readonly IRepository<desen> _desen = null;
        private readonly IRepository<marka> _marka = null;
        private readonly IRepository<material> _material = null;
        private readonly IRepository<stil> _stil = null;
        private readonly IRepository<kullanimAlani> _kullanimAlani = null;
        private readonly IRepository<kumashtipi> _kumashtipi = null;
        private readonly IRepository<qelip> _qelip = null;
        private readonly IRepository<qoltipi> _qoltipi = null;
        private readonly IRepository<yaka> _yaka = null;
        //private readonly IRepository<qaime> _qaime = null;
        private readonly IRepository<product> _itemdetail = null;
       // private readonly IRepository<itemorderd> _itemorderd = null;
      //  private readonly IRepository<itemorderm> _itemorderm = null;
        private readonly IRepository<prodphoto> _photo = null;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<shipper> _ShippingDetail = null;        
        public SettingsController(IHostingEnvironment host, IRepository<store> firma, IRepository<beden> beden, IRepository<gender> gender, IRepository<categoriy> item_categ,

            IRepository<color> item_color, IRepository<desen> item_desen, IRepository<marka> item_marka, IRepository<material> item_materal,
            IRepository<stil> item_stil, IRepository<kullanimAlani> kullanimAlani, IRepository<kumashtipi> kumashtipi,
            IRepository<qelip> qelip, IRepository<qoltipi> qoltipi, IRepository<yaka> yaka, UserManager<ApplicationUser> userManager,
           // IRepository<qaime> items_qaime,
            IRepository<product> itemdetail,// IRepository<itemorderd> itemorderd, IRepository<itemorderm> itemorderm,
            IRepository<prodphoto> items_photo,           
            IRepository<shipper> ShippingDetail)
        {
            _host = host;
            _userManager = userManager;
            _firma = firma;
            _beden = beden;
            _gender = gender;
            _categ = item_categ;
            _color = item_color;
            _desen = item_desen;
            _marka = item_marka;
            _material = item_materal;
           // _sales = item_sales;
            _stil = item_stil;
            _itemdetail = itemdetail;
            //_itemorderd = itemorderd;
            //_itemorderm = itemorderm;
            _photo = items_photo;
          //  _qaime = items_qaime;
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
        public IEnumerable<store> firma()
        {
           // int Xx = unitOfWork.FirmaRepository.Get().OrderBy(o => o.firmaId).Count();
            // int c= _firma.GetAll().OrderBy(o => o.firmaId).Count();
            return _firma.GetAll().OrderBy(o => o.storId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postfirma")]
        public async Task<IActionResult> postfirma([FromBody] store p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.storId != "")
            {
                var _p = _firma.GetAll().FirstOrDefault(x => x.storId == p.storId);
                _p.storId = _p.storId;
                _p.storname = p.storname;
                _p.storphone = p.storphone;
                _p.storadress = p.storadress;
                _p.storemail = p.storemail;
                _p.userId = p.userId;
                _p.storvoen = p.storvoen;
                await _firma.EditAsync(_p);
                var user = await _userManager.FindByEmailAsync(p.storemail);
                var phoneNumber = user.PhoneNumber;
                if (p.storphone != phoneNumber)
                {
                    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, p.storphone);
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
                 if (_firma.GetAll().FirstOrDefault(f => f.firmaname == p.firmaname && f.firma_email == p.firma_email) == null)
                 {
                     var pp = new _firma();
                     pp.firmaId = Guid.NewGuid().ToString();                    
                     pp.firmaname = p.firmaname;
                     pp.firma_telefon = p.firmatelefon;
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
        public async Task<IActionResult> delfirma([FromBody] store p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _firma.GetAll().FirstOrDefault(f => f.storId == p.storId);
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
                       join c in _gender.GetAll() on a.genId equals c.genId
                       join b in _categ.GetAll() on a.catId equals b.catId
                       select new
                       {
                           a.bedenId,
                           a.bedeni,
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
                           a.genId,
                           c.genname,
                           a.catId,
                           b.catname
                       });
            return res.OrderBy(o => o.trEu).OrderBy(k => k.ayakUz).ToList();
            // return _beden.GetAll().OrderBy(o => o.bedenId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postbeden")]
        public async Task<IActionResult> postbeden([FromBody] beden p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.bedenId != "")
            {

                var _p = _beden.GetAll().FirstOrDefault(x => x.bedenId == p.bedenId);
                _p.bedenId = _p.bedenId;
                _p.bedeni = p.bedeni;
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
                //_p.kanvas = p.kanvas;
                _p.uzunluk = p.uzunluk;
                _p.catId = p.catId;
                _p.genId = p.genId;

                await _beden.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_beden.GetAll().FirstOrDefault(x => x.bedenId == p.bedenId && x.genId == p.genId && x.catId == p.catId) == null)
                {
                    var pp = new beden();
                    pp.bedenId = Guid.NewGuid().ToString();
                    pp.bedeni = p.bedeni;
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
                    pp.catId = p.catId;
                    pp.genId = p.genId;
                    await _beden.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delbeden")]
        public async Task<IActionResult> delbeden([FromBody] beden p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _beden.GetAll().FirstOrDefault(x => x.bedenId == p.bedenId && x.genId == p.genId && x.catId == p.catId);
            await _beden.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ------------- gender
        [HttpGet]
        [Route("gender")]
        public IEnumerable<gender> gender()
        {
            return _gender.GetAll().OrderBy(o => o.genId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postgender")]
        public async Task<IActionResult> postgender([FromBody] gender p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.genId != "")
            {
                var _p = _gender.GetAll().FirstOrDefault(x => x.genId == p.genId);
                _p.genId = _p.genId;
                _p.genname = p.genname;
                await _gender.EditAsync(_p);
                return Ok();
            }
            else
            {
                if (_gender.GetAll().FirstOrDefault(x => x.genname == p.genname && x.genId == p.genId) == null)
                {
                    var pp = new gender();
                    pp.genId = Guid.NewGuid().ToString();
                    pp.genname = p.genname;
                    await _gender.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delgender")]
        public async Task<IActionResult> delgender([FromBody] gender p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _gender.GetAll().FirstOrDefault(x => x.genname == p.genname && x.genId == p.genId);
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
            var res = (from a in _categ.GetAll()
                       join b in _gender.GetAll() on a.genId equals b.genId
                       select new
                       {
                           a.catId,
                           a.catname,
                           a.parid,
                           a.genId,
                           b.genname
                       });

            return res.OrderBy(o => o.genname).ToList();
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
                    // if (_it.FirstOrDefault(x => x.item_categoriy_name == p.item_categoriy_name && x.genId == p.genId) == null)
                   //  {
                         var pp = new _item_categoriy();
                         pp.item_categoriy_Id = Guid.NewGuid().ToString();
                         pp.item_categoriy_name = p.item_categoriy_name;   
                         if (_it.FirstOrDefault(x => x.item_categoriy_name == p.parid && x.genId == p.genId) != null) {
                             pp.parid = _it.FirstOrDefault(x => x.item_categoriy_name == p.parid && x.genId == p.genId).item_categoriy_Id;
                         }                      
                         pp.genId = p.genId;
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
        public async Task<IActionResult> postcategoriy([FromBody] categoriy p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.catId != "")
            {
                var _p = _categ.GetAll().FirstOrDefault(x => x.catId == p.catId);
                _p.catId = _p.catId;
                _p.catname = p.catname;
                _p.parid = p.parid;
                _p.genId = p.genId;
                await _categ.EditAsync(_p);
                return Ok();
            }
            else
            {
                try
                {
                    if (_categ.GetAll().FirstOrDefault(x => x.catname == p.catname && x.parid == p.parid && x.genId == p.genId) == null)
                    {
                        var pp = new categoriy();
                        pp.catId = Guid.NewGuid().ToString();
                        pp.catname = p.catname;

                        // var pparid = _item_categ.GetAll().FirstOrDefault(f => f.item_categoriy_name.Trim().Contains(p.parid.Trim()) && f.genId == p.genId);
                        //  e.Notes.Contains(plan.Notes)
                        // if (pparid != null) { pp.parid = pparid.parid; }
                        // else { pp.parid = p.parid; }

                        pp.parid = p.parid;
                        pp.genId = p.genId;
                        await _categ.InsertAsync(pp);
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
        public async Task<IActionResult> delcategoriy([FromBody] categoriy p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _categ.GetAll().FirstOrDefault(x => x.catId == p.catId);
            await _categ.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------color
        [HttpGet]
        [Route("itemcolor")]
        public IEnumerable<color> itemcolor()
        {
            return _color.GetAll().OrderBy(o => o.colId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemcolor")]
        public async Task<IActionResult> postitemcolor([FromBody] color p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.colId != "")
            {
                var _p = _color.GetAll().FirstOrDefault(x => x.colId == p.colId);
                _p.colId = _p.colId;
                _p.colname = p.colname;
                _p.colurl = p.colurl;
                await _color.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_color.GetAll().FirstOrDefault(f => f.colname == p.colname) == null)
                {
                    var pp = new color();
                    pp.colId = Guid.NewGuid().ToString();
                    pp.colname = p.colname;
                    pp.colurl = p.colurl;
                    await _color.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemcolor")]
        public async Task<IActionResult> delitemcolor([FromBody] color p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _color.GetAll().FirstOrDefault(f => f.colId == p.colId);
            await _color.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------_desen
        [HttpGet]
        [Route("itemdesen")]
        public IEnumerable<desen> itemdesen()
        {
            return _desen.GetAll().OrderBy(o => o.desId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemdesen")]
        public async Task<IActionResult> postitemdesen([FromBody] desen p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.desId != "")
            {
                var _p = _desen.GetAll().FirstOrDefault(x => x.desId == p.desId);
                _p.desId = _p.desId;
                _p.desname = p.desname;
                await _desen.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_desen.GetAll().FirstOrDefault(f => f.desId == p.desId) == null)
                {
                    var pp = new desen();
                    pp.desId = Guid.NewGuid().ToString();
                    pp.desname = p.desname;
                    await _desen.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemdesen")]
        public async Task<IActionResult> delitemdesen([FromBody] desen p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _desen.GetAll().FirstOrDefault(f => f.desId == p.desId);
            await _desen.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------marka
        [HttpGet]
        [Route("itemmarka")]
        public IEnumerable<marka> itemmarka()
        {
            return _marka.GetAll().OrderBy(o => o.markaId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemmarka")]
        public async Task<IActionResult> postitemmarka([FromBody] marka p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.markaId != "")
            {
                var _p = _marka.GetAll().FirstOrDefault(x => x.markaId == p.markaId);
                _p.markaId = _p.markaId;
                _p.markaname = p.markaname;
                await _marka.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_marka.GetAll().FirstOrDefault(f => f.markaname == p.markaname) == null)
                {
                    var pp = new marka();
                    pp.markaId = Guid.NewGuid().ToString();
                    pp.markaname = p.markaname;
                    await _marka.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemmarka")]
        public async Task<IActionResult> delitemmarka([FromBody] marka p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _marka.GetAll().FirstOrDefault(f => f.markaId == p.markaId);
            await _marka.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------materal
        [HttpGet]
        [Route("itemmateral")]
        public IEnumerable<material> itemmateral()
        {
            return _material.GetAll().OrderBy(o => o.matId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemmateral")]
        public async Task<IActionResult> postitemmateral([FromBody] material p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.matId != "")
            {
                var _p = _material.GetAll().FirstOrDefault(x => x.matId == p.matId);
                _p.matId = _p.matId;
                _p.matname = p.matname;
                await _material.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_material.GetAll().FirstOrDefault(f => f.matname == p.matname) == null)
                {
                    var pp = new material();
                    pp.matId = Guid.NewGuid().ToString();
                    pp.matname = p.matname;
                    await _material.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemmateral")]
        public async Task<IActionResult> delitemmateral([FromBody] material p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _material.GetAll().FirstOrDefault(f => f.matId == p.matId);
            await _material.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------item_stil
        [HttpGet]
        [Route("itemstil")]
        public IEnumerable<stil> itemstil()
        {
            return _stil.GetAll().OrderBy(o => o.stilId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postitemstil")]
        public async Task<IActionResult> postitemstil([FromBody] stil p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.stilId != "")
            {
                var _p = _stil.GetAll().FirstOrDefault(x => x.stilId == p.stilId);
                _p.stilId = _p.stilId;
                _p.stilname = p.stilname;
                await _stil.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_stil.GetAll().FirstOrDefault(f => f.stilname == p.stilname) == null)
                {
                    var pp = new stil();
                    pp.stilId = Guid.NewGuid().ToString();
                    pp.stilname = p.stilname;
                    await _stil.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delitemstil")]
        public async Task<IActionResult> delitemstil([FromBody] stil p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _stil.GetAll().FirstOrDefault(f => f.stilId == p.stilId);
            await _stil.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------kullanimAlani
        [HttpGet]
        [Route("kullanimAlani")]
        public IEnumerable<kullanimAlani> kullanimAlani()
        {
            return _kullanimAlani.GetAll().OrderBy(o => o.kulalanId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postkullanimAlani")]
        public async Task<IActionResult> postkullanimAlani([FromBody] kullanimAlani p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.kulalanId != "")
            {
                var _p = _kullanimAlani.GetAll().FirstOrDefault(x => x.kulalanId == p.kulalanId);
                _p.kulalanId = _p.kulalanId;
                _p.kullanimname = p.kullanimname;
                await _kullanimAlani.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_kullanimAlani.GetAll().FirstOrDefault(f => f.kullanimname == p.kullanimname) == null)
                {
                    var pp = new kullanimAlani();
                    pp.kulalanId = Guid.NewGuid().ToString();
                    pp.kullanimname = p.kullanimname;
                    await _kullanimAlani.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delkullanimAlani")]
        public async Task<IActionResult> delkullanimAlani([FromBody] kullanimAlani p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _kullanimAlani.GetAll().FirstOrDefault(f => f.kulalanId == p.kulalanId);
            await _kullanimAlani.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------kumashtipi
        [HttpGet]
        [Route("kumashtipi")]
        public IEnumerable<kumashtipi> kumashtipi()
        {
            return _kumashtipi.GetAll().OrderBy(o => o.kumashId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postkumashtipi")]
        public async Task<IActionResult> postkumashtipi([FromBody] kumashtipi p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.kumashId != "")
            {
                var _p = _kumashtipi.GetAll().FirstOrDefault(x => x.kumashId == p.kumashId);
                _p.kumashId = _p.kumashId;
                _p.kumashname = p.kumashname;
                await _kumashtipi.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_kumashtipi.GetAll().FirstOrDefault(f => f.kumashname == p.kumashname) == null)
                {
                    var pp = new kumashtipi();
                    pp.kumashId = Guid.NewGuid().ToString();
                    pp.kumashname = p.kumashname;
                    await _kumashtipi.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delkumashtipi")]
        public async Task<IActionResult> delkumashtipi([FromBody] kumashtipi p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _kumashtipi.GetAll().FirstOrDefault(f => f.kumashId == p.kumashId);
            await _kumashtipi.DeleteAsync(_p);

            return Ok();
        }
        #endregion
        #region ----------qelip
        [HttpGet]
        [Route("qelip")]
        public IEnumerable<qelip> qelip()
        {
            return _qelip.GetAll().OrderBy(o => o.qelipId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postqelip")]
        public async Task<IActionResult> postqelip([FromBody] qelip p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.qelipId != "")
            {
                var _p = _qelip.GetAll().FirstOrDefault(x => x.qelipId == p.qelipId);
                _p.qelipId = _p.qelipId;
                _p.qelipname = p.qelipname;
                await _qelip.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_qelip.GetAll().FirstOrDefault(f => f.qelipname == p.qelipname) == null)
                {
                    var pp = new qelip();
                    pp.qelipId = Guid.NewGuid().ToString();
                    pp.qelipname = p.qelipname;
                    await _qelip.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delqelip")]
        public async Task<IActionResult> delqelip([FromBody] qelip p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _qelip.GetAll().FirstOrDefault(f => f.qelipId == p.qelipId);
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
                       join b in _gender.GetAll() on a.genId equals b.genId
                       select new
                       {
                           a.qolId,
                           a.qoltipiname,
                           a.genId,
                           b.genname
                       });
            return res.OrderBy(o => o.qolId).ToList();
            // return _qoltipi.GetAll().OrderBy(o => o.qol_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postqoltipi")]
        public async Task<IActionResult> postqoltipi([FromBody] qoltipi p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.qolId != "")
            {
                var _p = _qoltipi.GetAll().FirstOrDefault(x => x.qolId == p.qolId);
                _p.qolId = _p.qolId;
                _p.qoltipiname = p.qoltipiname;
                _p.genId = p.genId;
                await _qoltipi.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_qoltipi.GetAll().FirstOrDefault(f => f.qoltipiname == p.qoltipiname && f.genId == p.genId) == null)
                {
                    var pp = new qoltipi();
                    pp.qolId = Guid.NewGuid().ToString();
                    pp.qoltipiname = p.qoltipiname;
                    pp.genId = p.genId;
                    await _qoltipi.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delqoltipi")]
        public async Task<IActionResult> delqoltipi([FromBody] qoltipi p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _qoltipi.GetAll().FirstOrDefault(f => f.qolId == p.qolId);
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
                       join b in _gender.GetAll() on a.genId equals b.genId
                       select new
                       {
                           a.yakaId,
                           a.yakaname,
                           a.genId,
                           b.genname
                       });
            return res.OrderBy(o => o.yakaId).ToList();
            //return _yaka.GetAll().OrderBy(o => o.yaka_Id);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postyaka")]
        public async Task<IActionResult> postyaka([FromBody] yaka p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.yakaId != "")
            {
                var _p = _yaka.GetAll().FirstOrDefault(x => x.yakaId == p.yakaId && x.genId == p.genId);
                _p.yakaId = _p.yakaId;
                _p.yakaname = p.yakaname;
                _p.genId = p.genId;
                await _yaka.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_yaka.GetAll().FirstOrDefault(f => f.yakaname == p.yakaname && f.genId == p.genId) == null)
                {
                    var pp = new yaka();
                    pp.yakaId = Guid.NewGuid().ToString();
                    pp.yakaname = p.yakaname;
                    pp.genId = p.genId;
                    await _yaka.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delyaka")]
        public async Task<IActionResult> delyaka([FromBody] yaka p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _yaka.GetAll().FirstOrDefault(f => f.yakaId == p.yakaId);
            await _yaka.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        //----------------------
        //#region ----------items_qaime
        //[HttpGet]
        //[Route("itemsqaime")]
        //public IEnumerable<qaime> itemsqaime()
        //{
        //    return _qaime.GetAll().OrderBy(o => o.qaimeId);
        //}
        //[Authorize(Roles = "Administrator")]
        //[HttpPost]
        //[Route("postitemsqaime")]
        //public async Task<IActionResult> postitemsqaime([FromBody] _qaime p)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    if (p.qaimeId != "")
        //    {
        //        var _p = _qaime.GetAll().FirstOrDefault(x => x.qaimeId == p.qaimeId && x.firmaId == p.firmaId);
        //        _p.qaimeId = _p.qaimeId;
        //        _p.qaimename = p.qaimename;
        //        _p.firmaId = p.firmaId;
        //        await _qaime.EditAsync(_p);

        //        return Ok();
        //    }
        //    else
        //    {
        //        if (_qaime.GetAll().FirstOrDefault(f => f.qaimename == p.qaimename && f.firmaId == p.firmaId) == null)
        //        {
        //            var pp = new _qaime();
        //            pp.qaimeId = Guid.NewGuid().ToString();
        //            pp.qaimename = p.qaimename;
        //            pp.firmaId = p.firmaId;
        //            await _qaime.InsertAsync(pp);
        //            return Ok();
        //        }
        //        else { return BadRequest(); }
        //    }
        //}
        //[Authorize(Roles = "Administrator")]
        //[HttpPost]
        //[Route("delitemsqaime")]
        //public async Task<IActionResult> delitemsqaime([FromBody] _qaime p)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var _p = _qaime.GetAll().FirstOrDefault(f => f.qaimeId == p.qaimeId && f.firmaId == p.firmaId);
        //    await _qaime.DeleteAsync(_p);
        //    return Ok();
        //}
        //#endregion
        #region ----------itemdetail
        [HttpGet]
        [Route("itemdetail")]
        [Authorize]
        public IEnumerable itemdetail(string userId)
        {
            var _itemd = _itemdetail.GetAll();
            if (_itemd.Count() == 0) { return _itemd; }
            if (userId != null && userId != "")
            {
                _itemd = _itemdetail.GetAll().Where(fv => fv.storId == _firma.GetAll().FirstOrDefault(f => f.userId == userId).storId);
                // int vv = _itemd.Count();
            }
           // int cas = _itemd.Count();
          //  if (_itemd.Count() == 0) {           return _itemd; }
            // var ffir= _firma.GetAll().FirstOrDefault(f=>f.userId== userId).firmaId
            var res = (from a in _itemd.ToList()
                       //join p in _items_photo.GetAll() on a.item_Id equals p.item_Id
                       join b in _firma.GetAll() on a.storId equals b.storId
                       join c in _gender.GetAll() on a.genId equals c.genId
                       join d in _categ.GetAll() on a.catId equals d.catId
                       join e in _marka.GetAll() on a.markaId equals e.markaId
                       join q in _color.GetAll() on a.colId equals q.colId
                       join s in _stil.GetAll() on a.stilId equals s.stilId
                       join i in _kullanimAlani.GetAll() on a.kulalanId equals i.kulalanId
                       join o in _kumashtipi.GetAll() on a.kumashId equals o.kumashId into kum
                       join f in _beden.GetAll() on a.bedenId equals f.bedenId into be
                       join w in _qelip.GetAll() on a.qelipId equals w.qelipId into qel
                       join r in _material.GetAll() on a.matId equals r.matId into mat
                       join t in _yaka.GetAll() on a.yakaId equals t.yakaId into ya
                       join y in _qoltipi.GetAll() on a.qolId equals y.qolId into qol
                       join u in _desen.GetAll() on a.desId equals u.desId into des
                       from _kum in kum.DefaultIfEmpty()
                       from _be in be.ToList().DefaultIfEmpty()

                       from _qel in qel.DefaultIfEmpty()
                       from _mat in mat.DefaultIfEmpty()
                       from _ya in ya.DefaultIfEmpty()
                       from _qol in qol.DefaultIfEmpty()
                       from _des in des.DefaultIfEmpty()

                       select new
                       {
                           a.proId,
                           //p.item_photo_Id,
                           //p.item_photo_url,
                           a.storId,
                           b.storname,
                           a.genId,
                           c.genname,
                           a.catId,
                           d.catname,
                           a.markaId,
                           e.markaname,
                           a.bedenId,
                           beden = _be?.bedeni ?? "",
                           trEu = _be?.trEu ?? "",
                           // beden = (_be.beden == null ? "" : _be.beden),
                           // _be.beden,
                           //_be.trEu,
                           a.colId,
                           q.colname,
                           a.qelipId,
                           //_qel.qelip_name,
                           qelipname = _qel?.qelipname ?? "",
                           a.matId,
                           // _mat.matname,
                           matname = _mat?.matname ?? "",
                           a.yakaId,
                           //_ya.yaka_name,
                           yaka_name = _ya?.yakaname ?? "",
                           a.qolId,
                           //_qol.qoltipi_name,
                           qoltipiname = _qol?.qoltipiname ?? "",
                           a.stilId,
                           s.stilname,
                           a.desId,
                           //_des.desname,
                           desname = _des?.desname ?? "",
                           a.kulalanId,
                           i.kullanimname,
                           a.kumashId,
                           // _kum.kumashname,
                           kumashname = _kum?.kumashname ?? "",
                           a.prodname,
                           a.barcode,
                           a.Discontinued,
                           //a.price,
                           //a.salesprice,
                           //a.quantity,
                           //a.discount,                           
                           //a.delivery,
                           //a.qaimedate
                       });

            int bv = res.Count();
            return res.OrderBy(o => o.proId).ToList();
            // return _itemdetail.GetAll().OrderBy(o => o.item_Id);
        }
        [HttpPost]
        [Route("postitemdetail")]
       // [Authorize]
        public async Task<IActionResult> postitemdetail([FromBody] product p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (p.proId != "")
            {
                var _p = _itemdetail.GetAll().FirstOrDefault(x => x.proId == p.proId);
                _p.proId = _p.proId;
                //_p.qaime_Id = _p.qaime_Id;
                _p.storId = _p.storId;
                _p.genId = p.genId;
                _p.catId = p.catId;
                _p.markaId = p.markaId;
                _p.bedenId = p.bedenId;
                _p.colId = p.colId;
                _p.qelipId = p.qelipId;
                _p.matId = p.matId;
                _p.qolId = p.qolId;
                _p.stilId = p.stilId;
                _p.desId = p.desId;
                _p.kulalanId = p.kulalanId;
                _p.kumashId = p.kumashId;
                _p.prodname = p.prodname;
                _p.barcode = p.barcode;
                //_p.price = p.price;
                //_p.salesprice = p.salesprice;
                //_p.quantity = p.quantity;
                //_p.discount = p.discount;
                //_p.hidden = p.hidden;
                //_p.yakaId = p.yakaId;
                //_p.qaimedate = p.qaimedate;
                //_p.delivery = p.delivery;
                await _itemdetail.EditAsync(_p);
                return Ok(_p);
            }
            else
            {
                // if (_itemdetail.GetAll().FirstOrDefault(f => f.item_name == p.item_name && f.firmaId == p.firmaId) == null)
                // {
                //var or = new orderdet();
                
                var pp = new product();
                pp.proId = Guid.NewGuid().ToString();
                // pp.qaime_Id = p.qaime_Id;
                var ff = _firma.GetAll().FirstOrDefault(f => f.storemail == p.storId);
                pp.storId = ff.storId;
                pp.genId = p.genId;
                pp.catId = p.catId;
                pp.markaId = p.markaId;
                pp.bedenId = p.bedenId;
                pp.colId = p.colId;
                pp.qelipId = p.qelipId;
                pp.matId = p.matId;
                pp.qolId = p.qolId;
                pp.stilId = p.stilId;
                pp.desId = p.desId;
                pp.kulalanId = p.kulalanId;
                pp.kumashId = p.kumashId;
                pp.prodname = p.prodname;
                pp.barcode = p.barcode;
                //pp.price = p.price;
                //pp.salesprice = p.salesprice;
                //pp.quantity = p.quantity;
                //pp.discount = p.discount;
                //pp.hidden = p.hidden;
                //pp.delivery = p.delivery;
                //// string formattedIdentifier = p.qaime_date.ToString(System.Globalization.CultureInfo.InvariantCulture); // "02/10/2016 12:33:00"
                //pp.qaimedate = p.qaimedate;
                pp.yakaId = p.yakaId;
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
        public async Task<IActionResult> delitemdetail([FromBody] product p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _itemdetail.GetAll().FirstOrDefault(f => f.proId == p.proId);
            await _itemdetail.DeleteAsync(_p);
            var _pp = _photo.GetAll().FirstOrDefault(f => f.proId == p.proId);
            await _photo.DeleteAsync(_pp);
            return Ok();
        }
        #endregion
        #region ----------items_photo--------------------------------------------
        [Authorize]
        [HttpPost]
        [Route("uplodeAvatar")]
        public async Task<IActionResult> uplodeAvatar()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            else
            {
                var pp = new prodphoto();
                pp.photoId = Guid.NewGuid().ToString();
                var file = Request.Form.Files["file"];
               // string exten = Path.GetExtension(file.FileName);
               // string url = "Images/profile/" +  exten;
                string _path = _host.ContentRootPath + "\\Images\\profile\\";
                if (!(Directory.Exists(_path))) { Directory.CreateDirectory(_path); }
                if (file.Length > 0)
                {
                    var path = Path.Combine(_path,file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }               
                return Ok();
            }
        }
            [HttpGet]
        [Route("itemsphoto")]
        [Authorize]
        public IEnumerable<prodphoto> itemsphoto(string itemid)
        {
            //var f = _items_photo.GetAll().Where(k => k.item_Id == itemid);
            return _photo.GetAll().Where(k => k.proId == itemid);
        }
        [Authorize]
        [HttpPost]
        [Route("postitemsphoto")]
        public async Task<IActionResult> postitemsphoto()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            else
            {
                var pp = new prodphoto();
                pp.photoId = Guid.NewGuid().ToString();
                var file = Request.Form.Files["file"];
                var ft = Request.Form["genId"];
                //  var fff = Request.Form["firmaId"].ToString();
                string fi = Request.Form["firmaId"].ToString();// _firma.GetAll().FirstOrDefault(f => f.firmaId == Request.Form["firmaId"]).firmaId ;
                var gen = Request.Form["genId"].ToString();// _gender.GetAll().FirstOrDefault(k => k.genId ==ft).genname;
                string exten = Path.GetExtension(file.FileName);
                string url = "Images/" + fi + "/" + gen + "/" + pp.photoId + exten;
                string _path = _host.ContentRootPath + "\\Images\\" + fi + "\\" + gen + "\\";

                if (!(Directory.Exists(_path))) { Directory.CreateDirectory(_path); }
                if (file.Length > 0)
                {
                    var path = Path.Combine(_path, pp.photoId + exten);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                pp.proId = Request.Form["proId"];
                pp.photourl = url;
                await _photo.InsertAsync(pp);
                return Ok();


            }
        }
        [Authorize]
        [HttpPost]
        [Route("delitemsphoto")]
        public async Task<IActionResult> delitemsphoto([FromBody] prodphoto p)//[FromBody] _items_photo p
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _photo.GetAll().FirstOrDefault(f => f.photoId == p.photoId);
            var fileSavePath = _host.ContentRootPath.Replace("\\", "/") + "/" + p.photourl;
            if (System.IO.File.Exists(fileSavePath))
            {
                System.IO.File.Delete(fileSavePath);
            }
            await _photo.DeleteAsync(_p);
            return Ok();
        }
        #endregion
        //---------------------------
        #region ----------itemsales
        //[HttpGet]
        //[Route("itemsales")]
        //public IEnumerable<itemsales> itemsales()
        //{
        //    return _item_sales.GetAll().OrderBy(o => o.item_sales_Id);
        //}
        //[Authorize(Roles = "Administrator")]
        //[HttpPost]
        //[Route("postitemsales")]
        //public async Task<IActionResult> postitemsales([FromBody] itemsales p)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    if (p.item_sales_Id != "")
        //    {
        //        var _p = _item_sales.GetAll().FirstOrDefault(x => x.item_sales_Id == p.item_sales_Id && x.item_Id == p.item_Id);
        //        _p.item_sales_Id = _p.item_sales_Id;
        //        _p.item_Id = p.item_Id;
        //        _p.shipId = p.shipId;
        //        _p.item_sale_date = p.item_sale_date;
        //        await _item_sales.EditAsync(_p);

        //        return Ok();
        //    }
        //    else
        //    {
        //        if (_item_sales.GetAll().FirstOrDefault(f => f.item_sales_Id == p.item_sales_Id && f.shipId == p.shipId) == null)
        //        {
        //            var pp = new itemsales();
        //            pp.item_sales_Id = Guid.NewGuid().ToString();
        //            pp.item_Id = p.item_Id;
        //            pp.shipId = p.shipId;
        //            pp.item_sale_date = DateTime.Now.Date;
        //            await _item_sales.InsertAsync(pp);
        //            return Ok();
        //        }
        //        else { return BadRequest(); }
        //    }
        //}
        //[Authorize(Roles = "Administrator")]
        //[HttpPost]
        //[Route("delitemsales")]
        //public async Task<IActionResult> delitemsales([FromBody] itemsales p)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var _p = _item_sales.GetAll().FirstOrDefault(f => f.item_sales_Id == p.item_sales_Id && f.shipId == p.shipId);
        //    await _item_sales.DeleteAsync(_p);
        //    return Ok();
        //}
        #endregion
        #region ---------ShippingDetail
        [HttpGet]
        [Route("ShippingDetail")]
        public IEnumerable<shipper> ShippingDetail()
        {
            return _ShippingDetail.GetAll().OrderBy(o => o.shipId);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("postShippingDetail")]
        public async Task<IActionResult> postShippingDetail([FromBody] shipper p)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (p.shipId != "")
            {
                var _p = _ShippingDetail.GetAll().FirstOrDefault(x => x.shipId == p.shipId && x.userId == p.userId);
                _p.shipId = _p.shipId;
                _p.userId = p.userId;
                //_p.client_name = p.client_name;
                _p.shipsity = p.shipsity;
                _p.shipstrit = p.shipstrit;
                _p.shiphouse = p.shiphouse;
               // _p.shipflat = p.shipflat;
                _p.shipphone = p.shipphone;
                _p.shipemail = p.shipemail;
                await _ShippingDetail.EditAsync(_p);

                return Ok();
            }
            else
            {
                if (_ShippingDetail.GetAll().FirstOrDefault(x => x.shipId == p.shipId && x.userId == p.userId) == null)
                {
                    var pp = new shipper();
                    pp.shipId = Guid.NewGuid().ToString();
                    pp.userId = p.userId;
                    //pp.client_name = p.client_name;
                    pp.shipsity = p.shipsity;
                    pp.shipstrit = p.shipstrit;
                    pp.shiphouse = p.shiphouse;
                   // pp.client_flat = p.client_flat;
                    pp.shipphone = p.shipphone;
                    pp.shipemail = p.shipemail;
                    await _ShippingDetail.InsertAsync(pp);
                    return Ok();
                }
                else { return BadRequest(); }
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("delShippingDetail")]
        public async Task<IActionResult> delShippingDetail([FromBody] shipper p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _p = _ShippingDetail.GetAll().FirstOrDefault(f => f.shipId == p.shipId && f.userId == p.userId);
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
