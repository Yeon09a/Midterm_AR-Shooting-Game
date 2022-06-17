using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 총알 제어 스크립트
public class PlayerBulletCtrl : MonoBehaviour
{
    public GameObject bulletObj; // 총알 프리팹을 넣기 위해 생성

    private int itemCase; // 아이템 획득 횟수에 따른 총알 발사 추가를 위한 변수로, 아이템을 획득한 횟수를 나타낸다.
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 우측 버튼 클릭시
        {
            BulletCheck(); // 아이템 횟수에 따른 총알 발사 함수 호출
        }
    }

    // 총알 발사 함수
    void Shot(Vector3 dir) // 매개변수로 총알 발사 방향을 받는다. 
    {
        GameObject obj = Instantiate(bulletObj); // 총알 프리팹을 복사하여 총알 오브젝트를 생성하여 obj에 넣는다.
        Vector3 shotPos = transform.position + transform.up * 0.05f; // 발사 위치를 플레이어의 위치에서 0.05f정도 더 위쪽 방향으로 올린다.

        obj.GetComponent<PlayerBulletMov>().SetPosDir(shotPos, dir); // 총알의 PlayerBulletMov 스크립트에서 SetPosDir을 호출한다. shotPos위치와 dir 방향으로 총알을 발사한다.
        Destroy(obj, 10); // 10초 뒤에 총알을 파괴한다.
    }

    // 아이템 횟수에 따른 총알 발사 함수
    void BulletCheck()
    {
        itemCase = GetComponent<PlayerCtrl>().itemCount;

        if (itemCase == 0) // 아이템 획득 횟수가 0인 경우(처음 시작시)
        {
            Shot(transform.forward); // 전방으로 총알 발사
        }
        else if (itemCase == 1) // 아이템 획득 횟수가 1인 경우(아이템을 1회 획득했을 시)
        {
            Shot(transform.forward); // 전방으로 총알 발사
            Shot(-transform.forward); // 후방으로 총알 발사

        }
        else if (itemCase == 2) // 아이템 획득 횟수가 2인 경우(아이템을 2회 획득했을 시)
        {
            Shot(transform.forward); // 전방으로 총알 발사
            Shot(-transform.forward); // 후방으로 총알 발사
            Shot(transform.right); // 우측으로 총알 발사
            Shot(-transform.right); // 좌측으로 총알 발사

        }
        else if (itemCase == 3) // 아이템 획득 횟수가 3인 경우(아이템을 3회 획득했을 시)
        {
            Shot(transform.forward); // 전방으로 총알 발사
            Shot(-transform.forward); // 후방으로 총알 발사
            Shot(transform.right); // 우측으로 총알 발사
            Shot(-transform.right); // 좌측으로 총알 발사
            Shot(transform.forward - transform.right); // 좌측 전방으로 발사
            Shot(transform.forward + transform.right); // 우측 전방으로 발사
            Shot(-transform.forward - transform.right); // 좌측 후방으로 발사
            Shot(-transform.forward + transform.right); // 우측 후방으로 발사
        }
    }
}
