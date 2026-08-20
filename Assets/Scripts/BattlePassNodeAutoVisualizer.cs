using UnityEngine;
using UnityEngine.UI;

public enum BackgroundType
{
    Epic,
    Legendary,
    Mythic,
    Uncommon,
    Rare,
    Collectable
}

public enum ClaimState
{
    Passive,
    Claimable,
    Claimed,
    CurrentProgress
}

[ExecuteAlways]
public class BattlePassNodeAutoVisualizer : MonoBehaviour
{
    [Header("1. Kilit Durumu (Bool)")]
    public bool isLocked = false;

    [Header("2. Claim Durumu (Enum)")]
    public ClaimState claimState = ClaimState.Passive;

    [Header("3. Arka Plan Türü (Enum)")]
    public BackgroundType backgroundType = BackgroundType.Uncommon;

    [Header("Grafik ve Obje Referansları")]
    [SerializeField] private Image cardBackgroundImage;     // Kart Arka Planı (ImageBack)
    [SerializeField] private GameObject lockIconObject;         // Kilit İkonu
    [SerializeField] private GameObject exclamationMarkObject; // Ünlem Bildirim İkonu (!)
    [SerializeField] private GameObject claimedCheckIcon;       // Onay / Tık İkonu (Opsiyonel)

    [Header("Shine Materyalleri")]
    [SerializeField] private Material activeShineMaterial;  // Claimable durumunda (M_UIShine.mat)
    [SerializeField] private Material passiveShineMaterial; // CurrentProgress durumunda (M_UIShinePassive.mat)

    [Header("Arka Plan Spriteları")]
    [SerializeField] private Sprite epicSprite;
    [SerializeField] private Sprite legendarySprite;
    [SerializeField] private Sprite mythicSprite;
    [SerializeField] private Sprite uncommonSprite;
    [SerializeField] private Sprite rareSprite;
    [SerializeField] private Sprite collectableSprite;

    private void OnValidate()
    {
#if UNITY_EDITOR
        // Materyaller unassigned ise otomatik yükle
        if (activeShineMaterial == null)
            activeShineMaterial = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/UIMaterials/M_UIShine.mat");
        
        if (passiveShineMaterial == null)
            passiveShineMaterial = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/UIMaterials/M_UIShinePassive.mat");

        // Spritelar boşsa otomatik yükle
        if (epicSprite == null) epicSprite = LoadSprite("ui_card_epic");
        if (legendarySprite == null) legendarySprite = LoadSprite("ui_card_legendary");
        if (mythicSprite == null) mythicSprite = LoadSprite("ui_card_mythic");
        if (uncommonSprite == null) uncommonSprite = LoadSprite("ui_card_uncommon");
        if (rareSprite == null) rareSprite = LoadSprite("ui_card_rare");
        if (collectableSprite == null) collectableSprite = LoadSprite("ui_event_pass_collectable");

        // ImageBack bulunamadıysa otomatik bul
        if (cardBackgroundImage == null)
        {
            Transform imgBack = transform.Find("TopCard/ImageBack");
            if (imgBack != null) cardBackgroundImage = imgBack.GetComponent<Image>();
            if (cardBackgroundImage == null) cardBackgroundImage = GetComponentInChildren<Image>();
        }
#endif
        UpdateVisuals();
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }

    [ContextMenu("Görselleri Yenile")]
    public void UpdateVisuals()
    {
        // 1. KİLİT İKONU KONTROLÜ (Bool)
        if (lockIconObject != null)
        {
            lockIconObject.SetActive(isLocked);
        }

        // 2. ÜNLEM İKONU KONTROLÜ (Sadece Claimable durumunda)
        if (exclamationMarkObject != null)
        {
            exclamationMarkObject.SetActive(claimState == ClaimState.Claimable);
        }

        // 3. ARKA PLAN SPRITE VE MATERYAL KONTROLÜ
        if (cardBackgroundImage != null)
        {
            // A) Arka Plan Sprite Seçimi
            switch (backgroundType)
            {
                case BackgroundType.Epic:
                    if (epicSprite != null) cardBackgroundImage.sprite = epicSprite;
                    break;
                case BackgroundType.Legendary:
                    if (legendarySprite != null) cardBackgroundImage.sprite = legendarySprite;
                    break;
                case BackgroundType.Mythic:
                    if (mythicSprite != null) cardBackgroundImage.sprite = mythicSprite;
                    break;
                case BackgroundType.Uncommon:
                    if (uncommonSprite != null) cardBackgroundImage.sprite = uncommonSprite;
                    break;
                case BackgroundType.Rare:
                    if (rareSprite != null) cardBackgroundImage.sprite = rareSprite;
                    break;
                case BackgroundType.Collectable:
                    if (collectableSprite != null) cardBackgroundImage.sprite = collectableSprite;
                    break;
            }

            // B) Claim State & Materyal Geçişleri
            if (claimState == ClaimState.CurrentProgress)
            {
                // CurrentProgress seçilince Materyali M_UIShinePassive olur
                cardBackgroundImage.material = passiveShineMaterial;
                cardBackgroundImage.color = Color.white;
            }
            else if (claimState == ClaimState.Claimable)
            {
                // Claimable seçilince Materyali M_UIShine (Active) olur
                cardBackgroundImage.material = activeShineMaterial;
                cardBackgroundImage.color = Color.white;
            }
            else if (claimState == ClaimState.Claimed)
            {
                cardBackgroundImage.material = null;
                cardBackgroundImage.color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            }
            else // Passive
            {
                cardBackgroundImage.material = null;
                cardBackgroundImage.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            }
        }

        // 4. CLAIMED CHECK İKONU KONTROLÜ
        if (claimedCheckIcon != null)
        {
            claimedCheckIcon.SetActive(claimState == ClaimState.Claimed);
        }
    }

#if UNITY_EDITOR
    private Sprite LoadSprite(string name)
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets(name + " t:Sprite");
        if (guids.Length > 0)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
            return UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
        return null;
    }
#endif
}