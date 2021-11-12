using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProperWaveManager : MonoBehaviour
{
			[SerializeField] private TextAsset taWaves;
			[SerializeField] private GameObject wavePrefab;
			[SerializeField] private GameObject waveHolder;

			public static ProperWaveManager instance;

			public UnityEvent m_StartWaveEvent;
			public UnityEvent m_EndWaveEvent;

			[SerializeField] private List<Wave> waves;
			private int waveCounter = 0;

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
												Debug.Log(item);
												if (toggle)
												{
															Debug.Log("INT");
															dummyWave.waveUnits.Add(new WaveSpawnUnit());
															dummyWave.waveUnits[waveUnitCounter].unit = Convert.ToInt32(item);
															toggle = false;

												}
												else
												{
															Debug.Log("FLOAT");
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
						if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Q))
						{
									Debug.Log("ProperWaveManager Registered R");

									StartWave();
									//StartNextWave();

						}
			}

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
												Debug.Log("INT");
												nextWave.waveUnits.Add(new WaveSpawnUnit());
												nextWave.waveUnits[waveUnitCounter].unit = Convert.ToInt32(item);
												toggle = false;

									}
									else
									{
												Debug.Log("FLOAT");
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
						waves[0].StartWave();
						waves.RemoveAt(0);
			}

			private void EndWave()
			{

			}






}
