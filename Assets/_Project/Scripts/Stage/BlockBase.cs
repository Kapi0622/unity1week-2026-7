using UnityEngine;

public class BlockBase : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp = 100f;
    float currentHp;

    [SerializeField] private AudioClip damageSound; // ダメージを受けたときの効果音
    private AudioSource audioSource;


    void Awake()
    {
        currentHp = maxHp;
        audioSource = GetComponent<AudioSource>();
    }

    public void ApplyDamage(DamageInfo info)
    {
        currentHp -= info.Amount;
        audioSource.PlayOneShot(damageSound); // ダメージ音を再生
        if (info.Type == DamageType.Explosion)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddExplosionForce(info.Amount, info.Point, info.Direction.magnitude);
        }
        if (currentHp <= 0f) Break();
    }

    protected virtual void Break()
    {
        // パーティクルを出して自分を消す
        Destroy(gameObject);
    }
}