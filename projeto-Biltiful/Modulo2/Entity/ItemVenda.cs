﻿using projeto_Biltiful.Modulo1.Entidades;
using projeto_Biltiful.Modulo2.ManipuladorArquivo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.Entity
{
    internal class ItemVenda
    {
        // Propriedades da classe
        public int idVenda { get; set; }                     //5     (0-4)    
        public string produto { get; set; }                 //13    (5-17)
        public int quantidade { get; set; }                 //3     (18-20)
        public float valorUnitario { get; set; }           //5     (21-25)
        public float totalItem { get; set; }               //6     (26-31)

        // Construtores
        public ItemVenda() { }

        public ItemVenda(int idVenda, string produto, int quantidade, float valorUnitario, float totalItem)
        {
            this.idVenda = idVenda;
            this.produto = produto;
            this.quantidade = quantidade;
            this.valorUnitario = valorUnitario;
            this.totalItem = totalItem;
        }

        // Métodos públicos da classe
        public string FormatarParaArquivo()
        {
            return $"{idVenda.ToString().PadLeft(5, '0')}" +
                    $"{produto}" +
                    $"{quantidade.ToString().PadLeft(3, '0')}" +
                    $"{valorUnitario.ToString().Replace(",", "").PadLeft(5, '0')}" +
                    $"{totalItem.ToString().Replace(",", "").PadLeft(6, '0')}";

        }

        public override string? ToString()
        {
            return "id da Venda : " + idVenda + " Produto: " + ConverterProdutoString(produto) + " Quantidade: " + quantidade
                + " Valor Unitario: " + valorUnitario.ToString().Insert(valorUnitario.ToString().Length - 2, ",") +
                " Total Item: " + totalItem.ToString().Insert(totalItem.ToString().Length - 2, ",");
        }

        public string ConverterProdutoString(string produto)
        {
            string path = @"C:\Biltiful\";
            string file = "Cosmetico.dat";

            RecuperarArquivosDeProduto rpC = new RecuperarArquivosDeProduto(path, file);
            string nomeData = rpC.RecuperarPrecoeDataNascimento(produto);

            return nomeData;
        }


        // Métodos privados de manipulação de dados
        public float GerarItemVenda(string cpf, int id)
        {
            float valorTotal = 0, valorProduto = 0, totalItem = 0;
            int quantidadeProd = 0, quantidadeItem = 0;
            string produto = "";
            List<ItemVenda> listaCadastro = new();

            quantidadeProd = ObterQuantidadeProdutos();

            for (int i = 0; i < quantidadeProd; i++)
            {
                Console.WriteLine("Digite o Codigo do produto comprado: ");
                produto = Console.ReadLine();

                if (!ValidarProduto(produto))
                {
                    Console.WriteLine("Produto não encontrado");
                    Console.ReadKey();
                    new MainVenda().Executar();
                }

                if (!ValidarAtivo(produto))
                {
                    Console.WriteLine("Produto Inativo");
                    Console.ReadKey();
                    new MainVenda().Executar();
                }

                valorProduto = ObterValorProduto(produto);
                quantidadeItem = ObterQuantidadeItem();
                totalItem = valorProduto * quantidadeItem;

                if (totalItem >= 1000000)
                {
                    Console.WriteLine("Ultrapassou os maximo permitido de valor total");
                    Console.ReadKey();
                    new MainVenda().Executar();
                }

                valorTotal += totalItem;
                ItemVenda itemVenda = new ItemVenda(id, produto, quantidadeItem, valorProduto, totalItem);
                listaCadastro.Add(itemVenda);
            }

            CadastrarNovoItemNoArquivo(listaCadastro);

            return valorTotal;
        }

        public bool ValidarAtivo(string? produto)
        {
            string path = @"C:\Biltiful\";
            string file = "Cosmetico.dat";

            RecuperarArquivosDeProduto raP = new RecuperarArquivosDeProduto(path, file);
            string estaAtivo = raP.RecuperarEstaAtivo(produto);

            return estaAtivo.Equals("A");
        }

        public void CadastrarNovoItemNoArquivo(List<ItemVenda> itemVenda)
        {
            string path = @"C:\Biltiful\";
            string file = "ItemVenda.dat";
            ManipularArquivoItemVenda mav = new ManipularArquivoItemVenda(path, file);

            List<ItemVenda> listaVenda = mav.CarregarArquivo();
            listaVenda.AddRange(itemVenda);
            mav.SalvarArquivo(listaVenda);
            Console.WriteLine("Cadastrado");
        }

        public int ObterQuantidadeItem()
        {
            int quantidadeItem = 0;
            do
            {
                Console.WriteLine("Digite a quantidade desse produto que deseja comprar [Máximo 999]: ");
                quantidadeItem = int.Parse(Console.ReadLine());

            } while (quantidadeItem <= 0 || quantidadeItem > 999);
            return quantidadeItem;
        }

        public int ObterQuantidadeProdutos()
        {
            int quantidadeProd = 0;
            do
            {
                Console.WriteLine("Quantos itens deseja comprar?[entre 1 ou 3]");
                try
                {
                    quantidadeProd = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            } while (quantidadeProd <= 0 || quantidadeProd > 3);

            return quantidadeProd;
        }

        public float ObterValorProduto(string? produto)
        {
            string path = @"C:\Biltiful\";
            string file = "Cosmetico.dat";
            float precoProduto = 0;

            RecuperarArquivosDeProduto raP = new RecuperarArquivosDeProduto(path, file);
            List<string> produtoCadastrado = raP.RecuperarCosmetico();

            if (produtoCadastrado.Contains(produto))
            {
                precoProduto = raP.RecuperarValor(produto);
            }
            return precoProduto;
        }

        public bool ValidarProduto(string? produto)
        {
            string path = @"C:\Biltiful\";
            string file = "Cosmetico.dat";

            RecuperarArquivosDeProduto raP = new RecuperarArquivosDeProduto(path, file);
            List<string> produtoCadastrado = raP.RecuperarCosmetico();

            return produtoCadastrado.Contains(produto);
        }
    }
}
