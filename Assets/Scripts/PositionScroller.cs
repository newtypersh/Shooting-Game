using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target; //현재 게임에서는 두개의 배경이 서로가 서로의 타켓
    [SerializeField]
    private float scrollRange = 9.9f;
    [SerializeField]
    private float moveSpeed = 3.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.down; //이동방향은 아래쪽
    
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //위치가 -9.9f 보다 작아질 때
        if(transform.position.y <= -scrollRange) 
        {
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
