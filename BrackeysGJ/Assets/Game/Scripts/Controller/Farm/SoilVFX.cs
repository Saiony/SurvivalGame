using UnityEngine;

public class SoilVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject _crop = null;
    private GameObject Crop => _crop;

    [SerializeField]
    private GameObject _plowed = null;
    private GameObject Plowed => _plowed;

    [SerializeField]
    private GameObject _plowedAndWatered = null;
    private GameObject PlowedAndWatered => _plowedAndWatered;

    private void DisableAllStates()
    {
        Crop.SetActive(false);
        Plowed.SetActive(false);
        PlowedAndWatered.SetActive(false);
    }

    public void Plow()
    {
        DisableAllStates();
        Plowed.SetActive(true);
    }

    public void Plant()
    {
        Crop.SetActive(true);
    }

    public void Water()
    {
        Plowed.SetActive(false);
        PlowedAndWatered.SetActive(true);
    }

    public void Harvest()
    {
        Plow();
    }
}