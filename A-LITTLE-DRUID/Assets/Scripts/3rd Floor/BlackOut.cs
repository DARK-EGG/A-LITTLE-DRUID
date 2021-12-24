using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//문
//보스전 시작 시 정전 스크립트 (문하고 부딪히면 정전)
public class BlackOut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& !DarkByButton.isLight)
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
