using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class BombBase : MonoBehaviour, IDamageable, IExplosive
{
    [SerializeField] float maxHp = 100f;
    float currentHp;
    private bool isBroken = false;

    [Header("壊れたときの演出（無くても動く）")]
    [SerializeField]
    protected GameObject breakEffectPrefab;
    [SerializeField] private AudioClip damageSound; // ダメージを受けたときの効果音
    AudioSource audioSource;
    
    [Header("爆発の設定")]
    [SerializeField] private float explosionRadius = 5f; // 爆発の半径
    [SerializeField] private float bombDamageAmount = 50f; // 爆発のダメージ量
    [SerializeField] private float explosionForce = 50f; // 爆発の力

    [Header("スコア")]
    [SerializeField] protected int scoreOnBreak = 10;
    public void ApplyDamage(DamageInfo info)
    {
        if (isBroken) return;

        currentHp -= info.Amount;
        if(damageSound != null) audioSource.PlayOneShot(damageSound); // ダメージ音を再生
        if (info.Type == DamageType.Explosion) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(info.Direction, ForceMode2D.Impulse);
        }
        if (currentHp <= 0f) Break();
    }

    protected virtual void Break()
    {
        isBroken = true;

        if (breakEffectPrefab != null) Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
        Explode();
        
        Destroy(gameObject);

    }

    public void Explode()
    {
        // 爆発の処理をここに書く
        IDamageable[] blocks = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                                       .OfType<IDamageable>()
                                       .ToArray();
        foreach (var block in blocks)
        {
            // 自分自身は除外する
            if (block as MonoBehaviour == this) continue;
            Debug.Log($"爆発の範囲内にいるブロック: {block}");
            Vector3 distanceVector = ((MonoBehaviour)block).transform.position - transform.position;
            float distance = distanceVector.magnitude;
            // 爆発の範囲内にいるかどうかを判定してダメージを与える
            if (distance <= explosionRadius)
            {
                Vector3 bombDirection = distanceVector * explosionForce;
                block.ApplyDamage(new DamageInfo(DamageType.Explosion, bombDamageAmount, transform.position, bombDirection)); 
            }
        }
    }
}
