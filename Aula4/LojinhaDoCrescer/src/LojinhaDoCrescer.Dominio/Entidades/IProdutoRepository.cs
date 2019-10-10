using System.Collections.Generic;

namespace LojinhaDoCrescer.Dominio.Entidades
{
    public interface IProdutoRepository
    {
        void Salvar(Produto produto);

        Produto BuscarPorId(int id);
    }
}