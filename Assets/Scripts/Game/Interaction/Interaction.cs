using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    //reference to the inventory so that the plot handler can distinguish between the plot handling and crop handling
    public Inventory inventoryManager;
    // Make sure the InteractionBox has RigidBody component.
    // In the rigidbody, freeze it's rotations and positions
    [SerializeField] Transform _player; // An empty object that I can assign the player capsule to.
    public Text toolTip; // Empty text box for tool tip to
   // [SerializeField] public TextMesh toolTip; // for pop up text, wishful thinking list
   // Look up worldspace ui if i want to do pop up text
    [Header("Offset")]
    [SerializeField] float _offsetx = 1f; // 
    [SerializeField] float _offsetz = 1f; // 
    //Bool to check whether the watering script is trying to refill the current water amount
    public bool refill = false;
    //bool to check whether the time script is trying to skip the time
    public bool skip = false;
    //bool to check whether the plot handler script is trying to handle a plot
    public bool isHandlingPlot = false;
    //bool to check whether the crop handler is trying to handle crops
    public bool isHandlingCrops = false;
   public  Interactable currentObject; // Calls for the currently interacted gameobject if it exists.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //this way it can automatically find the player model, if it is properly tagged.
        //Retrieves the inventory script from the manager
        inventoryManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowHead();
        //Checks to see if the watering script is not trying to refill, the time script is not trying to skip, and the plot handler script is not trying to water a plot
        if (!refill && !skip && !isHandlingPlot && !isHandlingCrops)
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
        //Checks to see if the time script is trying to skip the time
        if(skip)
        {
            //Checks to see if E has been pressed once
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Checks to see if there is an existing object that the player is trying to interact with
                if ((currentObject != null))
                {
                    //Runs the on interaction function on the attached script of the existing object
                    currentObject.OnInteraction();
                    //resets the current object
                    currentObject = null;
                    toolTip.text = "";
                }

            }
        }
        //Checks to see if the plot handler script is trying to water a plot
        if(isHandlingPlot)
        {
            //Checks to see if the player is holding down E
            if(Input.GetKey(KeyCode.E))
            {
                //Checks to see if there is an existin object the player is trying to interact with
                if(currentObject != null)
                {
                    //if so, runs the on interaction function attached to that object
                    currentObject.OnInteraction();

                }
            }
        }
        if(isHandlingCrops)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(currentObject != null)
                {
                    currentObject.OnInteraction();
                    //resets the current object
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
        if (Input.GetKey(KeybindManager.keys["Forward"]))
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z-_offsetz);
        }
        else if (Input.GetKey(KeybindManager.keys["Backward"]))
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + _offsetz);
        }

        if (Input.GetKey(KeybindManager.keys["Right"]))
        {
            transform.position = new Vector3(_player.transform.position.x-_offsetx, _player.transform.position.y, _player.transform.position.z);
        }
        else if (Input.GetKey(KeybindManager.keys["Left"]))
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
                //if so, set refill to true to show that the player is trying to refill their watering can
                refill = true;
            }
            //Checks to see if the object the player collided with has the skip time script attached to it
            if(other.GetComponent<SkipTime>() != null)
            {
                //if so, set skip to true to show that the player is trying to skip the time
                skip = true;
            }
            //Checks to see if the thing the player collided with has the plot handler script attached to it
            if(other.GetComponent<PlotHandler>() != null) 
            {
                if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Watering Can" ||
                    inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Hoe")
                {
                    isHandlingPlot = true;
                }
                else if(inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Tomato Seed" ||
                    inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Potato Seed" ||
                    inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Carrot Seed")
                {
                    isHandlingCrops = true;
                }
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
            refill = false; //sets refill off
            skip = false;//sets skip off
            isHandlingPlot = false;//sets isHandlingPlot off
            isHandlingCrops=false; //sets isHandlingCrops off
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
