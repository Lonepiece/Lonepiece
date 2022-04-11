using System;

// Serializable : 클래스나 구조체를 직렬화 시킵니다.
// 직렬화 : 컴퓨터 상의 특정 데이터를 어딘가로 전달하기 좋도록 가공하는 과정
// 어딘가로 -> 다른 프로세스, 다른 네트워크, 시스템 저장소 등등
[Serializable]
public class BestScore
{
    public int year    ;
    public int month   ;
    public int day     ;
    public int hour    ;
    public int minute  ;
    public int second  ;
    public double score;

    public BestScore(DateTime date, double score)
    {
        year   = date.Year  ;
        month  = date.Month ;
        day    = date.Day   ;
        hour   = date.Hour  ;
        minute = date.Minute;
        second = date.Second;

        this.score = score  ;

    }
}
