using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;    // �� ������ ���� �������� ũ�� ����
    [SerializeField]
    private GameObject enemyPrefab;    // �����ؼ� ������ �� ���ʹ� ������
    [SerializeField]
    private GameObject enemyHPSliderPrefab;    // �� ü���� ��Ÿ���� Slider UI prefab
    [SerializeField]
    private Transform canvasTransform;     // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    [SerializeField]
    private BGMController bgmController;    // ������� ���� (���� ���� �� ����)
    [SerializeField]
    private GameObject textBossWarning;     // ���� ���� �ؽ�Ʈ ������Ʈ
    [SerializeField]
    private GameObject panelBossHP; // ���� ü�� �г� ������Ʈ
    [SerializeField]
    private GameObject boss;    // ���� ������Ʈ
    [SerializeField]
    private float spawnTime;    // ���� �ֱ�
    [SerializeField]
    private int maxEnemyCount = 100;    // ���� ���������� �ִ� �� ���� ����

    private void Awake()
    {
        // ���� ���� �ؽ�Ʈ ��Ȱ��ȭ
        textBossWarning.SetActive(false);
        // ���� ü�� �г� ��Ȱ��ȭ
        panelBossHP.SetActive(false);
        // ���� ������Ʈ ��Ȱ��ȭ
        boss.SetActive(false);

        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy() 
    {
        int currentEnemyCount = 0;

        while (true) 
        {
            // x��ġ�� ���������� ũ�� ���� ������ ������ ���� ����
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            // �� ���� ��ġ
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            // �� ĳ���� ����
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            // �� ü���� ��Ÿ���� Slider UI ���� �� ����
            SpawnEnemyHPSlider(enemyClone);
            // �� ���� ���� ����
            currentEnemyCount++;
            // ���� �ִ� ���ڱ��� �����ϸ� �� ���� �ڷ�ƾ ����, ���� ���� �ڷ�ƾ ����
            if(currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            // spawnTime��ŭ ���
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // �� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ����
        // Tip. UI�� ĵ������ �ڽ� ������Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�
        sliderClone.transform.SetParent(canvasTransform);
        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        // Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss()
    {
        // ���� ���� BGM ����
        bgmController.ChangeBGM(BGMType.Boss);

        // ���� ���� �ؽ�Ʈ Ȱ��ȭ
        textBossWarning.SetActive(true);
        // 1�� ���
        yield return new WaitForSeconds(1.0f);
        
        // ���� ���� �ؽ�Ʈ ��Ȱ��ȭ
        textBossWarning.SetActive(false);
        // ���� ������Ʈ Ȱ��ȭ
        boss.SetActive(true);
        // ���� ü�� �г� Ȱ��ȭ
        panelBossHP.SetActive(true);
        // ������ ù ��° ������ ������ ��ġ�� �̵� ����
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
