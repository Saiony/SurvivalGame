using UnityEngine;

public interface IPlantable : IBaseInteractable
{
    void OnPlant(Vector3 pos);
}