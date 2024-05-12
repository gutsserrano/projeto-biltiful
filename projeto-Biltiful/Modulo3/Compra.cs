using Microsoft.Win32;
using projeto_Biltiful.Modulo1.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


//Contrutor();
//Contrutor(atributos);
//ter um for que limita 3 itens por compra;
//Lista<ItemCompra>
//verificação do cnpj(se não ta nos bloqueados) e com menos de 6 meses de abertura;
//imprimir;
//Arquivar;

namespace projeto_Biltiful.Modulo3
{
    internal class Compra
    {
        public int Id { get; }
        public DateOnly DataCompra { get; set; }
        public string Fornecedor { get; set; }
        public int ValorTotal { get; set; }

        public Compra()
        {
            //construtor vazio antes do contrutor, caso precise criar uma compra, editar etc. 
        }

        public Compra(int id, DateOnly dataCompra, string fornecedor, int valorTotal)

        {
            Id = id;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
        }

        public string FormatarParaArquivo()
        {
            return $"{ConverterIdParaArquivo(Id)}" +
                   $"{ConverterDataParaArquivo(DataCompra)}" +
                   $"{Fornecedor}" +
                   $"{ConverterValorParaArquivo(ValorTotal)}";
        }

        private string ConverterValorParaArquivo(float valor)
        {

            return valor.ToString().Replace(",", "").PadLeft(7, '0');
        }

        private string ConverterIdParaArquivo(int id)
        {
            return id.ToString().PadLeft(5, '0');
        }

        private string ConverterDataParaArquivo(DateOnly data)
        {
            return $"{data.Day:00}{data.Month:00}{data.Year:0000}";
        }

        public override string? ToString()
        {
            return $"\nID: {Id}" +
                   $"\nData compra: {DataCompra.ToString()}" +
                   $"\nFornecedor: {Fornecedor}" +
                   $"\nValor total: {ValorTotal}";
        }


        //cadastra compra
        public void CadastrarCompra(int id, DateOnly dataCompra, string fornecedor, int valorTotal)
        {
            //Verificar:
            //- Não permitir id de compra iguais
            //- Verificar se não atingiu o limite de valor

            //Id = id;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;

            if (valorTotal > 9999999)
            {
                Console.WriteLine("Valor excedido, refaça compra com valor permitido");
            }

        }

        //localiza compra 
        public void LocalizarCompra(int id, DateOnly dataCompra, string fornecedor, int valorTotal)
        {
            //Id = id;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;

            
        }

        // Exclui compra
        public void ExcluirCompra(int idCompra)
        {
            //Apaga permanentemente o registro de uma venda(a venda e TODOS seus itens respectivos)
        }

        // Imprimir
        public void ImpressaoPorRegistro()
        {
            //Impressão por Registro(Onde o usuário poderá “navegar” pelos registros cadastrados,
            //podendo ir para o próximo ou anterior e, também podendo ir para as extremidades (início e final da listagem).
        }
    }

}

