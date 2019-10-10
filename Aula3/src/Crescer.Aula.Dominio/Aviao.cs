namespace Crescer.Aula.Dominio
{
    public class Aviao
    {
        public Aviao(string nome, int quantidadeDeMotor, double valorDeFuncionamentoDoMotor, double percentualCalculoMotor){
            this.Nome = nome;
            this.QuantidadeDeMotor = quantidadeDeMotor;
            this.ValorDeFuncionamentoDoMotor = valorDeFuncionamentoDoMotor;
            this.PercentualCalculoMotor = percentualCalculoMotor;
        }
        public string Nome{get; set;}
        public int QuantidadeDeMotor{get; set;}
        public double ValorDeFuncionamentoDoMotor{get; set;}
        public double PercentualCalculoMotor{get; set;}
        public double CalcularValorBase(){
            return (this.ValorDeFuncionamentoDoMotor * (this.QuantidadeDeMotor * this.PercentualCalculoMotor));
        }
    }
}