using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projeto_Biltiful.Modulo4;

namespace projeto_Biltiful.Modulo4
{
    internal class MainProducao
    {
        

        public MainProducao()
        {
        }
        public void Executar()
        {
            Producao producao = new Producao();
            producao.MenuProducao("C:\\Biltiful\\", "Producao.dat", "ItemProducao.dat", "Cosmetico.dat", "Materia.dat");
        }
    }
}
