using System.Collections;
using UnityEngine;
using TMPro;

public class TMPColor : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.1f;
    private TextMeshProUGUI textBossWarning;

    private void Awake()
    {
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }

    private IEnumerator ColorLerpLoop()
    {
        while(true)
        {
            // ������ �Ͼ������ ����������
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            // ������ ���������� �Ͼ������
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    private IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1.0f)
        {
            // lerpTime �ð����� while() �ݺ��� ����
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            // Text - TextMeshPro�� ��Ʈ ������ startColor���� endColor�� ����
            textBossWarning.color = Color.Lerp(startColor, endColor, percent);

            yield return null;
        }
    }
}
