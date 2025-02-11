using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform Orientation;

    [Header("Detection")]
    [SerializeField] float WallDistanec = .5f, MinimumJumpHeigh = 1.5f;

    [Header("Wall Running")]
    [SerializeField] float WallRunGravity;
    [SerializeField] float WallRunJumpForce;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunfov;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }

    bool WallOnLeft;
    bool WallOnRight;

    RaycastHit LeftWallHit;
    RaycastHit RightWallHit;

    Rigidbody r;

    private void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    bool CanWallRun()
     {
        return !Physics.Raycast(transform.position, Vector3.down,MinimumJumpHeigh);
     }

    void CheckWall()
    {
        WallOnLeft = Physics.Raycast(transform.position,-Orientation.right,out LeftWallHit,WallDistanec);
        WallOnRight = Physics.Raycast(transform.position,Orientation.right,out RightWallHit, WallDistanec);
    }

    private void Update()
    {
        CheckWall();
        if(CanWallRun() )
        {
            if( WallOnLeft )
            {
                StartWallRun();
                Debug.Log("Wall on left");
            }

            else if (WallOnRight)
            {
                StartWallRun();
                Debug.Log("Wall on right");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        r.useGravity = false;

        r.AddForce(Vector3.down * WallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        if (WallOnLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (WallOnRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);


        if (Input.GetKey(KeyCode.Space))
        {
            if(WallOnLeft)
            {
                Vector3 WallRunJumpDirection = transform.up + LeftWallHit.normal;
                r.velocity = new Vector3(r.velocity.x, 0, r.velocity.z);
                r.AddForce(WallRunJumpDirection * WallRunJumpForce, ForceMode.Force);
            }

            else if (WallOnRight)
            {
                Vector3 WallRunJumpDirection = transform.up + RightWallHit.normal;
                r.velocity = new Vector3(r.velocity.x, 0, r.velocity.z);
                r.AddForce(WallRunJumpDirection * WallRunJumpForce, ForceMode.Force);
            }
        }
    }
    void StopWallRun()
    {
        r.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}
