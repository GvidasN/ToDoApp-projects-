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
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectsPage : ContentPage
    {
        public ProjectsPage()
        {
            InitializeComponent();
        }

        void DisplayProjects()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Project>();
                var projects = conn.Table<Project>().ToList();
                ListProjects.ItemsSource = projects;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DisplayProjects();
            CheckIfDateSoon();
        }

        private async void ProjectCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProject());
        }

        private async void ProjectEdit_Clicked(object sender, EventArgs e)
        {
            if (ListProjects.SelectedItem != null)
            {
                Project p = (Project)ListProjects.SelectedItem;
                await Navigation.PushAsync(new EditProjectPage(p));
            }
            else await DisplayAlert("Error", "please select a project", "Ok");
        }

        private void ProjectDelete_Clicked(object sender, EventArgs e)
        {
            if (ListProjects.SelectedItem != null)
            {
                Project p = (Project)ListProjects.SelectedItem;

                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
                {
                    //  conn.CreateTable<Project>();
                    // var projects = conn.Table<Project>().ToList();
                    conn.Delete(p);
                }
                DisplayProjects();
            }
            else DisplayAlert("Error", "please select a project", "Ok");
        }

        private async void Tasks_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TasksPage(ListProjects));
        }

        void CheckIfDateSoon()
        {
            List<Project> ProjectsToDisplay = new List<Project>();
            int id = 0;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Project>();
                ProjectsToDisplay = conn.Table<Project>().ToList();
            }

            TimeSpan date = new TimeSpan(0, 0, 14);

            foreach (var p in ProjectsToDisplay)
            {
                if (DateTime.Now.Subtract(p.Finish_date) < date)
                {
                    CrossLocalNotifications.Current.Show(p.Title, "Has less than 14 days to be finished", id);
                    id++;
                }
            }
        }
    }
}