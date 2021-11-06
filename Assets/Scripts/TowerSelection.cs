using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    private int currentTower = 0;
    public GameObject CurrentTower { get { return towers.Length > 0 ? towers[currentTower] : null; } }

    private void Update()
    {
        if (towers.Length == 0)
            return;
        else if (Input.mouseScrollDelta.y < -Mathf.Epsilon)
            if (currentTower == 0)
                currentTower = towers.Length;
            else
                currentTower--;
        else if (Input.mouseScrollDelta.y > Mathf.Epsilon)
            currentTower = (currentTower + 1) % towers.Length;
    }
}
