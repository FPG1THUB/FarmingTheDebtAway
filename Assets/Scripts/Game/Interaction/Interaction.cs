using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//This will make it so that if the object does not have a rigidbody, that it will put one on
//Make sure to then freeze the rotation and position
[RequireComponent(typeof(Rigidbody))]


/// <summary>
/// This class handles the backend of the interaction, making it possible for the player to interact with other objects using the 
/// Interactable interface.
/// Attach this script to any object that the player will use to interact with, such as the interaction box or the player themselves
/// Be sure that the object the script is attached to has a rigidbody and that the rotations and positions are frozen
/// </summary>
public class Interaction : MonoBehaviour
{
    #region variables
    //reference to the inventory so that the plot handler can distinguish between the plot handling and crop handling
    public Inventory inventoryManager;
    [SerializeField] Transform _player;// stores the transform component of the player
    public Text toolTip; // Empty text box for tool tip to display the action in which the player can do
    //Offsets to be used for placing and keeping the object in front of the player
    [Header("Offset")]
    [SerializeField] float _offsetx = 1f;  
    [SerializeField] float _offsetz = 1f; 
    //Bool to check whether the watering script is trying to refill the current water amount
    public bool refill = false;
    //bool to check whether the time script is trying to skip the time
    public bool skip = false;
    //bool to check whether the plot handler script is trying to handle a plot
    public bool isHandlingPlot = false;
    //bool to check whether the crop handler is trying to handle crops
    public bool isHandlingCrops = false;
    //stores the object interactable script the player is trying to interact with
    [SerializeField]public  Interactable currentObject; 
    //stores the original color of the object the player will be interacting with
    public Color originalColor;
    //Stores the color the interactable object will be when the player entered the collision of the object
    public Color selectedColor = new Color(0, 0.5f, 0);

    #endregion
    #region Unity Callbacks
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;//Finds the transform component on the player
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();//Finds and stores inventory script
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
                //If so, checks to see if an object is attached to the currentObject variable
                if (currentObject != null) 
                {
                    //Runs the OnInteraction on the object
                    currentObject.OnInteraction();
                    //Resets the currentObject variable. This is for when the object is destroyed so that the player cant
                    //stay still and keep spamming E to get a bunch of carrots for free
                    currentObject = null; 
                    toolTip.text = "";//Resets the tool tip
                }
            }
        }
        //Else check if refill is set to true
        else if(refill)
        {
            //If it can be refilled, then checks to see if R has been pressed
            if (Input.GetKey(KeyCode.R)) 
            {
                //Checks to see if the player is trying to interact with an object still
                if (currentObject != null) 
                {
                    //Runs the OnInteraction that is attached to the object
                    currentObject.OnInteraction();
                }
            }
        }
        //else check if the player is trying to skip the time
        else if(skip)
        {
            //Checks to see if E has been pressed once
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Checks to see if the player is trying to interact with an object still
                if ((currentObject != null))
                {
                    //Runs the on interaction function on the attached script of the existing object
                    currentObject.OnInteraction();
                    //resets the current object
                    currentObject = null;
                    //Resets the tool tip
                    toolTip.text = "";
                }

            }
        }
        //else Checks to see if the player is trying to handle a plot
        else if(isHandlingPlot)
        {
            //Checks to see if the player is holding down E
            if(Input.GetKey(KeyCode.E))
            {
                //Checks to see if the player is trying to interact with an object still
                if (currentObject != null)
                {
                    //if so, runs the on interaction function attached to that object
                    currentObject.OnInteraction();

                }
            }
        }
        //else check if the player is trying to handle crops
        else if(isHandlingCrops)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Checks to see if the player is trying to interact with an object still
                if (currentObject != null)
                {
                    //runs the objects onInteraction script
                    currentObject.OnInteraction();
                    //resets the current object
                    currentObject = null;
                    //resets the tool tip
                    toolTip.text = "";
                }
            }
        }


        
    }
    #endregion
    #region FollowHead Function
    /// <summary>
    /// Makes it so that the interaction box remains ahead of wherever the player is moving
    /// </summary>
    void FollowHead()
    {
        //Checks if the player is trying to move forward
        if (Input.GetKey(KeybindManager.keys["Forward"]))
        {
            //sets the transform of the object this script is attached to to the same as the player, but at an offset
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z-_offsetz);
        }
        //else, check if the player is trying to move backward
        else if (Input.GetKey(KeybindManager.keys["Backward"]))
        {
            //sets the transform of the object this script is attached to to the same as the player, but at an offset
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + _offsetz);
        }
        //Checks if the player is trying to move right
        if (Input.GetKey(KeybindManager.keys["Right"]))
        {
            //sets the transform of the object this script is attached to to the same as the player, but at an offset
            transform.position = new Vector3(_player.transform.position.x-_offsetx, _player.transform.position.y, _player.transform.position.z);
        }
        //sle, checks if the player is trying to move left
        else if (Input.GetKey(KeybindManager.keys["Left"]))
        {
            //sets the transform of the object this script is attached to to the same as the player, but at an offset
            transform.position = new Vector3(_player.transform.position.x + _offsetx, _player.transform.position.y, _player.transform.position.z);
        }
    }
    #endregion
    #region OnCollision functions
    /// <summary>
    /// Function that fetches the object the player is trying to interact with, and checks multiple things such as whether the object
    /// has time skip or watering on it so that certain actions can be made under only specific circumstances
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) // This will trigger whenever the InteractionBox collides with another object, all other physical objects.
    {
        //Checks to see if the object has the interactable interface
        if (other.TryGetComponent<Interactable>(out Interactable interactedObject)) // This will check to see if the object has a script that includes Interactable in the class section.
        {

            //Checks to see if the interactable object contains either the crop handler or the plot handler scripts
            //It does this because both of those have the object mesh within the children instead of teh direct object
            if(other.GetComponent<PlotHandler>() || other.GetComponent<CropHandler>())
            {
                //Sets the original colour of the children object to the current color of the objects
                originalColor = other.GameObject().GetComponentInChildren<MeshRenderer>().material.color;

            }
            else
            {
                //If it doesnt contain a plot or crop handler script, it will simply grab the color directly from the object
                originalColor = other.GetComponent<MeshRenderer>().material.color;
            }



            //Store the object the player is trying to interact with in the current object variable
            currentObject = interactedObject; 
            //Sets the tool tip text to the tool tip of the interactable object
            toolTip.text = interactedObject.ToolTip(); 
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



            //Checks to see if the interactable object has the plot or crop handler script on it
            if (other.GetComponent<PlotHandler>() || other.GetComponent<CropHandler>())
            {
                //Applies the selected color to the children objects
                other.GameObject().GetComponentInChildren<MeshRenderer>().material.color = selectedColor;
            }
            else
            {
                //otherwise it applies the selected color to the interactable object 
                other.GetComponent<MeshRenderer>().material.color = selectedColor;
            }
        }
    }
    /// <summary>
    /// Function to state what occurs when no longer selecting the interactable object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) // This triggers when it stops colliding with the object.
    {
        //Checks to see if the player is no longer trying to interact with the object
        if (other.TryGetComponent<Interactable>(out Interactable interactedObject)) 
        {
            currentObject = null; // resets it
            toolTip.text = "";//  resets the tool tip.
            refill = false; //sets refill off
            skip = false;//sets skip off
            isHandlingPlot = false;//sets isHandlingPlot off
            isHandlingCrops=false; //sets isHandlingCrops off



            //Checks to see if the interacted object has either the plot or crop handler script on them
            if (other.GetComponent<PlotHandler>() || other.GetComponent<CropHandler>())
            {
                //If so, set the colour of the children objects back to its original colour
                other.GameObject().GetComponentInChildren<MeshRenderer>().material.color = originalColor;

            }
            else
            {
                //If not, apply the original colour back to the interacted object
                other.GetComponent<MeshRenderer>().material.color = originalColor;
            }





        }
    }
    #endregion

}
