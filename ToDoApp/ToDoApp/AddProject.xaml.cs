using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    public partial class AddProject : ContentPage
    {
        

        public AddProject()
        {
            InitializeComponent();           

            this.Title = "Project";
            NewProjectCreate.Clicked += NewProjectCreate_Clicked;
        }
       
        private async void NewProjectCreate_Clicked(object sender, EventArgs e)
        {
            Project project = new Project()
            {
                Title = NewProjectTitle.Text,
                Created_by = 99,
                Finish_date = NewProjectDate.Date             
            };

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoAppDB.db")))
            {
                conn.CreateTable<Project>();
                conn.Insert(project);
            }

            await DisplayAlert(null, project.Title + " Saved" + project.Finish_date, "Ok");
            await Navigation.PopAsync();
        }
    }
}