using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* [진실의방 엔딩 LevelChanger 스크립트]
 * - 오브젝트에 적용된 animator로 fadein/fadeout 작동
 */
public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    /* 보여줄 이미지들의 배열 */
    public Transform[] cartoons;
    public GameObject cartoon;

    /* 현재 보여주고 있는 이미지의 인덱스 */
    public int page = 0;

    /* 페이드 아웃 */
    public void FadeOut()
    {
        animator.SetTrigger("fadeOut");
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            CartoonManager cM = FindObjectOfType<CartoonManager>();
            //cM.isChecked = false;
        }
    }

    /* 페이드 아웃이 된 뒤, 다음 페이지로 넘기는 메소드 */
    public void OnFadeComplete()
    {
        cartoons[page].gameObject.SetActive(false);
        page++;
        if(page == 14)
        {
            page = 0;
            cartoon.gameObject.SetActive(false);
        }
    }

}
