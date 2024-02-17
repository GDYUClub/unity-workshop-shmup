using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;

    public Transform propellerModel;
    public GameObject bulletPrefab;

    public Player player;

    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Fire), 0, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        propellerModel.eulerAngles += new Vector3(0,0, 1000 * Time.deltaTime);

        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        if (transform.position.z > 100)
        {
            return;
        }

        Vector3 offset = transform.position + (transform.forward * 2) + (transform.right * 3);
        Instantiate(bulletPrefab, offset, Quaternion.LookRotation(transform.forward)).tag = "EnemyBullet";
        offset = transform.position + (transform.forward * 2) + (transform.right * -3);
        Instantiate(bulletPrefab, offset, Quaternion.LookRotation(transform.forward)).tag = "EnemyBullet";
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerBullet")
        {
            TakeDamage(50);
            Destroy(col.gameObject);
        }
    }

    private void TakeDamage(int amount)
    {
        if (health <= 0)
        {
            return;
        }

        health -= amount;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        player.Heal(10);
        Destroy(gameObject);
    }
}
