using RoadBattle.Physics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RoadBattle
{
    public class CarControllerScript : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        private VehicleStatsSO VehicleStats;
        [SerializeField, Range(0f,1f)]
        private float debugBraking = 0f;

        [Header("Controls")]
        [SerializeField]
        private InputAction SteerAction;
        [SerializeField]
        private InputAction Interact;

        // private varialbes
        private float deltaTime;
        private float passedTime;

        //models
        private SpeedModel speedModel;
        private AccelerationModel accelerationModel;

        void Start()
        {
            SteerAction = InputSystem.actions.FindAction("Move");
            Interact = InputSystem.actions.FindAction("Interact");

            // model initialization
            speedModel = new SpeedModel(VehicleStats);
            accelerationModel = new AccelerationModel(VehicleStats);
        }

        void Update()
        {
            deltaTime = Time.deltaTime;
            accelerationModel.DoLogic(debugBraking);

            speedModel.DoLogic(deltaTime, accelerationModel.CurrentAcceleration);

            passedTime += deltaTime;

            Debug.Log($"Curr Speed: {speedModel.CurrentSpeed} Acc:{accelerationModel.CurrentAcceleration} Time: {passedTime}");
        }
    } 
}
