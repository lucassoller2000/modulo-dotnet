using Geolocation;
namespace Crescer.Aula.Dominio
{
    public class Ponto
    {
        public Ponto(double latitude, double longitude){
            Local = new Coordinate()
            {
                Latitude = latitude,
                Longitude = longitude
            };
        }
        public Coordinate Local{get; private set;}
    }
}