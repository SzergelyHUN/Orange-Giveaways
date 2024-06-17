using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NagyGergelyProjekt3.Models;

namespace NagyGergelyProjekt3.Services
{
    public static class SQLiteService
    {
        static SQLiteAsyncConnection db;

        static async Task init()
        {
            if (db is not null) { return; }
            db = new SQLiteAsyncConnection(SQLiteConfig.DatabasePath, SQLiteConfig.flags);
            await db.CreateTableAsync<Giveaway>();
        }

        public static async Task<int> addGiveaway(Giveaway giveaway)
        {
            await init();
            return await db.InsertAsync(giveaway);
        }

        public static async Task<IEnumerable<Giveaway>> getAllGiveaway()
        {
            await init();
            var giveaways = await db.Table<Giveaway>().ToListAsync();
            return giveaways;
        }

        public static async Task removeGiveawayById(int id)
        {
            await init();
            if (id != 0)
            {
                await db.DeleteAsync<Giveaway>(id);
            }
        }

        public static async Task<Giveaway> getGiveawayById(int id)
        {
            await init();
            return await db.Table<Giveaway>().Where(x => x.id == id).FirstOrDefaultAsync();
        }

    }
}
