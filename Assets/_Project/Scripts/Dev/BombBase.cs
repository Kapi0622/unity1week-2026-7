using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class BombBase : MonoBehaviour, IDamageable, IExplosive
{
    [SerializeField] float maxHp = 100f;
    float currentHp;

    [SerializeField] private AudioClip damageSound; // ダメージを受けたときの効果音
    AudioSource audioSource;

    [SerializeField] private float explosionRadius = 5f; // 爆発の半径
    [SerializeField] private float bombDamageAmount = 50f; // 爆発のダメージ量
    public void ApplyDamage(DamageInfo info)
    {
        currentHp -= info.Amount;
        audioSource.PlayOneShot(damageSound); // ダメージ音を再生
        
        if(info.Type == DamageType.Explosion) {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddExplosionForce(info.Amount, info.Point, explosionRadius);
        }
        if (currentHp <= 0f) Break();
    }

    protected virtual void Break()
    {
        // パーティクルを出して自分を消す
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
            Vector3 bombDirection = ((MonoBehaviour)block).transform.position - transform.position;
            float distance = bombDirection.magnitude;
            // 爆発の範囲内にいるかどうかを判定してダメージを与える
            if (distance <= explosionRadius)
            {
                block.ApplyDamage(new DamageInfo(DamageType.Explosion, bombDamageAmount, transform.position, bombDirection)); 
            }
        }
    }
}
