using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Crescer.PetStore.Api.Models
{
    public class PetDatabase
    {
        public int Id{get; set;}
        public List<Pet> Pets {get; set;}

        public PetDatabase GetDatabase(){
            var DatabasePath = $"{Path.GetTempPath()}database.json";
            PetDatabase objPetDatabase;
            if(System.IO.File.Exists(DatabasePath)){
                objPetDatabase = JsonConvert.DeserializeObject<PetDatabase>(System.IO.File.ReadAllText(DatabasePath));
                this.Id = objPetDatabase.Id;
                this.Pets = objPetDatabase.Pets;
            }else{
                this.Id = 1;
                this.Pets = new List<Pet>();
            }
            return this;
        }

        public void salvar(){
            var DatabasePath = $"{Path.GetTempPath()}database.json";
            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(this));
        }
    }
}