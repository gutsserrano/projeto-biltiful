using projeto_Biltiful.Modulo1.Entidades;
using projeto_Biltiful.Modulo1.ManipuladorArquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1
{
    internal class MainCadastro
    {
        readonly string Caminho = "C:\\Biltiful\\";

        readonly string ArquivoClientes = "Clientes.dat";
        readonly string ArquivoFornecedor = "Fornecedor.dat";
        readonly string ArquivoProduto = "Cosmetico.dat";
        readonly string ArquivoMateriaPrima = "Materia.dat";
        readonly string ArquivoInadimplentes = "Risco.dat";
        readonly string ArquivoBloqueado = "Bloqueado.dat";

        ManipularCliente ArquivoCliente;
        ManipularFornecedor ArquivoFornecedores;
        ManipularProduto ArquivoProdutos;
        ManipularMPrima ArquivoMPrima;
        ManipularInadimplentes ArquivoRisco;
        ManipularBloqueados ArquivoBloqueados;

        public MainCadastro()
        {
            ArquivoCliente = new(Caminho, ArquivoClientes);
            ArquivoFornecedores = new(Caminho, ArquivoFornecedor);
            ArquivoProdutos = new(Caminho, ArquivoProduto);
            ArquivoMPrima = new(Caminho, ArquivoMateriaPrima);
            ArquivoRisco = new(Caminho, ArquivoInadimplentes, ArquivoCliente);
            ArquivoBloqueados = new(Caminho, ArquivoBloqueado, ArquivoFornecedores);
        }
        public void Executar()
        {
            int option;

            do
            {
                option = Menu();

                switch (option)
                {
                    case 1:
                        MenuClientes();
                        break;
                    case 2:
                        MenuFornecedores();
                        break;
                    case 3:
                        MenuProdutos();
                        break;
                    case 4:
                        MenuMPrimas();
                        break;
                    case 5:
                        MenuInadimplentes();
                        break;
                    case 6:
                        MenuBloqueados();
                        break;
                }
            } while(option != 0);
        }

        private int Menu()
        {
            int op;

            do
            {
                Console.Clear();
                Console.WriteLine("**Módulo 1**");
                Console.WriteLine("1 - clientes");
                Console.WriteLine("2 - Fornecedores");
                Console.WriteLine("3 - Produtos");
                Console.WriteLine("4 - Materias Primas");
                Console.WriteLine("5 - Clientes Inadimplentes");
                Console.WriteLine("6 - Fornecedores Bloqueados");
                Console.WriteLine("0 - Sair");
                op = LerInt("Digite a opção desejada: ");
            } while (op < 0 || op > 6);

            return op;
        }

        private void MenuClientes()
        {
            int op;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("**Clientes**");
                    Console.WriteLine("1 - Cadastrar");
                    Console.WriteLine("2 - Editar");
                    Console.WriteLine("3 - Navegar");
                    Console.WriteLine("4 - Localizar");
                    Console.WriteLine("0 - Sair");
                    op = LerInt("Digite a opção desejada: ");
                } while (op < 0 || op > 4);

                switch (op)
                {
                    case 1:
                        ArquivoCliente.Cadastrar();
                        break;
                    case 2:
                        ArquivoCliente.Editar();
                        break;
                    case 3:
                        ArquivoCliente.NavegarListaClientes();
                        break;
                    case 4:
                        ArquivoCliente.Localizar();
                        break;
                    default:
                        break;
                }
            } while (op != 0);
        }   

        private void MenuFornecedores()
        {
            int op;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("**Fornecedores**");
                    Console.WriteLine("1 - Cadastrar");
                    Console.WriteLine("2 - Editar");
                    Console.WriteLine("3 - Navegar");
                    Console.WriteLine("4 - Localizar");
                    Console.WriteLine("0 - Sair");
                    op = LerInt("Digite a opção desejada: ");
                } while (op < 0 || op > 4);

                switch (op)
                {
                    case 1:
                        ArquivoFornecedores.Cadastrar();
                        break;
                    case 2:
                        ArquivoFornecedores.Editar();
                        break;
                    case 3:
                        ArquivoFornecedores.NavegarListaFornecedores();
                        break;
                    case 4:
                        ArquivoFornecedores.Localizar();
                        break;
                    default:
                        break;
                }
            } while (op != 0);
        }

        private void MenuProdutos()
        {
            int op;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("**Produtos**");
                    Console.WriteLine("1 - Cadastrar");
                    Console.WriteLine("2 - Editar");
                    Console.WriteLine("3 - Navegar");
                    Console.WriteLine("4 - Localizar");
                    Console.WriteLine("0 - Sair");
                    op = LerInt("Digite a opção desejada: ");
                } while (op < 0 || op > 4);

                switch (op)
                {
                    case 1:
                        ArquivoProdutos.Cadastrar();
                        break;
                    case 2:
                        ArquivoProdutos.Editar();
                        break;
                    case 3:
                        ArquivoProdutos.NavegarListaProdutos();
                        break;
                    case 4:
                        ArquivoProdutos.Localizar();
                        break;
                    default:
                        break;
                }
            } while (op != 0);
        }

        private void MenuMPrimas()
        {
            int op;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("**Matérias-primas**");
                    Console.WriteLine("1 - Cadastrar");
                    Console.WriteLine("2 - Editar");
                    Console.WriteLine("3 - Navegar");
                    Console.WriteLine("4 - Localizar");
                    Console.WriteLine("0 - Sair");
                    op = LerInt("Digite a opção desejada: ");
                } while (op < 0 || op > 4);

                switch (op)
                {
                    case 1:
                        ArquivoMPrima.Cadastrar();
                        break;
                    case 2:
                        ArquivoMPrima.Editar();
                        break;
                    case 3:
                        ArquivoMPrima.NavegarListaMPrimas();
                        break;
                    case 4:
                        ArquivoMPrima.Localizar();
                        break;
                    default:
                        break;
                }
            } while (op != 0);
        }

        private void MenuInadimplentes()
        {
            int op;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("**Clientes Inadimplentes**");
                    Console.WriteLine("1 - Adicionar");
                    Console.WriteLine("2 - Remover");
                    Console.WriteLine("3 - Navegar");
                    Console.WriteLine("4 - Localizar");
                    Console.WriteLine("0 - Sair");
                    op = LerInt("Digite a opção desejada: ");
                } while (op < 0 || op > 4);

                switch (op)
                {
                    case 1:
                        ArquivoRisco.Adicionar();
                        break;
                    case 2:
                        ArquivoRisco.Remover();
                        break;
                    case 3:
                        ArquivoRisco.NavegarListaInadimplemtes();
                        break;
                    case 4:
                        ArquivoRisco.BuscarPorCpf();
                        break;
                    default:
                        break;
                }
            } while (op != 0);
        }

        private void MenuBloqueados()
        {
            int op;

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("**Fornecedores Bloqueados**");
                    Console.WriteLine("1 - Adicionar");
                    Console.WriteLine("2 - Remover");
                    Console.WriteLine("3 - Navegar");
                    Console.WriteLine("4 - Localizar");
                    Console.WriteLine("0 - Sair");
                    op = LerInt("Digite a opção desejada: ");
                } while (op < 0 || op > 4);

                switch (op)
                {
                    case 1:
                        ArquivoBloqueados.Adicionar();
                        break;
                    case 2:
                        ArquivoBloqueados.Remover();
                        break;
                    case 3:
                        ArquivoBloqueados.NavegarListaBloqueados();
                        break;
                    case 4:
                        ArquivoBloqueados.BuscarPorCnpj();
                        break;
                    default:
                        break;
                }
            } while (op != 0);
        }

        public static string LerString(string msg)
        {
            string? input;
            do
            {
                Console.Write("\n"+msg);
                input = Console.ReadLine();

                if(input == null)
                    InvalidoMsg();
            } while(input == null);

            return input;
        }

        public static int LerInt(string msg)
        {
            int input;
            bool conversao;
            do
            {
                Console.Write("\n"+msg);
                conversao = int.TryParse(Console.ReadLine(), out input);
                if (!conversao)
                {
                    Console.WriteLine("\n**Digite um número**\n");
                }
            }while(!conversao);

            return input;
        }

        public static float LerFloat(string msg)
        {
            float input;
            bool conversao;
            do
            {
                Console.Write("\n" + msg);
                conversao = float.TryParse(Console.ReadLine(), out input);
                if (!conversao)
                {
                    Console.WriteLine("\n**Digite um número**\n");
                }
            } while (!conversao);

            return input;
        }

        public static DateOnly LerData(string msg)
        {
            DateOnly data;
            bool dataValida;

            do
            {
                Console.Write("\n"+msg);
                dataValida = DateOnly.TryParse(Console.ReadLine(), out data);

                if (!dataValida)
                    InvalidoMsg();
            } while (!dataValida);

            return data;
        }

        public static char LerChar(string msg)
        {
            char input;
            bool inputValido;

            do
            {
                Console.Write("\n"+msg);
                inputValido = Char.TryParse(Console.ReadLine(), out input);

                if (!inputValido)
                {
                    InvalidoMsg();
                }
            } while(!inputValido);

            return input;
        }

        private static void InvalidoMsg()
        {
            Console.WriteLine("\n**Dado inválido**\n");
        }
    }
}
