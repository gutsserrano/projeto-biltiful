using Microsoft.VisualBasic.FileIO;
using projeto_Biltiful.Modulo1;
using projeto_Biltiful.Modulo1.Entidades;
using projeto_Biltiful.Modulo2;
using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace projeto_Biltiful.Modulo3
{
    internal class MainCompra
    {
        // public void Main()
        //{
        //}

        static string pasta = @"C:\Biltiful\";
        static string arquivo = "Compra.dat";
        static string arquivoItem = "itemCompra.dat";
        static int idCompra = 0;
        List<Compra> compras = new List<Compra>();

        public void Executar()
        {
            int op;
            do
            {
                //Console.Clear();
                op = Menu();

                switch (op)
                {
                    case 1:
                        
                        RecuperarId();
                        CadastrarCompras();
                        break;
                    case 2:
                        Console.WriteLine("Digite o ID da compra que deseja visualizar: ");
                        int idCompra = int.Parse(Console.ReadLine());
                        LocalizarCompra(idCompra);
                        LocalizarItensCompra(idCompra);
                        break;
                    case 3:
                        Console.WriteLine("Digite o ID da compra que sera excluida: ");
                        ExcluirCompra(int.Parse(Console.ReadLine()));
                        break;
                    case 4:
                        ImpressaoPorRegistro();
                        break;
                }

            } while (op != 0);

            Console.ReadLine();
        }

        static int Menu()
        {
            int option;
            do
            {
                Console.WriteLine("Selecione a opção desejada");
                Console.WriteLine("1 - Cadastrar compra");
                Console.WriteLine("2 - Localizar compra");
                Console.WriteLine("3 - Excluir compra");
                Console.WriteLine("4 - Impressão por registro");
                Console.WriteLine("0 - Sair");

                bool conversao = int.TryParse(Console.ReadLine(), out option);

                if (!conversao)
                {
                    Console.WriteLine("Voce deve digitar o ID da compra!");
                    return Menu();
                }

            } while (option < 0 || option > 4);

            return option;
        }
        //checo se os arquivos existem
        static void ChecarArquivoCompra()
        {
            if (!File.Exists(pasta + arquivo))
            {
                var aux = File.Create(pasta + arquivo);
                aux.Close();
            }
        }

        static void ChecarArquivoItemCompra()
        {
            if (!File.Exists(pasta + arquivoItem))
            {
                var aux = File.Create(pasta + arquivoItem);
                aux.Close();
            }
        }

        static void RecuperarId() // carrega lista de compras, id é referente lista de compras... carrega lista de compra já salva e conta registros, se for maior que zero joga no id compras se nào o id compras passa a valer 1 
        {
            ChecarArquivoCompra();

            List<Compra> compra = new List<Compra>();
            compra = CarregarListaCompras();

            if (compra.Count > 0)
                idCompra = compra.Count;
        }

        static List<Compra> CarregarListaCompras()//le o arquivo de compras que já ta salvo 
        {
            ChecarArquivoCompra();

            List<Compra> comprasSalvas = new List<Compra>();
            foreach (var linha in File.ReadAllLines(pasta + arquivo))
            {
                // nao entendi o pq, mas se nao tiver esse try catch aqui, ele da pau...
                // ele entra no catch direto, entao colouei p/ imprimir vazio, e assim ele continua o programa normalmente
                try {
                    //dividi linha por linha, jogando cada quantidade de caracter em sua respectivel variavel 
                    //vi no codigo do Luan e adaptei 
                    int id = int.Parse(linha.Substring(0, 4).Trim());
                    string dataCompra = linha.Substring(5, 12).Trim();
                    string fornecedor = linha.Substring(13, 26).Trim();
                    int valorTotal = int.Parse(linha.Substring(27, 33).Trim());
                    comprasSalvas.Add(new Compra(id, DateOnly.Parse(dataCompra), fornecedor, valorTotal));
                } catch (Exception erro)
                {
                    Console.WriteLine("");
                   
                }
            }
                
            return comprasSalvas;
        }

        static List<ItemCompra> CarregarListaItemCompra()
        {
            ChecarArquivoItemCompra();

            List<ItemCompra> itemCompraSalvos = new List<ItemCompra>();
            foreach (var linha in File.ReadAllLines(pasta + arquivoItem))
            {
                // nao entendi o pq, mas se nao tiver esse try catch aqui, ele da pau...
                // ele entra no catch direto, entao botei para imprimir vazio, e assim ele continua o programa normalmente
                try
                {
                    int id = int.Parse(linha.Substring(0, 10).Trim());
                    string dataCompra = linha.Substring(11, 18).Trim();
                    int materiaPrima = int.Parse(linha.Substring(19, 24).Trim());
                    int quantidade = int.Parse(linha.Substring(25, 29).Trim());
                    int valorUnitario = int.Parse(linha.Substring(30, 34).Trim());
                    int totalItem = int.Parse(linha.Substring(35, 40).Trim());
                    itemCompraSalvos.Add(new ItemCompra(id, DateOnly.Parse(dataCompra), materiaPrima, quantidade, valorUnitario, totalItem));
                }
                catch (Exception erro)
                {
                    Console.WriteLine("");
                }
                
            }

            return itemCompraSalvos;
        }

        static void CadastrarCompras()
        {
            ChecarArquivoCompra();

            List<Compra> compras = new List<Compra>();

            int id = idCompra++;

            Console.WriteLine("Infome a data da compra: ");
            DateOnly dataCompra = DateOnly.Parse(Console.ReadLine());

            // verificar se o fornecedor esta bloqueado, so vai prosseguir com o cadastro no caso de fornecedor desbloqueado
            string fornecedor;
            // adicionar o do while no final, quando o fornecedor estiver cadastrado, sem estar tudo cadastrado ele quebra o programa nessa parte
            //do
            //{
                Console.WriteLine("Informe o fornecedor: ");
                fornecedor = Console.ReadLine();
            //} while (!ChecarFornecedor(fornecedor));
            

            // o valor total so sera preenchido apos cadastrar todos os itens da compra
            // aqui vamos retornar o valor total apos ele concluir o cadastro dos itens
            int totalItem = CadastrarItens(dataCompra);

            Compra compra = new Compra(id, dataCompra, fornecedor, totalItem);
            compras.Add(compra);

            try
            {
                SalvarCompras(compras);
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocorreu um erro ao salvar sua compra, favor tentar novamente.");
            }
        }

        static int CadastrarItens(DateOnly dataCompra)
        {
            ChecarArquivoItemCompra();

            int valorTotal = 0;
            int maximoItens = 0;
            int opcao = 0;

            List<ItemCompra> listaItensCompra = new List<ItemCompra>();

            //preciso verificar porque o do while nào ta saindo quando completa 3 itens
            do
            {
                Console.WriteLine("Cadastrar item.");
                                
                Console.WriteLine("Informe Materia Prima: ");
                int materiaPrima = int.Parse(Console.ReadLine());//fazer validaçao do tamanho maximo dos valores, conforme especificado no documento de requesitos

                Console.WriteLine("Informe a quantidade: "); //fazer validaçao do tamanho maximo dos valores, conforme especificado no documento de requesitos
                int quantidade = int.Parse(Console.ReadLine());
                do
                {
                    if (quantidade > 99999 || quantidade < 1)
                        Console.WriteLine("Digite um número válido!");
                } while (quantidade > 99999 || quantidade < 1);

                Console.WriteLine("Informe o valor unitario da sua compra: ");//fazer validaçao do tamanho maximo dos valores, conforme especificado no documento de requesitos
                int valorUnitario = int.Parse(Console.ReadLine());
                do
                {
                    if (quantidade > 99999 || quantidade < 1)
                        Console.WriteLine("Digite um número válido!");
                } while (valorUnitario > 99999 || valorUnitario < 0);

                Console.WriteLine("Informe o valor total das somas do seu item: ");//fazer validaçao do tamanho maximo dos valores, conforme especificado no documento de requesitos
                int totalItem = int.Parse(Console.ReadLine());
                totalItem = valorUnitario * quantidade; if (totalItem > 999999)
                Console.WriteLine("Compra ultrapassou valor maximo permitidp, refaça sua compra!");

                //incrementa o valor total a cada iten 
                valorTotal += totalItem;

                // passei o idCompra para identificar que esse iten esta vinculado a compra de id X
                ItemCompra itemCompra = new ItemCompra(idCompra, dataCompra, materiaPrima, quantidade, valorUnitario, totalItem);
                listaItensCompra.Add(itemCompra);

                maximoItens++;

                Console.WriteLine("1 - Cadastrar novo item");
                Console.WriteLine("0 - Sair");

                opcao = int.Parse(Console.ReadLine());

            } while (opcao != 0 && maximoItens < 3 ); 


            //tentar salvar o item compra, e no final retorna valor total para o cadastro da compra 
            try
            {
                SalvarItensCompra(listaItensCompra);
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocorreu um erro ao salvar os itens da sua compra, favor tentar novamente.");
            }

            return valorTotal;
        }

        static void SalvarCompras(List<Compra> compras)
        {
            // Abrir o arquivo no modo de adição
            /*using (StreamWriter text = File.AppendText(pasta + arquivo))
            {
                text.WriteLine(compras.FormatarParaArquivo());
                text.Close();
            }*/
            StreamWriter sw = new StreamWriter(pasta + arquivo);
            foreach (Compra item in compras)
                sw.WriteLine(item.FormatarParaArquivo());
            sw.Close();
        }

        static void SalvarItensCompra(List<ItemCompra> listaItensCompra)
        {
            // Abrir o arquivo no modo de adição
            /*using (StreamWriter text = File.AppendText(pasta + arquivoItem))
            {
                text.WriteLine(listaItensCompra.FormatarParaArquivo());
                text.Close();
            }*/
            StreamWriter sw = new StreamWriter(pasta + arquivoItem);
            foreach (ItemCompra item in listaItensCompra)
                sw.WriteLine(item.FormatarParaArquivo());
            sw.Close();
        }

        static bool ChecarFornecedor(string cnpj) //checa fornecedor para ver se ta bloqueado ou não 
        {
            string arquivoFornecedor = "Fornecedor.dat";
            string arquivoFornecedorBloqueado = "Bloqueado.dat";

            bool fornecedorExistente = false;
            foreach (var line in File.ReadAllLines(pasta + arquivoFornecedor))
            {
                if (cnpj == line.Substring(0, 14).Trim())
                {
                    DateOnly dataAbertura = DateOnly.Parse(line.Substring(80, 2).Trim() + "/" + line.Substring(82, 4).Trim() + "/" + line.Substring(84, 4).Trim());
                    dataAbertura = dataAbertura.AddMonths(6);
                    if (dataAbertura < dataAbertura)
                        Console.WriteLine("Fonecedor não atende normas de conformidade interna, CNPJ com menos de 6 meses");

                    fornecedorExistente = true;
                    foreach (var item in File.ReadLines(pasta + arquivoFornecedorBloqueado))
                    {
                        if (cnpj == item)
                        {
                            Console.WriteLine("Fornecedor bloqueado.");
                            fornecedorExistente = false;
                        }

                    }//"Fonecedor não atende normas de conformidade interna, CNPJ com menos de 6 meses"
                }

            }
            return fornecedorExistente;
        }

        static void LocalizarCompra(int idProcurado)
        {
            ChecarArquivoCompra();

            List<Compra> compras = new List<Compra>();
            compras = CarregarListaCompras();//carregar lista de compras e colocar na lista instaciada dentro da fuinc;ao 
            bool encontrou = true;

            // foreach percorrendo a lista de compras
            foreach (var compra in compras)
            {
                // se o id da compra for igual ao id buscado, imprimi o valor
                if (compra.Id == idProcurado)
                {
                    encontrou = true;
                    Console.WriteLine(compra.ToString());
                    LocalizarItensCompra(idCompra);//se encontrou a compra, precisa imprimir os intens da compra 
                    Console.ReadKey();
                    break;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine("Compra não localizada.");
                
            }
        }

        static void LocalizarItensCompra(int idCompraProcurado)
        {
            ChecarArquivoItemCompra();

            List<ItemCompra> itemCompra = new List<ItemCompra>();
            itemCompra = CarregarListaItemCompra();//carregar lista de item e colocar na lista instanciada dentro da função 
            int maximoItens = 0;

            // foreach percorrendo os itens de compra
            foreach (var item in itemCompra)
            {
                // se o id do item for igual ao id da compra, retornamos
                if (item.Id == idCompraProcurado)
                {
                    Console.WriteLine(item.ToString());
                    maximoItens++;

                    // se for o numero maximo de itens, para o for
                    if (maximoItens == 3)
                        break;
                }
            }
            //obrigatoriamente uma compra precisa ter pelo menos um item então nào retorna mensagem de não encntrado 
        }

        static void ExcluirCompra(int idCompra)
        {
            ChecarArquivoCompra();

            List<Compra> compras = new List<Compra>();
            compras = CarregarListaCompras();
            bool encontrou = false;

            foreach (var compra in compras)
            {
                if (compra.Id == idCompra)
                {
                    encontrou = true;
                    // se a compra for encontrada, primeiro vamos excluir todos os itens dessa compra
                    ExcluirItensCompra(idCompra);
                    // apos a exclusao de todos os itens da compra, vamos excluir a compra
                    compras.Remove(compra);
                    Console.WriteLine("\nCompra excluida com sucesso.");
                    // salvar novamente o arquivo de compras removendo a compra excluida
                    SalvarCompras(compras);
                    Console.ReadKey();
                    break;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine($"\nCompra com ID {idCompra} não encontrada.");
            }
        }

        static void ExcluirItensCompra(int idCompra)
        {
            ChecarArquivoItemCompra();

            List<ItemCompra> itemCompra = new List<ItemCompra>();
            itemCompra = CarregarListaItemCompra();
            int maximoItens = 1;

            //foreach para percorrer todos os itens do item compra 
            foreach (var item in itemCompra)
            {
                if (item.Id == idCompra)//se encontrar o id do item compra entra no if 
                {
                    itemCompra.Remove(item);//remove o item do item compra
                    maximoItens++;

                    if (maximoItens == 3)//se atingior o maximo de 3 itens encontrados, para o for. 
                        break;
                }
            }

            // salvar novamente o arquivo de itens de compras removendo os itens excluidos
            SalvarItensCompra(itemCompra);
        }

        static void ImpressaoPorRegistro()
        {
            ChecarArquivoCompra();
            ChecarArquivoItemCompra();

            List<Compra> compras = new List<Compra>();
            compras = CarregarListaCompras();
            int opcao;
            int index = 0;

            do
            {
                Console.WriteLine("1 - Avançar registro");
                Console.WriteLine("2 - Voltar registro");
                Console.WriteLine("3 - Primeiro registro");
                Console.WriteLine("4 - Ultimo registro");
                Console.WriteLine("0 - Sair");

                opcao = int.Parse(Console.ReadLine());

                switch(opcao)
                {
                    case 1:
                        try
                        {
                            index++;//incrementa o index para pular pra proxima compra
                            Console.WriteLine(compras[index]);
                        } catch (Exception erro)
                        {
                            Console.WriteLine("Indice invalido.");//se estiver no ultimo registro, retorna que o indice é invalido
                        }                        
                        break;
                    case 2:
                        try
                        {
                            index--;//decrementa o index pra pular para a compra anterior 
                            Console.WriteLine(compras[index]);
                        }
                        catch (Exception erro)
                        {
                            Console.WriteLine("Indice invalido.");//se estiver no primeiro registro avisa que o indice é invalido 
                        }
                        break;
                    case 3:
                        Console.WriteLine(compras.First().ToString());//funçao para mostrar a primeira compra 
                        break;
                    case 4:
                        Console.WriteLine(compras.Last().ToString());//funçao para mostrar a ultima compra 
                        break;
                }

            } while (opcao != 0);
        }

        static ItemCompra CadastroItem()
        {
            int identificadorItemCompra = idCompra, materiaPrima, quantidade, valorUnitario, totalItem;
            DateOnly dataCompra;

            do
            {
                materiaPrima = int.Parse(Console.ReadLine());
            } while (materiaPrima > 99999);
            do
            {
                quantidade = int.Parse(Console.ReadLine());
            } while (quantidade > 99999);
            do
            {
                valorUnitario = int.Parse(Console.ReadLine());
            } while (valorUnitario > 99999);
            do
            {
                totalItem = valorUnitario * quantidade;
            } while (totalItem > 999999);
            ItemCompra itemCompra = new ItemCompra(idCompra, dataCompra, materiaPrima, quantidade, valorUnitario, totalItem);
            return itemCompra;
        }
    }
}
