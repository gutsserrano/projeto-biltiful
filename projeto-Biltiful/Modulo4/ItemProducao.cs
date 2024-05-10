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

        public ItemProducao(List<string> ID_Material)
        {
            Console.Write("\nDigite o número de materiais que compõe o produto: ");
            int qnt = InserirValor(1, 99999);
            for (int i = 0; i < qnt; i++)
            {
                bool correto = false;
                do
                {
                    Console.Write("Insira apenas os números do código da matéria prima: ");
                    int cod_mat = InserirValor(1, 9999);
                    string cod_material = FormatCodMaterial(cod_mat);
                    correto = ID_Material.Contains(cod_material);
                    if(correto == false)
                    {
                        Console.WriteLine("\n\nFavor, insira um código MPxxxx cadastrado");
                    }
                    else
                    {
                        MateriaPrima.Add($"MP{cod_mat}");
                        Console.Write($"\nInforme a quantidade do {i+1}º material: ");
                        int quantidade = InserirValor(1,99999);
                        QuantidadeMateriaPrima.Add(quantidade);
                    }
                } while (correto != true);
            } // Adicionando materiais á lista de quantidades
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
        public string FormatCodMaterial(int cod_mat)
        {
            string txt;
            if (cod_mat / 1000 > 1)
            {
                txt = $"{cod_mat}";
            }
            if (cod_mat / 100 > 1)
            {
                txt = $"0{cod_mat}";
            }
            if (cod_mat / 10 > 1)
            {
                txt = $"00{cod_mat}";
            }
            else
            {
                txt = $"000{cod_mat}";
            }
            return $"MP{txt}";
        }
    }
}
