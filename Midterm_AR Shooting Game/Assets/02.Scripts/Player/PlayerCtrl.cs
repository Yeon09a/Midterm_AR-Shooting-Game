using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 필요

// 플레이어 컨트롤 스크립트
public class PlayerCtrl : MonoBehaviour
{
    // 이동 관련 변수
    Vector3 prePos; // 이전의 좌표를 기억하기 위해 만든 변수

    // 플레이어 관련 변수
    private int hp = 10; // 플레이어 체력으로 10으로 설정한다.
    public int itemCount = 0; // 아이템 획득 횟수

    private int killCount; // 제거한 몬스터 개수

    public Text killCountText; // 제거한 몬스터 개수 UI
    public Text playerHPText; // 플레이어 체력 UI

    void Start()
    {
        killCountText.text = killCount.ToString(); // 처음에 killCountText.text을 killCount으로 초기화
        playerHPText.text = hp.ToString(); // 처음에 playerHPText.text을 hp로 초기화
    }

    void Update()
    {
        // 마우드의 이동량을 구하기 위해 이동한(현재) 위치에서 이전의 위치를 뺀다. (x와 y를 얼마나 움직였는가)
        Vector2 deltaPosSpin = Input.mousePosition - prePos;
        deltaPosSpin *= (Time.deltaTime * 10f); // 한 프레임 동안 이동한 거리를 동일하게 하기 위해, 속도를 일정하게 유지하기 위해 Time.deltaTime을 곱하고 속도를 높이기 위해 10를 곱한다.
        transform.Rotate(0, deltaPosSpin.x, 0, Space.World); // 월드 좌표로 y축에 대하여 회전한다. 마우스가 좌우로 움직일 때 y축을 기준으로 deltaPosSpin.x만큼 회전이 된다.

        if (Input.GetMouseButton(0)) // 마우스 좌측 버튼 클릭 시
        {
            // 마우드의 이동량을 구하기 위해 이동한(현재) 위치에서 이전의 위치를 뺀다. (x와 y를 얼마나 움직였는가)
            Vector2 deltaPosMov = Input.mousePosition - prePos;
            deltaPosMov *= (Time.deltaTime * 0.02f); // 한 프레임 동안 이동한 거리를 동일하게 하기 위해, 속도를 일정하게 유지하기 위해 Time.deltaTime을 곱하고 속도를 줄이기 위해 0.05f를 곱한다.
            transform.Translate(deltaPosMov.x, 0, deltaPosMov.y, Space.World); // 월드 좌표로 x, z평면에 대하여 x는 deltaPosMov.x만큼, y는 deltaPosMov.y만큼 움직인다. 
        }

        prePos = Input.mousePosition; // 현재 마우스의 위치, 즉 이전 프레임의 마우스의 위치를 저장한다.

        // 플레이어의 체력이 0이하면 플레이어 제거
        if(hp <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().gameOver(); // 게임 매니저의 GameManager 스크립트의 gameOver 함수를 호출한다.
            Destroy(gameObject);
        }

        // 몬스터를 10회 제거하면 클리어
        if(killCount == 10)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().gameClear(); // 게임 매니저의 GameManager 스크립트의 gameClear 함수를 호출한다.
        }
    }

    // 체력(hp)를 감소하는 함수
    public void DecreaseHP(int amount) // 매개변수로 받은 만큼 체력을 감소한다.
    {
        hp -= amount; // 체력을 amount만큼 감소시키고
        playerHPText.text = hp.ToString(); // 감소된 체력을 UI에 보여준다.
    }

    // 제거한 몬스터 개수를 증가시키는 함수
    public void IncreaseKill()
    {
        killCountText.text = (++killCount).ToString(); // killCount를 1 증가시킨 후 IU에 보여준다.
    }

    // 충돌 처리
    private void OnTriggerEnter(Collider other)
    {
        // 몬스터 & 플레이어
        if (other.tag == "MonsterAlive") // 몬스터와 플레이어가 충돌하면
        {
            Destroy(other.gameObject); // 충돌한 몬스터를 제거한다.
            DecreaseHP(2); // 체력을 2 감소시킨다.
        }

        // 몬스터 총알 & 플레이어
        if (other.tag == "MonsterBullet") // 몬스터 총알과 플레이어가 충돌하면
        {
            Destroy(other.gameObject); // 충돌한 몬스터 총알을 제거한다.
            DecreaseHP(1); // 체력을 1 감소시킨다.
        }

        // 플레이어 & 아이템
        if (other.tag == "Item") // 아이템과 플레이어가 충돌하면
        {
            if (itemCount < 3) // itemCount가 3보다 작으면
            {
                itemCount++; // itemCount을 1 증가시킨다.
            }
            else // 3 이상이면
            {
                itemCount = 3; // itemCount를 3으로 유지한다.
            }

            Destroy(other.gameObject); // 충돌한 아이템을 제거한다.
        }
        
    }
}
