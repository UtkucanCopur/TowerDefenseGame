using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    private GameObject placeableArea;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] public List<GameObject> placeableAreaList;

    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float fireRate = 1f;
    [HideInInspector] public Tower2Spawner spawner;
    

    private Transform currentTarget;
    private bool isShooting = false;
    public bool isPlaced = false;
    private Collider2D[] hitColliders;

    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        placeableAreaList = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlaceableArea"));
    }

    private void Update()
    {
        FindClosestEnemy();

        if (currentTarget != null && !isShooting)
        {
            StartCoroutine(Shoot());
        }

        FindEnemy();
    }

    private void FindClosestEnemy()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.transform;
                }
            }
        }

        currentTarget = closestEnemy;
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        while (true)
        {
            if (isPlaced && currentTarget != null && Vector2.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                for (int i = 0; i < 10; i++)
                {
                    float angle = i * 36f;
                    Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                    // Tower2Projectile kullanýyorsan burayý ona göre ayarla
                    if (bullet.TryGetComponent<Tower2Projectile>(out var projectile))
                    {
                        projectile.SetDirection(direction);
                    }

                    bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnMouseDrag()
    {
        if (isPlaced) return;

        foreach (GameObject area in placeableAreaList)
        {
            if (Vector2.Distance(transform.position, area.transform.position) < 1f)
            {
                placeableArea = area;
                break;
            }
        }

        if (placeableArea == null || (transform.position != placeableArea.transform.position && !FindEnemy()))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        if (isPlaced) return;

        if (placeableArea != null &&
            Vector2.Distance(transform.position, placeableArea.transform.position) < 1f &&
            int.Parse(gameManager.GetComponent<GameManager>().coinText.text) >= 40)
        {
            Vector3 pos = placeableArea.transform.position;
            transform.position = new Vector3(pos.x, pos.y, 0f);

            gameManager.GetComponent<GameManager>().AddCoins(-40);
            isPlaced = true;

            if (spawner != null)
            {
                spawner.SpawnNewTower();
            }
        }
        else
        {
            transform.position = new Vector3(-7.5f, -3.60f, 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private bool FindEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length >= 1;
    }
}
