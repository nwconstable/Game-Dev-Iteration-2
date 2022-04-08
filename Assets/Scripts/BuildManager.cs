using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
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
        turretToBuild = standardTurretPrefab;
    }
    
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    // Returns a list of GameObjects that can be built on
    public GameObject[] GetBuildAreas()
    {
        CustomTags[] tags = (CustomTags[]) GameObject.FindObjectsOfType(typeof(CustomTags));
        GameObject[] buildAreas = new GameObject[tags.Length];
        foreach(CustomTags tag in tags) {
            int i = 0;
            if(tag.HasTag("BuildArea")) {
                buildAreas[i] = tag.gameObject;
            }
            i++;
        }
        // Trim the null values from the array
        List<GameObject> buildAreasList = new List<GameObject>();
        foreach(GameObject buildArea in buildAreas) {
            if(buildArea != null) {
                buildAreasList.Add(buildArea);
            }
        }
        Debug.Log("Build Areas: " + buildAreasList);
        return buildAreasList.ToArray();
    }
}