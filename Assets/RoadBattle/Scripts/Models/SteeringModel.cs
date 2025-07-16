using RoadBattle;
using System;
using UnityEditor.Toolbars;
using UnityEngine;

namespace RoadBattle
{
    public class SteeringModel
    {
        private float steerStrength = 0.0f;
        private float prevTargetSteerValue = 0.0f;
        private float targetSteerValue = 0.0f;
        private float prevSteerValue = 0.0f;
        private float steerThreshold = 0.05f;
        private bool isTransitioningToLane = false;

        private SteerDirection prevSteerDirection;

        public SteeringModel(VehicleStatsSO vehicleStats, float steerThreshold = 0.05f)
        {
            this.steerStrength = vehicleStats.SteerStrength;
            this.steerThreshold = steerThreshold;
        }

        public void DoLogic(float deltaTime, float steeringInput)
        {
            if (Mathf.Abs(steeringInput) > steerThreshold)
            {
                if ((steeringInput > 0))
                {
                    SteerDirection = SteerDirection.Right;
                }
                else
                {
                    SteerDirection = SteerDirection.Left;
                }
            }
            else
            {
                SteerDirection = SteerDirection.None;
            }
        }

        public void RuntimeSpecUpdates(VehicleStatsSO vehicleStats)
        {
            steerStrength = vehicleStats.SteerStrength;
        }

        public SteerDirection SteerDirection
        {
            get;
            private set;
        }
    }

    public enum SteerDirection
    {
        None,
        Left,
        Right
    }
}