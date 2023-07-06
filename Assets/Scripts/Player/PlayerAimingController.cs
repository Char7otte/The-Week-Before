using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingController : MonoBehaviour
{
    private void Update()
    {
        var mousePositionInWorldSpace = Vector3.zero;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            mousePositionInWorldSpace = hit.point;
        
        var playerAimDirection = mousePositionInWorldSpace - transform.position;
        playerAimDirection.y = 0;

        transform.LookAt(transform.position + playerAimDirection, Vector3.up);
    }
}
