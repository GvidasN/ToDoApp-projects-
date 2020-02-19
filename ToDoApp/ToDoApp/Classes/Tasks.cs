using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ToDoApp.Classes
{
    public class Tasks
    {
        [PrimaryKey,AutoIncrement]
        public int Task_ID { get; set; }
        public int Project_ID { get; set; }
        public string Title { get; set; }
        public string Project_title { get; set; }
        public string Description { get; set; }
    }
}
