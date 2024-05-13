using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using projeto_Biltiful.Modulo2.Entity;
using projeto_Biltiful.Modulo2.ManipuladorArquivo;

namespace projeto_Biltiful.Modulo2
{
    internal class MainVenda
    {
        public MainVenda()
        {
            Executar();
        }

        public void Cadastrar(string cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            Venda venda = new Venda();
            Venda vendaValidada = venda.ReceberDados(cpf);

            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);

            List<Venda> listaVenda = mav.CarregarArquivo();
            listaVenda.Add(vendaValidada);

            mav.SalvarArquivo(listaVenda);


            Console.WriteLine("Cadastrado");
        }

        private void Localizar(int id)
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";

            string fileIV = "ItemVenda.dat";
            bool achado = false;


            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);
            List<Venda> listaVenda = mav.CarregarArquivo();


            ManipularArquivoItemVenda mavIV = new ManipularArquivoItemVenda(path, fileIV);
            List<ItemVenda> listaItemVenda = mavIV.CarregarArquivo();

            foreach (var item in listaVenda)
            {
                if (id == item.id)
                {
                    achado = true;
                    Console.WriteLine(item.ToString());
                    
                    foreach (ItemVenda iVenda in listaItemVenda)
                    {
                        if (iVenda.idVenda == id)

                            Console.WriteLine(iVenda.ToString());
                    }

                    Console.ReadKey();
                    break;
                } else
                {
                    achado = false;

                   
                }
            }

            if(!achado) {

                Console.WriteLine("Item não encontrado");
                Console.ReadKey();

            }

        }

        private void Excluir(int id)
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            string fileIV = "ItemVenda.dat";
            bool achado = false;

            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);
            List<Venda> listaVenda = mav.CarregarArquivo();

            ManipularArquivoItemVenda mavIv = new ManipularArquivoItemVenda(path, fileIV);
            List<ItemVenda> listaItemVenda = mavIv.CarregarArquivo();

            foreach (var item in listaVenda)
            {
                if (id == item.id)
                {

                    achado = true;
                                     
                }

            }

            if (achado)
            {
                listaVenda.RemoveAll(item => item.id == id);
                mav.SalvarArquivo(listaVenda);

                listaItemVenda.RemoveAll(item => item.idVenda == id);
                mavIv.SalvarArquivo(listaItemVenda);

                Console.WriteLine("Item Excluído com sucesso");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Item Não Encontrado");
                Console.ReadKey();
            }
           
         }

        public void ImpressaoRegistros()
        {

            string fileIV = "ItemVenda.dat";
            
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            int opc = 0;
            int indice = 0;

            Console.ReadKey();
            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);
            List<Venda> listaVenda = mav.CarregarArquivo();

            ManipularArquivoItemVenda mavIV = new ManipularArquivoItemVenda(path, fileIV);
            List<ItemVenda> listaItemVenda = mavIV.CarregarArquivo();
            
            Console.WriteLine("Inicio do Registro de Vendas: " + listaVenda[indice]);


            foreach (ItemVenda iVenda in listaItemVenda)
            {
                if(iVenda.idVenda == listaItemVenda[indice].idVenda)
               
                    Console.WriteLine(iVenda.ToString());

            }

            do
            {

                Console.WriteLine("Escola se deseja avançar [1], se deseja retroceder  [2], ver o ultimo rigistro [3] ou o primeiro registo[4], ou sair [0]");
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {

                    case 1:
                        try
                        {
                            indice++;
                            Console.WriteLine(listaVenda[indice]);
                            foreach (ItemVenda iVenda in listaItemVenda)
                            {
                                if (iVenda.idVenda == listaItemVenda[indice].idVenda)

                                    Console.WriteLine(iVenda.ToString());

                            }

                        }
                        catch (Exception ex) {
                            Console.WriteLine("Fora do indice");
                        }
                        break;
                    case 2:
                        try
                        {
                            indice--;
                            Console.WriteLine(listaVenda[indice]);
                            foreach (ItemVenda iVenda in listaItemVenda)
                            {
                                if (iVenda.idVenda == listaItemVenda[indice].idVenda)

                                    Console.WriteLine(iVenda.ToString());

                            }

                        }
                        catch (Exception ex) {
                            Console.WriteLine("Fora do indice");
                        }
                        break;
                    case 3:
                        Console.WriteLine(listaVenda.Last().ToString());
                        foreach (ItemVenda iVenda in listaItemVenda)
                        {
                            if (iVenda.idVenda == listaItemVenda.Last().idVenda)

                                Console.WriteLine(iVenda.ToString());

                        }

                        break;
                    case 4:
                        Console.WriteLine(listaVenda.First().ToString());
                        foreach (ItemVenda iVenda in listaItemVenda)
                        {
                            if (iVenda.idVenda == listaItemVenda.First().idVenda)

                                Console.WriteLine(iVenda.ToString());

                        }

                        break;
                    default:
                        Console.WriteLine("Opção não exisente");
                        break;

                }
            } while (opc != 0);


        }

       

        public void Executar()
        {
            int op;
            do
            {
                Venda venda = new Venda();
                int id = 0;

                Console.Clear();
                Console.WriteLine("Bem vindo ao Menu de Vendas, o que deseja fazer?");
                Console.WriteLine("Cadastrar - [1]");
                Console.WriteLine("Localizar - [2]");
                Console.WriteLine("Excluir - [3]");
                Console.WriteLine("Impressão por Registro - [4]");
                Console.WriteLine("Sair - [0]");

                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 0:
                        Console.WriteLine("Volte sempre");
                        break;
                    case 1:
                        Console.WriteLine("Digite seu Cpf: ");
                        string cpf = Console.ReadLine();

                        if (venda.ClienteValido(cpf))
                        {
                            Cadastrar(cpf);

                        }
                        else
                        {
                            Console.WriteLine("Dados invalidos");
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Digite o Id da Venda: ");
                        id = int.Parse(Console.ReadLine());

                        Localizar(id);

                        break;
                    case 3:
                        Console.WriteLine("Digite o Id da Venda: ");
                        id = int.Parse(Console.ReadLine());

                        Excluir(id);  
                        break;
                    case 4:
                        ImpressaoRegistros();
                        break;
                    default:
                        Console.WriteLine("Fim dos processos");
                        break;

                }

            } while (op != 0);


        }


    }
}
