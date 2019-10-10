namespace Crescer.Booking.Dominio.Entidades
{
    public class ReservaOpcional
    {
        public ReservaOpcional()
        {
        }

        public ReservaOpcional(Reserva reserva, Opcional opcional)
        {
            this.Reserva = reserva;
            this.Opcional = opcional;
        }
        
        public int Id { get; set; }

        public Reserva Reserva { get; private set; }

        public Opcional Opcional { get; private set; }
    }
}