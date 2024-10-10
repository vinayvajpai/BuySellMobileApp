using System;
using System.IO;
using System.Runtime.CompilerServices;
using BuySell.iOS.Persistence;
using BuySell.Persistence;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]
namespace BuySell.iOS.Persistence
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
