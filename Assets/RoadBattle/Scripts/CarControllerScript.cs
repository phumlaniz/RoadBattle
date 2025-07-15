using RoadBattle.Physics;
using System.Collections;
using System.ComponentModel;
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

        // private variables
        private float deltaTime;
        private float passedTime;
        private Rigidbody rb;
        private InputSystem_Actions inputActions;
        private InputAction SteerAction;
        private InputAction Interact;

        private Vector2 steerAction;
        private Vector3 vehiclePosition;
        private Lane CurrentLane;
        private SteerDirection previousSteerDirection;

        //models
        private SpeedModel speedModel;
        private AccelerationModel accelerationModel;
        private SteeringModel steeringModel;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            SteerAction = inputActions.Player.Move;
            Interact = inputActions.Player.Interact;

            SteerAction.Enable();
            Interact.Enable();

            Interact.started += Interact_started; ;
        }

        private void Interact_started(InputAction.CallbackContext context)
        {
            Debug.Log("Interact performed!");
        }

        private void OnDisable()
        {
            SteerAction.Disable();
            Interact.Disable();
        }

        void Start()
        {
            // model initialization
            speedModel = new SpeedModel(VehicleStats);
            accelerationModel = new AccelerationModel(VehicleStats);
            steeringModel = new SteeringModel(VehicleStats);
            if (CurrentLane == null)
            {
                CurrentLane = GameManager.Instance.SpwanLane;
            }
            transform.position = new Vector3(CurrentLane.LanePosition, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            steerAction = SteerAction.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            deltaTime = Time.fixedDeltaTime;

            accelerationModel.DoLogic(steerAction.y * -1f);
            accelerationModel.RuntimeSpecUpdates(VehicleStats);

            speedModel.DoLogic(deltaTime, accelerationModel.CurrentAcceleration);
            speedModel.RuntimeSpecUpdates(VehicleStats);

            steeringModel.DoLogic(deltaTime, steerAction.x, CurrentLane);
            steeringModel.RuntimeSpecUpdates(VehicleStats);


            transform.position += new Vector3(steerAction.x * VehicleStats.SteerStrength * deltaTime, 0f, speedModel.CurrentSpeed * deltaTime);

            passedTime += deltaTime;
            if (previousSteerDirection != steeringModel.SteerDirection)
            {
                switch (steeringModel.SteerDirection)
                {
                    case SteerDirection.Right:
                        if (CurrentLane.RightLane != null)
                        {
                            CurrentLane = CurrentLane.RightLane;
                        }
                        break;

                    case SteerDirection.Left:
                        if (CurrentLane.LeftLane != null)
                        {
                            CurrentLane = CurrentLane.LeftLane;
                        }
                        break;
                }
            }
            previousSteerDirection = steeringModel.SteerDirection;
        }
    }
}