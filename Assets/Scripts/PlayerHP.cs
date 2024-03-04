using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 10;    // �ִ� ���
    private float currentHP;    // ���� ü��
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    public float MaxHP => maxHP;    // maxHP ������ ������ �� �ִ� ������Ƽ (Get�� ����)
    public float CurrentHP  // currentHP ������ ������ �� �ִ� ������Ƽ (Set, Get ����)
    { 
        set=> currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }
        

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();   
    }
    
    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0) 
        {
            // ü���� 0�̸� onDIe() �Լ��� ȣ���ؼ� �׾��� �� ó���� �Ѵ�
            playerController.onDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        // �÷��̾��� ������ ����������
        spriteRenderer.color = Color.red;
        // 0.1�� ���� ���
        yield return new WaitForSeconds(0.1f);
        // �÷��̾��� ������ ���� ������ �Ͼ������
        // (���� ������ �Ͼ���� �ƴ� ��� ���� ���� ���� ����)
        spriteRenderer.color = Color.white;

    }
}
