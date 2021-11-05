using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleTowerScript : MonoBehaviour
{

			[SerializeField] private GameObject[] bulletSpawns;
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

									foreach (GameObject bs in bulletSpawns)
									{
												Debug.Log("hallo");
												Instantiate(projectile, bs.transform.position, this.transform.rotation);
												counter += shootEveryXSeconds;
									}
						
			}
}

