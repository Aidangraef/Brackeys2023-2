using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kevin3SpecialScene : MonoBehaviour
{
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gc.wasThatMe = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
