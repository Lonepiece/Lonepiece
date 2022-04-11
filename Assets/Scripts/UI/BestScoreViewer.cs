using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ְ������� �а� ����� Ŭ����
/// </summary>
public class BestScoreViewer : MonoBehaviour
{
    public Text lastRecord;
    public Text bestScore;

    void Start()
    {
        var bestScoreData = GameManager.Instance.LoadBestScore();

        string lastRecordStr = string.Empty;
        string bestScoreStr  = string.Empty;

        if (bestScoreData.score == 0)
        {
            lastRecordStr = "- -";
            bestScoreStr  = "��� ����";
        }
        else
        {
            lastRecordStr = 
                $"{bestScoreData.year} {bestScoreData.month}" + 
                $".{bestScoreData.day} {bestScoreData.hour}:{bestScoreData.minute}" +
                $":{bestScoreData.second}";
            bestScoreStr = bestScoreData.score.ToString("0.00");
        }

        lastRecord.text = lastRecordStr;
        bestScore.text  = bestScoreStr;
    }
}
