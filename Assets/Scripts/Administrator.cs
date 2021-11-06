using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(TowerSelection))]
public class Administrator : MonoBehaviour
{
    private static Administrator instance;
    public static Administrator Instance { get { return instance; } }
    public static float GridSize { get { return 0.25f; } }
    public static float TileSize { get { return 10f; } }

    [SerializeField] private Transform runtimeParent;

    private TowerSelection towerSelection;
    private Dictionary<int, Waypoint> waypoints;
    private List<BasicTower> towers;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        towerSelection = GetComponent<TowerSelection>();
        waypoints = new Dictionary<int, Waypoint>();
        towers = new List<BasicTower>();
    }

    public GameObject CurrentTower()
    {
        return towerSelection.CurrentTower;
    }

    public bool RegisterWaypoint(Waypoint waypoint)
    {
        try
        {
            Vector2 coordinate = new Vector2(waypoint.transform.position.x, waypoint.transform.position.z);
            waypoints.Add(CoordinateCalculator.CoordinateToInt(coordinate), waypoint);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public bool PlaceTower(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / 10);
        int z = Mathf.RoundToInt(position.z / 10);

        x *= 10;
        z *= 10;

        Waypoint tile;
        if (!waypoints.TryGetValue(CoordinateCalculator.CoordinateToInt(x, z), out tile)
            || tile.IsPlaceable == false)
            return false;

        tile.IsPlaceable = false;
        GameObject tower = Instantiate(towerSelection.CurrentTower,
            new Vector3(x, position.y, z),
            Quaternion.identity,
            runtimeParent);

        towers.Add(tower.GetComponent<BasicTower>());
        return true;
    }
}
