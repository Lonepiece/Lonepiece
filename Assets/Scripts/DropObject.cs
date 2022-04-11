using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropObjectType { Fish, Trash }

public class DropObject : MonoBehaviour
{

    // ������Ʈ Ÿ���� ��Ÿ���� �ʵ�
    public DropObjectType type;

    // ������Ʈ�� �������� �ӷ�
    public float fallSpeed;

    // �������� ȸ���� ����
    // -> 2�������� ȸ���� z�ุ �����
    // x�� ȸ�� -> pitch, y�� ȸ�� -> Yaw, z�� ȸ�� -> Roll
    private float rotRollSpeed = 90f;

    // ĳ���Ϳ� ������ ��, ����� ������(ü��)�� ������ ��
    // -> �������� ����(����), ������� ����(���)
    public float changeHungryValue;

    // �÷��̾� ����� ������ ������ ���� ��ü ������ ����
    public Player player;

    void Update()
    {
        FallDown();
        Rotate();
    }

    // �������� ���� ��ü�� ��� ����
    private void OnBecameInvisible()
    {
        // ��ü�� ȭ�� ���� ����� ��, 1�� ȣ��
        Destroy(gameObject);
    }

    /// <summary>
    /// ������Ʈ�� ������ �̵��� ����
    /// </summary>
    private void FallDown()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);

    }

    /// <summary>
    /// ������Ʈ�� ȸ���� ����
    /// </summary>
    private void Rotate()
    {
        // z���� �������� ȸ��
        transform.eulerAngles += Vector3.forward * rotRollSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Ư�� ������Ʈ�� ������ ��� ȣ��Ǵ� �ݹ�
    /// </summary>
    /// <param name="col">�浹 / ��ģ �ݶ��̴��� ����</param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Collider ������Ʈ�� ���� ��ü�� ����Ƽ���� �����ϴ� �ݹ��� ����� �� �ִ�.
        // �浹 ���� -> OnCollison ~
        // ��ħ ���� -> OnTrigger ~

        // Enter -> �浹/��ġ�� ���� 1�� ȣ��
        // Stay -> �浹/��ħ�� �߻��ϰ� �ִٸ� ����ؼ� ȣ��
        // Exit -> �浹/��ħ ���¿��� ����� ���� 1�� ȣ��

        // �ݹ��� �۵��Ϸ��� ���浹ü �߿� �ϳ��� ������ ��ü(Rigidbody)�� ������ �־���մϴ�.

        // Player�� ���ƴ��� Ȯ��
        if (col.CompareTag("Player"))
        {
            var isFish = type == DropObjectType.Fish;

            // ����� �������� ����
            player.ChangeHungryGauge(isFish ? changeHungryValue : -changeHungryValue);

            // ������ ����
            GameManager.Instance.AddScore(isFish ? 1f : -1f);

            // ��� ������Ʈ�� ����
            Destroy(gameObject);
        }
    }
}
