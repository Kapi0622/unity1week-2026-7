using UnityEngine;

/// <summary>
/// マウスのドラッグで弾を発射する。
/// 引っ張った方向と逆に、引っ張った距離に比例した速度で飛ぶ。
///
/// 【Input System 専用設定の場合】
///   using UnityEngine.InputSystem; を足して
///   Input.GetMouseButtonDown(0) -> Mouse.current.leftButton.wasPressedThisFrame
///   Input.GetMouseButton(0)     -> Mouse.current.leftButton.isPressed
///   Input.GetMouseButtonUp(0)   -> Mouse.current.leftButton.wasReleasedThisFrame
///   Input.mousePosition         -> Mouse.current.position.ReadValue()
///   に置き換えてください。
/// </summary>
public class Launcher : MonoBehaviour
{
    [SerializeField] Transform muzzle;                  // 弾が出る位置
    [SerializeField] WeaponData currentWeapon;
    [SerializeField] TrajectoryPreview preview;         // 無ければ null でも動く
    [SerializeField] float maxDragDistance = 3f;        // 引っ張れる最大距離

    Camera cam;
    bool isDragging;
    float lastFireTime = -999f;

    public bool CanFire => currentWeapon != null
                        && Time.time - lastFireTime >= currentWeapon.cooldown;

    void Awake()
    {
        cam = Camera.main;
        if (muzzle == null) muzzle = transform;
    }

    void Update()
    {
        if (currentWeapon == null) return;

        if (Input.GetMouseButtonDown(0) && CanFire)
        {
            isDragging = true;
        }
        else if (isDragging && Input.GetMouseButton(0))
        {
            if (preview != null)
            {
                preview.Show(muzzle.position, CalcVelocity(), currentWeapon.gravityScale);
            }
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (preview != null) preview.Hide();
            Fire(CalcVelocity());
        }
    }

    Vector2 MouseWorldPosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>引っ張った量から初速を計算する。</summary>
    Vector2 CalcVelocity()
    {
        Vector2 drag = (Vector2)muzzle.position - MouseWorldPosition();
        drag = Vector2.ClampMagnitude(drag, maxDragDistance);
        return drag * currentWeapon.launchPower;
    }

    void Fire(Vector2 velocity)
    {
        if (currentWeapon.projectilePrefab == null)
        {
            Debug.LogWarning("projectilePrefab が設定されていません", currentWeapon);
            return;
        }

        lastFireTime = Time.time;

        var obj = Instantiate(currentWeapon.projectilePrefab, muzzle.position, Quaternion.identity);
        obj.GetComponent<Projectile>().Launch(currentWeapon, velocity);
    }

    /// <summary>UI から武器を切り替えるとき用。</summary>
    public void SetWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;
    }
}
