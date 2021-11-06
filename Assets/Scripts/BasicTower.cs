using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    private GameObject towerHead;

    // Start is called before the first frame update
    void Start()
    {
        this.towerHead = this.transform.Find("Tower Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.towerHead.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }
}

