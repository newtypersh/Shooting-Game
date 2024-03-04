using System.Collections;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip boomAudio;
    [SerializeField]
    private int damage = 100;   // ��ź ������
    private float boomDelay = 0.5f;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter() 
    {
        Vector3 startPosiition = transform.position;
        Vector3 endPosiition = Vector3.zero;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;

            // boomDelay�� ������ �ð����� startPosition���� endPosition���� �̵�
            // curve�� ������ �׷���ó�� ó���� ������ �̵��ϰ�, �������� �ٴٸ����� õõ�� �̵�
            transform.position = Vector3.Lerp(startPosiition, endPosiition, curve.Evaluate(percent));

            yield return null;
        }

        // �̵��� �Ϸ�� �� �ִϸ��̼� ����
        animator.SetTrigger("OnBoom");
        // ���� ����
        audioSource.clip = boomAudio;
        audioSource.Play();
    }
    public void OnBoom()
    {
        // ���� ���� ������ "Enemy" �±׸� ���� ��� ������Ʈ ������ �����´�
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        // ���� ���� ������ "Meteorite" �±׸� ���� ��� ������Ʈ ������ �����´�
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");

        // ��� �� �ı�
        for(int i=0; i<enemys.Length; ++i)
        {
            enemys[i].GetComponent<Enemy>().OnDie();
        }
        // ��� � �ı�
        for (int i = 0; i < meteorites.Length; ++i)
        {
            meteorites[i].GetComponent<Meteorite>().OnDie();
        }

        // ���� ���� ���� �����ϴ� ��, ������ �߻�ü�� ��� �ı�
        GameObject[] projectils = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        for(int i=0;i<projectils.Length; i++)
        {
            projectils[i].GetComponent<EnemyProjectile>().OnDie();
        }

        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if(boss != null)
        {
            // ������ ü���� damage��ŭ ���ҽ�Ų��
            boss.GetComponent<BossHP>().TakeDamage(damage);
        }

        // Boom ������Ʈ ����
        Destroy(gameObject);
    }
}
