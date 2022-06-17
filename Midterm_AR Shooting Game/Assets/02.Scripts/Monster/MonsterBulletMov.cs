using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 몬스터 총알 움직임 스크립트
public class MonsterBulletMov : MonoBehaviour
{
    private float speed = 0.2f; // 총알의 속도

    private Vector3 direction; // 총알 발사 방향

    void Update()
    {
        Vector3 deltaPos = direction * speed * Time.deltaTime; // 한 프레임 당 이동할 수 있는 거리를 구한다. 모든 기기에서 동일한 속도로 이동하도록 Time.deltaTime을 곱한다.
        transform.Translate(deltaPos); // deltaPos만큼 이동한다.
    }

    // 총알의 방향과 위치를 설정하는 함수
    public void SetPosDir(Vector3 pos, Vector3 dir) // 매개변수로 총알의 위치와 발사 방향을 넣어준다.
    {
        transform.position = pos; // 총알의 위치를 매개변수로 받은 pos로 설정한다. 총알이 처음 만들어진 위치
        direction = dir; // 매개변수로 받은 dir을 direction에 저장해놓는다.
    }
}
