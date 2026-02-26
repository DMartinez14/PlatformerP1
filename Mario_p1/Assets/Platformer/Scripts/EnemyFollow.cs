using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 6f; // How far to move left and right
    private Vector3 startPosition;
    private int direction = 1;
    private float reverseCooldown = 0.2f; // Cooldown in seconds
    private float lastReverseTime = -1f;

    void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Move right and left on the x axis
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            ReverseDirection();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            ReverseDirection();
        }
    }

    private void ReverseDirection()
    {
        if (Time.time - lastReverseTime < reverseCooldown)
            return;
        direction *= -1;
        transform.localScale = new Vector3(direction, 1, 1);
        lastReverseTime = Time.time;
    }
}
