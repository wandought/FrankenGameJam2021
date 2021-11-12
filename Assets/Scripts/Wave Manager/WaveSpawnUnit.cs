using UnityEngine;

[System.Serializable]
public class WaveSpawnUnit
{
			[Tooltip("Matching int to Units.cs prefabs")] public int unit;
			[Tooltip("Time it takes after the previous Unit to spawn this one")] public float spawnTime;

			public WaveSpawnUnit (int _unit, float _spawnTime)
			{
						unit = _unit;
						spawnTime = _spawnTime;
			}
			public WaveSpawnUnit()
			{
						unit = 0;
						spawnTime = 0f;
			}

}
