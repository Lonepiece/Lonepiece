using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 생성할 오브젝트들의 프리팹 참조
    public DropObject[] fishPrefabs;
    public DropObject[] trashPrefabs;

    // 물고기가 떨어질 확률
    // 확률 범위 10 ~ 90
    [Range(10f, 90f)]
    public float fishDropPer;

    // 드롭 오브젝트 생성 딜레이
    // 최소 딜레이 (최소 딜레이에 가까울수록 더 느린 주기로 생성)
    private float minDelay = 1f;

    // 최대 딜레이 (최대 딜레이에 가까울수록 더 빠른 주기로 생성)
    private float maxDelay = .08f;

    // 현재 딜레이
    private float currentDelay;
    
    // 마지막으로 객체를 생성했던 시간을 저장할 필드
    private float dropCheckTime;

    // 플레이어 참조
    // -> 드롭 오브젝트 생성 후 드롭오브젝트에 플레이어 참조를 넘겨주기 위해서
    // -> 드롭 오브젝트 생성 시 생성되는 x축 범위를 지정하기 위해 (x축 범위 플레이어 이동영역)
    public Player player;

    private void Awake()
    {
        // 현재 생성 딜레이 설정
        currentDelay = minDelay;

        // 게임 시작 후 3초 뒤 오브젝트를 생성하기 위해
        dropCheckTime = Time.time + 3f;
    }

    void Update()
    {
        GenerateDropObject();
        ChangeDelayFaster();
        
    }

    /// <summary>
    ///  생성 주기를 점점 더 빠르게 변경하는 기능
    /// </summary>
    private void ChangeDelayFaster()
    {
        // 현재 딜레이가 최대 딜레이보다 값이 작아진다면 최대 딜레이로 값을 고정하고,
        // 아니라면 현재 딜레이를 조금씩 감소시킵니다.

        currentDelay = currentDelay < maxDelay ?
            maxDelay : currentDelay - (Time.deltaTime * 0.05f);
    }

    /// <summary>
    /// 드롭 오브젝트 생성 기능
    /// </summary>
    private void GenerateDropObject()
    {
        // 플레이어가 사망했다면 더이상 생성하지 않게
        if (player.IsDead)
            return;

        // 설정된 현재 딜레이마다 오브젝트를 생성하도록
        // Time.time : 게임을 시작하고 얼만큼 시간이 흘렀는지
        if (Time.time - dropCheckTime >= currentDelay)
        {
            // 드롭 오브젝트를 생성하였으므로, 현재 시간으로 갱신
            dropCheckTime = Time.time;

            //확률에 따라 물고기 또는 쓰레기 생성
            RandomDrop(Random.Range(1f, 100f) <= fishDropPer ?
                DropObjectType.Fish : DropObjectType.Trash);
        }

    }

    /// <summary>
    /// 랜덤한 위치에 드롭오브젝트를 생성
    /// </summary>
    /// <param name="type">생성할 드롭 오브젝트 타입</param>
    private void RandomDrop(DropObjectType type)
    {
        // 생성할 드롭오브젝트의 참조를 담을 변수
        DropObject dropObject = null;

        switch (type)
        {
            case DropObjectType.Fish:
                // Instantiate : 유니티 게임오브젝트 생성 기능
                dropObject = Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Length)]);
                break;

            case DropObjectType.Trash:
                dropObject = Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)]);
                break;
        }

        // 생성된 드롭 오브젝트에 플레이어 참조를 넘겨줌
        dropObject.player = player;

        // 떨어뜨릴 때 초기위치를 설정
        // 처음에 스포너의 위치를 넣음 (y값 위치는 스포너를 기준으로 고정된 y 값을 사용)
        Vector3 dropPos = transform.position;
        // x값만 랜덤하게 변경
        dropPos.x = Random.Range(player.maxLeftPos.position.x, player.maxRightPos.position.x);

        // 설정한 위치를 드롭오브젝트에 적용
        dropObject.transform.position = dropPos;
    }



}
