using RoadBattle.Physics;
using System.Collections;
using System.ComponentModel;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RoadBattle
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]
    public class CarControllerScript : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        private VehicleStatsSO vehicleStats;

        // private variables
        private float deltaTime;
        private float passedTime;
        private float stepFactor;
        private Rigidbody rb;
        private InputSystem_Actions inputActions;
        private InputAction SteerAction;
        private InputAction Interact;

        private Vector2 steerAction;
        private Vector3 vehiclePosition;
        private Lane CurrentLane;
        private Lane PreviousLane;
        private SteerDirection previousSteerDirection;

        //models
        private SpeedModel speedModel;
        private AccelerationModel accelerationModel;
        private SteeringModel steeringModel;

        private float MinSteeringExent;
        private float MaxSteeringExtent;

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
            speedModel = new SpeedModel(vehicleStats);
            accelerationModel = new AccelerationModel(vehicleStats);
            steeringModel = new SteeringModel(vehicleStats, steerThreshold: 0.05f, isLinearlySteering: true);
            if (CurrentLane == null)
            {
                CurrentLane = GameManager.Instance.SpwanLane;
            }
            transform.position = new Vector3(CurrentLane.LanePosition, transform.position.y, transform.position.z);
            vehiclePosition = transform.position;
            foreach (var lane in GameManager.Instance.CurrentLanes)
            {
                MinSteeringExent = 0;
                MaxSteeringExtent = 0;
                if (lane.transform.position.x < MinSteeringExent)
                {
                    MinSteeringExent = transform.position.x;
                }
                if (lane.transform.position.x > MaxSteeringExtent)
                {
                    MinSteeringExent = transform.position.x;
                }
            }
        }

        private void Update()
        {
            steerAction = SteerAction.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            deltaTime = Time.fixedDeltaTime;

            accelerationModel.DoLogic(steerAction.y * -1f);
            accelerationModel.RuntimeSpecUpdates(vehicleStats);

            speedModel.DoLogic(deltaTime, accelerationModel.CurrentAcceleration);
            speedModel.RuntimeSpecUpdates(vehicleStats);

            steeringModel.DoLogic(deltaTime, steerAction.x);
            steeringModel.RuntimeSpecUpdates(vehicleStats);

            vehiclePosition += new Vector3(0f, 0f, speedModel.CurrentSpeed * deltaTime);

            if (PreviousLane != null && CurrentLane != null && !steeringModel.IsLinearlySteering)
            {
                stepFactor += deltaTime * vehicleStats.SteerStrength;
                vehiclePosition.x = Mathf.Lerp(transform.position.x, CurrentLane.LanePosition, stepFactor);
            }
            if (steeringModel.IsLinearlySteering)
            {
                vehiclePosition.x += steeringModel.Steering;
                //vehiclePosition.x = Mathf.Clamp(vehiclePosition.x , MinSteeringExent, MaxSteeringExtent);
            }

            transform.position = vehiclePosition;

            passedTime += deltaTime;
            if (previousSteerDirection != steeringModel.SteerDirection)
            {
                switch (steeringModel.SteerDirection)
                {
                    case SteerDirection.Right:
                        if (CurrentLane.HasRightLane)
                        {
                            PreviousLane = CurrentLane;
                            CurrentLane = CurrentLane.RightLane;
                            stepFactor = 0;
                        }
                        else
                        {
                            //crashing to the bounds
                        }
                        break;

                    case SteerDirection.Left:
                        if (CurrentLane.HasLeftLane)
                        {
                            PreviousLane = CurrentLane;
                            CurrentLane = CurrentLane.LeftLane;
                            stepFactor = 0;
                        }
                        else
                        {
                            //crashing to the bounds
                        }
                        break;
                }
            }
            previousSteerDirection = steeringModel.SteerDirection;
        }

        public VehicleStatsSO VehicleStats
        {
            get
            {
                return vehicleStats;
            }
        }
    }
}