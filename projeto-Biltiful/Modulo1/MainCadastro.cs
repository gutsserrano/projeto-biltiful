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
        readonly string Arquivo = "Clientes.dat";

        ManipularCliente ArquivoCliente;

        public MainCadastro()
        {
            ArquivoCliente = new(Caminho, Arquivo);
        }
        public void Executar()
        {
            ArquivoCliente.NavegarListaClientes();
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
