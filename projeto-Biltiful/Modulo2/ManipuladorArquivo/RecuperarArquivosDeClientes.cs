using projeto_Biltiful.Modulo2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.ManipuladorArquivo
{
    internal class RecuperarArquivosDeClientes
    {
        protected string CaminhoDiretorio { get; set; }
        protected string CaminhoArquivo { get; set; }

        public RecuperarArquivosDeClientes(string c, string a)
        {
            CaminhoDiretorio = c;
            CaminhoArquivo = a;
            if (!Directory.Exists(CaminhoDiretorio))
                Directory.CreateDirectory(CaminhoDiretorio);
            if (!File.Exists(Path.Combine(CaminhoDiretorio, CaminhoArquivo)))
            {
                var aux = File.Create(Path.Combine(CaminhoDiretorio, CaminhoArquivo));
                aux.Close();
            }

        }

        public List<string> RecuperarCpf()
        {
            List<string> cpf = new();

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {

                //cliente    11 (13-23)

                string cliente = linha.Substring(13, 11).Trim();

                cpf.Add(cliente);
            }

            return cpf;
        }

        internal string recuperarEstaAtivo(string cpf)
        {
            var estado = "I";

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                if (cpf.Equals(linha.Substring(0, 11)))

                    estado = linha.Substring(86, 1).Trim();

            }

            return estado;
        }

        internal DateOnly recuperarEData(string? cpf)
        {
            var dataNascimento = new DateOnly();

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                if (cpf.Equals(linha.Substring(0, 11)))

                    dataNascimento = ConverterParaData(linha.Substring(61, 8));

            }

            return dataNascimento;
        }

        private DateOnly ConverterParaData(string data)
        {
            string dia = data.Substring(0, 2);
            string mes = data.Substring(2, 2);
            string ano = data.Substring(4, 4);

            return DateOnly.Parse($"{dia}/{mes}/{ano}");
        }
    }
}
