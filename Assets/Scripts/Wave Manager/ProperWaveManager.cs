using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProperWaveManager : MonoBehaviour
{
			[SerializeField] private TextAsset taWaves; // Textdocument with all waves
			[SerializeField] private GameObject wavePrefab;
			[SerializeField] private GameObject waveHolder; // Empty in Scene which will store the instantiated "wavePrefabs"
			public GameObject unitHolder; // Empty in Scene which will store all units. Gets used by Wave.cs when instantiating Units

			public static ProperWaveManager instance;

			public UnityEvent m_StartWaveEvent;
			public UnityEvent m_EndWaveEvent; // Currently does not get invoked. Kind off useless when each wave has it's own Event when it ends

			[SerializeField] private List<Wave> waves;
			public int waveCounter = 0;

			private void Awake()
			{
						if (instance != null && instance != this) Destroy(this.gameObject);
						else instance = this;

						waves = new List<Wave>();

						if (m_StartWaveEvent == null)
						{
									m_StartWaveEvent = new UnityEvent();
									m_StartWaveEvent.AddListener(StartWave);
						}
						if (m_EndWaveEvent == null)
						{
									m_EndWaveEvent = new UnityEvent();
									m_EndWaveEvent.AddListener(EndWave);
						}

			}

			// Start is called before the first frame update
			void Start()
			{
						// Preloads all waves (not good with dozens, if not hundreds of waves)
						SetupWaveList();


			}

			private void SetupWaveList()
			{
						Wave dummyWave;
						int waveUnitCounter = 0;
						int waveCounter = 0;
						bool toggle = true;

						foreach (var line in taWaves.text.SplitToLines())
						{
									dummyWave = Instantiate(wavePrefab, waveHolder.transform).GetComponent<Wave>();
									waveUnitCounter = 0;

									foreach (var item in line.Split(';'))
									{
												if (toggle)
												{
															//Debug.Log("INT");
															dummyWave.waveUnits.Add(new WaveSpawnUnit());
															dummyWave.waveUnits[waveUnitCounter].unit = Convert.ToInt32(item);
															toggle = false;

												}
												else
												{
															//Debug.Log("FLOAT");
															dummyWave.waveUnits[waveUnitCounter].spawnTime = float.Parse(item);
															toggle = true;
															waveUnitCounter++;
												}
									}

									waves.Add(dummyWave);
									waveCounter++;
						}


			}

			// Update is called once per frame
			void Update()
			{
						if (Input.GetKeyDown(KeyCode.R))
						{
									Debug.Log("ProperWaveManager Registered R");

									// Only needed with the other wave loading method
									StartWave();

									// Only load the wave that needs to start | TODO: fix loading error
								 //StartNextWave();

						}
			}

			// Todo: creates empty wave elements when no more lines are ready, also skips wave 0, problem with "GetSpecificLine(int)"
			private void StartNextWave()
			{
						bool toggle = true;
						int waveUnitCounter = 0;
						string line = "";
						Wave nextWave = Instantiate(wavePrefab, waveHolder.transform).GetComponent<Wave>();
	
						foreach (var item in taWaves.text.GetSpecificLine(waveCounter))
						{
									line = item;
						}
						Debug.Log(line);


						foreach (var item in line.Split(';'))
						{
									Debug.Log(item);
									if (toggle)
									{
												//Debug.Log("INT");
												nextWave.waveUnits.Add(new WaveSpawnUnit());
												nextWave.waveUnits[waveUnitCounter].unit = Convert.ToInt32(item);
												toggle = false;

									}
									else
									{
												//Debug.Log("FLOAT");
												nextWave.waveUnits[waveUnitCounter].spawnTime = float.Parse(item);
												toggle = true;
												waveUnitCounter++;
									}


						}
						nextWave.StartWave();
						waveCounter++;
			}



			private void StartWave()
			{
						if (waves.Count == 0 || waves == null)
						{
									Debug.Log("No more waves");
									return;
						}
						else
						{
									waves[0].StartWave();
									waves.RemoveAt(0);
						}
			}

			private void EndWave()
			{

			}






}
