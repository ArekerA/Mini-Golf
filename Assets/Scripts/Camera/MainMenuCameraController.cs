using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour {
    public float rotationSpeed;
    public enum State {ROTATING, WAITING };
    public State state;
    Quaternion angle;
	void Start () {
        state = State.WAITING;
	}
	
	void Update () {
		if(state == State.ROTATING)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, rotationSpeed * Time.deltaTime);
            if(transform.rotation == angle)
            {
                state = State.WAITING;
            }
        }
    }
    public void RotateToInfo()
    {
        if (state == State.WAITING)
        {
            angle.eulerAngles = new Vector3(0, 90, 0); ;
            state = State.ROTATING;
        }
    }
    public void RotateToOptions()
    {
        if (state == State.WAITING)
        {
            angle.eulerAngles = new Vector3(0, -90, 0); ;
            state = State.ROTATING;
        }
    }
    public void RotateToMain()
    {
        if (state == State.WAITING)
        {
            angle.eulerAngles = new Vector3(0, 0, 0); ;
            state = State.ROTATING;
        }
    }
    public void RotateToStart()
    {
        if (state == State.WAITING)
        {
            angle.eulerAngles = new Vector3(-90, 0, 0); ;
            state = State.ROTATING;
        }
    }
}
