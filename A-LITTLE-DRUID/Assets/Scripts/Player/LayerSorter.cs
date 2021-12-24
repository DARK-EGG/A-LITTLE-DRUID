using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* [Player의 레이어를 조정하는 스크립트]
 * - Player 하위 오브젝트인 LayerSorter에 적용
 */

public class LayerSorter : MonoBehaviour
{
    /* Player의 SpriteRenderer (레이어 조정을 위해) */
    private SpriteRenderer parentRenderer;

    /* 현재 Player와 맞닿아있는 장애물들의 리스트 */
    private List<Obstacle> obstacles = new List<Obstacle>();

    // Start is called before the first frame update
    void Start()
    {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 장애물과 부딪친 경우 */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("장애물 인식");
        if (collision.CompareTag("Obstacle") || collision.CompareTag("MiddleBoss") || collision.CompareTag("Boss"))
        {
            //Debug.Log("장애물 부딪힘");
            Obstacle o = collision.GetComponent<Obstacle>();

            /* 이전에 맞닿았던 장애물이 하나도 없고, o의 레이어가 player의 레이어보다 더 낮다면 */
            if (obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder - 1 < parentRenderer.sortingOrder)
            {
                parentRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;      // player의 레이어를 (o의 레이어 - 1)로 낮춤
            }

            obstacles.Add(o);                            // 리스트에 o 추가
        }
    }

    /* 부딪친 장애물과 떨어지는 경우 */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("MiddleBoss") || collision.CompareTag("Boss") || collision.CompareTag("NPC"))
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            obstacles.Remove(o);                        // 리스트에서 o 제거
            if (obstacles.Count == 0)                   // 리스트가 비었다면, Player의 레이어를 5로 조정
            {
                parentRenderer.sortingOrder = 5;
            }
            else                                        // 그렇지 않다면, 리스트 정렬 후 Player의 레이어를 (가장 낮은 레이어 - 1)로 조정
            {
                obstacles.Sort();
                parentRenderer.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
            }
        }

    }
}
