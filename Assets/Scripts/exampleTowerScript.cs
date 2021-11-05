using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleTowerScript : MonoBehaviour
{

  [SerializeField] private Vector3[] bulletSpawns;
  [SerializeField] private GameObject projectile;
  [SerializeField] private float shootEveryXSeconds;
  private float counter;

  // Start is called before the first frame update
  void Start()
  {
    if (bulletSpawns == null)
    {
      Debug.Log("ERROR: bulletSpawns[] empty!");
    }

    counter = shootEveryXSeconds;

  }

  // Update is called once per frame
  void Update()
  {
    counter -= Time.deltaTime;
    if (counter <= 0f)

      foreach (Vector3 bs in bulletSpawns)
      {
        Instantiate(projectile, bs, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
      }
    counter += shootEveryXSeconds;
  }
}

