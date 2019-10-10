namespace Crescer.Aula.Dominio
{
    public class TurboHelice : Aviao
    {
        public TurboHelice(string nome, int  quantidadeDeMotor, double valorDeFuncionamentoDoMotor)
            : base(nome, quantidadeDeMotor, valorDeFuncionamentoDoMotor, 1.2){
            }
    }
}