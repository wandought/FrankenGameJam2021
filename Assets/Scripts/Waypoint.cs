using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlacable = true;
    public bool IsPlaceable { get { return isPlacable; } }
}
