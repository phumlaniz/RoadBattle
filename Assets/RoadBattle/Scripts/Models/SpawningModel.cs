using System;
using UnityEngine;

namespace RoadBattle
{
	[Serializable]
	public class SpawningModel
	{
		[SerializeField]
		private EnemyCarController regularEnemyCar;
		[SerializeField, Range(0.5f, 0.9f)]
		private float UpperSpeedRangeThreshold = 0.75f;
        [SerializeField, Range(0.1f, 0.49f)]
        private float LwerSpeedRangeThreshold = 0.75f;

        private Vector3 spawnPosition;

		public void SpawnRegularCarAt(float distanceToSpawnAt, VehicleStatsSO vehicleSO)
		{
			int spawnLaneIndex = UnityEngine.Random.Range(0, GameManager.Instance.CurrentLanes.Length);
			Lane spawnLane = GameManager.Instance.CurrentLanes[spawnLaneIndex];
			spawnPosition = spawnLane.transform.position;
			spawnPosition.y = GameManager.Instance.MainCar.transform.position.y;
            spawnPosition.z = GameManager.Instance.MainCar.transform.position.z;
			spawnPosition.z += distanceToSpawnAt;
			EnemyCarController regularSpawnCar = MonoBehaviour.Instantiate(regularEnemyCar);
			regularSpawnCar.SetPosition(spawnPosition);

			regularEnemyCar.SetSpeed(vehicleSO.FinalSpeed * UnityEngine.Random.Range(LwerSpeedRangeThreshold, UpperSpeedRangeThreshold));
        }
	} 
}
