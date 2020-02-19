using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToDoApp.Classes;

namespace ToDoApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

           
        }

        bool CheckIfCorrect (string username, string password)
        {
            List<Users> users = new List<Users>();

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Users>();
                users = conn.Table<Users>().ToList();             
            }

            foreach (Users u in users)
            {
                if (u.Username == username && u.Password == password) return true;
            }

            DisplayAlert(":(", "Wrong password or username", "Try again");
            return false;
        }
    }
}