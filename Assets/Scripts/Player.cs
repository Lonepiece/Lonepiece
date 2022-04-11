using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾��� ��� ���¸� ��Ÿ���ϴ�.
    public bool IsDead { get; private set; }

    // �̵��� �� ����� �ӷ�
    public float moveSpeed = 8f;

    // ����� ���¸� ��Ÿ���ϴ� (ü��)
    public float Hungry { get; private set; } = 50f;

    // �̵��� ������ ��Ÿ�� ���� (���⺤��)
    private Vector2 direction;

    // �̵������� ��Ÿ���� Ʈ������ ��ü�� ������
    public Transform maxLeftPos, maxRightPos;

    void Update()
    {
        // �׾��ٸ� �÷��̾� ������ ������� �ʰ�
        if (IsDead)
            return;

        // Ű �Է�
        InputKey();
        
        //�̵�
        Move();
        
        // ����� ������ ������ ����
        ChangeHungryGauge(-Time.deltaTime * 2f);
    }

    /// <summary>
    /// Ű �Է� ���
    /// </summary>
    private void InputKey()
    {
        // ����Ƽ������ Ű �Է�
        // bool Input.GetKey(KeyCode key)
        // -> ������ Ű�� �������� ��� True�� ����

        // bool Input.GetKeyDown(KeyCode key)
        // -> ������ Ű�� ���� �� �� �� True�� ����

        // bool Input.GetKeyUp(KeyCode key)
        // -> ������ Ű�� ���ȴ� �������� �� �� �� True�� ����

        // float Input.GetAxis(string axisName)
        // -> -1f ~ 1f ������ ���� ����

        // float Input.GetAxisRaw(string axisName)
        // -> -1f, 0, 1f �� �� �ϳ��� ����

        direction.x = Input.GetAxisRaw("Horizontal");
    }

    /// <summary>
    /// �̵� ���
    /// </summary>
    private void Move()
    {
        // ����Ƽ���� ������Ʈ�� ��ġ�� �����ϴ� ���
        // 1. ������Ʈ ��ǥ�� Ư���� ��ǥ�� ����
        //      -> transform.position = Vector3(��ġ)

        // 2. Ư���� �������� �̵�
        //      -> transform.position += Vector3(���� * �ӷ�)
        //      -> transform.Translate(���� * �ӷ�)

        // 3. ���������� �̿��Ͽ� ����� ���� �־� �̵���ŵ�ϴ�.
        //      -> Rigidbody.AddForce()

        // transform : �ش� ��ũ��Ʈ�� ������Ʈ�� ���� ��ü�� Ʈ������
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        // transform.Translate (translation, relativeTo)
        //  -> relativeTo �� �������� translation ��ŭ �̵���ŵ�ϴ�.
        // translation : �̵���ų �Ÿ�
        // relativeTo : Space.Self  -> ������ �ڱ� �ڽ����� ����
        //              Space.World -> ������ ����� ����

        // ex)
        // ĳ���� �̵��ӵ� 10
        // - ���� PC
        //      1�� PC (60 FPS) -> 10 * 60 = �� 600
        //      2�� PC (30 FPS) -> 10 * 30 = �� 300

        // Time.deltaTime
        // ���� �����ӿ��� ���� �����ӱ��� �ɸ� �ð� ���ݿ� ���� �б� ���� ������Ƽ
        // PC ����� �ٸ� ������ �����Ű���� ���� ����� ���� �ٸ��� �ʵ��� ����

        // 60 FPS -> Time.deltaTime = (1/60) = 0.0166
        // 30 FPS -> Time.deltaTime = (1/30) = 0.0333

        // ĳ������ ��ġ�� ������ �� ���� �Ѿ�� �ʵ���
        // ������Ƽ�� ����� ���������� ������ �� �����Ƿ� �ӽú����� �̿�
        Vector3 currentPos = transform.position;

        // Mathf.Clamp(value, min, max) : value ���� min�� max ������ ������ �����Ͽ� ��ȯ�մϴ�.
        currentPos.x = Mathf.Clamp(currentPos.x, maxLeftPos.position.x, maxRightPos.position.x);
        transform.position = currentPos;
    }

    /// <summary>
    /// ����� �������� �����մϴ�.
    /// </summary>
    /// <param name="value">���� ���� ���ϰ��� �ϴ� ��</param>
    public void ChangeHungryGauge(float value)
    {
        // ����ߴٸ� ���� ���
        if (IsDead)
            return;

        Hungry = Mathf.Clamp(Hungry + value, 0, 100);

        // ��� üũ
        if (Hungry <= 0)
            IsDead = true;
    }
}
