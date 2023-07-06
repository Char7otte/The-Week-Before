using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform player;

    [Header("CameraControls")]
    [SerializeField]private Vector3 cameraOffset = new Vector3(0, 16, 0);
    [SerializeField]private float cameraPanSpeed = 50.0f;
    [SerializeField]private float cameraReturnSpeed = 0.1f;
    [SerializeField]private float cameraBorderLeeway = 10.0f; //I genuinely have no idea what to call this. It's how far away from the sides of the screen the cursor can be to still move the camera.
    [SerializeField]private float cameraMoveDistanceLimit = 5.0f;

    private Vector3 position;
    private Vector3 player_position;

    private void Update() {
        position = transform.position;
        player_position = player.transform.position;
        
        if (Input.GetMouseButton(1)) 
        {        
            if (Input.mousePosition.y >= Screen.height - cameraBorderLeeway)
                position.z += cameraPanSpeed * Time.deltaTime; //Top 
            if (Input.mousePosition.y <= cameraBorderLeeway)
                position.z -= cameraPanSpeed * Time.deltaTime; //Bottom
            if (Input.mousePosition.x >= Screen.width - cameraBorderLeeway)
                position.x += cameraPanSpeed * Time.deltaTime; //Right
            if (Input.mousePosition.x <= cameraBorderLeeway)
                position.x -= cameraPanSpeed * Time.deltaTime; //Left

            limitCameraPositionToPlayer();
        }
        else {
            position = Vector3.Lerp(position, player_position + cameraOffset, cameraReturnSpeed);
        }

        transform.localPosition = position;
    }

    private void limitCameraPositionToPlayer() {
        var min_x = player_position.x - cameraMoveDistanceLimit;
        var max_x = player_position.x + cameraMoveDistanceLimit;
        var min_z = player_position.z - cameraMoveDistanceLimit;
        var max_z = player_position.z + cameraMoveDistanceLimit;

        position.x = Mathf.Clamp(position.x, min_x, max_x);
        position.z = Mathf.Clamp(position.z, min_z, max_z);
    }
}
