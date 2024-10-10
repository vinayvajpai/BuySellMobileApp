using System;
using System.IO;
using System.Runtime.CompilerServices;
using BuySell.Droid.Persistence;
using BuySell.Persistence;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]
namespace BuySell.Droid.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        SQLiteConnection ISQLiteDb.GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "BuySell.db3");
            return new SQLiteConnection(path);
        }
    }
}
