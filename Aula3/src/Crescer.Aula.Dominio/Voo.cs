namespace Crescer.Aula.Dominio
{
    public class Voo
    {   

        public Voo(Rota rota, Aviao aviao){
            this.Rota = rota;
            this.Aviao = aviao;
        }
        public Rota Rota{get; set;}

        public Aviao Aviao{get; set;}

        public double calcularValorTotalDoVoo(){
            return this.Rota.calcularDistancia() * this.Aviao.CalcularValorBase();
        }

    }
}