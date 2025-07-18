using RoadBattle.Physics;
using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Cinemachine.AxisState;

namespace RoadBattle
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]

    public class EnemyCarController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        private float Speed = 90f;

        // private variables
        private float deltaTime;
        private Vector2 steerAction;
        private Vector3 vehiclePosition;


        internal void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public void SetPosition(Vector3 position)
        {
            vehiclePosition = position;
        }

        void FixedUpdate()
        {
            deltaTime = Time.fixedDeltaTime;
            vehiclePosition += new Vector3(0f, 0f, Speed * deltaTime);
            transform.position = vehiclePosition;
        }
    }
}