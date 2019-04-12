using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
    public Transform ball;
    private Vector3 eulerRotation = new Vector3();

    
    void LateUpdate () {
        eulerRotation.x = -90;
        eulerRotation.y = 0;
        eulerRotation.z = ball.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(eulerRotation);
        transform.position = ball.position;
	}
}
