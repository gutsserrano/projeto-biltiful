using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Ao tentar cadastrar uma compra e o valorTotal exceder o total, cancelar a compra e avisar o fornecedo
//  Ex: A compra nao pode ser cadastrada pois o valor total excede o maximo

namespace projeto_Biltiful.Modulo3
{
    internal class ItemCompra
    {
        int ID { get; set; }
        DateOnly DataCompra { get; set; }
        int MateriaPrima { get; set; }
        int Quantidade { get; set; }
        int ValorUnitario { get; set; }
        int TotalItem { get; set; }

        public ItemCompra()
        {
            //rever com grupo, porque do contrutor vazio
        }

        public ItemCompra(int iD, DateOnly dataCompra, int materiaPrima, int quantidade, int valorUnitario, int totalItem)
        {
            ID = iD;
            DataCompra = dataCompra;
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }

        //imprimir
        public void ImprimirItemCompra()
        {

        }

        // validacão
        //Verificação: quantidade (maior que 0,01 e menor que 99999), valor unitario (maior de 0,01 e menor que 99999)
        //validar valorTotal(no maximo 999999)
        //puxar lista<ItemCompra>
        //Imprimir
        //Arquivar
        public bool ValidarItemCompra()
        {
            if (Quantidade < 0.01 || Quantidade > 99999)
                return false;

            if (ValorUnitario < 0.01 || ValorUnitario > 99999)
                return false;

            return true;
        }
    }
}
