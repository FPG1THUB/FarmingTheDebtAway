using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    // Gonna try and parent it an invisible object to the players headed direction,
    [SerializeField] Transform _player;
    [Header("Offset")]
    //[SerializeField] int _offsetx = 1;
    //[SerializeField] int _offsetz = 1;
    public Text toolTip;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //this way it can automatically find the player model, if it is properly tagged.
    }
    void Update()
    {
        // Creates an invisible light that can come in contact with colliders.
        Ray interactLight;

        // Casts an invisible light from the position of the camera's center.
        //interactLight = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        interactLight = Camera.main.ScreenPointToRay(new Vector2(_player.transform.position.x, _player.transform.position.z));

        // Generates information on what the invisible light has hit, provides information on what we are interacting with.
        RaycastHit hitInfo;

        //Checks if the casted light comes in contact with a collider within our distance parameters and/or layer.
        if (Physics.Raycast(interactLight, out hitInfo, 10))
        {
            if (hitInfo.collider.TryGetComponent<Interactable>(out Interactable displayToolTip))
            {
                toolTip.text = displayToolTip.ToolTip();
            }
            //if our interaction button or key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Checks if the light hits a collider that has the "Interactable" script assigned to it.
                if (hitInfo.collider.TryGetComponent<Interactable>(out Interactable interact))
                {
                    // If yes, run "OnInteraction" function.
                    interact.OnInteraction();
                }
            }
        }
        else
        {
            if (toolTip.text != "")
            {
                toolTip.text = "";
            }
        }
    }
    void HeadOfPlayer()
    {
        transform.position = _player.transform.position;
    }
}
