using Geolocation;
using System;
namespace Crescer.Aula.Dominio
{
    public class Rota
    {
        public Rota(Ponto pontoA, Ponto pontoB){
            this.PontoA = pontoA;
            this.PontoB = pontoB;
        }
        
        public Ponto PontoA{get; set;}

        public Ponto PontoB{get; set;}

        public double calcularDistancia(){
            return Math.Floor(GeoCalculator.GetDistance(this.PontoA.Local.Latitude, this.PontoA.Local.Longitude, 
            this.PontoB.Local.Latitude, this.PontoB.Local.Longitude, 1));
        }
    }
}