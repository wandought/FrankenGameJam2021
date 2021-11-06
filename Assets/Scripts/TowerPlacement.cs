using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class TowerPlacement : MonoBehaviour
{
			[SerializeField] private float placementRange = 20f;

			private GameObject previewTower = null;

			private bool placementMode = false;


			// replacing materials is pretty fucky wucky
			[SerializeField] private Material towerCanBePlaced;
			[SerializeField] private Material towerCanNotBePlaced;
			// You HAVE to pass an array into .sharedMaterials
			private Material[] towerHeadCanBePlacedMatCount;
			private Material[] towerHeadCanNotBePlacedMatCount;
			private Material[] towerStandCanBePlacedMatCount;
			private Material[] towerStandCanNotBePlacedMatCount;

			private float yRotationEuler = 0f;

			private void Start()
			{
						TowerSelection.CurTowerChangedEvent += new TowerSelection.CurTowerChangedDelegate(updateMesh);
			}

			private void Update()
			{
						if (Input.GetKeyDown(KeyCode.E))
						{
									DisplayPreview();
						}
						else if (placementMode && Input.GetMouseButtonDown(1))
						{

									RemovePreview();

									RemoveTower();

						}
						else if (placementMode && Input.GetMouseButtonDown(0))
						{
									PlaceTower();
						}

						if (placementMode)
						{
									if (Input.GetKeyDown(KeyCode.R))
									{
												yRotationEuler += 90f;
												previewTower.transform.rotation = Quaternion.Euler(0f, yRotationEuler, 0f);
									}
									PlacePreview();
						}
			}

			private void updateMesh(GameObject curTower)
			{
						Debug.Log("Trying to update mesh");
						if (placementMode)
						{

									Destroy(previewTower);
									DisplayPreview();



						}
			}

			private void PlacePreview()
			{


						Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
						Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

						Ray ray = Camera.main.ScreenPointToRay(mousePosition);
						RaycastHit hit;

						if (Physics.Raycast(ray, out hit))
						{
									int x = Mathf.RoundToInt(hit.point.x / 10);
									int z = Mathf.RoundToInt(hit.point.z / 10);

									x *= 10;
									z *= 10;
									int key = CoordinateCalculator.CoordinateToInt(x, z);
									Waypoint tile;
									if (!Administrator.Instance.waypoints.TryGetValue(key, out tile) || tile.IsPlaceable == true
												&& Vector2.Distance(playerPosition, new Vector2(x, z)) <= placementRange)

									{
												// Make Blue
												previewTower.transform.Find("TowerHead").GetComponent<MeshRenderer>().sharedMaterials = towerHeadCanBePlacedMatCount;
												previewTower.transform.Find("TowerBase").GetComponent<MeshRenderer>().sharedMaterials = towerStandCanBePlacedMatCount;
									}
									else
									{
												// Make Red
												previewTower.transform.Find("TowerHead").GetComponent<MeshRenderer>().sharedMaterials = towerHeadCanNotBePlacedMatCount;
												previewTower.transform.Find("TowerBase").GetComponent<MeshRenderer>().sharedMaterials = towerStandCanNotBePlacedMatCount;
									}
									// new pos

									previewTower.transform.position = new Vector3(x, 0f, z);
									
						}

			}

			private void DisplayPreview()
			{
						Debug.Log("Placement Mode on");
						placementMode = true;
						//TODO

						Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
						Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

						Ray ray = Camera.main.ScreenPointToRay(mousePosition);
						RaycastHit hit;

						if (Physics.Raycast(ray, out hit))
						{
									int x = Mathf.RoundToInt(hit.point.x / 10);
									int z = Mathf.RoundToInt(hit.point.z / 10);

									x *= 10;
									z *= 10;

									previewTower = Instantiate(Administrator.Instance.towerSelection.CurrentTower, new Vector3(x, hit.point.y, z), Quaternion.Euler(0f, yRotationEuler, 0f), null);

									foreach (Transform item in previewTower.transform.Find("TowerHead")) // < bad
									{
												item.GetComponent<ParticleSystem>().Stop();
									}
									towerHeadCanBePlacedMatCount = new Material[previewTower.transform.Find("TowerHead").GetComponent<MeshRenderer>().sharedMaterials.Length];
									towerHeadCanNotBePlacedMatCount = new Material[previewTower.transform.Find("TowerHead").GetComponent<MeshRenderer>().sharedMaterials.Length];
									towerStandCanBePlacedMatCount = new Material[previewTower.transform.Find("TowerBase").GetComponent<MeshRenderer>().sharedMaterials.Length];
									towerStandCanNotBePlacedMatCount = new Material[previewTower.transform.Find("TowerBase").GetComponent<MeshRenderer>().sharedMaterials.Length];
									for (int i = 0; i < towerHeadCanBePlacedMatCount.Length; i++)
									{
												towerHeadCanBePlacedMatCount[i] = towerCanBePlaced;
												towerHeadCanNotBePlacedMatCount[i] = towerCanNotBePlaced;
									}
									for (int i = 0; i < towerStandCanBePlacedMatCount.Length; i++)
									{
												towerStandCanBePlacedMatCount[i] = towerCanBePlaced;
												towerStandCanNotBePlacedMatCount[i] = towerCanNotBePlaced;
									}
						}




			}

			private void RemovePreview()
			{
						Debug.Log("Placement Mode off");
						placementMode = false;
						Destroy(previewTower);
						yRotationEuler = 0f;
			}

			private void PlaceTower()
			{

						Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
						Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

						Ray ray = Camera.main.ScreenPointToRay(mousePosition);
						RaycastHit hit;

						if (Physics.Raycast(ray, out hit))
						{
									if (Vector2.Distance(playerPosition, new Vector2(hit.point.x, hit.point.z)) <= placementRange)
									{
												Administrator.Instance.PlaceTower(hit.point, new Vector3(0f, yRotationEuler, 0f));
												yRotationEuler = 0f;
												RemovePreview();
									}
						}
			}

			private void RemoveTower()
			{
						Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
						Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

						Ray ray = Camera.main.ScreenPointToRay(mousePosition);
						RaycastHit hit;
						if (Physics.Raycast(ray, out hit))
									Administrator.Instance.RemoveTower(hit.point);
			}



			void OnDrawGizmosSelected()
			{
						Gizmos.color = new Color(0f, 0f, 1f, 0.2f);
						Gizmos.DrawSphere(transform.position, placementRange);
			}
}
