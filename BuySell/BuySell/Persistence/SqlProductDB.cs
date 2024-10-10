using System;
using System.Collections.Generic;
using System.Linq;
using BuySell.Model;
using SQLite;
using Xamarin.Forms;

namespace BuySell.Persistence
{
    public class SqlProductDB
    {
        static object locker = new object();

        public SQLiteConnection _sqlconnection;

        public SqlProductDB()
        {
            _sqlconnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _sqlconnection.CreateTable<RecentProductModel>();
            _sqlconnection.CreateTable<DashboardProductModel>();
            _sqlconnection.CreateTable<BannerProductModel>();
            _sqlconnection.CreateTable<CachedImageDatabaseModel>();
        }
        public int Insert(RecentProductModel recentProduct)
        {
            lock (locker)
            {
                return _sqlconnection.Insert(recentProduct);
            }
        }

        public int InsertProduct(DashboardProductModel dashboardProductModel)
        {
            lock (locker)
            {
                return _sqlconnection.Insert(dashboardProductModel);
            }
        }

        public int InsertBanner(BannerProductModel bannerProductModel)
        {
            lock (locker)
            {
                return _sqlconnection.Insert(bannerProductModel);
            }
        }

        public int Update(RecentProductModel recentProduct)
        {
            lock (locker)
            {
                return _sqlconnection.Update(recentProduct);
            }
        }
        public int Delete(int id)
        {
            lock (locker)
            {
                return _sqlconnection.Delete<RecentProductModel>(id);
            }
        }

        public int DeleteAllProduct()
        {
            lock (locker)
            {
                return _sqlconnection.DeleteAll<DashboardProductModel>();
            }
        }

        public int DeleteProductViaStoreCat(int catID,int storeID)
        {
            lock (locker)
            {
                var productObj= _sqlconnection.Table<DashboardProductModel>().Where(t => t.CategoryID == catID&&t.StoreID==storeID).FirstOrDefault();
                if (productObj != null)
                    return _sqlconnection.Delete(productObj);
                else
                    return 0;
            }
        }

        public int DeleteAllBanner()
        {
            lock (locker)
            {
                return _sqlconnection.DeleteAll<BannerProductModel>();
            }
        }

        public IEnumerable<RecentProductModel> GetAll()
        {
            lock (locker)
            {
                return (from i in _sqlconnection.Table<RecentProductModel>() select i).ToList();
            }
        }
        public int FullDelete()
        {
            lock (locker)
            {
                return _sqlconnection.DeleteAll<RecentProductModel>();
            }
        }
        public RecentProductModel Get(int id)
        {
            lock (locker)
            {
                return _sqlconnection.Table<RecentProductModel>().FirstOrDefault(t => t.Id == id);
            }
        }
        public IEnumerable<RecentProductModel> GetRecentProductByUserID(int UserID)
        {
            lock (locker)
            {
                return _sqlconnection.Table<RecentProductModel>().Where(t => t.UserId == UserID);
            }
        }


        public DashboardProductModel GetAllProduct()
        {
            lock (locker)
            {
                return _sqlconnection.Table<DashboardProductModel>().FirstOrDefault();
            }
        }

        public DashboardProductModel GetAllProductByStoreCat(int catID, int storeID)
        {
            lock (locker)
            {
                return _sqlconnection.Table<DashboardProductModel>().Where(p=>p.StoreID==storeID && p.CategoryID == catID).FirstOrDefault() ;
            }
        }

        public BannerProductModel GetAllBanner()
        {
            lock (locker)
            {
                return _sqlconnection.Table<BannerProductModel>().FirstOrDefault();
            }
        }

        public void Dispose()
        {
            _sqlconnection.Dispose();
        }
    }
}
