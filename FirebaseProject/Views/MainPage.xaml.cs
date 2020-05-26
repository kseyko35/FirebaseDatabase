using System.ComponentModel;
using FirebaseProject.Database;
using FirebaseProject.Model;
using Xamarin.Forms;

namespace FirebaseProject
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //ObservableCollection<User> mList = new ObservableCollection<User>();
        private readonly DbFire firebaseHelper=new DbFire();

        public MainPage()
        {
            InitializeComponent();

            //lstPersons.ItemSelected += async (s, e) =>
            //{
            //    User select_item = (User)lstPersons.SelectedItem;
            //    var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Resend Invite", "Disable User", "Delete User");
            //    switch (action)
            //    {
            //        case "Resend Invite":
            //            Resend_Invite(select_item);
            //            break;
            //        case "Disable User":
            //            Disable_User(select_item);
            //            break;
            //        case "Delete User":
            //            Delete_User(select_item);
            //            break;
            //    }
            //};
        }
     

        void BtnRetrive_Clicked(System.Object sender, System.EventArgs e)
        {
            getListUser();
        }
        async void getListUser()
        {
            var allPersons = await firebaseHelper.GetUser();
            lstPersons.ItemsSource = allPersons;
 
        }
        async void BtnAdd_Clicked(System.Object sender, System.EventArgs e)
        {
            await firebaseHelper.AddUser(txtName.Text, txtSurname.Text);
            await DisplayAlert("Success", txtName.Text + "is added Successfully", "OK");
            txtName.Text = "";
            txtSurname.Text = "";
            getListUser();
        }
       
        async void OnDelete(User select_item)
        {
            await firebaseHelper.DeletePerson(select_item.Name);
            getListUser();
            await DisplayAlert("Success", "Person Deleted Successfully", "OK");
           
        }

        async void OnUpdate(string oldName,string newName,string surname)
        {
            //await firebaseHelper.DeletePerson(select_item.Name);
            //await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            //getListUser();
            await firebaseHelper.UpdatePerson(oldName,newName, surname);
            getListUser();
            await DisplayAlert("Success", "Person Updated Successfully", "OK");
        
        }

        async void lstPersons_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var user = e.Item as User;
            if (user != null)
            {
                 var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Delete User", "Update User");
                 switch (action)
                {
                    case "Delete User":
                        bool response = await DisplayAlert("Delete?", "Are you sure delete "+ user.Name+ " " + user.Surname, "Yes", "No");  //OnDelete(user);
                        if (response)
                        {
                            OnDelete(user);
                        }
                       
                        break;
                    case "Update User":
                        string nameDisplay = await DisplayPromptAsync("Name", "What's the name","Next" ,keyboard: Keyboard.Text);
                        string surnameDisplay = await DisplayPromptAsync("Surname", "What's the surname", keyboard: Keyboard.Text);

                        OnUpdate(user.Name,nameDisplay,surnameDisplay);
                        break;
                }
            }
        }
    }
}
