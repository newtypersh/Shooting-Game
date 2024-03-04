using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target; //���� ���ӿ����� �ΰ��� ����� ���ΰ� ������ Ÿ��
    [SerializeField]
    private float scrollRange = 9.9f;
    [SerializeField]
    private float moveSpeed = 3.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.down; //�̵������� �Ʒ���
    
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //��ġ�� -9.9f ���� �۾��� ��
        if(transform.position.y <= -scrollRange) 
        {
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
