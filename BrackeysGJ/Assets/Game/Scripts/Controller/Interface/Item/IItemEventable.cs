using System;
using System.Collections.Generic;

namespace Game.Controller.Interface.Item
{
    public interface IItemEventable
    {
        IList<EventHandler> OnChange { get; }
        IList<EventHandler> OnFoward { get; }
        IList<EventHandler> OnRewind { get; }
    }
}