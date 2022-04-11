using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 타이틀 씬을 제어하는 역할을 할 클래스
public class TitleController : MonoBehaviour
{
    // 첫번째 프레임이 업데이트 되기 전에 한 번 호출
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // 유니티에서 객체의 참조를 받는 가장 간단한 방법
    // -> public 필드로 받고자하는 객체 타입을 작성
    //    접근제한자를 public으로 설정할 시, 인스펙터상에서 해당 필드가 노출됨
    // public GameManager gameManager;
    // -> GameManager 싱글톤 사용으로 인해 주석처리


    // 매 프레임마다 호출
    // Update is called once per frame
    void Update()
    {
        // 엔터키를 눌렀는지 확인
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var gameManager = GameManager.Instance;

            // 게임을 시작 상태로 변경
            gameManager.isStarted = true;

            // 게임 시작 전 점수 초기화
            gameManager.CurrentScore = 0;

            gameManager.LoadScene("Ingame");
        }
            
    }
}
