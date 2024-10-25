using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController
{

    [SerializeField] private GameObject proyectilPrefab; //lo q va a lanzar el bicho
    [SerializeField] private Transform spawnpointProjectile;
    [SerializeField] private float forceProjectile;


    private void Start()
    {
    }
    public void ThrowProjectile()
    {
        GameObject clone = Instantiate(proyectilPrefab, spawnpointProjectile.position, spawnpointProjectile.rotation);

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

        rb.AddRelativeForce(clone.transform.right * forceProjectile);
        Destroy(clone,5f);
    }



}
