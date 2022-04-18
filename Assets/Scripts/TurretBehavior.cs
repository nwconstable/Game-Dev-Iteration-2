using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float fireRate;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float range;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Button turretButton;
    [SerializeField] private Button[] upgradeButtons;
    [SerializeField] private Canvas upgradeCanvas;
    [SerializeField] private float damage;
    private bool upgradeButtonsActive;
    private GameObject target;
    private float lastShot = 0;

    void Awake() {
        rangeCollider.radius = range;
        //Debug.Log("Turret Range: " + range);
        upgradeButtonsActive = false;
        foreach(Button button in upgradeButtons) {
            button.gameObject.SetActive(upgradeButtonsActive);
        }
    }

    void Start() {
        turretButton.onClick.AddListener(() => {
            if(upgradeButtonsActive) {
                upgradeButtonsActive = false;
                foreach(Button button in upgradeButtons) {
                    button.gameObject.SetActive(upgradeButtonsActive);
                }
            } else {
                upgradeButtonsActive = true;
                foreach(Button button in upgradeButtons) {
                    button.gameObject.SetActive(upgradeButtonsActive);
                }
                UpgradeTurret();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosestEnemy();
        if(target != null) {
            TurnToTarget(target);
            Shoot();
        }
        // TurnToTarget(Input.mousePosition);
        // if (Input.GetMouseButton(0) && (lastShot > fireRate)) {
        //     Shoot();
        //     lastShot = 0;
        // }
        lastShot += Time.deltaTime;
    }

    private void UpgradeTurret() {
        foreach(Button button in upgradeButtons) {
            button.onClick.AddListener(() => {
                string upgrade = button.gameObject.GetComponent<CustomTags>().GetAtIndex(0);
                if(upgrade == "Range") {
                    range += 1;
                    rangeCollider.radius = range;
                } else if(upgrade == "RateOfFire") {
                    fireRate -= 0.1f;
                } else if(upgrade == "Damage") {
                    damage += 1;
                }
                upgradeButtonsActive = false;
                foreach(Button button in upgradeButtons) {
                    button.gameObject.SetActive(upgradeButtonsActive);
                }
            });
        }
    }

    private void TurnToTarget(GameObject target) {
        // Turn the turret towrds the mouse (or a target)
        // Vector3 mousePosition = Input.mousePosition;
        Vector3 targetPos = target.transform.position;
        //Debug.Log("Target Position: " + targetPos);
        Vector2 direction = targetPos - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), turnSpeed * Time.deltaTime);
        upgradeCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Shoot() {
        if(lastShot >= fireRate) {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.SetPositionAndRotation(bulletSpawn.transform.position, bulletSpawn.rotation); 
            lastShot = 0;
        }
        
    }

    private GameObject FindClosestEnemy() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = range;
        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance) {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
