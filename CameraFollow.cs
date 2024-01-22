using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform myTrans;
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    
    private Vector3 velocity;
    
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPosition = target.position;
        Vector3 finalPosition = (target.position - mousePosition) / 2
        myTrans.position = Vector3.SmoothDamp(transform.position, finalPosition + offset, ref velocity, smoothTime);
    }
}