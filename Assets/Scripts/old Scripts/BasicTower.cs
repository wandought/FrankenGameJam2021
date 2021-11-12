using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    public float DegreesPerSecond = 0f;

    public float DamagePerBullet = 5;

    public int Price = 350;

    private GameObject towerHead;

			private void Awake()
			{
						this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
			}

			// Start is called before the first frame update
			void Start()
    {
        this.towerHead = this.transform.Find("TowerHead").gameObject;
    }

    // Update is called once per frame
    void Update()   
    {
        this.towerHead.transform.Rotate(0, DegreesPerSecond * Time.deltaTime, 0, Space.World);
    }
}

