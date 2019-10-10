namespace Crescer.Booking.Dominio.Entidades
{
    public class Opcional
    {
        public Opcional()
        {
        }

        public Opcional(string nome, string descricao, decimal valor)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Valor = valor;

        }
        public int Id { get; set; }

        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        public decimal? Valor { get; private set; }

        public void Atualizar(Opcional opcional)
        {
            this.Nome = opcional.Nome;
            this.Descricao = opcional.Descricao;
            this.Valor = opcional.Valor;
        }
    }
}