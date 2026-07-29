using System;

// ============================================================
// ゲーム全体への「お知らせ」をまとめた場所。
// 送る側は Raise○○() を呼ぶだけ。受け取る側が誰かは知らなくてよい。
//
// 【重要】購読する側は必ず OnDisable で -= してください。
//         忘れるとシーンを切り替えても購読が残り、原因不明のバグになります。
//
//   void OnEnable()  { GameEvents.OnKingDestroyed += HandleClear; }
//   void OnDisable() { GameEvents.OnKingDestroyed -= HandleClear; }
// ============================================================

public static class GameEvents
{
    /// <summary>王様が壊された（＝ステージクリア）</summary>
    public static event Action OnKingDestroyed;

    /// <summary>ブロックが壊された。引数は加算スコア。</summary>
    public static event Action<int> OnBlockDestroyed;

    public static void RaiseKingDestroyed() => OnKingDestroyed?.Invoke();

    public static void RaiseBlockDestroyed(int score) => OnBlockDestroyed?.Invoke(score);

    /// <summary>シーンを抜けるときに呼ぶ。購読の消し忘れ対策。</summary>
    public static void ClearAll()
    {
        OnKingDestroyed = null;
        OnBlockDestroyed = null;
    }
}
