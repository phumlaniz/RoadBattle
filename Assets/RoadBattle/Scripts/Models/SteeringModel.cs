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

        public void DoLogic(float deltaTime, float steeringInput, Lane currentLane)
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

            if (SteerDirection != prevSteerDirection)
            {
                switch (SteerDirection)
                {
                    case SteerDirection.Left:
                        if (currentLane.HasLeftLane)
                        {
                            targetSteerValue = currentLane.LeftLanePosition;
                            prevSteerValue = currentLane.LanePosition;
                            isTransitioningToLane = true;
                        }
                        break;
                    case SteerDirection.Right:
                        if (currentLane.HasRightLane)
                        {
                            targetSteerValue = currentLane.RightLanePosition;
                            prevSteerValue = currentLane.LanePosition;
                            isTransitioningToLane = true;
                        }
                        break;
                }
            }
            prevSteerDirection = SteerDirection;
            if (isTransitioningToLane)
            {
                CurrentSteeringDeflection = Mathf.Lerp(prevSteerValue, targetSteerValue, steerStrength * deltaTime);
            }
            if(CurrentSteeringDeflection == targetSteerValue)
            {
                isTransitioningToLane = false;
            }
        }

        public void RuntimeSpecUpdates(VehicleStatsSO vehicleStats)
        {
            steerStrength = vehicleStats.SteerStrength;
        }

        public float CurrentSteeringDeflection
        {
            get;
            private set;
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