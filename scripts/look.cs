using UnityEngine;

public class look : MonoBehaviour
{
    public Transform o;
    public float senx;
    public float seny;
    public Wallrunning wallRun;

    float xrotation;
    float yrotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senx;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * seny;

        yrotation += mousex;
        xrotation -= mousey;

        xrotation = Mathf.Clamp(xrotation, -90, 90);

        transform.rotation = Quaternion.Euler(xrotation, yrotation, wallRun.tilt);
        o.localRotation = Quaternion.Euler(0, yrotation,0);
    }

}
