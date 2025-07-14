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

        private InputSystem_Actions inputActions;
        private Vector2 steerAction;


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

            Interact.started += Interact_performed;
        }

        private void Interact_performed(InputAction.CallbackContext context)
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
        }

        private void Update()
        {
            steerAction = SteerAction.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            deltaTime = Time.fixedDeltaTime;
            accelerationModel.DoLogic(appliedBrakeFactor: 0.0f);

            speedModel.DoLogic(deltaTime, accelerationModel.CurrentAcceleration);
            speedModel.RuntimeSpecUpdates(VehicleStats);
            transform.position += new Vector3(0f, 0f, speedModel.CurrentSpeed * deltaTime);

            passedTime += deltaTime;

            //Debug.Log($"Curr Speed: {speedModel.CurrentSpeed} Acc:{accelerationModel.CurrentAcceleration} Time: {passedTime}");
        }
    }
}