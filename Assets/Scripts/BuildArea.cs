using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    public bool IsBuildable(string tag)
    {
        return GetComponent<CustomTags>().HasTag(tag);
    }
}