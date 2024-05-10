using projeto_Biltiful.Modulo2.ManipuladorArquivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.Entity
{
    internal class Venda
    {
        public int id { get; }                       // 5  (0 - 4)
        public DateOnly dataVenda { get; set; }      // 8  (5 - 12)
        public string cliente { get; set; }          // 11 (13-23)
        public float valorTotal { get; set; }          // 7  (24-30)

        public Venda() { }

        public Venda(int id, DateOnly dataVenda, string cliente, float valorTotal)
        {
            this.id = id;
            this.dataVenda = dataVenda;
            this.cliente = cliente;
            this.valorTotal = valorTotal;
        }

        public override string? ToString()
        {
            return "id: " + id + " dataVenda: " + dataVenda.ToString("") + " Cliente: " + cliente + " valor TOtal: " + valorTotal;
        }

        public string FormatarParaArquivo()
        {
            return $"{id}" +
                    $"{ConverterDataParaArquivo(dataVenda)}" +
                    $"{cliente}" +
                    $"{valorTotal}";
        }

        private string ConverterDataParaArquivo(DateOnly data)
        {
            return $"{data.Day:00}{data.Month:00}{data.Year:0000}";
        }

        internal Venda receberDados()
        {

            DateOnly dataVenda = DateOnly.FromDateTime(DateTime.Now);
            string cpf;
            float valorT = 0;

            Console.WriteLine("Digit Cpf da venda: ");
            cpf = Console.ReadLine();

            validarCpf(cpf);
           
            do{
                Console.WriteLine("Digit o valor Total: ");

                try
                {
                    valorT = float.Parse(Console.ReadLine());

                }catch(Exception ex)
                {
                    ex.ToString();
                }
               

            } while(valorT > 99999.99);
            

            Venda venda = new(1, dataVenda, cpf, valorT);

            return venda;
        }

        private bool validarCpf(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Clientes.dat";
            string fileRisco = "Risco.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            List<string> cpfCadastrado = rpC.RecuperarCpf();

            RecuperarArquivosDeClientes rpcRisco = new RecuperarArquivosDeClientes(path, fileRisco);
            List<string> cpfCadastraRisco = rpcRisco.RecuperarCpf();

            if (cpfCadastrado.Contains(cpf))
            {
                if (!cpfCadastraRisco.Contains(cpf))
                {
                    return true;
                }
            }

            return false;

        }
    }
}
