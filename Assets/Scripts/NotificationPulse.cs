using UnityEngine;
using DG.Tweening;

public class NotificationPulse : MonoBehaviour
{
    [Header("Animasyon Ayarları")]
    [SerializeField] private Vector3 punchScale = new Vector3(0.25f, 0.25f, 0f); // Büyüme gücü
    [SerializeField] private float punchDuration = 0.35f;                        // Vuruş süresi
    [SerializeField] private int vibrato = 2;                                    // Kalp gibi çift atış sayısı
    [SerializeField] private float delayInterval = 2.0f;                         // Bekleme süresi

    private Sequence pulseSequence;

    private void Start()
    {
        // Sonsuz döngüde çalışan Sequence zinciri
        pulseSequence = DOTween.Sequence();

        // 1. Kalp atışı / sekme efekti
        pulseSequence.Append(transform.DOPunchScale(punchScale, punchDuration, vibrato, 0.5f));

        // 2. İki atış arasındaki bekleme süresi
        pulseSequence.AppendInterval(delayInterval);

        // 3. Sonsuz tekrar
        pulseSequence.SetLoops(-1);
    }

    private void OnDestroy()
    {
        // Bellek sızıntısını ve obje yok olduğunda hata almayı önler
        pulseSequence?.Kill();
    }
}