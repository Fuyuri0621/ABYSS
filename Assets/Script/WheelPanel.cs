using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WheelPanel : MonoBehaviour
{
   [SerializeField] RectTransform wheelCenter;

    [SerializeField] Transform[] opts;
    [SerializeField] float radius;
    [SerializeField] int angleOffset;

    [SerializeField] private Vector2 inputDirection;

    [SerializeField] private int selectedIndex = -1;
    PlayerEffect playerEffect;


    void Start()
    {
        ArrangeOptionsInCircle(opts, radius);
        playerEffect =GameObject.Find("Player1").GetComponent<PlayerEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDirection == Vector2.zero) return;
        float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
        angle = (angle + 360f+ angleOffset) % 360f;

        int count = opts.Length;
        int index = Mathf.FloorToInt(angle / (360f / count));



        if (index != selectedIndex)
        {
            selectedIndex = index;
            UpdateHighlight();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame || Gamepad.current?.buttonSouth.wasPressedThisFrame == true)
        {
            Debug.Log(selectedIndex);
            SelectOption(selectedIndex);
        }

    }


    void UpdateHighlight()
    {
        for (int i = 0; i < opts.Length; i++)
        {
            opts[i].GetComponentInChildren<UnityEngine.UI.Image>().color =
                (i == selectedIndex) ? Color.yellow : Color.white;
        }
    }
    public void OnPointerPosition(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>();
        Vector2 menuCenterScreenPos = wheelCenter.position;
        inputDirection = (mousePos - menuCenterScreenPos).normalized;
    }
    public void OnLookDirection(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        if (look.magnitude > 0.1f)
            inputDirection = look.normalized;
    }
    void ArrangeOptionsInCircle(Transform[] options, float radius)
    {
        int count = options.Length;
        for (int i = 0; i < count; i++)
        {
            float angle = i * (360f / count);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
            options[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
  
    
    void SelectOption(int index)
    {
        switch (index)
        {
            case 6: playerEffect.SpeedUP(4, 3); break;
            case 5: playerEffect.AddShield(3); break;
            case 4: playerEffect.Invincible(3); break;
            case 1: playerEffect.SlowFall(0.3f, 3); break;
            case 2: playerEffect.AddTime(5f); break;
            case 3: playerEffect.Invincible(3); break;
        }
    }
}
