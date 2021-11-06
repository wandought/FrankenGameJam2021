using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Path))]
public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyTypes;
    [SerializeField] private List<Enemy> wave;

    private bool ready = true;
    private bool active = false;
    public bool Active { get { return active; } }

    private void Start()
    {
        if (!ready)
            return;

        if (ready)
        {
            TriggerWave();
            ready = false;
        }
    }

    private void TriggerWave()
    {
        wave.Add(Instantiate(enemyTypes[0],
            Path.Instance.First.transform.position,
            Quaternion.identity,
            Administrator.Instance.RuntimeParent));

        StartCoroutine(wave[0].Move());
    }

    public void SetReady()
    {
        ready = true;
    }
}
