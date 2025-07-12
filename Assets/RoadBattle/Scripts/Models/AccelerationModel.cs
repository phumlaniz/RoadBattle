using System;
using UnityEngine;

namespace RoadBattle.Physics
{
    public class AccelerationModel
    {
        private float acceleration;
        private float deceleration;

        /// <summary>
        /// Brake model decides how much deceleration will be applied based on applied brake factor strength
        /// </summary>
        /// <param name="acceleration">Acceleration worked out as a = (v-u)/t => (120km/h - 0km/h)/14 for corrolla</param>
        /// <param name="deceleration">The amount of strength required to reduce speed(km/h), calculated using v^2 = u^2 + 2*a*s for corrolla</param>
        public AccelerationModel(VehicleStatsSO vehicleStats)
        {            
            this.acceleration = vehicleStats.Acceleration;
            this.deceleration = vehicleStats.Deceleration;
        }

        public void DoLogic(float appliedBrakeFactor)
        {
            CurrentAcceleration = 0f;
            if (appliedBrakeFactor <= 0)
            {
                CurrentAcceleration = acceleration;
            }
            else
            {
                CurrentAcceleration = appliedBrakeFactor * deceleration;
            }
        }

        public float CurrentAcceleration
        {
            get;
            private set;
        }
    }
}