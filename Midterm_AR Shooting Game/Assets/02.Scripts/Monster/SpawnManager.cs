using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 1, 2 스폰 스크립트
public class SpawnManager : MonoBehaviour
{
    public Transform monsterMark1; // 몬스터 마커1을 Hierarchy창에서 가져온다.
    public Transform monsterMark2; // 몬스터 마커2을 Hierarchy창에서 가져온다.

    public GameObject monster1; // Monster1 프리팹을 넣기 위해 생성
    public GameObject monster2; // Monster2 프리팹을 넣기 위해 생성

    private GameManager gameManager; // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 필요하여 추가

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // GameManager오브젝트에서 GameManager 스크립트를 가져온다.

        // 몬스터 마커1에서 3초마다 몬스터1이 생성되도록 한다.
        InvokeRepeating("Mon1Spawn", 1, 3); // 1초 후에 Mon1Spawn함수를 매번 1초 간격으로 호출한다.

        // 몬스터 마커2에서 4초마다 몬스터2가 생성되도록 한다.
        InvokeRepeating("Mon2Spawn", 1, 4); // 1초 후에 Mon1Spawn함수를 매번 1초 간격으로 호출한다.
    }

    private void Update()
    {
        // 게임 오버로 플레이어가 삭제되어 발생하는 NullReferenceException 오류를 처리하기 위해 추가
        if (gameManager.finishState == 2) // 게임 오버 상태이면
        {
            CancelInvoke(); // 모든 invoke 함수 호출을 취소한다.
        }
    }

    // 몬스터1의 생성 함수
    private void Mon1Spawn()
    {
        GameObject obj = Instantiate(monster1); // 몬스터1 프리팹을 복사하여 몬스터1 오브젝트를 생성하여 obj에 넣는다.

        Vector3 spawnPos = monsterMark1.position; // 몬스터 마커1의 위치를 가져와 spawnPos에 넣는다.
        Vector3 standPos = new Vector3(0, 0.05f, 0); // 몬스터를 위로 0.05만큼 올려 몬스터의 전체가 땅 위에 보여지도록 하기 위해 필요한 벡터
        obj.transform.position = spawnPos + standPos; // 수정한 spawnPos로 몬스터1의 소환 위치를 정한다.

        Destroy(obj, 10); // 10초 후 몬스터1이 사라지도록 한다.
    }

    // 몬스터2의 생성 함수
    private void Mon2Spawn()
    {
        GameObject obj = Instantiate(monster2); // 몬스터2 프리팹을 복사하여 몬스터2 오브젝트를 생성하여 obj에 넣는다.

        Vector3 spawnPos = monsterMark2.position; // 몬스터 마커2의 위치를 가져와 spawnPos에 넣는다.
        Vector3 standPos = new Vector3(0, 0.05f, 0); // 몬스터를 위로 0.05만큼 올려 몬스터의 전체가 땅 위에 보여지도록 하기 위해 필요한 벡터
        obj.transform.position = spawnPos + standPos; // 수정한 spawnPos로 몬스터2의 소환 위치를 정한다.

        Destroy(obj, 10); // 10초 후 몬스터2가 사라지도록 한다.
    }


}
