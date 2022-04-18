using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineControl : MonoBehaviour
{
    [SerializeField]
    RocketEngine engine;
    [SerializeField]
    KeyCode left;
    [SerializeField]
    KeyCode right;
    [SerializeField]
    KeyCode front;
    [SerializeField]
    KeyCode back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 desiredPos = new Vector2();
        if (Input.GetKey(right))
            desiredPos.x += engine.MaxAngle;
        if (Input.GetKey(left)) 
            desiredPos.x -= engine.MaxAngle;
        if (Input.GetKey(front)) 
            desiredPos.y -= engine.MaxAngle;
        if (Input.GetKey(back)) 
            desiredPos.y += engine.MaxAngle;
        engine.DesiredPosition = desiredPos;
    }
}
