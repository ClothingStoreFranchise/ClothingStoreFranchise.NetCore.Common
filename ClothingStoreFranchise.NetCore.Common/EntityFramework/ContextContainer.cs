using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStoreFranchise.NetCore.Common.EntityFramework
{
    public interface IContextContainer
    {
        DbContext Context { get; }
    }

    public interface ITypedEFContextContainer<TContext> : IContextContainer where TContext : DbContext
    {
    }

    public class ContextContainer<TContext> : ITypedEFContextContainer<TContext> where TContext : DbContext
    {
        public DbContext Context { get; private set; }

        public ContextContainer(TContext context)
        {
            Context = context;
        }
    }
}
