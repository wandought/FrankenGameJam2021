using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    public float degreesPerSecond = 0f;

    public int damagePerBullet = 5;

    public int price = 350;

    private GameObject towerHead;

    // Start is called before the first frame update
    void Start()
    {
        this.towerHead = this.transform.Find("TowerHead").gameObject;
    }

    // Update is called once per frame
    void Update()   
    {
        this.towerHead.transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0, Space.World);
    }
}

