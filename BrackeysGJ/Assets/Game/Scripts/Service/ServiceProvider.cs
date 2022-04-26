using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Scripts.Service.Interface;

namespace Game.Scripts.Service
{
    public class ServiceProvider
    {
        public static ServiceProvider Instance;

        private IList<IBaseService> Services;
        
        public ServiceProvider(IList<IBaseService> services)
        {
            Instance = this;
            Services = services.ToList();
        }

        public T Get<T>() where T : IBaseService
        {
            var manager = Services.FirstOrDefault(x => x is T);
            if(manager == null)
                throw new InvalidOperationException("Null service");

            return (T) manager;
        }
    }
}