using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour
{
    
    
    public Transform centre;
    public Vector3 axis = Vector3.up;
    public float radius = 2.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 40.0f;
    

    public void Start()
    {
        //set initial spawn position 
        transform.position = (transform.position - centre.position).normalized * radius + centre.position;
        Debug.Log("Electron  Spawn Position: " + transform.position);
    }


    public void Update()
    {   
        
        transform.RotateAround(centre.position, transform.up, rotationSpeed * Time.deltaTime);
       
    }
    
}

