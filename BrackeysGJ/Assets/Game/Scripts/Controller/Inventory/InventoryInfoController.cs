using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryInfoController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title = null;
    private TextMeshProUGUI Title => _title;

    [SerializeField]
    private Image _image = null;
    private Image Image => _image;

    [SerializeField]
    private TextMeshProUGUI _description = null;
    private TextMeshProUGUI Description => _description;

    private void Awake()
    {
        Sequence seq = DOTween.Sequence();
        seq.Insert(0, Image.DOFade(0, 0));
        seq.Join(Title.DOFade(0, 0));
        seq.Join(Description.DOFade(0, 0));
        seq.Play();
    }

    public void DisplayItem(Item item)
    {
        if (item == null)
            return;

        //Set infos
        Title.text = item.Name;
        Description.text = item.Description;
        Image.sprite = item.Image;

        //ShowWithFade
        Sequence seq = DOTween.Sequence();
        seq.Insert(0, Image.DOFade(1, 0.3f));
        seq.Join(Title.DOFade(1, 0.3f));
        seq.Join(Description.DOFade(1, 0.3f));
        seq.Play();
    }
}
