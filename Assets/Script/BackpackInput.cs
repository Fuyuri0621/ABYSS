using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackpackInput : MonoBehaviour
{
   [SerializeField] GameObject backpackPanel;
    [SerializeField] GameObject wheelPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnOpenWheel(InputValue value)
    {

        bool isPressed = value.isPressed;

        wheelPanel.SetActive(isPressed);

    }
    public void OnOpenBackpack()
    {

        if (backpackPanel.activeInHierarchy == false)
            backpackPanel.SetActive(true);
        else
            backpackPanel.SetActive(false);
    }
}
