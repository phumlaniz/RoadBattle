using System;
using UnityEngine;

namespace RoadBattle
{
	[Serializable]
	public class SpawningModel
	{
		[SerializeField]
		private EnemyCarController regularEnemyCar;

		private Vector3 spawnPosition;

		public void SpawnRegularCarAt(float distanceToSpawnAt)
		{
			int spawnLaneIndex = UnityEngine.Random.Range(0, GameManager.Instance.CurrentLanes.Length);
			Lane spawnLane = GameManager.Instance.CurrentLanes[spawnLaneIndex];
			spawnPosition = spawnLane.transform.position;
			spawnPosition.y = GameManager.Instance.MainCar.transform.position.y;
            spawnPosition.z = GameManager.Instance.MainCar.transform.position.z;
			spawnPosition.z += distanceToSpawnAt;
			EnemyCarController regularSpawnCar = ScriptableObject.Instantiate(regularEnemyCar);
            regularSpawnCar.transform.position = spawnPosition;
            regularSpawnCar.gameObject.SetActive(true);
		}
	} 
}
