using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    public IEnumerator Move()
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
    }
}
