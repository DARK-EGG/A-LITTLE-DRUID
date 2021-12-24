using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewpointMove : MonoBehaviour
{
    public bool occurCheck = false;
    public Camera templeCam;
    public float fullTime;
    public float waitTime;
    public Vector3 idlePosition;
    public Vector3 positionToMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (occurCheck ==  false && collision.gameObject.CompareTag("Player"))
        {
            var collisionObj = collision.gameObject;
            StartCoroutine(CameraMoveAndBack(collisionObj));
            occurCheck = true;
        }
    }

    IEnumerator CameraMoveAndBack(GameObject collisionObj)
    {
        var moveScript = collisionObj.GetComponent<PlayerMove>();
        var followScript = templeCam.GetComponent<playerfollow>();
        var rigid = collisionObj.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.zero;
        collisionObj.GetComponent<Animator>().enabled = false;
        moveScript.enabled = false;
        followScript.enabled = false;
        idlePosition = templeCam.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < fullTime)
        {
            templeCam.transform.position = Vector3.Lerp(idlePosition, positionToMove, (elapsedTime / fullTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        templeCam.transform.position = positionToMove;
        yield return new WaitForSeconds(waitTime);

        elapsedTime = 0f;
        while (elapsedTime < fullTime)
        {
            templeCam.transform.position = Vector3.Lerp(positionToMove, idlePosition, (elapsedTime / fullTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        templeCam.transform.position = idlePosition;
        moveScript.enabled = true;
        followScript.enabled = true;
        collisionObj.GetComponent<Animator>().enabled = true;
    }
}
