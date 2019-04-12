using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonForce : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public BallController ball;
    private bool isPressed;
    private bool isSet;
    private GameController gc;
    void Start()
    {
        StaticUI.isButtonPressed = false;
        gc = FindObjectOfType<GameController>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (StaticUI.canButtonPressed)
        {
            StaticUI.isButtonPressed = true;
            isPressed = true;
            isSet = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPressed)
        {
            StaticUI.isButtonPressed = false;
            isPressed = false;
            isSet = true;
        }
    }
    void FixedUpdate()
    {
        if (isPressed)
        {
            ball.IncreaseForce();
        }
        else if (isSet)
        {
            isSet = false;
            ball.SetForce();
            gc.AddCounter();
        }
    }
}
