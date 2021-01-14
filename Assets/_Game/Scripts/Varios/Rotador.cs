using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotador : MonoBehaviour
{
    public Vector3 velRotacion;
    
    void Update()
    {
        transform.Rotate(velRotacion * Time.deltaTime);
    }
}
