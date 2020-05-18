using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using User = FirebaseProject.Model.User;

namespace FirebaseProject.Database
{
    public class DbFire
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinfirst-440bf.firebaseio.com/");
        
        public async Task<List<User>>  GetUser()
        {
            return (await firebase
               .Child("user")
               .OnceAsync<User>()).Select(item => new User
               {
                   Name = item.Object.Name,
                   Surname = item.Object.Surname,
                   //Age = item.Object.Age
               }).ToList();
        }
        public async Task AddUser(string name, string surname)
        {
            await firebase
                 .Child("user")
                 .PostAsync(new User() { Name = name, Surname = surname });
        }
        public async Task DeletePerson(string name)
        {
            var toDeletePerson = (await firebase
              .Child("user")
              .OnceAsync<User>()).Where(a => a.Object.Name == name).FirstOrDefault(); // Getting object unique key
            await firebase.Child("user").Child(toDeletePerson.Key).DeleteAsync();
        }
        public async Task UpdatePerson(string oldName,string newName ,string surname)
        {
            var toUpdatePerson = (await firebase
              .Child("user")
              .OnceAsync<User>()).Where(a => a.Object.Name == oldName).FirstOrDefault();

            await firebase
              .Child("user")
              .Child(toUpdatePerson.Key)
              .PutAsync(new User() { Name = newName, Surname = surname });
        }


    }
}
