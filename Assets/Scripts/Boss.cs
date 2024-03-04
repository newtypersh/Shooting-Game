using System.Collections;
using UnityEngine;

public enum BossState { MoveToAppearPoint=0, Phase01, Phase02, Phase03 }

public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField] 
    private PlayerController playerController;
    [SerializeField] 
    private string nextSceneName;   // ���� �� �̸� (���� �������� or ���� Ŭ����)
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossHP bossHP;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
    }

    public void ChangeState(BossState newState)
    {
        // Tip. ������ ���� .ToString()�� �ϰ� �Ǹ� �������� ���ǵ�
        // ���� �̸��� string���� �޾ƿ��� �ȴ�.
        // ex) bossState�� ���� BossState,MoveToAppearPoint�̸� "MoveToAppearPoint"

        // �̸� �̿��� �������� �̸��� �ڷ�ƾ �̸��� ��ġ����
        // ������ ������ ���� �ڷ�ƾ �Լ� ����� ������ �� �ִ�
        StopCoroutine(bossState.ToString());
        // ���� ����
        bossState = newState;
        // ���ο� ���� ���
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        // �̵����� ���� (�ڷ�ƾ ���� �� 1ȸ ȣ��)
        movement2D.MoveTo(Vector3.down);

        while (true)
        {
            if( transform.position.y <= bossAppearPoint) 
            { 
                // �̵������� (0, 0, 0)���� ������ ���ߵ��� �Ѵ�
                movement2D.MoveTo(Vector3.zero);
                // Phase01 ���·� ����
                ChangeState(BossState.Phase01);
            }
            yield return null;
        }
    }


    
    private IEnumerator Phase01()
    {
        // �� ������ ��� ���� ����
        bossWeapon.StartFiring(AttackType.CircleFire);
        
        // �ش� ����(�ڷ�ƾ)�� ���۵� �� 1ȸ ȣ��
        while (true)
        {
            // ������ ü���� 70% ���ϰ� �Ǹ�
            if( bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)
            {
                // �� ��� ������ ���� ����
                bossWeapon.StopFiring(AttackType.CircleFire);
                // Phase02�� ����
                ChangeState (BossState.Phase02);
            }

            yield return null;
        }
        
    }

    private IEnumerator Phase02()
    {
        // �÷��̾� ��ġ�� �������� ���� �ݻ�ü ���� ����
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        // ó�� �̵� ������ ���������� ����
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            // �¿� �̵� �� ���� ���� �����ϰ� �Ǹ� ������ �ݴ�� ����
            if(transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }

            // ������ ���� ü���� 30% ���ϰ� �Ǹ�
            if( bossHP.CurrentHP < bossHP.MaxHP * 0.3f)
            {
                // �÷��̾� ��ġ�� �������� ���� �߻�ü ���� ����
                bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);
                // Phase03���� ����
                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        // �� ��� ������ ���� ����
        bossWeapon.StartFiring(AttackType.CircleFire);
        // �÷��̾� ��ġ�� �������� ���� �߻�ü ���� ����
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        // ó�� �̵� ������ ���������� ����
        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            // �¿� �̵� �� ���� ���� �����ϰ� �Ǹ� ������ �ݴ�� ����
            if (transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }


            yield return null;
        }
    }
    
    public void OnDie()
    {
        // ���� �ı� ��ƼŬ ����
        GameObject clone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // ��ƼŬ ��� �Ϸ� �� �� ��ȯ�� ���� ����
        clone.GetComponent<BossExplosion>().SetUp(playerController, nextSceneName);
        // ���� ������Ʈ ����
        Destroy(gameObject);
    }
}
