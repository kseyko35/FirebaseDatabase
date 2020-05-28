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
                    PhotoSize = PhotoSize.Small
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
        async void onTakePhoto(Object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available", "OK");
            }
            file = await CrossMedia.Current.TakePhotoAsync(
                  new StoreCameraMediaOptions
                  {
                      AllowCropping = true,
                      PhotoSize = PhotoSize.Small,
                      SaveToAlbum = false
                  });

            imgView.Source = ImageSource.FromStream(file.GetStream);
            

        }



        async void onStorePhoto(Object sender, EventArgs e)
        {
            try
            {
                if (file == null)
                {
                    file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {

                        PhotoSize = PhotoSize.Small
                    }); ;
                    if (file == null) return;
                    imgView.Source = ImageSource.FromStream(file.GetStream);

                }
                mGridLayout.IsEnabled = false;
                mIndicator.IsRunning = true;
                var result= await firebaseHelper.StoreImages(file.GetStream());
                mIndicator.IsRunning = false;
                mGridLayout.IsEnabled = true;
                if (result.Contains("http"))
                {
                   
                    await DisplayAlert("Successful", "Image is sended", "Ok");
                   
                }
                else
                {
                    await DisplayAlert("Error", "There is a problem", "Ok");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

    }
}
