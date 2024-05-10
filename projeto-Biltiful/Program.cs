using projeto_Biltiful.Modulo1;
using projeto_Biltiful.Modulo2;
using projeto_Biltiful.Modulo3;
using projeto_Biltiful.Modulo4;

namespace projeto_Biltiful
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int op;
            do
            {
                Console.Clear();
                op = Menu();

                switch (op)
                {
                    case 1:
                        new MainCadastro().Executar();
                        break;
                    case 2:
                        MainVenda.Executar();
                        break;
                    case 3:
                        new MainCompra().Executar();
                        break;
                    case 4:
                        //MainCadastro.Executar();
                        break;
                }

            } while (op != 0);
        }

        static int Menu()
        {
            int option;
            do
            {
                Console.WriteLine("Selecione a opção desejada");
                Console.WriteLine("1 - Cadastrar Clientes, Fornecedores, Produtos ou Matéria-prima");
                Console.WriteLine("2 - Vender um Produto");
                Console.WriteLine("3 - Comprar Matéria-prima");
                Console.WriteLine("4 - Produção");
                Console.WriteLine("0 - Sair");

                bool conversao = int.TryParse(Console.ReadLine(), out option);

                if (!conversao)
                {
                    Console.WriteLine("Voce deve digitar um numero!");
                    return Menu();
                }

            } while (option < 0 || option > 4);

            return option;
        }
    }
}
