using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���� ��ü�� �����͸� ������ Ŭ����
// -> ���� ����Ǿ �����Ǿ���ϴ� ������
public class GameManager : Singleton<GameManager>
{
    // ���� ���� ���¸� ��Ÿ���ϴ�.
    public bool isStarted;

    // �ְ������� ���� ������ �����ϱ� ���� ��θ� ��Ÿ�� �ʵ�
    private string savePath;

    // �ְ� ������ ��Ÿ���ϴ�.
    public double BestScore { get; private set; }

    // ���� ������ ��Ÿ���ϴ�.
    public double CurrentScore { get; set; }

    protected override void Awake()
    {
        base.Awake();

        // ������ ��� �ۼ� �ÿ��� �׻� �����ΰ� �ƴ� ����η� �ۼ� !!!
        savePath = $"{Application.dataPath}/Resources/Json/";
        
        
    }

    private void FixedUpdate()
    {
        ScoreUpdate();
    }


    /// <summary>
    /// ���� ������ ������Ʈ �ϴ� ���
    /// -> �ð��� ������ ���� ���� ����
    /// </summary>
    private void ScoreUpdate()
    {
        if (isStarted)
            CurrentScore += 0.1f;

    }

    /// <summary>
    /// ������ ������Ű�� ���
    /// </summary>
    /// <param name="score">������ų ����</param>
    public void AddScore(float score)
    {
        CurrentScore += score;

        // ������ 0 �̸��� �� �� ������
        CurrentScore = CurrentScore < 0 ? 0 : CurrentScore;
    }

    /// <summary>
    /// �ְ������� ������ ������ �о�ɴϴ�.
    /// </summary>
    /// <returns>�о�� �ְ����� ������</returns>
    public BestScore LoadBestScore()
    {
        string json = null;

        try
        {
            json = File.ReadAllText($"{savePath}BestScore.json");
        }
        catch (DirectoryNotFoundException)
        {
            return new BestScore(DateTime.Now, 0);
        }
        catch (FileNotFoundException)
        {
            return new BestScore(DateTime.Now, 0);
        }

        // ���������� ������ �о��ٸ�, json ���ڿ��� BestScore�� ��ȯ�Ͽ� ��ȯ
        return JsonUtility.FromJson<BestScore>(json);
    }

    /// <summary>
    /// �ְ� ���� ���� �õ�
    /// </summary>
    public void TryUpdateBestScore()
    {
        bool isNewRecord = BestScore > CurrentScore ? false : true;

        if (isNewRecord)
        {
            BestScore = CurrentScore;
            // ���� �޼��� �ְ������� ����
            // json �������� �ְ������� ����
            SaveBestScore();
        }
    }

    /// <summary>
    /// �ְ������� json ���Ϸ� ����
    /// </summary>
    public void SaveBestScore()
    {
        // ����ó��
        // ���� ��ο� ������ �����ϴ��� 
        if (!Directory.Exists(savePath))
        {
            // �ش� ��ο� ������ ���ٸ� ����
            Directory.CreateDirectory(savePath);
        }

        // �����ϰ����ϴ� �����͵��� �̸� ������ �����ͼ����� ����
        BestScore bestScore = new BestScore(DateTime.Now, BestScore);

        // JsonUtility.ToJson() �� ���� �����ͼ��� json ������ string���� ��ȯ
        string json = JsonUtility.ToJson(bestScore);

        // json ���ڿ� �����͸� ���Ϸ� ����
        File.WriteAllText($"{savePath}BestScore.json", json);
    }

    /// <summary>
    /// ���� �����ϴ� ���
    /// </summary>
    /// <param name="sceneName"> �����ϰ��� �ϴ� ���� �̸�</param>
    public void LoadScene(string sceneName)
    {
        // SceneManager : ���� ���õ� ���� �޼��带 �����ϴ� Ŭ����
        // LoadScene(sceneName) : sceneName�� ��ġ�ϴ� ������ ��ȯ
        // ��ȯ�Ϸ��� ���� �׻� "Scenes In Build" ��Ͽ� ��ϵǾ� �־���մϴ�.
        SceneManager.LoadScene(sceneName);
    }
}
