using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProjectPage : ContentPage
    {
        Project _project;      
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db");

        public EditProjectPage(Project obj)
        {            
            InitializeComponent();

            this.Title = "Edit Project";

            _project = obj;

            EditProjectTitle.Placeholder = _project.Title;
        }

        private async void EditProject_Clicked(object sender, EventArgs e)
        {          
            _project.Title = EditProjectTitle.Text.ToString();

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Project>();
                var projects = conn.Table<Project>().ToList();
                conn.Update(_project);
            }

            await Navigation.PopAsync();
        }
    }
}