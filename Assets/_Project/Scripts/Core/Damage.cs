using UnityEngine;

// ============================================================
// 武器側とステージ側をつなぐ「共通の受け渡し口」。
// このファイルはカピが管理します。編集したい場合は必ず相談してください。
// ============================================================

/// <summary>攻撃の種類。武器を増やすときはここに追加する。</summary>
public enum DamageType
{
    Impact,     // 大砲など、ぶつかった衝撃
    Explosion,  // 爆弾
    Termite,    // シロアリ
    Ghost,      // 幽霊
}

/// <summary>1回の攻撃の情報をまとめたもの。</summary>
public struct DamageInfo
{
    public DamageType Type;      // どの種類の攻撃か
    public float Amount;         // 威力
    public Vector2 Point;        // 当たった位置
    public Vector2 Direction;    // 飛んできた向き

    public DamageInfo(DamageType type, float amount, Vector2 point, Vector2 direction)
    {
        Type = type;
        Amount = amount;
        Point = point;
        Direction = direction;
    }
}

/// <summary>
/// ダメージを受け取れるものが実装するインターフェース。
/// ブロック・王様など、壊れるものはすべてこれを実装する。
/// </summary>
public interface IDamageable
{
    void ApplyDamage(DamageInfo info);
}
