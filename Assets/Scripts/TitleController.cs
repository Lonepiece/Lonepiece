using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ÿ��Ʋ ���� �����ϴ� ������ �� Ŭ����
public class TitleController : MonoBehaviour
{
    // ù��° �������� ������Ʈ �Ǳ� ���� �� �� ȣ��
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // ����Ƽ���� ��ü�� ������ �޴� ���� ������ ���
    // -> public �ʵ�� �ް����ϴ� ��ü Ÿ���� �ۼ�
    //    ���������ڸ� public���� ������ ��, �ν����ͻ󿡼� �ش� �ʵ尡 �����
    // public GameManager gameManager;
    // -> GameManager �̱��� ������� ���� �ּ�ó��


    // �� �����Ӹ��� ȣ��
    // Update is called once per frame
    void Update()
    {
        // ����Ű�� �������� Ȯ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var gameManager = GameManager.Instance;

            // ������ ���� ���·� ����
            gameManager.isStarted = true;

            // ���� ���� �� ���� �ʱ�ȭ
            gameManager.CurrentScore = 0;

            gameManager.LoadScene("Ingame");
        }
            
    }
}
