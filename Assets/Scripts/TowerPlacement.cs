using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private float placementRange = 20f;

    private bool placementMode = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DisplayPreview();
        }
        else if (placementMode && Input.GetMouseButtonDown(1))
        {
            RemovePreview();
        }
        else if (placementMode && Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void DisplayPreview()
    {
        Debug.Log("Placement Mode on");
        placementMode = true;
        //TODO
    }

    private void RemovePreview()
    {
        Debug.Log("Placement Mode off");
        placementMode = false;
    }

    private void PlaceTower()
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            if (Vector2.Distance(playerPosition, new Vector2(hit.point.x, hit.point.y)) <= placementRange)
                Administrator.Instance.PlaceTower(hit.point);

        RemovePreview();
    }

    private void RemoveTower()
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            Administrator.Instance.RemoveTower(hit.point);
    }
}
