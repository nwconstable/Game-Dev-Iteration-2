using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject landTurretPrefab;
    public GameObject waterTurretPrefab;
    private GameObject turretToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    void Start() {
        turretToBuild = landTurretPrefab;
    }
    
    public GameObject GetTurretToBuild(string type) {
        if(type == "Land") {
            SetTurretToBuild(landTurretPrefab);
        } else if (type == "Water") {
            SetTurretToBuild(waterTurretPrefab);
        }
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    // Returns a list of GameObjects that can be built on
    public GameObject[] GetBuildAreas()
    {
        Tilemap[] tilemaps = GameObject.FindObjectsOfType<Tilemap>();
        List<GameObject> buildAreas = new List<GameObject>();
        foreach (Tilemap tilemap in tilemaps)
        {
            buildAreas.Add(tilemap.gameObject);
        }
        return buildAreas.ToArray();










        // CustomTags[] tags = (CustomTags[]) GameObject.FindObjectsOfType(typeof(CustomTags));
        // GameObject[] buildAreas = new GameObject[tags.Length];
        // foreach(CustomTags tag in tags) {
        //     int i = 0;
        //     if(tag.HasTag("BuildArea")) {
        //         buildAreas[i] = tag.gameObject;
        //     }
        //     i++;
        // }
        // // Trim the null values from the array
        // List<GameObject> buildAreasList = new List<GameObject>();
        // foreach(GameObject buildArea in buildAreas) {
        //     if(buildArea != null) {
        //         buildAreasList.Add(buildArea);
        //     }
        // }
        // foreach(GameObject buildArea in buildAreasList) {
        //     Debug.Log(buildArea.name);
        // }
        // return buildAreasList.ToArray();
    }
}