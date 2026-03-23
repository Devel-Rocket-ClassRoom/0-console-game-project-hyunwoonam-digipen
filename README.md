# 0-console-game-project-hyunwoonam-digipen
0-console-game-project-hyunwoonam-digipen created by GitHub Classroom


# Console Maze Game (콘솔 미로 게임)

C#과 사용자 정의 프레임워크(`Framework.Engine`)를 기반으로 제작된 콘솔 미로 탈출 게임입니다. 플레이어는 무작위로 생성되는 미로 속에서 몬스터의 추적을 피해 코인을 모으고, 제한 시간 내에 탈출구를 찾아야 합니다.

## 🎮 게임 특징 (Features)

* **절차적 미로 생성 (Procedural Maze Generation):** 재귀적 백트래킹(Recursive Backtracker) 알고리즘과 Braid Maze(막힌 길 제거) 기법을 사용하여 매번 새로운 형태의 미로가 생성됩니다.
* **A* 알고리즘 몬스터 AI:** 몬스터는 A* 길찾기 알고리즘을 사용하여 플레이어의 위치를 실시간으로 파악하고 최적의 경로로 추적합니다.
* **게임 클리어 조건 (수집 및 탈출):** 맵에 무작위로 나타나는 코인을 3개 모아야만 탈출구가 활성화됩니다.
* **제한 시간 (Time Limit):** 120초의 제한 시간 내에 탈출하지 못하면 게임 오버 처리됩니다.

## 🕹️ 조작 방법 (Controls)

* **이동:** `W`, `A`, `S`, `D` 또는 `방향키 (Arrow Keys)`
* **선택/시작/재시작:** `Enter`
* **게임 종료:** `ESC`

## 📖 게임 규칙 (How to Play)

1. 게임을 시작하면 미로 어딘가에 플레이어(`P`, Cyan), 몬스터(`M`, Magenta), 코인(`C`, Yellow), 탈출구(`E`, Yellow/Red)가 배치됩니다.
2. 몬스터를 피해 미로를 탐색하며 **코인 3개**를 수집해야 합니다.
3. 코인을 3개 모두 모으면 탈출구(`E`)의 색상이 노란색에서 **빨간색(Red)**으로 변경되며 활성화됩니다.
4. 활성화된 빨간색 탈출구에 도착하면 승리(Game Win)합니다.
5. **게임 오버 조건:**
   * 제한 시간 120초가 모두 소진된 경우
   * 몬스터에게 잡힌 경우

## 📂 프로젝트 구조 (Project Structure)

* `MazeGame.cs`: 게임의 메인 진입점 및 씬(Scene) 매니저 역할을 하는 메인 앱 클래스입니다.
* `TitleScene.cs` / `PlayScene.cs`: 게임의 시작 화면과 메인 플레이 로직을 담당하는 씬 클래스입니다.
* `Maze.cs`: 미로의 벽을 생성하고 막힌 길을 뚫어주는 미로 생성 로직을 포함합니다.
* `Player.cs`: 사용자 입력에 따라 그리드를 이동하는 플레이어 객체입니다.
* `Monster.cs` & `Astar.cs`: 플레이어와 가장 먼 곳에서 스폰되며, A* 알고리즘을 통해 플레이어를 추적하는 몬스터 객체 및 길찾기 시스템입니다.
* `Coin.cs` & `Escape.cs`: 게임의 목표인 수집 아이템과 최종 탈출구 객체입니다.

