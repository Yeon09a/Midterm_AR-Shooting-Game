using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터1 제어 스크립트
public class Monster1Ctrl : MonoBehaviour
{
    private float speed = 0.03f; // 몬스터1이 이동하는 속도
    private Vector3 moveDir; // 몬스터1이 이동하는 방향

    private GameObject player; // 플레이어를 가져오기 위한 변수. 가져온 플레이어를 넣기 위해 생성

    private Renderer monsterColor; // 몬스터의 색을 바꾸기 위해 필요한 몬스터의 Renderer

    private GameManager gameManager; // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 필요하여 추가

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 몬스터1이 플레이어 쪽으로 회전을 해야하기 때문에 플레이어 오브젝트를 얻어온다.

        monsterColor = GetComponent<Renderer>(); // 몬스터의 Renderer을 가져온다.

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // GameManager오브젝트에서 GameManager 스크립트를 가져온다.
    }

    void Update()
    {
        if (gameManager.finishState == 0 || gameManager.finishState == 1) // 게임 플레이 또는 게임 클리어 상태에서만 몬스터 작동하도록 한다.
        {
            if (gameObject.tag == "MonsterAlive") // 몬스터가 살아있을 때(몬스터의 태그가 MonsterAlive일 때)
            {
                // 몬스터가 살아있을 때만 이동
                moveDir = player.transform.position - transform.position; // 플레이어의 위치(가고자하는 목표 지점)에서 자신(몬스터1)의 위치를 빼 방향 벡터를 구한다.
                moveDir.y = 0; // 몬스터가 위로 올라가지는 않기 때문에 방향 벡터의 y값을 0으로 한다.

                // 플레이어와 몬스터1이 바라보는 각도 차이가 얼마인가를 계산하기 위한 코드로 두 벡터 사이의 끼어있는 사이 각도를 구하는 SignedAngle을 사용하여 구한 후 angle에 그 값을 넣는다.
                float angle = Vector3.SignedAngle(transform.forward, // 자기 자신(몬스터1)의 앞쪽 방향
                                                    moveDir.normalized, // 플레이어까지 이동하는 방향으로 정규화를 하여 벡터의 길이를 1로 만든다.
                                                    Vector3.up); // 어떤 축을 기준으로 회전을 할 것인지 정하는 것으로 up을 하여 위의 축을 기준으로 회전을 하도록 한다.

                transform.Rotate(0, angle, 0); // 몬스터1이 플레이어를 향하도록 몬스터1을 회전시킨다.

                Vector3 deltaPos = moveDir.normalized * speed * Time.deltaTime; // 한 프레임 당 몬스터1이 이동할 수 있는 이동치를 구한다. 모든 기기에서 동일한 속도로 이동하도록 Time.deltaTime을 곱한다.
                transform.Translate(deltaPos, Space.World); // 월드 좌표계에서 deltaPos만큼 이동한다.
            }
            else if (gameObject.tag == "MonsterDead") // 몬스터가 죽었을 때(몬스터의 태그가 MonsterDead 때)
            {
                StateMonsterDead(); // 몬스터의 색을 검은 색으로 바꾼다.
            }
        }
        // 게임 오버 시(gameManager.finishState == 2)에는 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 몬스터가 작동하지 않도록 한다.
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
