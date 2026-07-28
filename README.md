# 🎮 Unity1Week 2026年7月

Unity1週間ゲームジャム（2026年7月開催）のチーム開発リポジトリです。

## 開発環境

| 項目 | 内容 |
| --- | --- |
| Unity | 6.3 LTS |
| レンダーパイプライン | URP |
| ビルドターゲット | WebGL |
| バージョン管理 | Git + GitHub（CLI） |

## フォルダ構成

```
Assets/
├── _Project/        ← チームの成果物
│   ├── Scripts/
│   ├── Scenes/
│   │   ├── Main/    ← 本番シーン
│   │   └── Dev/     ← 個人テスト用（名前フォルダを作る）
│   ├── Prefabs/
│   ├── UI/
│   ├── Art/
│   ├── Audio/
│   ├── Animations/
│   ├── Materials/
│   └── Resources/
├── ExternalAssets/   ← アセットストア・外部素材
└── Settings/         ← URP設定など（触らない）
```

## ブランチルール

- `main` への直接 push は禁止
- 作業は `feature/名前-作業内容` ブランチで行い、PR を出す
- マージはカピまたは餅山が対応

## ドキュメント

チーム開発のガイドやタスク管理は Notion にまとめています。

🔗 Notion：（URL）
