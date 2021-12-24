using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Air 몹
//3층의 메인 보스 주변 도는 몹들 움직이는 스크립트
public class AirMove : MonoBehaviour
{
    /* 원의 중심(보스)로부터 몬스터가 위치하는 각도 */
    public int degree;

    /* 원 지름 */
    public float cirWidth = 2f;

    /* 원 중심 역할을 할 부모 오브젝트(보스) */
    private Transform parent = null;

    /* 시간 측정 변수*/
    private float t = 0;

    /* 몬스터가 움직이는 속도 */
    public int speed = 2;

    //바람 몹 보스 하위로 이동
    private void Awake()
    {
        parent = GameObject.Find("Boss").transform;
    }

    void Start()
    {
        transform.SetParent(parent);
    }

    //움직임
    void FixedUpdate()
    {
        t += speed * Time.deltaTime;    // 시간 * 스피드를 t에 저장
        if (t > 60f) t = 0f;            // 60초를 넘어선 경우 0으로 리셋

        double radian = (double)(t / 60.0 * 360.0 + degree) / 180 * Math.PI;    // 라디안 계산
        
        /* 다음 위치 계산 및 이동 */
        double x = parent.position.x + (cirWidth / 2) * Math.Cos(radian);
        double y = parent.position.y + (cirWidth / 2) * Math.Sin(radian);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2((float)x, (float)y), Time.deltaTime);
    }
}
