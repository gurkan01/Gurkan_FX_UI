using UnityEngine;

public class UIFX : MonoBehaviour
{
    [SerializeField] private RectTransform targetImage;
    [SerializeField] private RectTransform fx;

    private void Start()
    {
        // FX'i Image'ın üzerine taşı
        fx.SetParent(targetImage.parent);

        // Image'ın pozisyonuna getir
        fx.position = targetImage.position;

        // UI sıralamasında en üste al
        fx.SetAsLastSibling();
    }
}