using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private StageData stageData; 
    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;     // space 누를 때 공격
    [SerializeField]
    private KeyCode keyCodeBoom = KeyCode.Z;
    private bool isDie = false;
    private Movement2D movement2D;      // 플레이어 움직임 
    private Weapon weapon;      //무기
    private Animator animator;

    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 플레이어가 사망 애니메이션 재생 중일 때, 이동, 공격이 불가능하게 설정
        if (isDie == true) return;

        // 방향 키를 눌러 이동 방향 설정
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));

        // 공갹 키를 Down/Up으로 공격 시작/종료
        if(Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if(Input.GetKeyUp(keyCodeAttack)) 
        {
            weapon.StopFiring();
        }


        // 폭탄 키를 눌러 폭탄 생성
        if (Input.GetKeyDown(keyCodeBoom))
        {
            weapon.StartBoom();
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x), Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    public void onDie()
    {
        // 이동 방향 초기화
        movement2D.MoveTo(Vector3.zero);
        // 사망 애니메이션 재생
        animator.SetTrigger("onDie");
        // 적들과 충돌하지 않도록 충돌 박스 삭제
        Destroy(GetComponent<CircleCollider2D>());
        //  사망 시 키 플레이어 조작 등을 하지 못하게 하는 변수
        isDie = true;
    }

    public void onDieEvent() 
    {
        // 디바이스에 획득한 점수 score 저장
        PlayerPrefs.SetInt("Score", score);
        // 플레이어 사망 시 nextSceneName으로 이동
        SceneManager.LoadScene(nextSceneName);
    }

}
