using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Crescer.PetStore.Api.Models
{
    public class UserDatabase
    {
        public int Id{get; set;}
        public List<User> Users {get; set;}

        public UserDatabase GetDatabase(){
            var DatabasePath = $"{Path.GetTempPath()}databaseUser.json";
            UserDatabase objUserDatabase;
            if(System.IO.File.Exists(DatabasePath)){
                objUserDatabase = JsonConvert.DeserializeObject<UserDatabase>(System.IO.File.ReadAllText(DatabasePath));
                this.Id = objUserDatabase.Id;
                this.Users = objUserDatabase.Users;
            }else{
                this.Id = 1;
                this.Users = new List<User>();
            }
            return this;
        }

        public void salvar(){
            var DatabasePath = $"{Path.GetTempPath()}databaseUser.json";
            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(this));
        }
    }
}