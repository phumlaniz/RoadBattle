using System.ComponentModel;
using UnityEngine;

namespace RoadBattle
{
    [CreateAssetMenu(fileName = "VehicleStatsSO", menuName = "Scriptable Objects/VehicleStatsSO")]
    public class VehicleStatsSO : ScriptableObject
    {
        [SerializeField]
        private float finalSpeed;
        [SerializeField, Tooltip("Acceleration worked out as a = (v-u)/t => (120km/h - 0km/h)/14 for corrolla")]
        private float acceleration = (120f - 0f) / 14f;
        [SerializeField, Tooltip("The amount of strength required to reduce speed(km/h), calculated using v^2 = u^2 + 2*a*s for corrolla")]
        private float deceleration = (0f * 0f - 120f * 120f) / (2f * 100f);
        [SerializeField]
        private float steerStrength;
        [SerializeField]
        private float mass;

        public float FinalSpeed
        {
            get
            {
                return finalSpeed;
            }
        }

        public float Acceleration
        {
            get
            {
                return acceleration;
            }
        }

        public float Deceleration
        {
            get
            {
                return deceleration;
            }
        }

        public float Mass
        {
            get
            {
                return mass;
            }
        }

        public float SteerStrength
        {
            get
            {
                return steerStrength;
            }
        }
    }
}