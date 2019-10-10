using System.Collections.Generic;
using PetStore.Dominio.Entidades;

namespace PetStore.Dominio.Contratos
{
    public interface IPetRepository
    {
        Pet Cadastrar(Pet pet);

        void Alterar(int id, Pet pet);

        void Excluir(int id);

        List<Pet> Buscar();

        Pet BuscarPorId(int id);
    }
}