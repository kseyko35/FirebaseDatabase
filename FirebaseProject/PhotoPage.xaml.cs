using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;
using FirebaseProject.Database;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Xamarin.Forms;

namespace FirebaseProject
{
    public partial class PhotoPage : ContentPage
    {
        MediaFile file;
        private readonly DbFire firebaseHelper = new DbFire();

        public PhotoPage()
        {
            InitializeComponent();
          
        }

        async void onPickPhoto(System.Object sender, System.EventArgs e)
        {
           await CrossMedia.Current.Initialize();

            //var imgData = await  CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions());

            // imgView.Source = ImageSource.FromStream(imgData.GetStream);
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;

                imgView.Source = ImageSource.FromStream(file.GetStream);

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        

        void onStorePhoto(System.Object sender, System.EventArgs e)
        {
            firebaseHelper.StoreImages(file.GetStream());
        }
        
    }
}
