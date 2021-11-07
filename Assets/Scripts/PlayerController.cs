using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 10f;
	[SerializeField] private float rotationRate = 4.5f;
	[SerializeField] private float attackCooldown = 5f;
	[SerializeField] private ParticleSystem magic; 

	private TowerPlacement towerPlacement;
	private Rigidbody rb;
	private Vector3 moveInput;
	private bool moving = false;
	private float lastActivation = 0f;

    private void Start()
    {
		towerPlacement = GetComponent<TowerPlacement>();
		rb = GetComponent<Rigidbody>();
		moveInput = Vector3.zero;
    }

    private void Update()
	{
		moveInput.z = Input.GetAxisRaw("Vertical");
		moveInput.x = Input.GetAxisRaw("Horizontal");

		if (Mathf.Approximately(moveInput.z, 0f) && Mathf.Approximately(moveInput.x, 0f))
			moving = false;
		else
			moving = true;

		if (Input.GetKeyDown(KeyCode.R))
			WaveManager.Instance.SetReady();

		if (Input.GetKeyDown(KeyCode.Space))
        {
			if (Time.time - lastActivation > attackCooldown)
			{
				magic.Play();
				lastActivation = Time.time;
			}
        }
	}

    private void FixedUpdate()
    {
		if (!moving)
			rb.velocity = Vector3.zero;
		else
			rb.AddForce(moveInput * movementSpeed * Time.fixedDeltaTime);

		if (towerPlacement.PlacementMode)
        {
			//rotate towards mouse
			Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
			Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
			float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

			transform.rotation = Quaternion.Lerp(transform.rotation,
				Quaternion.Euler(new Vector3(0f, -angle - 90f, 0f)),
				Time.fixedDeltaTime * rotationRate);
		}
		else
        {
			if (!moving)
				return;

			transform.rotation = Quaternion.Lerp(transform.rotation,
				Quaternion.LookRotation(moveInput, Vector3.up),
				Time.fixedDeltaTime * rotationRate);
		}
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
	{
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
}
