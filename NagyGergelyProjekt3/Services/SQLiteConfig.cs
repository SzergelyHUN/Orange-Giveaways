using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagyGergelyProjekt3.Services
{
    public static class SQLiteConfig
    {
        public const string DatabaseFileName = "UserClaimedGiveaways.db3";

        public const SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
            }
        }
    }
}
