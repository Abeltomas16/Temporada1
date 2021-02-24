using System;

namespace ObjectoDeTransferencia
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public DateTime Datanascimento { get; set; }
        public Boolean Sexo { get; set; }
        public decimal LimiteCompra { get; set; }
    }
}
