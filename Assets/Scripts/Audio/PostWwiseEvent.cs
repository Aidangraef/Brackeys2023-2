using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Use this for initialization.
    public void PlayEventSound()
    {
        MyEvent.Post(gameObject);
        Debug.Log("Posted" + MyEvent);
    }

    // Update is called once per frame.
    void Update()
    {

    }
}
