﻿using ProjetoIndividual.Dominio;
using ProjetoIndividual.Dominio.Interfaces;
using System.Data.Entity;

namespace Persistencia.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DbContext context) : base(context)
        {

        }
    }
}
