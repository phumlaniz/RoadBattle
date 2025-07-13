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

		public SpeedModel(VehicleStatsSO vehicleStats)
		{
			this.finalSpeed = vehicleStats.FinalSpeed;
			this.acceleration = vehicleStats.Acceleration;
		}

        public void DoLogic(float deltaTime, float currentBrakeStrength)
        {
            CurrentSpeedCalculator(deltaTime, currentBrakeStrength);
        }

        public void RuntimeSpecUpdates(VehicleStatsSO vehicleStats)
        {
            this.finalSpeed = vehicleStats.FinalSpeed;
            this.acceleration = vehicleStats.Acceleration;
        }

        public float CurrentSpeed
        {
            get;
            private set;
        }

        private void CurrentSpeedCalculator(float deltaTime, float currentAcceleration)
        {
            float currentSpeed;
            // using equation of motion v = u + a*t
            currentSpeed = CurrentSpeed + currentAcceleration * deltaTime;
            CurrentSpeed = Math.Clamp(currentSpeed, 0, finalSpeed);
        }
    } 
}
