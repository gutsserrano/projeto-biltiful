﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using projeto_Biltiful.Modulo2.Entity;
using projeto_Biltiful.Modulo2.ManipuladorArquivo;

namespace projeto_Biltiful.Modulo2
{
    internal class MainVenda
    {
        public MainVenda()
        {
            Executar();
        }

        public void Cadastrar(string cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            Venda venda = new Venda();
            Venda vendaValidada = venda.receberDados(cpf);

            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);

            List<Venda> listaVenda = mav.CarregarArquivo();
            listaVenda.Add(vendaValidada);

            mav.SalvarArquivo(listaVenda);


            Console.WriteLine("Cadastrado");
        }

        private void Localizar(int id)
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            bool achado = false;


            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);

            List<Venda> listaVenda = mav.CarregarArquivo();

            foreach (var item in listaVenda)
            {
                if (id == item.id)
                {
                    achado = true;
                    Console.WriteLine(item.ToString());
                    Console.ReadKey();
                    break;
                } else
                {
                    achado = false;

                   
                }
            }

            if(!achado) {

                Console.WriteLine("Item não encontrado");
                Console.ReadKey();

            }

        }


        private void Excluir(int id)
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            bool achado = false;

            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);

            List<Venda> listaVenda = mav.CarregarArquivo();

            foreach (var item in listaVenda)
            
            {
                if (id == item.id)
                {
                    listaVenda.Remove(item);
                    mav.SalvarArquivo(listaVenda);
                    achado = true;
                   
                    break;
                }
                else
                {
                    achado = false;

                }
            }

            if (achado)
            {
                Console.WriteLine("Item Excluido com sucesso");
                Console.ReadKey();
            
            }
            else
            {
                Console.WriteLine("Item Não Encontrado");
                
                Console.ReadKey();
            }


        }
        public void Executar()
        {
            int op;
            do
            {
                Venda venda = new Venda();
                int id = 0;

                Console.Clear();
                Console.WriteLine("Bem vindo ao Menu de Vendas, o que deseja fazer?");
                Console.WriteLine("Cadastrar - [1]");
                Console.WriteLine("Localizar - [2]");
                Console.WriteLine("Excluir - [3]");
                Console.WriteLine("Impressão por Registro - [4]");
                Console.WriteLine("Sair - [0]");

                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 0:
                        Console.WriteLine("Volte sempre");
                        break;
                    case 1:
                        Console.WriteLine("Digite seu Cpf: ");
                        string cpf = Console.ReadLine();

                        if (venda.clienteValido(cpf))
                        {
                            Cadastrar(cpf);

                        }
                        else
                        {
                            Console.WriteLine("Dados invalidos");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Digite o Id da Venda: ");
                        id = int.Parse(Console.ReadLine());

                        Localizar(id);

                        break;
                    case 3:
                        Console.WriteLine("Digite o Id da Venda: ");
                        id = int.Parse(Console.ReadLine());

                        Excluir(id);  
                        break;
                    case 4:
                        Console.WriteLine("Impressão por Registro");
                        break;
                    default:
                        Console.WriteLine("Fim dos processos");
                        break;

                }

            } while (op != 0);


        }

    }
}
