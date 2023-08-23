using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveButtonController : MonoBehaviour
{
    public GameObject diveButton;

    // Start is called before the first frame update
    void Start()
    {
        diveButton.SetActive(false);
    }

    public void ShowDiveButton()
    {
        diveButton.SetActive(true);
    }

    public void HideDiveButton()
    {
        diveButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
