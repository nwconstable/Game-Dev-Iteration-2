using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddTurret : MonoBehaviour
{
    [SerializeField] private Button turretButton;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Collider2D[] allowedAreas;
    private GameObject turret;
    private bool isTurretPlaced;

    void Start() {
        turretButton.onClick.AddListener(() => {
            Debug.Log("Turret Button Clicked");
            isTurretPlaced = false;
            StartCoroutine(TurretPlacer());
        });
    }

    void Update() {

    }
    
    private IEnumerator TurretPlacer() {
        //Debug.Log("Turret Placer Started");
        yield return null;
        buttonText.text = "Click Again To Place Turret";
        //Debug.Log("Waited one frame");
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        string turretTag = turretToBuild.tag;
        while(!isTurretPlaced) {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log("Mouse Position: " + mousePos);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //Debug.Log("World Position Updated: " + worldPos);
            // Make sure the turret is placed within the allowed areas
            bool isWithinAllowedArea = false;
            if(Input.GetMouseButtonDown(0)) {
                foreach(Collider2D area in allowedAreas) {
                    if(area.OverlapPoint(worldPos) && area.CompareTag(turretTag)) {
                        isWithinAllowedArea = true;
                        break;
                    }
                }
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
        buttonText.text = "Add New Turret";
    }
}
