using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] Transform _player;

    [Header("Offset")]
    [SerializeField] float _offsetx = 2f;
    [SerializeField] float _offsetz = 2f;

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
        if ()

    }

    void FollowHead()
    {
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
}
