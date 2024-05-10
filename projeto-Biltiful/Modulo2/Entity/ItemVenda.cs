using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.Entity
{
    internal class ItemVenda
    {
        public int idVenda { get; }                     //5     (0-4)    
        public string produto { get; set; }             //13    (5-17)
        public int quantidade { get; set; }             //3     (18-20)
        public int valorUnitario { get; set; }          //5     (21-25)
        public int totalItem { get; set; }              //6     (26-31)

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
