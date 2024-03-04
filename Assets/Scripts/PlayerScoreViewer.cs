using UnityEngine;
using TMPro;

public class PlayerScoreViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    private TextMeshProUGUI textScore;
    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        // Text UI�� ���� ���� ������ ������Ʈ
        textScore.text = "Score " + playerController.Score;
    }
}
