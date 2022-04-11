using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어의 사망 상태를 나타냅니다.
    public bool IsDead { get; private set; }

    // 이동할 때 적용될 속력
    public float moveSpeed = 8f;

    // 배고픔 상태를 나타냅니다 (체력)
    public float Hungry { get; private set; } = 50f;

    // 이동할 방향을 나타낼 벡터 (방향벡터)
    private Vector2 direction;

    // 이동영역을 나타내는 트랜스폼 객체의 참조들
    public Transform maxLeftPos, maxRightPos;

    void Update()
    {
        // 죽었다면 플레이어 로직이 실행되지 않게
        if (IsDead)
            return;

        // 키 입력
        InputKey();
        
        //이동
        Move();
        
        // 배고픔 게이지 서서히 감소
        ChangeHungryGauge(-Time.deltaTime * 2f);
    }

    /// <summary>
    /// 키 입력 기능
    /// </summary>
    private void InputKey()
    {
        // 유니티에서의 키 입력
        // bool Input.GetKey(KeyCode key)
        // -> 지정된 키가 눌려있을 경우 True를 리턴

        // bool Input.GetKeyDown(KeyCode key)
        // -> 지정된 키가 눌릴 때 한 번 True를 리턴

        // bool Input.GetKeyUp(KeyCode key)
        // -> 지정된 키가 눌렸다 떼어졌을 때 한 번 True를 리턴

        // float Input.GetAxis(string axisName)
        // -> -1f ~ 1f 사이의 값을 리턴

        // float Input.GetAxisRaw(string axisName)
        // -> -1f, 0, 1f 셋 중 하나만 리턴

        direction.x = Input.GetAxisRaw("Horizontal");
    }

    /// <summary>
    /// 이동 기능
    /// </summary>
    private void Move()
    {
        // 유니티에서 오브젝트의 위치를 변경하는 방법
        // 1. 오브젝트 좌표를 특정한 좌표로 변경
        //      -> transform.position = Vector3(위치)

        // 2. 특정한 방향으로 이동
        //      -> transform.position += Vector3(방향 * 속력)
        //      -> transform.Translate(방향 * 속력)

        // 3. 물리엔진을 이용하여 방향과 힘을 주어 이동시킵니다.
        //      -> Rigidbody.AddForce()

        // transform : 해당 스크립트를 컴포넌트로 갖는 객체의 트랜스폼
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        // transform.Translate (translation, relativeTo)
        //  -> relativeTo 를 기준으로 translation 만큼 이동시킵니다.
        // translation : 이동시킬 거리
        // relativeTo : Space.Self  -> 기준을 자기 자신으로 설정
        //              Space.World -> 기준을 월드로 설정

        // ex)
        // 캐릭터 이동속도 10
        // - 실행 PC
        //      1번 PC (60 FPS) -> 10 * 60 = 약 600
        //      2번 PC (30 FPS) -> 10 * 30 = 약 300

        // Time.deltaTime
        // 이전 프레임에서 다음 프레임까지 걸린 시간 간격에 대한 읽기 전용 프로퍼티
        // PC 사양이 다른 곳에서 실행시키더라도 실행 결과가 서로 다르지 않도록 적용

        // 60 FPS -> Time.deltaTime = (1/60) = 0.0166
        // 30 FPS -> Time.deltaTime = (1/30) = 0.0333

        // 캐릭터의 위치가 빙하의 양 끝을 넘어가지 않도록
        // 프로퍼티의 멤버를 직접적으로 수정할 수 없으므로 임시변수를 이용
        Vector3 currentPos = transform.position;

        // Mathf.Clamp(value, min, max) : value 값을 min과 max 사이의 값으로 변경하여 반환합니다.
        currentPos.x = Mathf.Clamp(currentPos.x, maxLeftPos.position.x, maxRightPos.position.x);
        transform.position = currentPos;
    }

    /// <summary>
    /// 배고픔 게이지를 변경합니다.
    /// </summary>
    /// <param name="value">기존 값에 더하고자 하는 값</param>
    public void ChangeHungryGauge(float value)
    {
        // 사망했다면 변경 취소
        if (IsDead)
            return;

        Hungry = Mathf.Clamp(Hungry + value, 0, 100);

        // 사망 체크
        if (Hungry <= 0)
            IsDead = true;
    }
}
