using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    
    private Vector3 velocity;
    
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
    }
}