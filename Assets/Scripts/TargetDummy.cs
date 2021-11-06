using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Owwwww :(");
        Destroy(this.gameObject);
    }
}
