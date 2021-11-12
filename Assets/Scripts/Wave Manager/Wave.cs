using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wave : MonoBehaviour
{

			public UnityEvent m_StartWaveEvent;
			public UnityEvent m_EndWaveEvent;

			public List<WaveSpawnUnit> waveUnits = new List<WaveSpawnUnit>();
			[SerializeField] private List<GameObject> myUnits = new List<GameObject>();

			private bool isRunning = false;
			private bool isSpawning = false;
			

			private void Awake()
			{
						//Debug.Log("Wave Awake func");

						if (m_StartWaveEvent == null)
						{
									m_StartWaveEvent = new UnityEvent();
						}
						if (m_EndWaveEvent == null)
						{
									m_EndWaveEvent = new UnityEvent();
						}
						m_StartWaveEvent.AddListener(OnStartWave);
						m_EndWaveEvent.AddListener(OnEndWave);
			}


			

			// Start is called before the first frame update
			void Start()
			{

			}

			// Update is called once per frame
			void Update()
			{
						if (isRunning)
						{
									WhileRunning();
						}
						if (isSpawning)
						{
									WhileSpawning();
						}
						if (isRunning && myUnits.Count == 0)
						{
									OnEndWave();
						}
			}

			/// <summary>
			/// while the wave is active execute this every frame
			/// </summary>
			private void WhileRunning()
			{

						
			}

			/// <summary>
			/// spawns units as long as there are some left
			/// </summary>
			private void WhileSpawning()
			{
						waveUnits[0].spawnTime -= Time.deltaTime;

						if (waveUnits[0].spawnTime <= 0f)
						{
									// TODO: PreInstatiate units to prevent lag. Overhault on units needed before implementation!!!
									myUnits.Add( Instantiate(Units.instance.UnitPrefabs[waveUnits[0].unit], Vector3.zero, Quaternion.identity, null)  );
									waveUnits.RemoveAt(0);
									
						}



						if (waveUnits.Count == 0)
						{
									isSpawning = false;
						}

			}


			private void OnStartWave()
			{
						isRunning = true;
						isSpawning = true;

			}

			private void OnEndWave()
			{



						Destroy(this.gameObject);
			}



			/// <summary>
			/// Starts the wave
			/// </summary>
			public void StartWave()
			{
						// Dont allow multiple starts
						if (isRunning)
						{
									return;
						}
						

						// Check if you have no units to spawn, if so immediately call finish
						if (waveUnits == null || waveUnits.Count == 0)
						{
									Debug.Log("Forced wave stop due to lack of Units");
									m_EndWaveEvent.Invoke();
									return;
						}
						
						m_StartWaveEvent.Invoke();
			}

}
