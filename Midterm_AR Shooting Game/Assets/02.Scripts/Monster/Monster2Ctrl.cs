using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터2 제어 스크립트
public class Monster2Ctrl : MonoBehaviour
{
    float speed = 0.03f; // 몬스터2이 이동하는 속도

    private GameObject player; // 플레이어를 가져오기 위한 변수. 가져온 플레이어를 넣기 위해 생성

    private Renderer monsterColor; // 몬스터의 색을 바꾸기 위해 필요한 몬스터의 Renderer

    private GameManager gameManager; // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 필요하여 추가

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 태그를 사용하여 플레이어 오브젝트를 얻어온다.

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // GameManager오브젝트에서 GameManager 스크립트를 가져온다.

        monsterColor = GetComponent<Renderer>(); // 몬스터의 Renderer을 가져온다.

        // 2초마다 랜덤 방향으로 설정하도록 한다.
        InvokeRepeating("RandDir", 0, 2); // 0초 후에 RandMov 함수를 매번 2초 간격으로 호출한다.

    }

    private void Update()
    {
        if (gameObject.tag == "MonsterAlive") // 몬스터가 살아있을 때(몬스터의 태그가 MonsterAlive일 때)
        {
            // 몬스터가 살아있을 때만 이동
            // 몬스터2 이동. 몬스터가 바라보는 방향으로 앞으로 이동한다.
            Vector3 deltaPos = transform.forward * speed * Time.deltaTime; // 한 프레임 당 몬스터1가 앞으로 이동할 수 있는 이동치를 구한다. 모든 기기에서 동일한 속도로 이동하도록 Time.deltaTime을 곱한다.
            transform.Translate(deltaPos, Space.World); // 월드 좌표계에서 deltaPos만큼 이동한다.
        }
        else if (gameObject.tag == "MonsterDead") // 몬스터가 죽었을 때(몬스터의 태그가 MonsterDead 때)
        {
            StateMonsterDead(); // 몬스터의 색을 검은 색으로 바꾼다.
            CancelInvoke("RandDir"); // RandDir 함수 호출을 취소한다.
        }

        // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 추가
        if (gameManager.finishState == 2) // 게임 오버 상태이면
        {
            CancelInvoke(); // 모든 invoke 함수 호출을 취소한다.
        }
    }


    // 몬스터2가 랜덤으로 방향을 설정하는 함수
    private void RandDir()
    {
        float randDeg = Random.Range(0, 360); // 몬스터2가 바라볼 방향으로 0~359까지 랜덤으로 골라 값을 넣는다. 
        transform.rotation = Quaternion.Euler(0, randDeg, 0); // 랜덤 방향을 바라보도록 한다. Quaternion 형태로 Euler angle을 바꾼다. -> Euler angle을 사용하면 짐벌락이라는 문제가 발생하여 Quaternion을 사용한다. 
    }

    // 충돌 처리
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 총알 & 몬스터
        if (gameObject.tag == "MonsterAlive" && other.tag == "PlayerBullet") // 몬스터가 살아있을 때(몬스터의 태그가 MonsterAlive일 때) 플레이어 총알과 몬스터가 충돌하면
        {
            Destroy(other.gameObject); // 충돌한 플레이어 총알을 제거한다.
            gameObject.tag = "MonsterDead"; // 몬스터의 tag를 MonsterDead로 바꾸어 몬스터가 죽은 것을 표시한다.
            player.GetComponent<PlayerCtrl>().IncreaseKill(); // 플레이어 오브젝트의 PlayerCtrl 스크립트의 IncreaseKill 함수를 호출하여 제거한 몬스터의 개수를 1 증가한다.
            Destroy(gameObject, 1); // 몬스터를 1초 후에 제거한다.
            
        }
    }

    // 몬스터를 검은색으로 변경하는 함수
    private void StateMonsterDead()
    {
        monsterColor.material.color = Color.black; // 몬스터 material의 색을 검은색으로 변경한다.
    }
}
