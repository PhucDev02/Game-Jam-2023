using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitter : MonoBehaviour
{
    [SerializeField] SpriteRenderer bound;
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Camera.main.orthographicSize = bound.bounds.size.x * Screen.height / Screen.width * 0.5f;
    }
}
