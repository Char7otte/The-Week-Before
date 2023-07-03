using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
