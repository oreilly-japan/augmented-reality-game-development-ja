# UnityによるARゲーム開発

---

![表紙](augmented-reality-game-development-ja.png)

---

本リポジトリはオライリー・ジャパン発行書籍『[UnityによるARゲーム開発](http://www.oreilly.co.jp/books/9784873118109/)』（原書名『[Augmented Reality Game Development](https://www.packtpub.com/application-development/augmented-reality-game-development)』）のサポートサイトです。

## サンプルコード

サンプルコードの解説は本書籍をご覧ください。

### ダウンロード方法
本書のプロジェクト（FoodyGO）は本文の解説とこのリポジトリのリソースを組合せて作ることができます。各章で使うリソースパッケージは下記のリンクからダウンロードできます。

* [2章用：Chapter2.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter2.unitypackage)
* [3章用：Chapter3.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter3.unitypackage)
* [4章用-その1：Chapter4_import1.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter4_import1.unitypackage)
* [4章用-その2：Chapter4_import2.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter4_import2.unitypackage)
* [5章用-その1：Chapter5_import1.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter5_import1.unitypackage)
* [5章用-その2：Chapter5_import2.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter5_import2.unitypackage)
* [6章用-その1：Chapter6_import1.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter6_import1.unitypackage)
* [6章用-その2：Chapter6_import2.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter6_import2.unitypackage)
* [7章用-その1：Chapter7_import1.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter7_import1.unitypackage)
* [7章用-その2：Chapter7_import2.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter7_import2.unitypackage)
* [8章用：Chapter8_import1.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter8_import1.unitypackage)
* [9章用：Chapter9_Firebase.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter9_Firebase.unitypackage)
* [10章用：Chapter10_debugging.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter10_debugging.unitypackage)
* [10章用：Chapter10.unitypackage](https://github.com/oreilly-japan/augmented-reality-game-development-ja/raw/master/resources/Chapter10.unitypackage)


また、各章ごとに終了時点の状態のUnityプロジェクトをブランチを分けて保存してあります。下記の各章のリンクからダウンロードできます。

* [1章：Chapter_1_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_1_End.zip)
* [2章：Chapter_2_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_2_End.zip)
* [3章：Chapter_3_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_3_End.zip)
* [4章：Chapter_4_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_4_End.zip)
* [5章：Chapter_5_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_5_End.zip)
* [6章：Chapter_6_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_6_End.zip)
* [7章：Chapter_7_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_7_End.zip)
* [8章（完成状態）：Chapter_8_End](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_8_End.zip)

* [付録 A](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_11a_End.zip)
* [付録 B](https://github.com/oreilly-japan/augmented-reality-game-development-ja/archive/Chapter_12b_End.zip)

### 使用方法

上記各章ごとのリンクからダウンロードするとzip形式の圧縮ファイルでダウンロードされるので解凍します。
Unityでプロジェクトを開く際に`FoodyGO`フォルダーを指定してください。
ただし、Unity標準アセットは含まれていないので、プロジェクトをUnityで開いたら次の手順で必要なものをインポートしてください。

1. メインメニューの［Assets］を選択します。それから［Import Package］→［Cameras］の順に操作します。
2. インポート可能なすべてのものをリスト表示したインポートダイアログがポップアップするので［Import］をクリックします。
3. 同様にして［Assets］→［Import Package］→［Characters］の順に操作し、ポップアップしたインポートダイアログで［Import］をクリックします。
4. 同様にして［Assets］→［Import Package］→［CrossPlatformInput］の順に操作し、ポップアップしたインポートダイアログで［Import］をクリックします。
5. 同様にして［Assets］→［Import Package］→［ParticleSystems］の順に操作し、ポップアップしたインポートダイアログで［Import］をクリックします。
6. Projectパネルの`Assets`を選択し、シーン（Unityのロゴがアイコンになっているもの）をダブルクリックして開くとSceneビューにオブジェクトが表示されます。


## 実行環境

日本語版で検証に使用した各ソフトウェアのバージョン、およびハードウェアは次のとおりです。

#### ソフトウェア

* Unity 5.6.2f1
* Android SDK API level 23
* Xcode 8.3.3

#### ハードウェア（括弧内はOSのバージョン）

* ASUS ZenFone AR（Android 7.0）
* Nexus 5X（Android 7.1.1）
* iPhone SE（iOS 10.3.2）
* MacBook Pro Retina, 13-inch, Mid 2014（Mac OS X 10.11.5）

## 正誤表

下記の誤りがありました。お詫びして訂正いたします。

本ページに掲載されていない誤植・間違いを見つけた方は、japan＠oreilly.co.jpまでお知らせください。

### 第1刷まで

#### 付録B P.313 14行目

_誤_

```
4. 引き続きCatchMonsterが選択された状態で、［Hierarchy］ウィンドウの
   MosnterParentをドラッグして、［Unity AR Hit Test Example (Script)］コンポーネン
   トの［Hit Transform］のフィールドにドロップします。
```

_正_

```
4. 引き続きCatchMonsterが選択された状態で、［Hierarchy］ウィンドウの
   MosnterParentをドラッグして、［Unity AR Hit Test Example (Script)］コンポーネン
   トの［Hit Transform］のフィールドにドロップします。そして、CatchMonsterの［Transform］
   の［Position］の値を0, 0, 0、［Scale］の値を0.2, 0.2,0.2に設定します。
```
