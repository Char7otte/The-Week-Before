using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public Transform player;

    [Header("CameraControls")]
    public Vector3 camera_offset = new Vector3(0, 16, 0);
    public float camera_pan_speed = 50.0f;
    public float camera_return_speed = 0.1f;
    public float camera_border_leeway = 10.0f; //I genuinely have no idea what to call this. It's how far away from the sides of the screen the cursor can be to still move the camera.
    public float camera_move_limit = 5.0f;

    Vector3 position;
    Vector3 player_position;

    void Update() {
        position = transform.position;
        player_position = player.transform.position;
        
        if (Input.GetMouseButton(1)) 
        {        
            if (Input.mousePosition.y >= Screen.height - camera_border_leeway)
                position.z += camera_pan_speed * Time.deltaTime; //Top 
            if (Input.mousePosition.y <= camera_border_leeway)
                position.z -= camera_pan_speed * Time.deltaTime; //Bottom
            if (Input.mousePosition.x >= Screen.width - camera_border_leeway)
                position.x += camera_pan_speed * Time.deltaTime; //Right
            if (Input.mousePosition.x <= camera_border_leeway)
                position.x -= camera_pan_speed * Time.deltaTime; //Left

            limit_camera_position_away_from_player();
        }
        else {
            position = Vector3.Lerp(position, player_position + camera_offset, camera_return_speed);
        }

        transform.localPosition = position;
    }

    void limit_camera_position_away_from_player() {
        var min_x = player_position.x - camera_move_limit;
        var max_x = player_position.x + camera_move_limit;
        var min_z = player_position.z - camera_move_limit;
        var max_z = player_position.z + camera_move_limit;

        position.x = Mathf.Clamp(position.x, min_x, max_x);
        position.z = Mathf.Clamp(position.z, min_z, max_z);
    }
}
