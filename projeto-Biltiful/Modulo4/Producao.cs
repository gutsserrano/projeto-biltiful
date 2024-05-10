using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo4
{
    internal class Producao
    {
        int Id_atual = 0;
        string Id;
        DateOnly DataProducao;
        string CodigoProduto;
        int Quantidade;
        List<Producao> ListaProducao = new List<Producao>();
        List<ItemProducao> ItemProducao = new List<ItemProducao>();
        List<string> Id_MatPrima = new List<string>();
        List<string> Id_Produtos = new List<string>();

        //// Abertura para leitura do arquivo 
        // Extração de dados através da leitura do arquivo (Producao.dat, ItemProducao.dat, Materia.dat, Cosmetico.dat) --> Foreach
        // Salvamento em listas
        // Get do código de produção atual, através da última linha do foreach de Producao.dat
        //// Fechamento de leitura de arquivos
        //// Abertura de escrita do arquivo
        // Funções CRUD
        //// Fechamento e escrita do arquivo
        // FUNÇÕES AUXILIARES PARA O MÓDULO

        public void MenuProducao(string Producao, string ItemProducao, string Cosmeticos, string Material)
        {

        }
        public int InserirValor(int menor,  int maior)
        {
            int valor;
            do
            {
                valor = int.Parse(Console.ReadLine());
                if((valor < menor || valor > maior))
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
