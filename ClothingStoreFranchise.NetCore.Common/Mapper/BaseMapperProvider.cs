using AutoMapper;
using System.Collections.Generic;
using System.Reflection;

namespace ClothingStoreFranchise.NetCore.Common.Mapper
{
    public interface IMapperProvider
    {
        ICollection<Assembly> Assemblies { get; }
    }

    public abstract class BaseMapperProvider : IMapperProvider
    {
        public ICollection<Assembly> Assemblies { get; private set; }

        protected BaseMapperProvider()
        {
            Assemblies = new List<Assembly>();
            AddAssemblies();
        }

        private void AddAssemblies()
        {
            Assembly assembly = GetType().Assembly;
            string assemblyName = assembly.GetName().Name;
            string assemblyPrefix = assemblyName.Substring(0, assemblyName.IndexOf('.'));

            Assemblies.Add(assembly);
        }

        protected virtual MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration(cfg => { });
        }

        public AutoMapper.Mapper GetMapper()
        {
            return new AutoMapper.Mapper(CreateConfiguration());
        }
    }
}
