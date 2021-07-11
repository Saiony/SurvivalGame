using UnityEngine;

public interface IPickable : IBaseInteractable
{
    void OnPick(Vector3 pos);
}