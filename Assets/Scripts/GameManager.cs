using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 전체의 데이터를 관리할 클래스
// -> 씬이 변경되어도 유지되어야하는 데이터
public class GameManager : Singleton<GameManager>
{
    // 게임 시작 상태를 나타냅니다.
    public bool isStarted;

    // 최고점수에 대한 파일을 저장하기 위한 경로를 나타낼 필드
    private string savePath;

    // 최고 점수를 나타냅니다.
    public double BestScore { get; private set; }

    // 현재 점수를 나타냅니다.
    public double CurrentScore { get; set; }

    protected override void Awake()
    {
        base.Awake();

        // 데이터 경로 작성 시에는 항상 절대경로가 아닌 상대경로로 작성 !!!
        savePath = $"{Application.dataPath}/Resources/Json/";
        
        
    }

    private void FixedUpdate()
    {
        ScoreUpdate();
    }


    /// <summary>
    /// 현재 점수를 업데이트 하는 기능
    /// -> 시간이 지남에 따라 점수 증가
    /// </summary>
    private void ScoreUpdate()
    {
        if (isStarted)
            CurrentScore += 0.1f;

    }

    /// <summary>
    /// 점수를 증가시키는 기능
    /// </summary>
    /// <param name="score">증가시킬 점수</param>
    public void AddScore(float score)
    {
        CurrentScore += score;

        // 점수가 0 미만이 될 수 없도록
        CurrentScore = CurrentScore < 0 ? 0 : CurrentScore;
    }

    /// <summary>
    /// 최고점수를 저장한 파일을 읽어옵니다.
    /// </summary>
    /// <returns>읽어온 최고점수 데이터</returns>
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

        // 정상적으로 파일을 읽었다면, json 문자열을 BestScore로 변환하여 반환
        return JsonUtility.FromJson<BestScore>(json);
    }

    /// <summary>
    /// 최고 점수 갱신 시도
    /// </summary>
    public void TryUpdateBestScore()
    {
        bool isNewRecord = BestScore > CurrentScore ? false : true;

        if (isNewRecord)
        {
            BestScore = CurrentScore;
            // 새로 달성한 최고점수를 저장
            // json 포맷으로 최고점수를 저장
            SaveBestScore();
        }
    }

    /// <summary>
    /// 최고점수를 json 파일로 저장
    /// </summary>
    public void SaveBestScore()
    {
        // 예외처리
        // 저장 경로에 폴더가 존재하는지 
        if (!Directory.Exists(savePath))
        {
            // 해당 경로에 폴더가 없다면 생성
            Directory.CreateDirectory(savePath);
        }

        // 저장하고자하는 데이터들을 미리 정의한 데이터셋으로 생성
        BestScore bestScore = new BestScore(DateTime.Now, BestScore);

        // JsonUtility.ToJson() 을 통해 데이터셋을 json 형태의 string으로 변환
        string json = JsonUtility.ToJson(bestScore);

        // json 문자열 데이터를 파일로 쓰기
        File.WriteAllText($"{savePath}BestScore.json", json);
    }

    /// <summary>
    /// 씬을 변경하는 기능
    /// </summary>
    /// <param name="sceneName"> 변경하고자 하는 씬의 이름</param>
    public void LoadScene(string sceneName)
    {
        // SceneManager : 씬과 관련된 정적 메서드를 제공하는 클래스
        // LoadScene(sceneName) : sceneName과 일치하는 씬으로 전환
        // 전환하려는 씬은 항상 "Scenes In Build" 목록에 등록되어 있어야합니다.
        SceneManager.LoadScene(sceneName);
    }
}
