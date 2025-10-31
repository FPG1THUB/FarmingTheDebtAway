using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    // Make sure the InteractionBox has RigidBody component.
    // In the rigidbody, freeze it's rotations and positions
    [SerializeField] Transform _player; // An empty object that I can assign the player capsule to.
    public Text toolTip; // Empty text box for tool tip to
   // [SerializeField] public TextMesh toolTip; // for pop up text, wishful thinking list
   // Look up worldspace ui if i want to do pop up text
    [Header("Offset")]
    [SerializeField] float _offsetx = 1f; // 
    [SerializeField] float _offsetz = 1f; // 

    Interactable currentObject; // Calls for the currently interacted gameobject if it exists.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //this way it can automatically find the player model, if it is properly tagged.
    }

    // Update is called once per frame
    void Update()
    {
        FollowHead();

        if (Input.GetKeyDown(KeyCode.E)) // GetKeyDown means it will only trigger once, then needs to be pressed again.
        {
            if (currentObject != null) // Checks to see if something is there before doing anything.
            {
                currentObject.OnInteraction();// Goes to the gameobject, and runs it's OnInteraction function specified to the object.

            }
        }
    }
    #region new Vector3 interaction attempt, make sure it's unparented, with FollowHead() function
    void FollowHead()
    {
        // Places the InteractionBox directly ahead of wherever the player is heading.
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z-_offsetz);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + _offsetz);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(_player.transform.position.x-_offsetx, _player.transform.position.y, _player.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(_player.transform.position.x + _offsetx, _player.transform.position.y, _player.transform.position.z);
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other) // This will trigger whenever the InteractionBox collides with another object, all other physical objects.
    {
        if (other.TryGetComponent<Interactable>(out Interactable interactedObject)) // This will check to see if the object has a script that includes Interactable in the class section.
        {
            currentObject = interactedObject; // Sets the currentObject to the object that has the necessary script class addition.
            toolTip.text = interactedObject.ToolTip(); // If the object does not have the ToolTip function from Interactable, it will error. 

            //toolTip.transform.position = new Vector3(currentObject.transform.position.x, currentObject.transform.position.y + 1, currentObject.transform.position.z);
            // Above is for pop up text, wishful thinking for now.
        }
    }
    private void OnTriggerExit(Collider other) // This triggers when it stops colliding with the object.
    {
        if (other.TryGetComponent<Interactable>(out Interactable interactedObject)) // Checking that it's no longer interacting with that specific object.
        {
            currentObject = null; // resets it
            toolTip.text = "";//  resets the tool tip.
        }
    }
    #region testing collision triggers
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("lmao");
    //}

    /*private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Lol even");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BOXED");
    }*/
    #endregion

}
