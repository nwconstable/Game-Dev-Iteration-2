using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Debug.Log("Enemy: " + enemy.transform.position);
            Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
            // Direction from camera to enemy
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            // Draw a line in the Scene View from the position of the camera to the position of the enemy.
            //Debug.DrawLine(transform.position, transform.position + direction * 10, Color.red);
            if (Physics.Raycast(transform.position, direction, out hit, 100f)) {
                Debug.Log("Hit: " + hit.collider.gameObject.transform.position);
                Debug.DrawLine(transform.position, hit.point, Color.red);
                Destroy(enemy);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
