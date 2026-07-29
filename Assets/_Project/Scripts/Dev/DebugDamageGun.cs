using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 【ステージ担当のみなさんへ】
/// 弾がまだ完成していなくても、ブロックの動作確認ができるツールです。
/// クリックした場所に、設定した種類・威力のダメージを与えます。
///
/// 使い方
///   1. 自分の Dev シーンの空の GameObject にアタッチする
///   2. Inspector で Damage Type と Amount を設定する
///   3. 再生してブロックをクリックする
///   4. Console に「○○ に 50 ダメージ」と出れば成功
///
/// 本番のシーンには絶対に置かないでください。
/// </summary>
public class DebugDamageGun : MonoBehaviour
{
    [SerializeField] DamageType damageType = DamageType.Impact;
    [SerializeField] float amount = 50f;
    [SerializeField] float radius = 0.1f;   // クリック位置の判定の広さ

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector2 world = cam.ScreenToWorldPoint(Input.mousePosition);
        var hits = Physics2D.OverlapCircleAll(world, radius);
        var damagedTargets = new HashSet<IDamageable>();

        bool hitSomething = false;

        foreach (var hit in hits)
        {
            var target = hit.GetComponentInParent<IDamageable>();
            if (target == null || !damagedTargets.Add(target)) continue;

            target.ApplyDamage(new DamageInfo(damageType, amount, world, Vector2.down));
            Debug.Log($"[DebugGun] {hit.name} に {amount} ダメージ（{damageType}）", hit);
            hitSomething = true;
        }

        if (!hitSomething)
        {
            Debug.Log("[DebugGun] 壊せるものがありません。Collider 2D が付いているか確認してください");
        }
    }
}
