using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class FitOnScreen : MonoBehaviour
{
    private Camera cam;
    public float sceneWidth = 10;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float unitsPerPixel = sceneWidth / Screen.width;
        float desireHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        cam.orthographicSize = desireHalfHeight;
    }
}
