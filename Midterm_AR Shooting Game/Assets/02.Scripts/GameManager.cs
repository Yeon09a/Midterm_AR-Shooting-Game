using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 필요

// 게임 매니저 스크립트
public class GameManager : MonoBehaviour
{
    // 아이템 생성
    public GameObject item; // 아이템 프리팹을 넣기 위해 생성
    private GameObject player; // 플레이어를 가져오기 위한 변수. 가져온 플레이어를 넣기 위해 생성

    public Text gameState; // Game Clear/Over UI

    public int finishState = 0; // 게임을 클리어했는지 게임 오버인지 구분하기 위해 만든 변수(0 : 게임 플레이 중, 1 : 게임 클리어, 2 : 게임 오버)

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 아이템이 플레이어를 기준으로 랜덤 위치에 생성되어야 하기 때문에 플레이어 오브젝트를 얻어온다. -> AR에서 어떤 오브젝트를 만들 애, 기준점이 있어야하고, 기준점 근처에 상대적인 거리로 놔두는 것이 좋다.

        // 랜덤시간(5~10초) 간격으로 아이템이 생성되도록 한다.
        InvokeRepeating("ItemSpawn", Random.Range(5, 11), Random.Range(5, 11)); // 랜덤시간(5~10초) 후에 ItemSpawn함수를 매번 랜덤시간(5~10초) 간격으로 호출한다.

        gameState.enabled = false; // 게임을 처음 시작할 때 gameState을 비활성화하여 Game Clear/Over UI가 보이지 않게 한다.
    }

    private void Update()
    {
        // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 추가
        if (finishState == 2) // 게임 오버 상태이면
        {
            CancelInvoke(); // 모든 invoke 함수 호출을 취소한다.
        }
    }

    // 아이템 생성 함수
    private void ItemSpawn()
    {
        GameObject obj = Instantiate(item); // 아이템 프리팹을 복사하여 아이템 오브젝트를 생성하여 obj에 넣는다.
        Vector3 itemPos = new Vector3(Random.Range(-0.15f, 0.15f), 0.05f, Random.Range (-0.15f, 0.15f)); // 아이템의 위치 지정. x와 z는 -0.2f ~ 0.2f 사이의 실수값을 랜덤으로 갖도록 한다.(플레이어를 기준점으로 하여 상대적인 거리), y는 땅에서 조금 띄워지도록 하기 위해 0.05로 하였다.
        obj.transform.position = player.transform.position + itemPos; // 플레이어를 기준으로 하여 랜덤 위치에 아이템이 위치하도록 한다. (플레이어를 기준으로 x는 -1.5f ~ 1.6f 사이의 실수값, z는 - 0.1f, 1.6f 사이의 실수값 범위에 위치하도록 한다.)
        Destroy(obj, 5); // 5초 후 아이템이 사라지도록 한다.
    }

    // 게임 클리어 함수
    public void gameClear()
    {
        finishState = 1; // finishState을 1로 하여 게임이 클리어 된 것을 표시한다.
        gameState.enabled = true; // gameState UI를 활성화한다.
        gameState.text = "Game Clear"; // gameState UI의 text를 Game Clear로 바꾼다.
    }

    // 게임 오버 함수
    public void gameOver()
    {
        finishState = 2; // finishState을 2로 하여 게임 오버 된 것을 표시한다.
        gameState.enabled = true; // gameState UI를 활성화한다.
        gameState.text = "<color=#ff00ffff>Game Over</color>"; // gameState UI의 text를 마젠타 색 Game Clear로 바꾼다.
    }
}
