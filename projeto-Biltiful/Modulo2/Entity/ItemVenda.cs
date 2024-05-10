using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.Entity
{
    internal class ItemVenda
    {
        public int idVenda { get; }
        public string produto { get; set; }
        public int quantidade { get; set; }
        public int valorUnitario { get; set; }
        public int totalItem { get; set; }

        public ItemVenda() { }

        public ItemVenda(int idVenda, string produto, int quantidade, int valorUnitario, int totalItem)
        {
            this.idVenda = idVenda;
            this.produto = produto;
            this.quantidade = quantidade;
            this.valorUnitario = valorUnitario;
            this.totalItem = totalItem;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
