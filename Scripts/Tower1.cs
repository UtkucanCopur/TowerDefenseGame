using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tower1 : MonoBehaviour
{
    [SerializeField] private GameObject placeableArea;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] public List<GameObject> placeableAreaList;

    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float fireRate = 1f;
    [HideInInspector] public Tower1Spawner spawner;

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

        while (currentTarget != null && Vector2.Distance(transform.position, currentTarget.position) <= attackRange)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Tower1Projectile>().SetDirection(currentTarget.position);
            yield return new WaitForSeconds(fireRate);
        }

        isShooting = false;
    }

    private void OnMouseDrag()
    {
        foreach (GameObject area in placeableAreaList)
        {
            if (Vector2.Distance(transform.position, area.transform.position) < 1f)
            {
                placeableArea = area;
                break;
            }
        }


        if (transform.position != placeableArea.transform.position && !FindEnemy())
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;

        }





    }

    private void OnMouseUp()
    {
        if (isPlaced) return;
        

        if (Vector2.Distance(transform.position, placeableArea.transform.position) < 1f 
            && int.Parse(gameManager.GetComponent<GameManager>().coinText.text) >= 20)
        {
            
            transform.position = placeableArea.transform.position;
            gameManager.GetComponent<GameManager>().AddCoins(-20);
            isPlaced = true;
            if (spawner != null)
            {
                spawner.SpawnNewTower();
            }
        }
        else
        {
            transform.position = new Vector3(-9.5f, -3.75f, 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    private bool FindEnemy()
    {
        GameObject[] enemiess = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemiess.Length >= 1) return true; else return false;

    }


}
