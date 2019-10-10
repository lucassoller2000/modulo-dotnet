namespace Crescer.Aula.Dominio
{
    public class TurboJato : Aviao
    {
        public TurboJato(string nome, int  quantidadeDeMotor, double valorDeFuncionamentoDoMotor)
            : base(nome, quantidadeDeMotor, valorDeFuncionamentoDoMotor, 1.5){
            }
    }
}