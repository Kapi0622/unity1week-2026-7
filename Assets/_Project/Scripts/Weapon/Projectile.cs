using UnityEngine;

/// <summary>
/// 飛んでいく弾。当たった相手が IDamageable なら ApplyDamage を呼ぶだけで、
/// 相手が木か石か王様かは一切知らない。
///
/// 【Prefab側の設定】
///   Rigidbody 2D : Collision Detection = Continuous （すり抜け防止）
///   Layer        : Projectile
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] GameObject hitEffectPrefab;

    WeaponData data;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>Launcher から呼ばれる。生成直後に必ず1回だけ呼ぶこと。</summary>
    public void Launch(WeaponData weaponData, Vector2 velocity)
    {
        data = weaponData;
        rb.gravityScale = data.gravityScale;

        // Unity 6 から velocity は linearVelocity に名前が変わっています
        rb.linearVelocity = velocity;

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (data == null) return;

        var target = collision.collider.GetComponentInParent<IDamageable>();

        var contact = collision.GetContact(0);
        Vector2 point = contact.point;

        // 接触面の法線の逆＝弾が進んでいた向き
        Vector2 direction = -contact.normal;

        if (target != null)
        {
            // 速く当たるほど痛い。遅い跳ね返りではほとんどダメージが入らない
            float speed = collision.relativeVelocity.magnitude;
            float amount = data.baseDamage * Mathf.Clamp01(speed / data.referenceSpeed);

            target.ApplyDamage(new DamageInfo(data.damageType, amount, point, direction));
        }

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, point, Quaternion.identity);
        }
    }
}
