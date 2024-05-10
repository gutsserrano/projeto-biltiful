using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projeto_Biltiful.Modulo2.Entity;

namespace projeto_Biltiful.Modulo2
{
    internal class MainVenda
    {
        public MainVenda()
        {
            Executar();
        }

        public void Executar()
        {
            int op;
            do
            {
                Console.WriteLine("Bem vindo ao Menu de Vendas, o que deseja fazer?");
                Console.WriteLine("Cadastrar - [1]");
                Console.WriteLine("Localizar - [2]");
                Console.WriteLine("Excluir - [3]");
                Console.WriteLine("Impressão por Registro - [4]");
                Console.WriteLine("Sair - [0]");

                op = int.Parse(Console.ReadLine());

            } while (op != 0);

            switch (op)
            {
                case 0:
                    Console.WriteLine("Volte sempre");
                    break;
                case 1:
                    Venda venda = new Venda();
                    Venda vendaValidada = venda.receberDados();
                    Console.WriteLine("Cadastrar");
                    break;
                case 2:
                    Console.WriteLine("Localizar");
                    break;
                case 3:
                    Console.WriteLine("Exluir");
                    break;
                case 4:
                    Console.WriteLine("Impressão por Registro");
                    break;
                default:
                    Console.WriteLine("Fim dos processos");
                    break;

            }
        }

    }
}
