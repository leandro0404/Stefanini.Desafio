using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Stefanini.Desafio.Services;
using Stefanini.Desafio.Views;

namespace Stefanini.Desafio
{
    public partial class App : Application
    {
        public static string DB_PATH = "/data/user/0/com.companyname/files/favorites.sqlite";
        public App(string DB_PATH)
        {
            InitializeComponent();
            DB_PATH = DB_PATH;
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
