using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string name { get; set; }
        public string Password { get; set; }

        //public List<string> UserNotes { get; set; }

        //public string UserNotes { get; set; }

        // public string Camera { get; set; }

        //        public string Location { get; set; }

        // public List<Notes> UserNotes { get; set; }


    }
}
