using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _player;

    [Header("Offset the Camera")]
    [SerializeField] int _offsetx = 0;  // Making the offset negative moves the camera to the right relative to the player.
    [SerializeField] int _offsetz = 10; // Positive offset moves camera south, negative offset moves camera north


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //this way it can automatically find the player model, if it is properly tagged.
    }
    // Update is called once per frame at the start
    // LateUpdate updates end of each frame, so it moves the camera after the player moves.
    void LateUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x + _offsetx, this.transform.position.y, _player.transform.position.z + _offsetz);
        // the player.transform.position grabs the relevent coordinate from the object.
    }

}
