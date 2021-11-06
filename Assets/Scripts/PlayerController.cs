using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed = 0.04f;

	// Update is called once per frame
	void Update()
	{
		float verticalMovement = Input.GetAxisRaw("Vertical") * movementSpeed;
		float horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
		
		transform.Translate(new Vector3(horizontalMovement, 0f, verticalMovement));

		if (Input.GetKeyDown(KeyCode.R))
			WaveManager.Instance.SetReady();
	}
}
