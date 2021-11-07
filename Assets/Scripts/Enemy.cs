using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health health;
    public float Health { get { return health.CurrentHealth; } }
    private float speed = 2f;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private IEnumerator Move()
    {
        foreach (Transform trans in Path.Instance.GetPath())
        {
            Vector3 start_pos = transform.position;
            float travelPercent = 0f;

            transform.LookAt(trans.transform.position);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(start_pos, trans.transform.position, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        Player.Instance.Health = 0;
        this.gameObject.SetActive(false);
        transform.position = Path.Instance.First.position;
    }

    private void OnEnable()
    {
        transform.position = Path.Instance.First.position;
        this.GetComponent<Health>().ResetHealth();
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopCoroutine(Move());
    }
}
