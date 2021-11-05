using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] [RequireComponent(typeof(MeshRenderer))]
public class MaterialSwitcher : MonoBehaviour
{
    [SerializeField] private Material sand;
    [SerializeField] private Material dirt;

    private MeshRenderer renderer;

    private Waypoint waypoint;
    bool lastPlacable;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        waypoint = GetComponentInParent<Waypoint>();
        lastPlacable = waypoint.IsPlaceable;
    }

    private void Update()
    {
        if (lastPlacable != waypoint.IsPlaceable)
        {
            lastPlacable = waypoint.IsPlaceable;

            if (lastPlacable)
                renderer.material = sand;
            else
                renderer.material = dirt;
        }
    }
}
