using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health = 100;
    private Waypoints Wpoints;
    private int waypointIndex;

    void Start()
    {
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Destroy(gameObject);
            UIManager.instance.IncrementTreasure();
            Spawner.enemiesAlive--;
        }
    }

    public static void DamageEnemy(Transform enemyShip)
    {
        Enemy e = enemyShip.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(50); //for now hard coded damage number
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointIndex].position, speed * Time.deltaTime);

        //the following rotates the enemy on its z axis to simulate turning
        Vector3 dir = Wpoints.waypoints[waypointIndex].position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);

        if (Vector2.Distance(transform.position, Wpoints.waypoints[waypointIndex].position) < 0.1f)
        {
            if (waypointIndex < Wpoints.waypoints.Length - 1)
            {
                waypointIndex++;
            }
            else //ship has reached island
            {
                Destroy(gameObject);
                UIManager.instance.DecrementTreasure();
                Spawner.enemiesAlive--;
            }

        }

         //CheckForHit();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            DamageEnemy(transform); //impact the ship for damage, kill if ship damage is zero
            Destroy(other.gameObject); //destroys bullet after it hits an enemy ship 
            //inc treasure method now called when ship is destroyed 
        }
    }
}
