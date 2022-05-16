using Game.Scripts.Controller.Player.Commands;

namespace Game.Scripts.Domain.Interface.Items
{
    public interface ITool : IEquipment
    {
        Command ToolEquipAction { get; }
        Command ToolUnequipAction { get; }
    }
}