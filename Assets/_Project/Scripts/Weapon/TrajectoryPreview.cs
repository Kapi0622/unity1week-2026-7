using UnityEngine;

/// <summary>
/// 発射前の軌道予測線。
/// 2Dで空気抵抗がなければ弾道はただの放物線なので、
/// 物理演算を試しに走らせる必要はなく、計算式で点を並べるだけでよい。
///
///   位置(t) = 初期位置 + 初速 * t + 0.5 * 重力 * t^2
///
/// 【GameObject側の設定】
///   LineRenderer をアタッチし、Width を 0.05 程度、Material を Sprites-Default にする
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    [SerializeField] int pointCount = 30;      // 線を構成する点の数
    [SerializeField] float timeStep = 0.05f;   // 何秒おきに点を打つか

    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        Hide();
    }

    public void Show(Vector2 origin, Vector2 velocity, float gravityScale)
    {
        line.enabled = true;
        line.positionCount = pointCount;

        Vector2 gravity = Physics2D.gravity * gravityScale;

        for (int i = 0; i < pointCount; i++)
        {
            float t = i * timeStep;
            Vector2 p = origin + velocity * t + 0.5f * gravity * (t * t);
            line.SetPosition(i, p);
        }
    }

    public void Hide()
    {
        line.enabled = false;
    }
}
