using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldCameraController : MonoBehaviour
{
    public GameObject playerCharacter;
    public Vector3 cameraPosOffset = new Vector3(0, 15, -8);

    //How fast the camera will move upon player control
    public float cameraPanSpeed = 50f;

    //How far away from the screen edges the cursor is allowed to be to still control the camera
    public float cameraBorderThickness = 10f;

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;

        if (Input.GetMouseButton(1)) 
        {
            if (Input.mousePosition.y >= Screen.height - cameraBorderThickness)                                       //Top of the screen
            {
                pos.z += cameraPanSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y <= cameraBorderThickness)                                                       //Bottom of the screen
            {
                pos.z -= cameraPanSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= Screen.width - cameraBorderThickness)                                        //Right of the screen
            {
                pos.x += cameraPanSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x <= cameraBorderThickness)                                                       //Left of the screen
            {
                pos.x -= cameraPanSpeed * Time.deltaTime;
            }
            transform.position = pos;

            // Stop the mouse from moving the camera too far away
            var minZ = playerCharacter.transform.position.z - 12;
            var maxZ = playerCharacter.transform.position.z - 5;
            var minX = playerCharacter.transform.position.x - 5;
            var maxX = playerCharacter.transform.position.x + 5;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, Mathf.Clamp(transform.position.z, minZ, maxZ));
        }
        else
        {
            //Makes the camera snap to the player.
            transform.position = Vector3.Lerp(transform.position, playerCharacter.transform.position + cameraPosOffset, Time.deltaTime * 5);
        }
    }
}
