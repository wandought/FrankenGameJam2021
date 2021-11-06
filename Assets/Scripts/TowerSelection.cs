using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    private int currentTower = 0;
    public GameObject CurrentTower
    {
        get
        {
            return towers.Length > 0 ? towers[currentTower] : null;
        }
    }

    public delegate void CurTowerChangedDelegate(GameObject newTower);
    public static event CurTowerChangedDelegate CurTowerChangedEvent;


    private void Update()
    {
        if (towers.Length == 0)
            return;
        else if (Input.mouseScrollDelta.y > Mathf.Epsilon)
        {


            currentTower = (currentTower + 1) % towers.Length;
            CurTowerChangedEvent(towers[currentTower]);
        }
        else if (Input.mouseScrollDelta.y < -Mathf.Epsilon)
            if (currentTower == 0)
            {
                currentTower = towers.Length - 1;
                CurTowerChangedEvent(towers[currentTower]);
            }
            else
            {
                currentTower--;
                CurTowerChangedEvent(towers[currentTower]);
            }
    }
}
