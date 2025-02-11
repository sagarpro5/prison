using System;
using System.Globalization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class movement : MonoBehaviour
{
    float PlayerHeight = 2f;

    [Header("Movement")]
    public float speed = 7f;
    float airmultiplir = .4f;

    [Header("Jump")]
    [SerializeField] float Jumpforce = 5f;

    [Header("Sprint")]
    bool IsSprint;


    [Header("KeyBinds")]
    [SerializeField] KeyCode Jumpkey = KeyCode.Space;
    [SerializeField] KeyCode Sprintkey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    float horizontalInputs;
    float verticalInputs;

    [Header("Ground Detection")]
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask groundlayer;
    bool isgrounded = false;
    float grounddistance = 0.4f;

    Rigidbody r;
    Vector3 movedir;
    Vector3 slopmovedir;

    RaycastHit slophit;

    private bool OnSloap()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out slophit,PlayerHeight /2 + 0.5f))
        {
            if(slophit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    return false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<Rigidbody>();
        r.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInputs();
        dragecontrol();
        isgrounded = Physics.CheckSphere(GroundCheck.position, grounddistance, groundlayer);
        if(Input.GetKeyDown(Jumpkey) && isgrounded)
        {
            Jump();
        }
        if(Input.GetKeyDown(Sprintkey))
        {
            IsSprint = true;
        }
        slopmovedir = Vector3.ProjectOnPlane(movedir, slophit.normal);
    }
    private void FixedUpdate()
    {
        moveplayer();
    }
    void MyInputs()
    {
        float horizontalInputs = Input.GetAxisRaw("Horizontal");
        float verticalInputs = Input.GetAxisRaw("Vertical");
        movedir = transform.forward * verticalInputs + transform.right * horizontalInputs;
    }

    void dragecontrol()
    {
        if(isgrounded)
        {
            r.drag = groundDrag;
        }
        else
        {
            r.drag = airDrag;
        }
    }

    void moveplayer()
    {
        if (IsSprint && OnSloap())
        {
            r.AddForce(slopmovedir.normalized * speed * 12f, ForceMode.Acceleration);
        }
        if(isgrounded && !OnSloap())
        {
            r.AddForce(movedir.normalized * speed * 10f, ForceMode.Acceleration);
        }
        else if(isgrounded && OnSloap())
        {
            r.AddForce(slopmovedir.normalized * speed * 10f, ForceMode.Acceleration);
        }
        else if(!isgrounded)
        {
            r.AddForce(movedir.normalized * speed * 10f * airmultiplir, ForceMode.Acceleration);
        }
        else if(isgrounded && IsSprint && !OnSloap())
        {
            r.AddForce(movedir.normalized * speed * 15f, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        if(isgrounded)
        {
            r.velocity = new Vector3(r.velocity.x, 0, r.velocity.z);
            r.AddForce(transform.up * Jumpforce, ForceMode.Impulse);
        }
    }
}
