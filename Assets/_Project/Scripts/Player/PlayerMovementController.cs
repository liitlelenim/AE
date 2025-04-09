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
        
        private PlayerInputActions _playerInputActions;
        private CharacterController _characterController;

        private Vector2 _movementDirectionInput;
        private float _currentFallingSpeed;

        private void OnEnable()
        {
            if (_playerInputActions == null)
            {
                InitializePlayerInputActions();
            }

            _playerInputActions?.Enable();
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void OnDisable()
        {
            _playerInputActions?.Disable();
        }

        private void Update()
        {
            _characterController.Move(GetRelativeMovementDirection(_movementDirectionInput) * 
                                      (movementSpeed * Time.deltaTime));

            HandleGravity();
        }

        private void HandleGravity()
        {
            _currentFallingSpeed += Physics.gravity.y * Time.deltaTime;
            _currentFallingSpeed = Mathf.Clamp(_currentFallingSpeed, -minFallingSpeed, -maxFallingSpeed);
            _characterController.Move(Vector3.up * (_currentFallingSpeed * Time.deltaTime));
        }

        private void HandleMoveInput(InputAction.CallbackContext callbackContext)
        {
            Vector2 input = callbackContext.ReadValue<Vector2>();
            _movementDirectionInput = input;
        }

        private Vector3 GetRelativeMovementDirection(Vector2 movementDirectionInput) =>
            (transform.right * movementDirectionInput.x + transform.forward * movementDirectionInput.y).normalized;

        private void InitializePlayerInputActions()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Navigation.Move.performed += HandleMoveInput;
            _playerInputActions.Navigation.Move.canceled += HandleMoveInput;
        }
    }
}