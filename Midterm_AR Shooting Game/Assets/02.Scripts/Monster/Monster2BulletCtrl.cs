using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 몬스터2 총알 제어 스크립트
public class Monster2BulletCtrl : MonoBehaviour
{
    public GameObject bulletObj; // 총알 프리팹을 넣기 위해 생성

    private GameObject player; // 플레이어를 가져오기 위한 변수. 가져온 플레이어를 넣기 위해 생성

    private GameManager gameManager; // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 필요하여 추가

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 총알이 플레이어를 향해 발사되어야하기 때문에 플레이어 오브젝트를 얻어온다.

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // GameManager오브젝트에서 GameManager 스크립트를 가져온다.

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

        // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 추가
        if (gameManager.finishState == 2) // 게임 오버 상태이면
        {
            CancelInvoke(); // 모든 invoke 함수 호출을 취소한다.
        }
    }

    // 플레이어를 향해 총알 발사
    void Shot()
    {
        GameObject obj = Instantiate(bulletObj); // 총알 프리팹을 복사하여 총알 오브젝트를 생성하여 obj에 넣는다.
        Vector3 shotPos = transform.position + transform.up * 0.05f; // 발사 위치를 플레이어의 위치에서 0.05f정도 더 위쪽 방향으로 올린다.

        Vector3 bullDir = player.transform.position - transform.position; // 플레이어의 위치(가고자하는 목표 지점)에서 자신(몬스터2)의 위치를 빼 방향 벡터를 구한다.
        bullDir.y = 0; // 총알이 위로 올라가지는 않기 때문에 방향 벡터의 y값을 0으로 한다.

        obj.GetComponent<MonsterBulletMov>().SetPosDir(shotPos, bullDir); // 총알의 PlayerBulletMov 스크립트에서 SetPosDir을 호출한다. shotPos위치와 bullDir 방향으로 총알을 발사한다.
        Destroy(obj, 10); // 10초 뒤에 총알을 파괴한다.
    }
}
