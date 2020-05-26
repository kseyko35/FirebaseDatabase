using System;
using System.Collections.Generic;
using Firebase.Database;
using FirebaseProject.Database;
using FirebaseProject.Model;
using Xamarin.Forms;

namespace FirebaseProject
{
    public partial class RealTimePlayer : ContentPage
    {

        public RealTimePlayer()
        {
            
            InitializeComponent();
            FirebaseClient firebase = new FirebaseClient("https://xamarinfirst-440bf.firebaseio.com/");
            List<Player> groups = new List<Player>();
            firebase.Child("player").AsObservable<Player>().Subscribe(d =>
            {
                groups.Add(d.Object);
                              
            });


            mLstPlayer.BindingContext = groups;
            Console.Write("");
        }
    }
}
