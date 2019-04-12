using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float maxForce;
    public float forceIncrease;
    public Transform controller;
    public GameController gameController;


    private Rigidbody rb;
    private Vector3 oldPosition;
    private Vector3 eulerRotation = new Vector3();
    private float force;
    private bool canIncrease;
    private UIController uiController;
    bool canMove;
    private AudioSource audioSource;
    private Vector3[] raycastDir = {
        new Vector3(1,-1,1),
        new Vector3(1,-1,-1),
        new Vector3(-1,-1,-1),
        new Vector3(-1,-1,1),
        new Vector3(0,-.662f,0),
        new Vector3(1,0,1),
        new Vector3(1,0,-1),
        new Vector3(-1,0,-1),
        new Vector3(-1,0,1)
    };
    private int layerMask = 1 << 12;
    
    private float raycastDistance = .609f;
    void Start()
    {
        layerMask = ~layerMask;
        canMove = true;
        uiController = GameObject.FindObjectOfType<UIController>();
        audioSource = gameObject.GetComponent<AudioSource>();
        uiController.SetFillAmount(0);
        rb = GetComponent<Rigidbody>();
        canIncrease = true;
        force = 0;
    }
    private void Update()
    {
        if (Debug.isDebugBuild)
        {
            foreach (Vector3 dir in raycastDir)
                Debug.DrawRay(transform.position, dir * raycastDistance, Color.yellow);
        }
        // ( x0 - slope, x5 - wall ) -> same direction
        // x0 | x5 | output | !output
        // 0  | 0  | 1      | 0
        // 0  | 1  | 1      | 0
        // 1  | 0  | 0      | 1
        // 1  | 1  | 1      | 0
        if ((rb.velocity.magnitude < 1.5f) &&
            !(Physics.Raycast(transform.position, raycastDir[0], out RaycastHit hit, raycastDistance, layerMask) && !(Physics.Raycast(transform.position, raycastDir[5], out RaycastHit hit6, raycastDistance, layerMask))) &&
            !(Physics.Raycast(transform.position, raycastDir[1], out RaycastHit hit2, raycastDistance, layerMask) && !(Physics.Raycast(transform.position, raycastDir[6], out RaycastHit hit7, raycastDistance, layerMask))) &&
            !(Physics.Raycast(transform.position, raycastDir[2], out RaycastHit hit3, raycastDistance, layerMask) && !(Physics.Raycast(transform.position, raycastDir[7], out RaycastHit hit8, raycastDistance, layerMask))) &&
            !(Physics.Raycast(transform.position, raycastDir[3], out RaycastHit hit4, raycastDistance, layerMask) && !(Physics.Raycast(transform.position, raycastDir[8], out RaycastHit hit9, raycastDistance, layerMask))) &&
            (Physics.Raycast(transform.position, raycastDir[4], out RaycastHit hit5, raycastDistance, layerMask)))
        {
            //Debug.Log(hit.distance + "'" + hit2.distance + "'" + hit3.distance + "'" + hit4.distance + "'" + hit5.distance);
            rb.isKinematic = true;
        }


#if UNITY_EDITOR
        MouseController();
#endif
    }
    private void FixedUpdate()
    {
        if (oldPosition == transform.position)
        {
            canMove = true;
            StaticUI.canButtonPressed = true;
        }
        else
        {
            oldPosition = transform.position;
        }
    }
    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0) && canMove == true)
        {
            IncreaseForce();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetForce();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)//10 = hole
        {
            gameController.EndLvl();
            rb.velocity = Vector3.zero;
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        if ((collision.gameObject.layer == 11) || (collision.gameObject.layer == 4))//terrian|water
        {
            gameController.ResetLevel();
            rb.velocity = Vector3.zero;
        }
    }
    public void IncreaseForce()
    {
        if (force + forceIncrease > maxForce)
        {
            canIncrease = false;
        }
        else if (force - forceIncrease < 0)
        {
            canIncrease = true;
        }
        if (canIncrease)
        {
            force += forceIncrease;
            uiController.SetFillAmount(force / maxForce);
        }
        else
        {
            force -= forceIncrease;
            uiController.SetFillAmount(force / maxForce);
        }
    }
    public void SetForce()
    {
        if (canMove == true)
        {
            audioSource.Play();
            rb.isKinematic = false;
            StaticUI.canButtonPressed = false;
            uiController.SetFillAmount(0);
            eulerRotation.x = 0;
            eulerRotation.y = controller.transform.eulerAngles.y;
            eulerRotation.z = 0;
            transform.rotation = Quaternion.Euler(eulerRotation);
            rb.AddForce(transform.forward * force);
            oldPosition.x = -9999;
            force = 0;
            canMove = false;
        }
    }
}
