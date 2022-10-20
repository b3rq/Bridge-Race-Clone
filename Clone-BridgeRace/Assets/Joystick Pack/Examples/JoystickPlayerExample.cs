using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{

    public float speed;
    public FloatingJoystick variableJoystick;
    public Rigidbody rb;
  
    private void Update()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
     
        //cc.Move(direction*speed*Time.deltaTime);
      
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //rb.velocity = direction * speed * Time.fixedDeltaTime;
        //rb.MovePosition(transform.position + (direction * speed * Time.fixedDeltaTime));
    }
}