using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Path))]
public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    private struct EnemyList
    {
        public List<bool> list;
    }
    private static WaveManager instance;
    public static WaveManager Instance { get { return instance; } }

    [SerializeField] private List<Enemy> enemyTypes;
    [SerializeField] [Tooltip("Each Element represents a Wave.\n" +
        "Each List the enemys that spawn in order.\n" + 
        "true = drone, false = truck")]
    private List<EnemyList> waveComposition;
    [SerializeField] private float enemySpawnDelay = 1f;
    private Enemy[] wave;
    private int nextWave = 0;
    public int NextWave { get { return nextWave; } }

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
        int maxDrones = 0, maxTrucks = 0, drones = 0, trucks = 0;
        foreach (EnemyList l in waveComposition)
        {
            drones = trucks = 0;
            foreach (bool t in l.list)
            {
                if (t)
                    trucks++;
                else
                    drones++;
            }
            if (trucks > maxTrucks)
                maxTrucks = trucks;
            if (drones > maxDrones)
                maxDrones = drones;
        }

        wave = new Enemy[maxDrones + maxTrucks];

        //create all enemys at the start for better performance
        for (int i = 0; i < maxDrones; i++)
        {
            wave[i] = Instantiate(enemyTypes[0],
                Path.Instance.First.transform.position,
                Quaternion.identity,
                Administrator.Instance.RuntimeParent);

            wave[i].gameObject.SetActive(false);
        }
        for (int i = wave.Length - 1; i > wave.Length - maxTrucks - 1; i--)
        {
            wave[i] = Instantiate(enemyTypes[1],
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
        if (nextWave < waveComposition.Count)
        {
            stillSpawning = true;
            int activeTrucks = 0, activeDrones = 0;
            foreach (bool t in waveComposition[nextWave].list)
            {
                active = true;
                if (t)
                {
                    wave[wave.Length - 1 - activeTrucks].gameObject.SetActive(true);
                    activeTrucks++;
                }
                else
                {
                    wave[activeDrones].gameObject.SetActive(true);
                    activeDrones++;
                }
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
