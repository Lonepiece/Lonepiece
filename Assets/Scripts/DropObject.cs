using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropObjectType { Fish, Trash }

public class DropObject : MonoBehaviour
{

    // 오브젝트 타입을 나타내는 필드
    public DropObjectType type;

    // 오브젝트가 떨어지는 속력
    public float fallSpeed;

    // 떨어지며 회전할 각도
    // -> 2차원에서 회전은 z축만 사용함
    // x축 회전 -> pitch, y축 회전 -> Yaw, z축 회전 -> Roll
    private float rotRollSpeed = 90f;

    // 캐릭터와 겹쳤을 때, 배고픔 게이지(체력)에 더해줄 값
    // -> 쓰레기라면 감소(음수), 물고기라면 증가(양수)
    public float changeHungryValue;

    // 플레이어 배고픔 게이지 변경을 위해 객체 참조를 받음
    public Player player;

    void Update()
    {
        FallDown();
        Rotate();
    }

    // 렌더러를 가진 객체만 사용 가능
    private void OnBecameInvisible()
    {
        // 객체가 화면 밖을 벗어났을 때, 1번 호출
        Destroy(gameObject);
    }

    /// <summary>
    /// 오브젝트의 떨어짐 이동을 구현
    /// </summary>
    private void FallDown()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);

    }

    /// <summary>
    /// 오브젝트의 회전을 구현
    /// </summary>
    private void Rotate()
    {
        // z축을 기준으로 회전
        transform.eulerAngles += Vector3.forward * rotRollSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 특정 오브젝트와 겹쳤을 경우 호출되는 콜백
    /// </summary>
    /// <param name="col">충돌 / 겹친 콜라이더의 정보</param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Collider 컴포넌트를 갖는 객체는 유니티에서 제공하는 콜백을 사용할 수 있다.
        // 충돌 감지 -> OnCollison ~
        // 겹침 감지 -> OnTrigger ~

        // Enter -> 충돌/겹치는 순간 1번 호출
        // Stay -> 충돌/겹침이 발생하고 있다면 계속해서 호출
        // Exit -> 충돌/겹침 상태에서 벗어나는 순간 1번 호출

        // 콜백이 작동하려면 피충돌체 중에 하나는 무조건 강체(Rigidbody)를 가지고 있어야합니다.

        // Player와 겹쳤는지 확인
        if (col.CompareTag("Player"))
        {
            var isFish = type == DropObjectType.Fish;

            // 배고픔 게이지를 변경
            player.ChangeHungryGauge(isFish ? changeHungryValue : -changeHungryValue);

            // 점수를 변경
            GameManager.Instance.AddScore(isFish ? 1f : -1f);

            // 드롭 오브젝트를 제거
            Destroy(gameObject);
        }
    }
}
