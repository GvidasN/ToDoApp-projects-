using Android;
using Android.App;
using Android.OS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using ToDoApp.Classes;

namespace ToDoApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //smh();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            List<Users> users = new List<Users>();

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Users>();
                users = conn.Table<Users>().ToList();
            }

            foreach (Users u in users)
            {
                if (u.Username == LoginUsername.Text && u.Password == LoginPassword.Text)
                {
                    App.Current.MainPage = new NavigationPage(new ProjectsPage());
                }
            }

            DisplayAlert(":(", "Wrong password or username", "Try again");
        }

        /*void smh ()
        {
            Users user = new Users()
            {
                Username = "admin",
                Password = "123",
                Name = "John"
            };

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Users>();
                conn.Insert(user);
            }
        }*/
    }
}
