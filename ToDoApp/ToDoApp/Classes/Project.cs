using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ToDoApp
{
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Created_by { get; set; }
        public string Title { get; set; }
        public DateTime Finish_date { get; set; }
        
    }
}
