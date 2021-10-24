using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float grappleDistance = 10f;
    float normalSpeed = 60f;
    float grappleSpeed;
    float decelerationModifer = 0.8f;
    float accelerationModifer = 5f;
    float rotationSpeed = 100f;
    float maxWidthLr = 0.06f;
    float minWidthLr = 0.02f;
    public int health = 100;


    LineRenderer lr;
    GameObject grappleObject;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        grappleSpeed = normalSpeed + 10f;

        // jeremy is dumb, delete this later.

    }
    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    // Update is called once per frame
    void Update()
    {
 

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.y = 0.5f;
        
        if (Input.GetMouseButton(1))
        {
            if (grappleObject == null)
            {


                if (Physics.Raycast(transform.position, worldPosition - transform.position, out RaycastHit hitInfo, grappleDistance, 1 << 6))
                {
                    grappleObject = hitInfo.transform.gameObject;


                }
            }


        }
        else
        {
            grappleObject = null;
        }

        if (grappleObject == null)
        {
            lr.enabled = false;
        }
        else
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, grappleObject.transform.position);
            lr.startWidth = grappleDistance / (grappleObject.transform.position - transform.position).magnitude * maxWidthLr;
            lr.endWidth = lr.startWidth - .07f;
            transform.LookAt(grappleObject.transform.position);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                rb.AddForce(transform.right * rotationSpeed, ForceMode.Force);

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                rb.AddForce(-transform.right * rotationSpeed, ForceMode.Force);

            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                rb.AddForce(-transform.right * rotationSpeed, ForceMode.Force);

            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                rb.AddForce(transform.right * rotationSpeed, ForceMode.Force);

            }

            rb.AddForce((grappleObject.transform.position - transform.position).normalized * grappleSpeed * Time.deltaTime * accelerationModifer, ForceMode.Force);


        }
        rb.AddForce((-rb.velocity).normalized * grappleSpeed * Time.deltaTime * decelerationModifer, ForceMode.Force);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, grappleDistance);


    }
}












