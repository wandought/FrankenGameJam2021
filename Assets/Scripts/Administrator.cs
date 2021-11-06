using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(TowerSelection))] [RequireComponent(typeof(WaveManager))] [RequireComponent(typeof(ApplicationQuit))]
public class Administrator : MonoBehaviour
{
    private static Administrator instance;
    public static Administrator Instance { get { return instance; } }
    public static float GridSize { get { return 0.25f; } }

    private Transform runtimeParent;
    public Transform RuntimeParent { get { return runtimeParent; } }

    private TowerSelection towerSelection;
    public TowerSelection TowerSelection { get { return towerSelection; } }
    private Dictionary<int, Waypoint> waypoints;
    public Dictionary<int, Waypoint> Waypoints { get { return waypoints; } }
    private Dictionary<int, BasicTower> towers;

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
        towers = new Dictionary<int, BasicTower>();

        GameObject collection = new GameObject("Runtime Object Collection");
        runtimeParent = collection.transform;
    }

    public GameObject CurrentTower()
    {
        return towerSelection.CurrentTower;
    }

    public bool RegisterWaypoint(Waypoint waypoint)
    {
        Vector2 coordinate = new Vector2(waypoint.transform.position.x, waypoint.transform.position.z);
        try
        {
            waypoints.Add(CoordinateCalculator.CoordinateToInt(coordinate), waypoint);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public bool PlaceTower(Vector3 position, Vector3 rotation)
    {
        int x = Mathf.RoundToInt(position.x / 10);
        int z = Mathf.RoundToInt(position.z / 10);

        x *= 10;
        z *= 10;

        int key = CoordinateCalculator.CoordinateToInt(x, z);
        Waypoint tile;
        if (!waypoints.TryGetValue(key, out tile)
            || tile.IsPlaceable == false)
            return false;

        tile.IsPlaceable = false;
        GameObject tower = Instantiate(towerSelection.CurrentTower,
            new Vector3(x, position.y, z),
            Quaternion.Euler(rotation.x, rotation.y, rotation.z),
            runtimeParent);

        towers.Add(key, tower.GetComponent<BasicTower>());
        return true;
    }

    public bool RemoveTower(Vector3 position)
    {
        int key = CoordinateCalculator.CoordinateToInt(
            CoordinateCalculator.RawCoordinatesToTileCoordinates(position));
        Waypoint waypoint;

        if (!waypoints.TryGetValue(key, out waypoint))
            return false;

        waypoints[key].IsPlaceable = true;

        BasicTower tower;
        if (!towers.TryGetValue(key, out tower))
            return false;

        Destroy(tower.gameObject);
        return towers.Remove(key);
    }
}
