using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed;
    public float bulletLife;

    void Start()
    {
        Destroy(gameObject, bulletLife);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, bulletSpeed * Time.deltaTime);
    }
}
