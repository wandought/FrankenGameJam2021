using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSceneScript : MonoBehaviour
{
			private static AudioSceneScript instance;

			[SerializeField] private AudioSource audioSource;
			[SerializeField] private AudioClip[] loops;
			[SerializeField, Tooltip("Has to be the same size as 'loops' and the last entry has to be unreachable(999)")] private int[] loopChangeAtWave;
			[SerializeField] private int counter = 0;
			public int waveNr = 0;

			private void Awake()
			{
						if (instance == null)
						{
									instance = this;
						}
						else
						{
									Destroy(gameObject);
						}
			}

			// Start is called before the first frame update
			void Start()
			{

			}

			// Update is called once per frame
			void Update()
			{
						if (ProperWaveManager.instance.waveCounter >= loopChangeAtWave[counter])
						{
									counter++;
									if (counter < loopChangeAtWave.Length)
									{
												audioSource.loop = false;
									}
						}

						if (!audioSource.isPlaying)
						{
									audioSource.clip = loops[counter];
									audioSource.Play();
									audioSource.loop = true;
						}


			}






}
