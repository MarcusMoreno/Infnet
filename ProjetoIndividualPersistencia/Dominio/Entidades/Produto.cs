﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIndividual.Dominio
{
    public class Produto
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Marca { get; set; }

        public string Estoque { get; set; }

        public Forncedor Fornecedor { get; set; }
    }
}
