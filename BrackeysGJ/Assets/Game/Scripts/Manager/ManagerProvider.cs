using System;
using System.Collections.Generic;
using System.Linq;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;

namespace BrackeysGJ.Assets.Game.Scripts.Manager
{
    public class ManagerProvider
    {
        public static ManagerProvider Instance;

        private IList<IBaseManager> Managers;
        
        public ManagerProvider(IList<IBaseManager> managers)
        {
            Instance = this;
            Managers = managers.ToList();
        }

        public T Get<T>() where T : IBaseManager
        {
            var manager = Managers.FirstOrDefault(x => x.GetType() == typeof(T));
            if(manager == null)
                throw new InvalidOperationException("Null manager");

            return (T) manager;
        }
    }
}