using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.Entity
{
    internal class Venda
    {
        public int id { get; }
        public DateOnly dataVenda { get; set; }
        public string cliente { get; set; }
        public int valorTotal { get; set; }

        public Venda() { }

        public Venda(int id, DateOnly dataVenda, string cliente, int valorTotal)
        {
            this.id = id;
            this.dataVenda = dataVenda;
            this.cliente = cliente;
            this.valorTotal = valorTotal;
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        internal Venda receberDados()
        {

            Venda venda = new(1, new DateOnly(2020, 3, 1), "343434", 122);

            return venda;
        }
    }
}
