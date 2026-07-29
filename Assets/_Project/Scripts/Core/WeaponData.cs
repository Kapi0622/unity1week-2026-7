using UnityEngine;

/// <summary>
/// 武器1種類ぶんの設定値。
/// Projectウィンドウで 右クリック > Create > ShootingCastle > Weapon Data から作る。
/// バランス調整はコードではなくこのアセットの数値をいじって行う。
/// </summary>
[CreateAssetMenu(fileName = "WeaponData_", menuName = "ShootingCastle/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("表示")]
    public string displayName = "大砲";
    public Sprite icon;

    [Header("弾")]
    [Tooltip("Projectile をアタッチした Prefab")]
    public GameObject projectilePrefab;

    [Header("発射")]
    [Tooltip("ドラッグ距離1あたりの初速")]
    public float launchPower = 8f;

    [Tooltip("次に撃てるまでの秒数。強い弾ほど長くする")]
    public float cooldown = 0.5f;

    [Tooltip("重力の効き具合。1が通常、0で浮いたまま飛ぶ")]
    public float gravityScale = 1f;

    [Header("ダメージ")]
    public DamageType damageType = DamageType.Impact;

    [Tooltip("referenceSpeed で当たったときの威力")]
    public float baseDamage = 50f;

    [Tooltip("この速度で当たると baseDamage になる。遅いほど威力が下がる")]
    public float referenceSpeed = 10f;
}
