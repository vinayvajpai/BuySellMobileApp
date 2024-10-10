using System;
using SQLite;

namespace BuySell.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteConnection GetConnection();
    }
}
