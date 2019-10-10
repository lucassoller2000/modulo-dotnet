namespace Crescer.Booking.Dominio.Entidades
{
    public class Suite
    {
        public Suite()
        {
        }

        public Suite(string nome, string descricao, int capacidade, decimal valorDiaria)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Capacidade = capacidade;
            this.ValorDiaria = valorDiaria;

        }
        public int Id { get; set; }

        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        public int? Capacidade { get; private set; }

        public decimal? ValorDiaria { get; private set; }

        public void Atualizar(Suite suite)
        {
            this.Nome = suite.Nome;
            this.Descricao = suite.Descricao;
            this.Capacidade = suite.Capacidade;
            this.ValorDiaria = suite.ValorDiaria;
        }
    }
}