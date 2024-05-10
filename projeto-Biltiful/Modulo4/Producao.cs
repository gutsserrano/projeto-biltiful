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
            int Id_Producao_Proximo = 0;
            DateOnly Data_Producao;
            string Cod_Barras;
            int Qnt_Producao;

            List<string> Historico_Producao = new List<string>();
            List<string> Historico_ItemProducao = new List<string>();
            List<string> ID_Cosmetico = new List<string>();
            List<string> ID_Produtos = new List<string>();
            List<string> ID_Material = new List<string>();
            List<string> Arquivos = new List<string> { Producao, ItemProducao, Cosmetico, Material};

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
                            Id_Producao_Proximo = int.Parse(line.Substring(0, 5))+1;
                            Id_Produtos.Add(line.Substring(13,13));
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
                            ID_Material.Add(line.Substring(2, 4));
                        }
                    }
                    sr.Close();
                }
            } // Lendo e salvando cópia das informações necessárias
            int op;
            do
            {
                Console.WriteLine("\nFavor, digite uma opção para a produção:\n" +
                    "1-) Cadastrar nova produção;\n2-) Localizar Produção;\n3-) Cancelar e Excluir Produção;\n4-) Imprimir Registros;\n5-) Voltar.");
                op = InserirValor(1, 5);
                switch (op)
                {
                    case 1: // Inserção de novos objetos ItemProducao em Historico_Producao
                        Console.Write("\nInsira a quantidade a ser produzida: ");
                        int quantidade_a_produzir = InserirValor(1,99999);
                        Historico_Producao = CadastrarProducao(quantidade_a_produzir,Id_Producao_Proximo, Historico_Producao, ID_Material, ID_Cosmetico, ID_Produtos);
                        Historico_ItemProducao = CadastrarMateriaPrima(quantidade_a_produzir,Id_Producao_Proximo, Historico_ItemProducao, ID_Material);
                        break;
                    case 2: // Usuário insere o código da produção e devo mostrar cod barras e os codigos dos MPs dele
                        int opt = 0;
                        do
                        {
                            Console.Write("\nInsira os cinco primeiros dígitos do código da produção:  ");
                            string cod_producao = Console.ReadLine();
                            int existe = 0, achou = 0;
                            foreach (string linha in Historico_Producao)
                            {
                                if (linha.Substring(0, 5) == cod_producao)
                                {
                                    existe++;
                                    achou++;
                                }
                                if(achou > 0)
                                {
                                    // Imprimir Resultados
                                    Console.Clear();
                                    Console.WriteLine($"\nO produto procurado possui código de barras: {linha.Substring(13,13)}" +
                                        $"\n E suas matérias primas são os códigos: ");
                                    foreach (string lin in Historico_ItemProducao)
                                    {
                                        if ( lin.Substring(13,6) == cod_producao)
                                        {
                                            Console.Write($" {lin.Substring(13, 6)} ");
                                        }
                                    }
                                    break;
                                }
                            }
                            if (existe == 0)
                            {
                                Console.WriteLine("\nNão há código registrado com o valor digitado\nDeseja tentar novamente?" +
                                    "\n1-) Sim\n2-) Não: ");
                                opt = InserirValor(1, 2);
                            }
                        } while (opt != 2);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            } while (op!=5);
            // Gravar nos arquivos
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
        public void Jump()
        {
            Console.WriteLine("\nDigite enter para continuar ...");
            Console.ReadKey();
            Console.Clear();
        }
        public List<string> CadastrarProducao(int qnt_producao,int indice, List<string> producao, List<string> materia_prima, List<string> produto_final, List<string> produtos_existentes)
        {   // Cadastrar Produção deve atualizar a variável "Lista Produção"
            // Id_atual(5 digitos) + Data_atual (8 digitos) + Cod_Barras (13 digitos) + Quantidade_produzida (5 digitos)
            Console.Write("\nInsira o código do produto que deseja produzir: ");
            string cod_barras = Console.ReadLine();
            if (produtos_existentes.Contains(cod_barras))
            {
                string codigo = $"{Format5dig(indice)}{Hoje()}{cod_barras}{Format5dig(qnt_producao)}";
                producao.Add(codigo);
            }
            else
            {
                Console.WriteLine("\nProduto não cadastrado até o momento");
                Jump();
            }
            return producao;
        }
        public List<string> CadastrarMateriaPrima(int qnt,int indice, List<string> item_producao, List<string> materia_prima)
        {
            // Id_Producao_Proximo, Historico_ItemProducao, ID_Material
            foreach (string item in item_producao){
                string ultima_producao = item.Substring(0, 5);
                string txt = "";
                ItemProducao produzindo;
                if (indice-1 == int.Parse(ultima_producao))
                {
                    produzindo = new ItemProducao(materia_prima,indice);
                    foreach(String mp in produzindo.GetMateriaPrima())
                    {
                        int qnt_mp = produzindo.GetQuantidadeMateriaPrima(mp.Substring(0, 6));
                        txt = $"{Format5dig(indice)}{Hoje()}{mp}{Format5dig(qnt_mp)}";
                        item_producao.Add(txt);
                    }
                }
            }
            return item_producao;
        }
        public string Format5dig(int indice)
        {
            string txt;
            if (indice / 10000 > 1)
            {
                txt = $"{indice}";
            }
            if (indice / 1000 > 1)
            {
                txt = $"0{indice}";
            }
            if (indice / 100 > 1)
            {
                txt = $"00{indice}";
            }
            if (indice / 10 > 1)
            {
                txt = $"000{indice}";
            }
            else
            {
                txt = $"0000{indice}";
            }
            return txt;
        }
        public string Hoje()
        {
            return DateTime.Now.ToString("ddMMyyyy");
        }
    }

}
