using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class BlockBase : MonoBehaviour, IDamageable
{
    [Header("耐久力")]
    [SerializeField] float maxHp = 100f;
    float currentHp;
    private bool isBroken = false;

    [Header("壊れたときの演出（無くても動く）")]
    [SerializeField]
    protected GameObject breakEffectPrefab;
    [SerializeField] private AudioClip damageSound; // ダメージを受けたときの効果音
    private AudioSource audioSource;

    [Header("スコア")]
    [SerializeField] protected int scoreOnBreak = 10;


    void Awake()
    {
        currentHp = maxHp;
        audioSource = GetComponent<AudioSource>();
    }

    public void ApplyDamage(DamageInfo info)
    {
        if (isBroken) return;

        currentHp -= info.Amount;
        if (damageSound != null) audioSource.PlayOneShot(damageSound); // ダメージ音を再生
        if (info.Type == DamageType.Explosion)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(info.Direction, ForceMode2D.Impulse);

            Debug.Log($"Explosion damage applied to {gameObject.name}. Direction: {info.Direction}");
        }
        if (currentHp <= 0f) Break();
    }

    protected virtual void Break()
    {
        isBroken = true;

        if (breakEffectPrefab != null) Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}