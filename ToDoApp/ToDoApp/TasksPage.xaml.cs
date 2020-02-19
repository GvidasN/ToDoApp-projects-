

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksPage : ContentPage
    {
        ListView _projects;

        private const string Url = "http://10.0.2.2:UsersJSON";

        public TasksPage(ListView projects)
        {
            InitializeComponent();
            _projects = projects;
        }

        void DisplayTasks()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Tasks>();
                var tasks = conn.Table<Tasks>().ToList();
                var sorted = tasks.OrderBy(o => o.Project_title).ToList();
                ListTasks.ItemsSource = sorted;
            }           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DisplayTasks();
        }

        private async void TaskCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTask(_projects));
        }

        private void TaskDelete_Clicked(object sender, EventArgs e)
        {
            if (ListTasks.SelectedItem != null)
            {
                Tasks t = (Tasks)ListTasks.SelectedItem;

                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
                {
                    //  conn.CreateTable<Project>();
                    // var projects = conn.Table<Project>().ToList();
                    conn.Delete(t);                 
                }
                DisplayTasks();
            }
            else DisplayAlert("Error", "please select a tasm", "Ok");
            
            
        }

        private async void Projects_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

    }
}