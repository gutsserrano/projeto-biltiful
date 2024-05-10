using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo4
{
    internal class ItemProducao
    {
        List<string> MateriaPrima = new List<string>();
        List<int> QuantidadeMateriaPrima = new List<int>();

        public ItemProducao(List<String> ID_Material)
        {
            Console.Write("\nDigite o número de materiais que compõe o produto: ");
            int qnt = InserirValor(1, 999999);
            for (int i = 0; i < qnt; i++)
            {
                bool correto = false;
                do
                {
                    Console.Write("Insira o código da matéria prima: MP");
                    int cod_mat = InserirValor(1, 9999);

                } while (correto != true);
            }
        }
        public int InserirValor(int menor, int maior)
        {
            int valor;
            do
            {
                valor = int.Parse(Console.ReadLine());
                if ((valor < menor || valor > maior))
                {
                    Console.WriteLine("\n\nInsira uma opção válida\n\nDigite enter para continuar");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (valor < menor || valor > maior);
            return valor;
        }
    }
}
