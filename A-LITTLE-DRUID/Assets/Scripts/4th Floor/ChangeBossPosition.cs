using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBossPosition : MonoBehaviour
{
    public void ChangeLocation()
    {
        Vector2 v = new Vector2(0, 0);
        this.transform.position = v;
    }
}
