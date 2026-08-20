using UnityEngine;
using UnityEngine.UI;

public class ScrollObjectToggler : MonoBehaviour
{
    [Header("Scroll Referansları")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform viewportRect;
    [SerializeField] private RectTransform targetItem;

    [Header("Açılıp Kapanacak Objeler")]
    [SerializeField] private GameObject defaultObject; // Ekranda değilken aktif olan obje
    [SerializeField] private GameObject activeObject;  // Ekrana girince aktif olacak obje

    private readonly Vector3[] itemCorners = new Vector3[4];
    private readonly Vector3[] viewportCorners = new Vector3[4];
    private bool isCurrentlyVisible = false;

    private void OnEnable()
    {
        if (scrollRect != null)
            scrollRect.onValueChanged.AddListener(OnScrollChanged);
    }

    private void OnDisable()
    {
        if (scrollRect != null)
            scrollRect.onValueChanged.RemoveListener(OnScrollChanged);
    }

    private void Start()
    {
        // Başlangıç durumunu kontrol et ve uygula
        UpdateObjectStates(CheckOverlap(), forceUpdate: true);
    }

    private void OnScrollChanged(Vector2 pos)
    {
        bool isVisible = CheckOverlap();
        if (isVisible != isCurrentlyVisible)
        {
            UpdateObjectStates(isVisible);
        }
    }

    private bool CheckOverlap()
    {
        if (targetItem == null || viewportRect == null) return false;

        targetItem.GetWorldCorners(itemCorners);
        viewportRect.GetWorldCorners(viewportCorners);

        // Viewport sınırları
        float viewMinX = viewportCorners[0].x;
        float viewMaxX = viewportCorners[2].x;
        float viewMinY = viewportCorners[0].y;
        float viewMaxY = viewportCorners[2].y;

        // Hedef Obje sınırları
        float itemMinX = itemCorners[0].x;
        float itemMaxX = itemCorners[2].x;
        float itemMinY = itemCorners[0].y;
        float itemMaxY = itemCorners[2].y;


        return (itemMaxX >= viewMinX && itemMinX <= viewMaxX) &&
               (itemMaxY >= viewMinY && itemMinY <= viewMaxY);
    }

    private void UpdateObjectStates(bool isVisible, bool forceUpdate = false)
    {
        if (isVisible == isCurrentlyVisible && !forceUpdate) return;

        isCurrentlyVisible = isVisible;

        if (defaultObject != null)
            defaultObject.SetActive(!isCurrentlyVisible); // Görününce kapanır

        if (activeObject != null)
            activeObject.SetActive(isCurrentlyVisible);   // Görününce açılır
    }
}