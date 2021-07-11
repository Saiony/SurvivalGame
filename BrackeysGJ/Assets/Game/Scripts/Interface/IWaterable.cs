using UnityEngine;

public interface IWaterable : IBaseInteractable
{
    void OnWater(Vector3 pos);
}