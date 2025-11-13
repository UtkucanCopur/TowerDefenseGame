using UnityEngine;

public class Tower1Projectile : MonoBehaviour
{
    private Vector2 direction;
    public float speed = 5f;

    public void SetDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject); 
            other.GetComponent<Enemy>().TakeDamage(50); 
        }
    }
}
