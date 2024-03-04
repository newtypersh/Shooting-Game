using UnityEngine;

public enum BGMType { stage = 0, Boss}

public class BGMController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips;   // ������� ���
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeBGM(BGMType index)
    {
        // ���� ��� ���� ������� ����
        audioSource.Stop();

        // TIp. �ٸ� Ŭ�������� BGM�� ������ �� ������ ����ϸ� � BGM�� ������� Ȯ���Ϸ���
        // inspector view�� bgmClips[]�� Ȯ���ؾ� �� �� �ֱ� ������ �������� �̿��� �������� �����ش�.

        // ������� ���� ��Ͽ��� index���� ����������� ���� ��ü
        audioSource.clip = bgmClips[(int)index];
        // �ٲ� ������� ���
        audioSource.Play();
    }

}
