using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 아이템 회전 스크립트
public class ItemCtrl : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, (100 * Time.deltaTime), 0, Space.World); // 아이템이 제자리에서 y축을 기준으로 회전, 모든 기기에서 동일한 속도로 이동하도록 Time.deltaTime을 곱한다.
    }
}
