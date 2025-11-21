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
    public bool refill = false;
    public bool skip = false;
   public  Interactable currentObject; // Calls for the currently interacted gameobject if it exists.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //this way it can automatically find the player model, if it is properly tagged.
        toolTip = GameObject.Find("ToolTip").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowHead();
        //Checks to see if the it cannot be refilled
        if (!refill && !skip)
        {
            //If so, checks to see if the player has pressed E once
            if (Input.GetKeyDown(KeyCode.E)) // GetKeyDown means it will only trigger once, then needs to be pressed again.
            {
                //Is so, checks to see if an object is attached to the currentObject variable
                if (currentObject != null) // Checks to see if something is there before doing anything.
                {
                    //Runs the OnInteraction on the object
                    currentObject.OnInteraction();// Goes to the gameobject, and runs it's OnInteraction function specified to the object.
                    currentObject = null; // this means that it will stop interacting with the object, good for if the item get's destroyed and the player can do a quick spin to re-interact with the item again.
                    toolTip.text = "";
                }
            }
        }
        else
        {
            //If it can be refilled, then checks to see if R has been pressed
            if (Input.GetKey(KeyCode.R)) // GetKeyDown means it will only trigger once, then needs to be pressed again.
            {
                //Checks to see if the object is stored in the variable
                if (currentObject != null) // Checks to see if something is there before doing anything.
                {
                    //Runs the OnInteraction that is attached to the object
                    currentObject.OnInteraction();// Goes to the gameobject, and runs it's OnInteraction function specified to the object.
                }
            }
        }
        if(skip)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if ((currentObject != null))
                {
                    currentObject.OnInteraction();
                    currentObject = null;
                    toolTip.text = "";
                }

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
            //Checks to see if the thing it collided with has the watersource script attached to it
            if (other.GetComponent<WaterSource>() != null)
            {
                refill = true;
            }
            if(other.GetComponent<SkipTime>() != null)
            {
                skip = true;
            }
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
            refill = false;
            skip = false;
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
