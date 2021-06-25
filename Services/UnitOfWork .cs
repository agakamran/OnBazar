using Microsoft.AspNetCore.Identity;
using OnBazar.Data;
using OnBazar.Models;
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
        private GenericRepository<shipper> shipDetail;
        private GenericRepository<store> firma;
        private GenericRepository<beden> beden;
        private GenericRepository<gender> gender;
        private GenericRepository<categoriy> categoriy;
        private GenericRepository<color> color;
        private GenericRepository<desen> desen;
        private GenericRepository<marka> marka;
        private GenericRepository<material> materal;
        private GenericRepository<stil> stil;
        private GenericRepository<kullanimAlani> kullanimAlani;
        private GenericRepository<kumashtipi> kumashtipi;
        private GenericRepository<qelip> qelip;
        private GenericRepository<qoltipi> qoltipi;
        private GenericRepository<yaka> yaka;        
       // private GenericRepository<qaime> qaime;        
        private GenericRepository<product> detail;
        private GenericRepository<prodphoto> photo;
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
        public GenericRepository<shipper> ShipDetailRepository
        {
            get
            {
                if (this.shipDetail == null)
                {
                    this.shipDetail = new GenericRepository<shipper>(context);
                }
                return shipDetail;
            }
        }
        public GenericRepository<store> FirmaRepository
        {
            get
            {

                if (this.firma == null)
                {
                    this.firma = new GenericRepository<store>(context);
                }
                return firma;
            }
        }
        public GenericRepository<beden> BedenRepository
        {
            get
            {

                if (this.beden == null)
                {
                    this.beden = new GenericRepository<beden>(context);
                }
                return beden;
            }
        }
        public GenericRepository<gender> GenderRepository
        {
            get
            {

                if (this.gender == null)
                {
                    this.gender = new GenericRepository<gender>(context);
                }
                return gender;
            }
        }
        public GenericRepository<categoriy> CategoriyRepository
        {
            get
            {

                if (this.categoriy == null)
                {
                    this.categoriy = new GenericRepository<categoriy>(context);
                }
                return categoriy;
            }
        }
        public GenericRepository<color> ColorRepository
        {
            get
            {

                if (this.color == null)
                {
                    this.color = new GenericRepository<color>(context);
                }
                return color;
            }
        }
        public GenericRepository<desen> DesenRepository
        {
            get
            {

                if (this.desen == null)
                {
                    this.desen = new GenericRepository<desen>(context);
                }
                return desen;
            }
        }
        public GenericRepository<marka> MarkaRepository
        {
            get
            {

                if (this.marka == null)
                {
                    this.marka = new GenericRepository<marka>(context);
                }
                return marka;
            }
        }
        public GenericRepository<material> MateralRepository
        {
            get
            {

                if (this.materal == null)
                {
                    this.materal = new GenericRepository<material>(context);
                }
                return materal;
            }
        }
        public GenericRepository<stil> StilRepository
        {
            get
            {

                if (this.stil == null)
                {
                    this.stil = new GenericRepository<stil>(context);
                }
                return stil;
            }
        }
        public GenericRepository<kullanimAlani> KullanimAlaniRepository
        {
            get
            {

                if (this.kullanimAlani == null)
                {
                    this.kullanimAlani = new GenericRepository<kullanimAlani>(context);
                }
                return kullanimAlani;
            }
        }
        public GenericRepository<kumashtipi> KumashtipiRepository
        {
            get
            {

                if (this.kumashtipi == null)
                {
                    this.kumashtipi = new GenericRepository<kumashtipi>(context);
                }
                return kumashtipi;
            }
        }
        public GenericRepository<qelip> QelipRepository
        {
            get
            {

                if (this.qelip == null)
                {
                    this.qelip = new GenericRepository<qelip>(context);
                }
                return qelip;
            }
        }
        public GenericRepository<qoltipi> QoltipiRepository
        {
            get
            {

                if (this.qoltipi == null)
                {
                    this.qoltipi = new GenericRepository<qoltipi>(context);
                }
                return qoltipi;
            }
        }
        public GenericRepository<yaka> YakaRepository
        {
            get
            {

                if (this.yaka == null)
                {
                    this.yaka = new GenericRepository<yaka>(context);
                }
                return yaka;
            }
        }
        //public GenericRepository<qaime> QaimeRepository
        //{
        //    get
        //    {

        //        if (this.qaime == null)
        //        {
        //            this.qaime = new GenericRepository<qaime>(context);
        //        }
        //        return qaime;
        //    }
        //}
        public GenericRepository<product> DetailRepository
        {
            get
            {

                if (this.detail == null)
                {
                    this.detail = new GenericRepository<product>(context);
                }
                return detail;
            }
        }
        public GenericRepository<prodphoto> PhotoRepository
        {
            get
            {

                if (this.photo == null)
                {
                    this.photo = new GenericRepository<prodphoto>(context);
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
