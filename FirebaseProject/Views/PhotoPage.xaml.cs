using System;
using System.Diagnostics;
using FirebaseProject.Database;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

        async void onPickPhoto(Object sender, EventArgs e)
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

        

        void onStorePhoto(Object sender, EventArgs e)
        {
            firebaseHelper.StoreImages(file.GetStream());
        }
        
    }
}
