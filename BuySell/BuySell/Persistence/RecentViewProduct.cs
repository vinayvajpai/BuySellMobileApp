using System;
using BuySell.Model;
using SQLite;
using Xamarin.Forms;

namespace BuySell.Persistence
{
    public class RecentViewProduct
    {
        SQLiteAsyncConnection _sqlconnection;
        public RecentViewProduct()
        {
            //_sqlconnection = DependencyService.Get<ISQLiteDb>().GetConnection();
            //_sqlconnection.CreateTableAsync<RecentViewModel>();
        }

        //public int Insert(RecentViewModel recentView)
        //{
        //        return _sqlconnection.InsertAsync(recentView);
        //}

        //public IEnumerable<RecentViewModel> GetAll()
        //{
        //        return (from i in _sqlconnection.Table<RecentViewModel>() select i).ToList();
        //}

        //public Places Get(int id)
        //{
        //    lock (locker)
        //    {
        //        return _sqlconnection.Table<Places>().FirstOrDefault(t => t.Id == id);
        //    }
        //}
        //public void Dispose()
        //{
        //    _sqlconnection.Dispose();
        //}
    }
}
