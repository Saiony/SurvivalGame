using UnityEngine;

public class SoilVFX : MonoBehaviour
{

    [SerializeField]
    private GameObject _plowed = null;
    private GameObject Plowed => _plowed;

    [SerializeField]
    private GameObject _plowedAndWatered = null;
    private GameObject PlowedAndWatered => _plowedAndWatered;

    private void DisableAllStates()
    {
        Plowed.SetActive(false);
        PlowedAndWatered.SetActive(false);
    }

    public void Plow()
    {
        DisableAllStates();
        Plowed.SetActive(true);
    }

    public void Plant(GameObject crop)
    {
    }

    public void Water()
    {
        Plowed.SetActive(false);
        PlowedAndWatered.SetActive(true);
    }

    public void UnWater()
    {
        Plowed.SetActive(true);
        PlowedAndWatered.SetActive(false);
    }

    public void Harvest()
    {

    }
}