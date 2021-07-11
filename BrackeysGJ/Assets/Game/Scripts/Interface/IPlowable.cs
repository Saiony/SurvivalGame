using UnityEngine;

public interface IPlowable : IBaseInteractable
{
    void OnPlow(Vector3 pos);
}