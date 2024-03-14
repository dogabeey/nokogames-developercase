using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownJoystickController : MonoBehaviour
{
    public Joystick joystick;
    public Entity entity;

    public float speedMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        entity.Move(direction * speedMultiplier);
    }
}
