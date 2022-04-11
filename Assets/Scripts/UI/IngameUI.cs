using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임 씬에 ui 들을 제어할 클래스
/// </summary>
public class IngameUI : MonoBehaviour
{
    // 마지막으로 게임을 진행한 시간을 저장
    // -> 게임오버 후 3초 후에 씬 변경을 위해 
    private float lastGamePlayTime;

    // 플레이어의 체력이 변경됨에 따라 펭귄의 위치를 이동시켜야하므로 RT참조를 받아옴
    public RectTransform currentGauge;

    // 현재 체력을 나타내는 펭귄 이미지의 위치를 계산하기 위해, 부모 객체의 RT를 받아옴
    public RectTransform lifeBar;

    // 점수를 표현할 텍스트 컴포넌트 참조 
    public Text score;

    // 게임오버 UI 객체 참조
    public GameObject gameOver;

    // UI에서 플레이어의 체력을 이용하여 표현해야 하는 부분이 존재하여 참조
    public Player player;

    void Update()
    {
        CalulateLifeGauge();
        ScoreUpdate();
        CheckGameOver();
    }

    /// <summary>
    /// 체력게이지 연산
    /// </summary>
    private void CalulateLifeGauge()
    {
        // 현재 체력을 나타내는 펭귄 이미지의 현재위치를 받아옴
        Vector2 pos = currentGauge.anchoredPosition;
        // position -> transform.position (월드 포지션)
        // anchoredPosition -> UI 요소 위치 계산 시 Anchor 값의 영향을 받는 위치 값

        // 좌측을 0%, 우측을 100%로 보이도록 펭귄 이미지의 x 위치를 변경
        // -> 플레이어의 배고픔 값이 100을 넘어가면 안됨(배고픔 값의 범위 0~100)
        pos.x = lifeBar.sizeDelta.x * (player.Hungry / 100);

        // 변경한 위치를 적용
        currentGauge.anchoredPosition = pos;
    }

    /// <summary>
    /// 점수 텍스트를 업데이트
    /// </summary>
    private void ScoreUpdate()
    {
        score.text = GameManager.Instance.CurrentScore.ToString("현재 점수\n0.00");
    }

    /// <summary>
    /// 게임 오버인지 검사하여 게임 오버 시, 게임오버 UI를 활성화
    /// </summary>
    private void CheckGameOver()
    {
        // 플레이어가 죽지 않았다면
        if (!player.IsDead)
        {
            // 현재 시간을 저장
            lastGamePlayTime = Time.time;
            return;
        }

        // 플레이어가 죽었다면 

        // 게임이 진행 중이라면
        var gameManager = GameManager.Instance;
        if (gameManager.isStarted)
        {
            // 게임 오버 UI를 활성화
            gameOver.SetActive(true);
            gameManager.isStarted = false;

            // 최고점수를 갱신했는지 체크
            gameManager.TryUpdateBestScore();
            
        }

        // 게임 오버가 된 후 3초가 지났는지 체크
        if (Time.time > lastGamePlayTime + 3f)
            gameManager.LoadScene("Title");
    }
}
