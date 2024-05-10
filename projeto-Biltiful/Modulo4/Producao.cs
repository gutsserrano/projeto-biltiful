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

        public void MenuProducao(string Diretorio, string Producao, string ItemProducao, string Cosmetico, string Material)
        { // DIretório equivale ao path, enquanto os demais parâmetros, o snomes dos arquivos
            int Id_Producao = 0;
            DateOnly Data_Producao;
            string Cod_Barras;
            int Qnt_Producao;

            List<string> Historico_Producao = new List<string>();
            List<string> Historico_ItemProducao = new List<string>();
            List<string> ID_Cosmetico = new List<string>();
            List<string> ID_Material = new List<string>();
            List<string> Arquivos = new List<string> { Producao, ItemProducao, Cosmetico, Material };

            if (!Directory.Exists(Diretorio))
            {
                Directory.CreateDirectory(Diretorio);
            } // Verificando se existe o destino a salvar
            foreach (string arquivo in Arquivos)
            {
                if (File.Exists(Diretorio + arquivo)) // Verificando se existe o arquivo de Producao
                {
                    StreamReader sr = new StreamReader(Diretorio + arquivo);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (arquivo == Producao)
                        {
                            Historico_Producao.Add(line);
                        }
                        if (arquivo == ItemProducao)
                        {
                            Historico_ItemProducao.Add(line);
                        }
                        if (arquivo == Cosmetico)
                        {
                            ID_Cosmetico.Add(line.Substring(0, 13));
                        }
                        if (arquivo == Material)
                        {
                            ID_Material.Add(line.Substring(0, 6));
                        }
                    }
                    sr.Close();
                }
            } // Lendo e salvando cópia das informações necessárias


            /*
            foreach (string arquivo in Arquivos)
            {
                if (File.Exists(Diretorio + arquivo)) // Verificando se existe o arquivo de Producao
                {
                    StreamReader sr = new StreamReader(Diretorio + arquivo);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {

                    }
                    sr.Close();
                }
            } // Lendo e salvando cópia das informações necessárias
            */

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
