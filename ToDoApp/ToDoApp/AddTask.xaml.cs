using System;
using System.IO;
using ToDoApp.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTask : ContentPage
    {
        public AddTask(ListView projects)
        {
            InitializeComponent();
            this.Title = "Task";
            Projects.ItemsSource = projects.ItemsSource;
        }

        private async void CreateTask_Clicked(object sender, EventArgs e)
        {
            if (Projects.SelectedItem != null)
            {
                Project p = (Project)Projects.SelectedItem;

                Tasks task = new Tasks()
                {
                    Title = TaskTitle.Text,
                    Description = TaskDescription.Text,
                    Project_ID = p.ID,
                    Project_title = p.Title
                };

                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
                {
                    conn.CreateTable<Tasks>();
                    conn.Insert(task);
                }

                await DisplayAlert(null, task.Title + " Saved", "Ok");
            }         
        }
    }
}