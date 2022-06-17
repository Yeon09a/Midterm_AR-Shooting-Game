using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 몬스터1 총알 제어 스크립트
public class Monster1BulletCtrl : MonoBehaviour
{
    public GameObject bulletObj; // 총알 프리팹을 넣기 위해 생성


    void Start()
    {
    // 1초마다 총알을 발사한다.
        InvokeRepeating("Shot", 1, 1); // 1초 후에 shot함수를 매번 1초 간격으로 호출한다.
    }

    private void Update()
    {
        // 몬스터가 죽었을 때 공격 중지
        if (gameObject.tag == "MonsterDead") // 몬스터가 죽었을 때(몬스터의 태그가 MonsterDead 때)
        {
            CancelInvoke("Shot"); // Shot 함수 호출을 취소한다.
        }
    }

    // 총알을 랜덤 방향으로 발사한다.
    void Shot() 
    {
        GameObject obj = Instantiate(bulletObj); // 총알 프리팹을 복사하여 총알 오브젝트를 생성하여 obj에 넣는다.
        Vector3 shotPos = transform.position + transform.up * 0.05f; // 발사 위치를 플레이어의 위치에서 0.05f정도 더 위쪽 방향으로 올린다.
        float randDeg = Random.Range(0, 360); // 0 ~ 359까지의 숫자 중 하나를 뽑아 방향을 결정한다. 랜덤 방향으로 총알이 발사되도록 랜덤으로 방향을 결정한다.
        obj.transform.Rotate(0, randDeg, 0); // 랜덤 방향을 나타낸다. 위에서 랜덤으로 고른 숫자 만큼 y축으로 회전한다.

        obj.GetComponent<MonsterBulletMov>().SetPosDir(shotPos, obj.transform.forward); // 총알의 PlayerBulletMov 스크립트에서 SetPosDir을 호출한다. shotPos위치와 랜덤 회전 방향으로 총알을 발사한다.
        Destroy(obj, 5); // 5초 뒤에 총알을 파괴한다.
    }
}
