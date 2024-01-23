using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector2 offset;
    public float threshold;
    public int cameraZ;
    public float smoothing;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector2 mousePosition = gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector2 finalPosition = ((Vector2)target.position + mousePosition) / 2;

        finalPosition.x = Mathf.Clamp(finalPosition.x, -threshold + target.position.x, threshold + target.position.x);
        finalPosition.y = Mathf.Clamp(finalPosition.y, -threshold + target.position.y, threshold + target.position.y);

        Vector3 movePos = new Vector3(finalPosition.x, finalPosition.y, cameraZ);

        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, smoothing);
    }
}