using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleBulletScript : MonoBehaviour
{
			[SerializeField] private float bulletSpeed;
			[SerializeField] private float dieAfterXSeconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
						this.transform.position -= transform.forward * bulletSpeed * Time.deltaTime;

						dieAfterXSeconds -= Time.deltaTime;
						if (dieAfterXSeconds <= 0f)
						{
									Destroy(this.transform.gameObject);
						}
				}
}
