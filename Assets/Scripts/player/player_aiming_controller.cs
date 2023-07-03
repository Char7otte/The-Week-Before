using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_aiming_controller : MonoBehaviour
{
    void Update()
    {
        var mouse_position_in_world_space = Vector3.zero;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            mouse_position_in_world_space = hit.point;
        
        var player_aim_direction = mouse_position_in_world_space - transform.position;
        player_aim_direction.y = 0;

        transform.LookAt(transform.position + player_aim_direction, Vector3.up);
    }
}
