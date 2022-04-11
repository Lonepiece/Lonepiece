using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ������ ������Ʈ���� ������ ����
    public DropObject[] fishPrefabs;
    public DropObject[] trashPrefabs;

    // ����Ⱑ ������ Ȯ��
    // Ȯ�� ���� 10 ~ 90
    [Range(10f, 90f)]
    public float fishDropPer;

    // ��� ������Ʈ ���� ������
    // �ּ� ������ (�ּ� �����̿� �������� �� ���� �ֱ�� ����)
    private float minDelay = 1f;

    // �ִ� ������ (�ִ� �����̿� �������� �� ���� �ֱ�� ����)
    private float maxDelay = .08f;

    // ���� ������
    private float currentDelay;
    
    // ���������� ��ü�� �����ߴ� �ð��� ������ �ʵ�
    private float dropCheckTime;

    // �÷��̾� ����
    // -> ��� ������Ʈ ���� �� ��ӿ�����Ʈ�� �÷��̾� ������ �Ѱ��ֱ� ���ؼ�
    // -> ��� ������Ʈ ���� �� �����Ǵ� x�� ������ �����ϱ� ���� (x�� ���� �÷��̾� �̵�����)
    public Player player;

    private void Awake()
    {
        // ���� ���� ������ ����
        currentDelay = minDelay;

        // ���� ���� �� 3�� �� ������Ʈ�� �����ϱ� ����
        dropCheckTime = Time.time + 3f;
    }

    void Update()
    {
        GenerateDropObject();
        ChangeDelayFaster();
        
    }

    /// <summary>
    ///  ���� �ֱ⸦ ���� �� ������ �����ϴ� ���
    /// </summary>
    private void ChangeDelayFaster()
    {
        // ���� �����̰� �ִ� �����̺��� ���� �۾����ٸ� �ִ� �����̷� ���� �����ϰ�,
        // �ƴ϶�� ���� �����̸� ���ݾ� ���ҽ�ŵ�ϴ�.

        currentDelay = currentDelay < maxDelay ?
            maxDelay : currentDelay - (Time.deltaTime * 0.05f);
    }

    /// <summary>
    /// ��� ������Ʈ ���� ���
    /// </summary>
    private void GenerateDropObject()
    {
        // �÷��̾ ����ߴٸ� ���̻� �������� �ʰ�
        if (player.IsDead)
            return;

        // ������ ���� �����̸��� ������Ʈ�� �����ϵ���
        // Time.time : ������ �����ϰ� ��ŭ �ð��� �귶����
        if (Time.time - dropCheckTime >= currentDelay)
        {
            // ��� ������Ʈ�� �����Ͽ����Ƿ�, ���� �ð����� ����
            dropCheckTime = Time.time;

            //Ȯ���� ���� ����� �Ǵ� ������ ����
            RandomDrop(Random.Range(1f, 100f) <= fishDropPer ?
                DropObjectType.Fish : DropObjectType.Trash);
        }

    }

    /// <summary>
    /// ������ ��ġ�� ��ӿ�����Ʈ�� ����
    /// </summary>
    /// <param name="type">������ ��� ������Ʈ Ÿ��</param>
    private void RandomDrop(DropObjectType type)
    {
        // ������ ��ӿ�����Ʈ�� ������ ���� ����
        DropObject dropObject = null;

        switch (type)
        {
            case DropObjectType.Fish:
                // Instantiate : ����Ƽ ���ӿ�����Ʈ ���� ���
                dropObject = Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Length)]);
                break;

            case DropObjectType.Trash:
                dropObject = Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)]);
                break;
        }

        // ������ ��� ������Ʈ�� �÷��̾� ������ �Ѱ���
        dropObject.player = player;

        // ����߸� �� �ʱ���ġ�� ����
        // ó���� �������� ��ġ�� ���� (y�� ��ġ�� �����ʸ� �������� ������ y ���� ���)
        Vector3 dropPos = transform.position;
        // x���� �����ϰ� ����
        dropPos.x = Random.Range(player.maxLeftPos.position.x, player.maxRightPos.position.x);

        // ������ ��ġ�� ��ӿ�����Ʈ�� ����
        dropObject.transform.position = dropPos;
    }



}
