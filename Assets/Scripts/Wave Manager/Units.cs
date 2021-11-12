using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
			public static Units instance;

			private void Awake()
			{
						if (instance != null && instance != this) Destroy(this.gameObject);
						else instance = this;
			}

			// ReadOnly list of all unit prefabs
			[SerializeField] private List<GameObject> unitPrefabs;
			public List<GameObject> UnitPrefabs
			{
						get { return unitPrefabs; }

						private set { unitPrefabs = value; }
			}



}
