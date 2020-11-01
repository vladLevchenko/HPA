using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{

    public GameObject player;
    public float cameraHeight = 1.0f;
    public float cameraBackOffset = 1.0f;
    public float turnSpeed = 4.0f;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(player.transform.position.x, player.transform.position.y + cameraHeight, player.transform.position.z - cameraBackOffset);
    }

    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position+ new Vector3(0,1,0));
    }
}
