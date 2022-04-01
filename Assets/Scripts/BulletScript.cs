using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletLifeTime;

    void Start()
    {
        StartCoroutine(MoveBullet());
    }

    private IEnumerator MoveBullet() {
        float elapsedTime = 0;
        while(elapsedTime <= bulletLifeTime) {
            elapsedTime += Time.deltaTime;
            bulletTransform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
