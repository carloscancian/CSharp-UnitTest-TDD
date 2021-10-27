namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor { get; }

        public Lance(Interessada cliente, double valor)
        {
            if(valor < 0)
            {
                throw new System.ArgumentException("Valor do lance deve ser maior ou igual a 0");
            }
            Cliente = cliente;
            Valor = valor;
        }
    }
}
