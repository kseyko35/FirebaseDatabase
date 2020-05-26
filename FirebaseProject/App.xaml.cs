using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirebaseProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();  // Used FirebaseDatabase.net plugin for all platform

            MainPage = new RealTimePlayer();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
