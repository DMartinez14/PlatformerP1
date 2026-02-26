using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
  public Transform target; // Assign Mario (Player) here
    public Vector3 offset = new Vector3(0, 5, -10); // Adjust as needed
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
