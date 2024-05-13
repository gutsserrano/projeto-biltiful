using projeto_Biltiful.Modulo1.Entidades;
using projeto_Biltiful.Modulo2.Entity;
using projeto_Biltiful.Modulo4;
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
        public int Id { get; } //tirei o set do Id
        public DateOnly DataCompra { get; set; }
        public int MateriaPrima { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnitario { get; set; }
        public int TotalItem { get; set; }

        public ItemCompra()
        {
            //onstrutor vazio antes do contrutor, caso precise criar uma compra, editar etc.
        }

        public ItemCompra(int id, DateOnly dataCompra, int materiaPrima, int quantidade, int valorUnitario, int totalItem)
        {
            Id = id;
            DataCompra = dataCompra;
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }
  
        public override string? ToString()
        {
            return $"\nID: {Id}" +
                   $"\nData compra: {DataCompra.ToString()}" +
                   $"\nMateria prima: {MateriaPrima}" +
                   $"\nQuantidade: {Quantidade}" +
                   $"\nValor unitario: {ValorUnitario}" +
                   $"\nTotal item: {TotalItem}";
        }

        public string FormatarParaArquivo() //código do luan adapitado, funciona ta otimo 
        {
            return $"{ConverterIdParaArquivo(Id)}" +
                   $"{ConverterDataParaArquivo(DataCompra)}" +
                   $"{ConverterMateriaPrimaParaArquivo(MateriaPrima)}" +
                   $"{ConverterQuantidadeParaArquivo(Quantidade)}" +
                   $"{ConverterValorParaArquivo(ValorUnitario)}" +
                   $"{ConverterTotalItemParaArquivo(TotalItem)}";

        }

        private string ConverterValorParaArquivo(float valor)
        {
            return valor.ToString().Replace(",", "").PadLeft(5, '0');
        }

        private string ConverterMateriaPrimaParaArquivo(int id)
        {
            return id.ToString().PadLeft(6, '0');
        }

        private string ConverterTotalItemParaArquivo(float totalItem)
        {
            return totalItem.ToString().Replace(",", "").PadLeft(6, '0');
        }

        private string ConverterQuantidadeParaArquivo(int quantidade)
        {
            return quantidade.ToString().PadLeft(5, '0');
        }

        private string ConverterIdParaArquivo(int id)
        {
            return id.ToString().PadLeft(11, '0');
        }

        private string ConverterDataParaArquivo(DateOnly data)
        {
            return $"{data.Day:00}{data.Month:00}{data.Year:0000}";
        }

        //imprimir
        //public void ImprimirItemCompra()
        //{
        //    Console.WriteLine(Id + "\n" + MateriaPrima + "\n" + Quantidade + "\n" + ValorUnitario + "\n" + TotalItem);
            
        //}

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

        public bool ValidarValorTotal()
        {
            if (TotalItem < 0.01 || TotalItem > 999999)
                return false;
            else
            {
                return true;
            }
            
        }
    }
}
