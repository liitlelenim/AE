using AE.Manager;
using AE.Manager.Locator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        
        [SerializeField] private float movementSpeed = 7.5f;

        [SerializeField] private float minFallingSpeed = 9f;
        [SerializeField] private float maxFallingSpeed = 25f;

        private CharacterController _characterController;
        private InputManager _inputManager;
        
        private Vector2 _movementDirectionInput;
        private float _currentFallingSpeed;
        

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _inputManager = ManagersLocator.Instance.GetManager<InputManager>();
        }
        

        // It's worth noting that the Character Controller is not based on rigidbody
        // so the movement using it should be performed in the "Update"
        private void Update()
        {
            ReadInput();
            _characterController.Move(GetRelativeMovementDirection(_movementDirectionInput) *
                                      (movementSpeed * Time.deltaTime));

            HandleGravity();
        }

        private void ReadInput()
        {
            _movementDirectionInput = _inputManager.MoveInput;
        }

        private void HandleGravity()
        {
            if(_characterController.isGrounded)
            {
                _currentFallingSpeed = -minFallingSpeed;
            }
            else
            {
                _currentFallingSpeed += Physics.gravity.y * Time.deltaTime;
                _currentFallingSpeed = Mathf.Clamp(_currentFallingSpeed, -maxFallingSpeed, -minFallingSpeed);
            }

            _characterController.Move(Vector3.up * (_currentFallingSpeed * Time.deltaTime));
        }
        
        private Vector3 GetRelativeMovementDirection(Vector2 movementDirectionInput) =>
            (transform.right * movementDirectionInput.x + transform.forward * movementDirectionInput.y).normalized;
        
    }
}