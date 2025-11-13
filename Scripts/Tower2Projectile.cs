using UnityEngine;

public class Tower2Projectile : MonoBehaviour
{
    private Vector2 direction;
    public float speed = 5f;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        Destroy(gameObject, 2f); // Mermiyi 3 saniye sonra yok et
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.GetComponent<Enemy>().TakeDamage(30);
        }
    }

}
