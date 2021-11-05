using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{

    [SerializeField] private GameObject[] bulletSpawner;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootEveryXSeconds;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        if (bulletSpawner == null)
        {
            Debug.Log("ERROR: bulletSpawns[] empty!");
        }

        counter = shootEveryXSeconds;

    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter >= 0f)
            return;

        foreach (GameObject spawner in bulletSpawner)
        {
            Instantiate(projectile, spawner.transform.position, spawner.transform.rotation);
            counter += shootEveryXSeconds;
        }

    }
}

