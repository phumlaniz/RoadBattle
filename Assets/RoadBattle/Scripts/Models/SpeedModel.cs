using System;
using UnityEngine;

namespace RoadBattle.Physics
{
	/// <summary>
	/// Speed model, units used are metric
	/// </summary>
	public class SpeedModel
	{
		private float finalSpeed;
		private float acceleration;

		public SpeedModel(float finalSpeed, float acceleration)
		{
			this.finalSpeed = finalSpeed;
			this.acceleration = acceleration;
		}

		public void DoLogic(float deltaTime, float currentBrakeStrength)
		{
			CalculateCurrentSpeed(deltaTime, acceleration - currentBrakeStrength);
		}

        private void CalculateCurrentSpeed(float deltaTime, float currentAcceleration)
        {
            // using equation of motion v = u + a*t
            CurrentSpeed = CurrentSpeed + currentAcceleration * deltaTime;
			CurrentSpeed = Math.Clamp(CurrentSpeed, 0, finalSpeed);
        }

        public float CurrentSpeed
		{
			get;
			private set;
		}
	} 
}
