using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �߻�ü�� �ε��� ������Ʈ�� �±װ� "Enemy" �̸�
        if (collision.CompareTag("Enemy"))
        {
            // �ε��� ������Ʈ ���ó�� (��)
            collision.GetComponent<EnemyHP>().TakeDamage(damage);
            // �� ������Ʈ ���� (�߻�ü)
            Destroy(gameObject);
        }
        // �߻�ü�� �ε��� ������Ʈ�� �±װ� "Boss" �̸�
        else if (collision.CompareTag("Boss"))
        {
            // �ε��� ������Ʈ ü�� ���� (����)
            collision.GetComponent<BossHP>().TakeDamage(damage);
            // �� ������Ʈ ���� (�߻�ü)
            Destroy(gameObject);
        }
    }
}
