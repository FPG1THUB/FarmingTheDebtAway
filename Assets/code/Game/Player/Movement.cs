using UnityEngine;

// Make compatible with current Keybind manager from Elijah
public class PlayerMovement : MonoBehaviour
{
    // this stores the players direction as they move
    [SerializeField] Vector3 _moveDirection;

    // this refers to the character controller component 
    [SerializeField] CharacterController _characterController;

    // lists the current movement speed and movement values for player walk, run, crouch, jump and graviy.
    [SerializeField] float _movementSpeed, _walk = 5, _run = 10, _crouch = 2.5f, _jump = 8, _gravity = 20;

    // Stores horizontal and Vertiacl player inputs, (x) (y) inputs
    Vector2 newInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets the character controller component attached to the same GameObject and Stores it in _character controller 
        _characterController = GetComponent<CharacterController>();

    }
    void Move()
    {
        // Check if the CharacterController is properly assigned (not null)
        if (_characterController != null)
        {
            // Check if the character is grounded (on the floor or surface)
            if (_characterController.isGrounded)
            {
                // Check if the player is pressing the Left movement key
                if (Input.GetKey(KeyBinds.keys["Left"]))
                {
                    // Set the horizontal input to -1 (move left)
                    newInput.x = -1;
                }
                // Check if the player is pressing the Right movement key
                else if (Input.GetKey(KeyBinds.keys["Right"]))
                {
                    // Set the horizontal input to 1 (move right)
                    newInput.x = 1;
                }
                // No left or right key is pressed 
                else
                {
                    // Stop horizontal movement
                    newInput.x = 0;
                }

                // Check if the player is pressing the Backward movement key
                if (Input.GetKey(KeyBinds.keys["Backward"]))
                {
                    // Set the vertical input to -1 (move backward)
                    newInput.y = -1;
                }
                // Check if the player is pressing the Forward movement key
                else if (Input.GetKey(KeyBinds.keys["Forward"]))
                {
                    // Set the vertical input to 1 (move forward)
                    newInput.y = 1;
                }
                // No forward or backward key is pressed
                else
                {
                    // Stop vertical movement
                    newInput.y = 0;
                }

                // Apply movement direction based on the horizontal and vertical input
                _moveDirection = new Vector3(newInput.x, 0, newInput.y);
                // Transform movement direction relative to the player's rotation (to maintain movement direction)
                _moveDirection = transform.TransformDirection(_moveDirection);
                // Multiply by the movement speed to adjust the character's movement speed
                _moveDirection *= _movementSpeed;

                //// Check if the player is pressing the Jump key
                //if (Input.GetKey(KeyBinds.keys["Jump"]))
                //{
                //    // Apply upward force to the movement direction (make the character jump)
                //    _moveDirection.y = _jump;
                //}
            }

            // Apply gravity to the y-axis of the movement direction
            _moveDirection.y -= _gravity * Time.deltaTime;
            // Move the character based on the movement direction (scaled by deltaTime for frame-independent movement)
            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
}