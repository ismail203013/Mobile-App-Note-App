using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _ConnectionString;
        public Database(String dbPath)
        {
            _ConnectionString = new SQLiteAsyncConnection(dbPath);
            _ConnectionString.CreateTableAsync<User>();

        }
        public Task<List<User>> GetUsersAsync()
        {
            return _ConnectionString.Table<User>().ToListAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _ConnectionString.InsertAsync(user);
        }
       
        public Task<int> DeleteUserAsync(User user)
        {
            return _ConnectionString.DeleteAsync(user);
        }
        public Task<int> UpdateUserAsync(User user)
        {
            return _ConnectionString.UpdateAsync(user);
        }


 /*  public Task<List<User>> DisplayUserOnlyAsync(User user)
          {

              return _ConnectionString.Table<User>().ElementAtAsync(1);

          }*/
    }
}
