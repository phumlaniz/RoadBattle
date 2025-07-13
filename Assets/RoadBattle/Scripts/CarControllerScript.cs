using RoadBattle.Physics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RoadBattle
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]
    public class CarControllerScript : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        private VehicleStatsSO VehicleStats;
        [SerializeField, Range(0f, 1f)]
        private float debugBraking = 0f;

        [Header("Controls")]
        [SerializeField]
        private InputAction SteerAction;
        [SerializeField]
        private InputAction Interact;

        // private variables
        private float deltaTime;
        private float passedTime;
        private Rigidbody rb;

        //models
        private SpeedModel speedModel;
        private AccelerationModel accelerationModel;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            SteerAction = InputSystem.actions.FindAction("Move");
            Interact = InputSystem.actions.FindAction("Interact");

            // model initialization
            speedModel = new SpeedModel(VehicleStats);
            accelerationModel = new AccelerationModel(VehicleStats);
        }

        void FixedUpdate()
        {
            deltaTime = Time.fixedDeltaTime;
            accelerationModel.DoLogic(debugBraking);

            speedModel.DoLogic(deltaTime, accelerationModel.CurrentAcceleration);
            transform.position += new Vector3(0f, 0f, speedModel.CurrentSpeed * deltaTime);

            passedTime += deltaTime;

            Debug.Log($"Curr Speed: {speedModel.CurrentSpeed} Acc:{accelerationModel.CurrentAcceleration} Time: {passedTime}");
        }
    }
}