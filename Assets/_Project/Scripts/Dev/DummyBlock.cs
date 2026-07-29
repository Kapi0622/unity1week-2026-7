using UnityEngine;

/// <summary>
/// 餅山さんの BlockBase ができるまでの、動作確認用の仮ブロック。
/// 弾側のテストにだけ使い、BlockBase が完成したら削除してください。
/// </summary>
public class DummyBlock : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp = 100f;

    float currentHp;

    void Awake()
    {
        currentHp = maxHp;
    }

    public void ApplyDamage(DamageInfo info)
    {
        currentHp -= info.Amount;
        Debug.Log($"{name} : -{info.Amount:F0} → 残りHP {currentHp:F0}", this);

        if (currentHp <= 0f)
        {
            GameEvents.RaiseBlockDestroyed(10);
            Destroy(gameObject);
        }
    }
}
