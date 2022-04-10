using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Notes
    {
       [PrimaryKey, AutoIncrement]
        
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserNotes { get; set; }

        public string NoteTitle { get; set; }
        public string Context { get; set; }

        public string Location { get; set; }
        public string Pic { get; set; }




    }
}


