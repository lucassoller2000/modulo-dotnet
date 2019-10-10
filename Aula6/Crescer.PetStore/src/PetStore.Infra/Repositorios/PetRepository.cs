using System.Collections.Generic;
using PetStore.Dominio.Contratos;
using PetStore.Dominio.Entidades;

namespace PetStore.Infra.Repositorios
{
    public class PetRepository : IPetRepository
    {
        private PetStoreContext contexto;

        public PetRepository(PetStoreContext contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(int id, Pet pet)
        {
            throw new System.NotImplementedException();
        }

        public List<Pet> Buscar()
        {
            throw new System.NotImplementedException();
        }

        public Pet BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public Pet Cadastrar(Pet pet)
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}