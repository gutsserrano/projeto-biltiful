﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo4
{
    internal class MainProducao
    {
        

        public MainProducao()
        {
        }
        public void Executar()
        {
            List<string> ids = new List<string> { "MP0001", "MP0002", "MP0003", "MP0004" };
            ItemProducao ip = new ItemProducao(ids);

        }
    }
}
