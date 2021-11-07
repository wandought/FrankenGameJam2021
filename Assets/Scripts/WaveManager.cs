using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Path))]
public class WaveManager : MonoBehaviour
{
    private static WaveManager instance;
    public static WaveManager Instance { get { return instance; } }

    [SerializeField] private List<Enemy> enemyTypes;
    [SerializeField] private List<int> enemyCounts;
    [SerializeField] private float enemySpawnDelay = 1f;
    private Enemy[] wave;
    private int nextWave = 0;
			public int NextWave
			{
						get { return nextWave; }
						private set { nextWave = value; }
			}

    private bool stillSpawning = false;
    private bool ready = false;
    private bool active = false;
    public bool Active { get { return active; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        //count how many enemys are needed
        int max = 0;
        foreach (int i in enemyCounts)
            if (i > max)
                max = i;

        wave = new Enemy[max];

        //create all enemys at the start for better performance
        for (int i = 0; i < max; i++)
        {
            wave[i] = Instantiate(enemyTypes[0],
                            Path.Instance.First.transform.position,
                            Quaternion.identity,
                            Administrator.Instance.RuntimeParent);

            wave[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (stillSpawning)
            return;

        if (active)
        {
            foreach (Enemy e in wave)
            {
                if (e.gameObject.activeInHierarchy)
                    return;
            }
            active = false;
            ready = false;
        }

        if (ready)
        {
            StartCoroutine(TriggerWave());
            ready = false;
        }
    }

    private IEnumerator TriggerWave()
    {
        if (nextWave < enemyCounts.Count)
        {
            stillSpawning = true;

            for (int i = 0; i < enemyCounts[nextWave]; i++)
            {
                active = true;
                wave[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(enemySpawnDelay);
            }
            nextWave++;
            stillSpawning = false;
        }
    }

    public void SetReady()
    {
        ready = true;
    }
}
