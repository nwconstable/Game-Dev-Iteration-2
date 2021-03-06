using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

public class AddTurret : MonoBehaviour
{
    [SerializeField] private Button landTurretButton;
    [SerializeField] private Button waterTurretButton;
    private TMP_Text buttonText;
    private GameObject turret;
    private bool isTurretPlaced;

    void Start() {
        landTurretButton.onClick.AddListener(() => {
            //Debug.Log("Turret Button Clicked");
            if(UIManager.instance.Treasure > 800) {
                isTurretPlaced = false;
                StartCoroutine(TurretPlacer(landTurretButton));
            } else {
                StartCoroutine(UIManager.instance.NotEnoughTreasure(landTurretButton.GetComponentInChildren<TMP_Text>().text));
            }
        });
        waterTurretButton.onClick.AddListener(() => {
            //Debug.Log("Turret Button Clicked");
            if(UIManager.instance.Treasure > 800) {
                isTurretPlaced = false;
                StartCoroutine(TurretPlacer(waterTurretButton));
            } else {
                StartCoroutine(UIManager.instance.NotEnoughTreasure( waterTurretButton.GetComponentInChildren<TMP_Text>().text));
            }
        });
    }

    void Update() {

    }
    
    private IEnumerator TurretPlacer(Button button) {
        //Debug.Log("Turret Placer Started");
        //yield return null;
        buttonText = button.GetComponentInChildren<TMP_Text>();
        string buttonTextString = buttonText.text;
        buttonText.text = "Click Again To Place Turret";
        //Debug.Log("Waited one frame");
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild(button.gameObject.GetComponent<CustomTags>().GetAtIndex(0));
        string turretTag = turretToBuild.GetComponent<CustomTags>().GetAtIndex(0);
        //Debug.Log("Turret Tag: " + turretTag);
        //Change the color of all invalid build areas to red
        foreach (GameObject buildArea in BuildManager.instance.GetBuildAreas()) {
            if (buildArea.GetComponent<BuildArea>().IsBuildable(turretTag)) {
                buildArea.GetComponent<Tilemap>().color = Color.green;
            } else {
                buildArea.GetComponent<Tilemap>().color = Color.red;
            }
        }
        while(!isTurretPlaced) {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log("Mouse Position: " + mousePos);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //Debug.Log("World Position Updated: " + worldPos);
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            bool isWithinAllowedArea = false;
            //Debug.Log("Is in allowed area : " + isWithinAllowedArea);
            if(Input.GetMouseButtonDown(0)) {
                // This part checks whether the selected turret placement is within the allowed area for that turret
                Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos, 0.3f);
                foreach(Collider2D collider in colliders) {
                    if  (collider.gameObject.GetComponent<CustomTags>().HasTag(turretTag) && !(collider.gameObject.GetComponent<CustomTags>().HasTag("Turret"))) {
                        isWithinAllowedArea = true;
                        //Debug.Log("Is in allowed area : " + isWithinAllowedArea);
                        break;
                    }
                    if (turretTag == "Water") {
                        if ((collider.gameObject.GetComponent<CustomTags>().HasTag("Land") && !collider.gameObject.GetComponent<CustomTags>().HasTag("Turret")) || collider.gameObject.GetComponent<CustomTags>().HasTag("Path")) {
                            isWithinAllowedArea = false;
                            //Debug.Log("Is water turret and is in water area: " + isWithinAllowedArea);
                            //Debug.Log("Collider Tag: " + collider.gameObject.GetComponent<CustomTags>().GetAtIndex(0));
                            break;
                        }
                    }
                }
                // This part checks whether the turret placement is too close to another turret
                foreach(GameObject turret in turrets) {
                    if(Vector2.Distance(turret.transform.position, worldPos) < 0.4f) {
                        isWithinAllowedArea = false;
                        //Debug.Log("Is far enough away from another turret: " + isWithinAllowedArea);
                        break;
                    }
                }
                // Places the turret
                if(isWithinAllowedArea){
                    Vector3 newPos = new Vector3(worldPos.x, worldPos.y, 0);
                    turret = (GameObject)Instantiate(turretToBuild, newPos, transform.rotation);
                    //Debug.Log("Turret Instantiated");
                    isTurretPlaced = true;
                    //Debug.Log("Turret placed");
                }
            }
            yield return null;
        }
        //Debug.Log("Turret Placer Ended");
        // Change th color of the build areas back to color
        foreach (GameObject buildArea in BuildManager.instance.GetBuildAreas()) {
            buildArea.GetComponent<Tilemap>().color = Color.white;
        }
        buttonText.text = buttonTextString;
        UIManager.instance.DecrementTreasure(800);
    }
}
