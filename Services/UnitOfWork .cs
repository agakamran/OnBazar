using Microsoft.AspNetCore.Identity;
using OnBazar.Data;
using OnBazar.Models;
using OnBazar.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnBazar.Services
{
    public class UnitOfWork : IDisposable
    {
        #region
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<UserManager<ApplicationUser>> userManager;
        private GenericRepository<shipDetail> shipDetail;
        private GenericRepository<_firma> firma;
        private GenericRepository<_beden> beden;
        private GenericRepository<_gender> gender;
        private GenericRepository<_categoriy> categoriy;
        private GenericRepository<_color> color;
        private GenericRepository<_desen> desen;
        private GenericRepository<_marka> marka;
        private GenericRepository<_material> materal;
        private GenericRepository<_stil> stil;
        private GenericRepository<_kullanimAlani> kullanimAlani;
        private GenericRepository<_kumashtipi> kumashtipi;
        private GenericRepository<_qelip> qelip;
        private GenericRepository<_qoltipi> qoltipi;
        private GenericRepository<_yaka> yaka;        
        private GenericRepository<_qaime> qaime;        
        private GenericRepository<_itemdetail> detail;
        private GenericRepository<_photo> photo;
        #endregion
        #region
        public GenericRepository<UserManager<ApplicationUser>> UserManager
        {
            get
            {
                if (this.userManager == null)
                {
                    this.userManager = new GenericRepository<UserManager<ApplicationUser>>(context);
                }
                return userManager;
            }
        }
        public GenericRepository<shipDetail> ShipDetailRepository
        {
            get
            {
                if (this.shipDetail == null)
                {
                    this.shipDetail = new GenericRepository<shipDetail>(context);
                }
                return shipDetail;
            }
        }
        public GenericRepository<_firma> FirmaRepository
        {
            get
            {

                if (this.firma == null)
                {
                    this.firma = new GenericRepository<_firma>(context);
                }
                return firma;
            }
        }
        public GenericRepository<_beden> BedenRepository
        {
            get
            {

                if (this.beden == null)
                {
                    this.beden = new GenericRepository<_beden>(context);
                }
                return beden;
            }
        }
        public GenericRepository<_gender> GenderRepository
        {
            get
            {

                if (this.gender == null)
                {
                    this.gender = new GenericRepository<_gender>(context);
                }
                return gender;
            }
        }
        public GenericRepository<_categoriy> CategoriyRepository
        {
            get
            {

                if (this.categoriy == null)
                {
                    this.categoriy = new GenericRepository<_categoriy>(context);
                }
                return categoriy;
            }
        }
        public GenericRepository<_color> ColorRepository
        {
            get
            {

                if (this.color == null)
                {
                    this.color = new GenericRepository<_color>(context);
                }
                return color;
            }
        }
        public GenericRepository<_desen> DesenRepository
        {
            get
            {

                if (this.desen == null)
                {
                    this.desen = new GenericRepository<_desen>(context);
                }
                return desen;
            }
        }
        public GenericRepository<_marka> MarkaRepository
        {
            get
            {

                if (this.marka == null)
                {
                    this.marka = new GenericRepository<_marka>(context);
                }
                return marka;
            }
        }
        public GenericRepository<_material> MateralRepository
        {
            get
            {

                if (this.materal == null)
                {
                    this.materal = new GenericRepository<_material>(context);
                }
                return materal;
            }
        }
        public GenericRepository<_stil> StilRepository
        {
            get
            {

                if (this.stil == null)
                {
                    this.stil = new GenericRepository<_stil>(context);
                }
                return stil;
            }
        }
        public GenericRepository<_kullanimAlani> KullanimAlaniRepository
        {
            get
            {

                if (this.kullanimAlani == null)
                {
                    this.kullanimAlani = new GenericRepository<_kullanimAlani>(context);
                }
                return kullanimAlani;
            }
        }
        public GenericRepository<_kumashtipi> KumashtipiRepository
        {
            get
            {

                if (this.kumashtipi == null)
                {
                    this.kumashtipi = new GenericRepository<_kumashtipi>(context);
                }
                return kumashtipi;
            }
        }
        public GenericRepository<_qelip> QelipRepository
        {
            get
            {

                if (this.qelip == null)
                {
                    this.qelip = new GenericRepository<_qelip>(context);
                }
                return qelip;
            }
        }
        public GenericRepository<_qoltipi> QoltipiRepository
        {
            get
            {

                if (this.qoltipi == null)
                {
                    this.qoltipi = new GenericRepository<_qoltipi>(context);
                }
                return qoltipi;
            }
        }
        public GenericRepository<_yaka> YakaRepository
        {
            get
            {

                if (this.yaka == null)
                {
                    this.yaka = new GenericRepository<_yaka>(context);
                }
                return yaka;
            }
        }
        public GenericRepository<_qaime> QaimeRepository
        {
            get
            {

                if (this.qaime == null)
                {
                    this.qaime = new GenericRepository<_qaime>(context);
                }
                return qaime;
            }
        }
        public GenericRepository<_itemdetail> DetailRepository
        {
            get
            {

                if (this.detail == null)
                {
                    this.detail = new GenericRepository<_itemdetail>(context);
                }
                return detail;
            }
        }
        public GenericRepository<_photo> PhotoRepository
        {
            get
            {

                if (this.photo == null)
                {
                    this.photo = new GenericRepository<_photo>(context);
                }
                return photo;
            }
        }
        #endregion
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
