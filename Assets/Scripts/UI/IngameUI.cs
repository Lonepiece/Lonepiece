using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ΰ��� ���� ui ���� ������ Ŭ����
/// </summary>
public class IngameUI : MonoBehaviour
{
    // ���������� ������ ������ �ð��� ����
    // -> ���ӿ��� �� 3�� �Ŀ� �� ������ ���� 
    private float lastGamePlayTime;

    // �÷��̾��� ü���� ����ʿ� ���� ����� ��ġ�� �̵����Ѿ��ϹǷ� RT������ �޾ƿ�
    public RectTransform currentGauge;

    // ���� ü���� ��Ÿ���� ��� �̹����� ��ġ�� ����ϱ� ����, �θ� ��ü�� RT�� �޾ƿ�
    public RectTransform lifeBar;

    // ������ ǥ���� �ؽ�Ʈ ������Ʈ ���� 
    public Text score;

    // ���ӿ��� UI ��ü ����
    public GameObject gameOver;

    // UI���� �÷��̾��� ü���� �̿��Ͽ� ǥ���ؾ� �ϴ� �κ��� �����Ͽ� ����
    public Player player;

    void Update()
    {
        CalulateLifeGauge();
        ScoreUpdate();
        CheckGameOver();
    }

    /// <summary>
    /// ü�°����� ����
    /// </summary>
    private void CalulateLifeGauge()
    {
        // ���� ü���� ��Ÿ���� ��� �̹����� ������ġ�� �޾ƿ�
        Vector2 pos = currentGauge.anchoredPosition;
        // position -> transform.position (���� ������)
        // anchoredPosition -> UI ��� ��ġ ��� �� Anchor ���� ������ �޴� ��ġ ��

        // ������ 0%, ������ 100%�� ���̵��� ��� �̹����� x ��ġ�� ����
        // -> �÷��̾��� ����� ���� 100�� �Ѿ�� �ȵ�(����� ���� ���� 0~100)
        pos.x = lifeBar.sizeDelta.x * (player.Hungry / 100);

        // ������ ��ġ�� ����
        currentGauge.anchoredPosition = pos;
    }

    /// <summary>
    /// ���� �ؽ�Ʈ�� ������Ʈ
    /// </summary>
    private void ScoreUpdate()
    {
        score.text = GameManager.Instance.CurrentScore.ToString("���� ����\n0.00");
    }

    /// <summary>
    /// ���� �������� �˻��Ͽ� ���� ���� ��, ���ӿ��� UI�� Ȱ��ȭ
    /// </summary>
    private void CheckGameOver()
    {
        // �÷��̾ ���� �ʾҴٸ�
        if (!player.IsDead)
        {
            // ���� �ð��� ����
            lastGamePlayTime = Time.time;
            return;
        }

        // �÷��̾ �׾��ٸ� 

        // ������ ���� ���̶��
        var gameManager = GameManager.Instance;
        if (gameManager.isStarted)
        {
            // ���� ���� UI�� Ȱ��ȭ
            gameOver.SetActive(true);
            gameManager.isStarted = false;

            // �ְ������� �����ߴ��� üũ
            gameManager.TryUpdateBestScore();
            
        }

        // ���� ������ �� �� 3�ʰ� �������� üũ
        if (Time.time > lastGamePlayTime + 3f)
            gameManager.LoadScene("Title");
    }
}
