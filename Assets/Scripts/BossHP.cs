using System.Collections;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 1000;
    private float currentHP;
    private SpriteRenderer spriteRenderer;
    private Boss boss;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    private void Awake()
    {
        currentHP = maxHP;      // ���� ü���� �ִ� ü�°� ���� ����
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(float damage)
    {
        // ���� ü���� damage��ŭ ����
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        // ü���� 0���� = �÷��̾� ĳ���� ���
        if( currentHP <= 0)
        {
            // ü���� 0�̸� OnDie() �Լ��� ȣ���ؼ� �׾��� �� ó���� �Ѵ�
            boss.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        // �÷��̾��� ������ ����������
        spriteRenderer.color = Color.red;
        // 0.05�� ���� ���
        yield return new WaitForSeconds(0.05f);
        // �÷��̾��� ������ ���� ������ �Ͼ������
        // (���� ������ �Ͼ���� �ƴ� ��� ���� ���� ���� ����)
        spriteRenderer.color = Color.white;
    }
   
}
