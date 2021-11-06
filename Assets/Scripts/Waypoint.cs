using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlacable = true;
    public bool IsPlaceable { get { return isPlacable; } set { isPlacable = value; } }

    private Administrator administrator;

    private void Start()
    {
        if (!Administrator.Instance.RegisterWaypoint(this))
            Destroy(this.gameObject);
    }
}
