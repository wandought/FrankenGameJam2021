using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> path;

    private static Path instance;
    public static Path Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public Transform First
    {
        get
        {
            if (!IsEmpty)
                return path[0];
            else
                return null;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return path.Count == 0;
        }
    }

    public List<Transform> GetPath()
    {
        return path;
    }
}
