using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCameraX(int x)
    {
        Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
    public void MoveCameraY(int y)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, y, Camera.main.transform.position.z);
    }
}
