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
            _ConnectionString.CreateTableAsync<Notes>();
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
        public Task<List<Notes>> GetNotesAsync()
        {
            return _ConnectionString.Table<Notes>().ToListAsync();
        }

        public Task<int> SaveNotesAsync(Notes notes)
        {
            return _ConnectionString.InsertAsync(notes);
        }

        public Task<int> DeleteNotesAsync(Notes notes)
        {
            return _ConnectionString.DeleteAsync(notes);
        }
        public Task<int> UpdateNotesAsync(Notes notes)
        {
            return _ConnectionString.UpdateAsync(notes);
        }


        /*  public Task<List<User>> DisplayUserOnlyAsync(User user)
                 {

                     return _ConnectionString.Table<User>().ElementAtAsync(1);

                 }*/
    }
}
