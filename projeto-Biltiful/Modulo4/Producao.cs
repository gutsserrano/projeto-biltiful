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
            List<string> Arquivos = new List<string> {Producao, ItemProducao, Cosmetico, Material};

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
                        if (arquivo == Producao && line != "")
                        {
                            Historico_Producao.Add(line);
                            Id_Producao_Proximo = int.Parse(line.Substring(0, 5))+1;
                            string codigo_barras_produto = line.Substring(13, 13);
                            ID_Produtos.Add(codigo_barras_produto);
                        }
                        if (arquivo == ItemProducao && line != "")
                        {
                            Historico_ItemProducao.Add(line);
                        }
                        if (arquivo == Cosmetico && line != "")
                        {
                            ID_Cosmetico.Add(line.Substring(0, 13));
                        }
                        if (arquivo == Material && line != "")
                        {
                            ID_Material.Add(line.Substring(0, 6));
                        }
                    }
                    sr.Close();
                }
                else
                {
                    var aux = File.Create(Diretorio + arquivo);
                    aux.Close();
                }
            } // Lendo e salvando cópia das informações necessárias
            int op;
            do
            {
                Jump();
                Console.WriteLine("\nFavor, digite uma opção para a produção:\n" +
                    "1-) Cadastrar nova produção;\n2-) Localizar Produção;\n3-) Cancelar e Excluir Produção;\n4-) Imprimir Registros;\n5-) Voltar.");
                op = InserirValor(1, 5);
                switch (op)
                {
                    case 1: // Inserção de novos objetos ItemProducao em Historico_Producao
                        Console.Write("\nInsira a quantidade a ser produzida: ");
                        int quantidade_a_produzir = InserirValor(1,99999);
                        Console.Write("\nInsira a quantidade necessária de matéria prima: ");
                        int quantidade_materia_prima = InserirValor(2,9999);
                        Historico_Producao = CadastrarProducao(quantidade_a_produzir,Id_Producao_Proximo, Historico_Producao, ID_Material, ID_Cosmetico, ID_Produtos);
                        Historico_ItemProducao = CadastrarMateriaPrima(quantidade_materia_prima, Id_Producao_Proximo, Historico_ItemProducao, ID_Material);
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
                                    Console.Write($"\nO produto procurado possui código de barras: {linha.Substring(13,13)}" +
                                        $"\nE suas matérias primas são os códigos: ");
                                    foreach (string lin in Historico_ItemProducao)
                                    {
                                        if ( lin.Substring(0,5) == cod_producao)
                                        {
                                            Console.Write($" {lin.Substring(13, 6)} ");
                                        }
                                    }
                                    opt = 2;
                                    break;
                                }
                            }
                            if (existe == 0)
                            {
                                Console.WriteLine("\nNão há código registrado com o valor digitado\nDeseja tentar novamente?" +
                                    "\n1-) Sim\n2-) Não\n: ");
                                opt = InserirValor(1, 2);
                            }
                        } while (opt != 2);
                        break;
                    case 3:
                        Console.Write("\nInsira o código da produção (os cinco primeiros digitos): ");
                        int cod_exclusao = InserirValor(1, 99999);
                        Historico_Producao = ExcluirProducao(cod_exclusao, Historico_Producao);
                        Historico_ItemProducao = ExcluirItemProducao(cod_exclusao, Historico_ItemProducao);
                        break;
                    case 4:
                        Console.Write("\nSelecione qual histórico deseja imprimir\n1-) Produção\n2-) Materiais de Produção\n: ");
                        int op_impressao = InserirValor(1, 2);
                        if(op_impressao == 1)
                        {
                            Imprimir_Historico(1, Historico_Producao);
                        }
                        else
                        {
                            Imprimir_Historico(2, Historico_ItemProducao);
                        }
                        break;
                }
            } while (op!=5);
            // Gravar nos arquivos
            #region
            StreamWriter sw_producao = new StreamWriter(Diretorio + Producao);
            foreach (string linha in Historico_Producao)
            {
                sw_producao.WriteLine(linha);
            }
            sw_producao.Close();
            StreamWriter sw_item_producao = new StreamWriter(Diretorio + ItemProducao);
            foreach (string linha in Historico_ItemProducao)
            {
                sw_item_producao.WriteLine(linha);
            }
            sw_item_producao.Close();
            #endregion
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
            bool stop = false;
            do
            {
                Console.Write("\nInsira o código de barras do produto que deseja produzir: ");
                string cod_barras = Console.ReadLine();
                if (produto_final.Contains(cod_barras))
                {
                    string codigo = $"{indice:00000}{Hoje()}{cod_barras}{qnt_producao:00000}";
                    producao.Add(codigo);
                    stop = true;
                }
                else
                {
                    Console.WriteLine("\nProduto não cadastrado até o momento");
                    Jump();
                }

            } while (stop == false);
            return producao;
        }
        public List<string> CadastrarMateriaPrima(int qnt,int proxima_producao, List<string> item_producao, List<string> materia_prima)
        {

            // Id_Producao_Proximo, Historico_ItemProducao, ID_Material
            try
            {
                string txt = "";
                ItemProducao produzindo = new ItemProducao(materia_prima, qnt);
                foreach (String mp in produzindo.GetMateriaPrima())
                {
                    int qnt_mp = produzindo.GetQuantidadeMateriaPrima(mp.Substring(0, 6));
                    txt = $"{proxima_producao:00000}{Hoje()}{mp}{qnt_mp:00000}";
                    item_producao.Add(txt);
                }
            }
            catch (InvalidOperationException ex)
            {

            }
            return item_producao;
        }
        public string Format5dig(int indice)
        {
            string txt="";
            float valor = indice;
            if (valor / 10000 > 1 && valor / 100000 < 1)
            {
                txt = $"{indice}";
            }
            if (valor / 1000 > 1 && valor / 10000 < 1)
            {
                txt = $"0{indice}";
            }
            if (valor / 100 > 1 && valor / 1000 < 1)
            {
                txt = $"00{indice}";
            }
            if (valor / 10 > 1 && valor / 100 < 1)
            {
                txt = $"000{indice}";
            }
            else if(valor / 10 <1)
            {
                txt = $"0000{indice}";
            }
            return txt;
        }
        public string Hoje()
        {
            return DateTime.Now.ToString("ddMMyyyy");
        }

        public List<string> ExcluirProducao(int cod_exclusao, List<string> Historico_Producao_Atual)
        {
            string codigo = Format5dig(cod_exclusao);
            for(int i = Historico_Producao_Atual.Count - 1; i> 0; i--)
            {
                if (Historico_Producao_Atual[i].Substring(0, 5) == codigo)
                {
                    Historico_Producao_Atual.RemoveAt(i);
                }
            }
            return Historico_Producao_Atual;
        }
        public List<string> ExcluirItemProducao(int cod_exclusao, List<string> Historico_ItemProducao_Atual)
        {
            string codigo = Format5dig(cod_exclusao);
            for (int i = Historico_ItemProducao_Atual.Count - 1; i > 0; i--)
            {
                if (Historico_ItemProducao_Atual[i].Substring(0, 5) == codigo)
                {
                    Historico_ItemProducao_Atual.RemoveAt(i);
                }
            }
            return Historico_ItemProducao_Atual;
        }

        public void Imprimir_Historico(int op_historico, List<string> historico)
        {
            int indice = 0;
            int op_impressao = 0;
            do
            {
                Jump();
                Console.WriteLine("\nDigite uma opção de consulta para impressão" +
                    "\n1-) Primeiro Registro\n2-) Próximo Registro\n3-) Registro Anterior\n4-) Último Registro\n5-) Sair");
                op_impressao = InserirValor(1, 5);
                switch (op_impressao)
                {
                    case 1:
                        indice = 0;
                        break;
                    case 2:
                        indice = Math.Min(historico.Count - 1, indice + 1);
                        break;
                    case 3:
                        indice = Math.Max(0, indice - 1);
                        break;
                    case 4:
                        indice = historico.Count-1;
                        break;

                }
                if (op_historico == 1)
                {
                    Console.WriteLine("\nHISTÓRICO DE PRODUÇÃO");
                    Console.WriteLine($"Código de Produção: {historico[indice].Substring(0,5)}\n" +
                        $"Data Produzido: {historico[indice].Substring(5, 2)} / {historico[indice].Substring(7, 2)} / {historico[indice].Substring(9, 4)}\n" +
                        $"Código de Barras: {historico[indice].Substring(13,13)}\n" +
                        $"Quantidade Produzida: {historico[indice].Substring(26,5)}"); // Formatar com substrings
                }
                if (op_historico == 2)
                {
                    Console.WriteLine("\nHISTÓRICO DE ITENS DE PRODUÇÃO");
                    Console.WriteLine($"Código de Produção: {historico[indice].Substring(0,5)}\n" +
                        $"Data Produzido: {historico[indice].Substring(5, 2)} / {historico[indice].Substring(7, 2)} / {historico[indice].Substring(9, 4)}\n" +
                        $"Código da Matéria Prima: {historico[indice].Substring(13,6)}\n" +
                        $"Quantidade de Matéria Prima Necessária: {historico[indice].Substring(19,5)}"); // Formatar com substrings
                }
            } while (op_impressao != 5);
        }
    }

}
