using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToDoApp.Classes;

namespace ToDoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        public Test()
        {
            InitializeComponent();
            Login();
        }

        public async void Login()
        {
            var httpClient = new HttpClient();
            var Response = await httpClient.GetStringAsync("http://localhost:62551/UsersJSON");
            var login = JsonConvert.DeserializeObject<List<Users>>(Response);
            ListUsers.ItemsSource = login;
        }
    }
}