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
        clone.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(forceProjectile,0));
        Destroy(clone,5f);
    }



}
