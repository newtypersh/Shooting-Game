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
        // Text UI에 현재 점수 정보를 업데이트
        textScore.text = "Score " + playerController.Score;
    }
}
