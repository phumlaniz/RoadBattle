using System;
using UnityEngine;

namespace RoadBattle
{
	[Serializable]
	public class BoundArea
	{
		[SerializeField]
		private Transform minBound;
		[SerializeField]
		private Transform maxBound;

		public Transform MinBound
		{
			get
			{
				return minBound;
			}
		}

		public Transform MaxBound
		{
			get
			{
				return maxBound;

			}
		}
	}
}