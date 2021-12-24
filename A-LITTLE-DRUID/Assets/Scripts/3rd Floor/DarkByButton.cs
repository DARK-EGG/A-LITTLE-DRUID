using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//버튼
//버튼으로 정전 해제
public class DarkByButton : MonoBehaviour
{
    public static bool isLight = false;//불이 켜져 있는가
    private GameObject player;
    public Sprite pushedButton;
    GameObject scanObject;
    SpriteRenderer sr;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = gameObject.GetComponent<SpriteRenderer>();
        isLight = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어와 부딪혔을 시 정전상태 끝 불켜짐 = ture
        scanObject = collision.gameObject;
        if (scanObject && scanObject.CompareTag("Player"))
        {
            player.transform.GetChild(0).gameObject.SetActive(false);
            sr.sprite = pushedButton;
            isLight = true;
        }
    }
}
