using UnityEngine;

public class look : MonoBehaviour
{
    public Transform o;
    public float senx;
    public float seny;

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
        MyInputs();
        transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        o.localRotation = Quaternion.Euler(0, yrotation,0);
    }

    void MyInputs()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senx;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * seny;

        yrotation += mousex;
        xrotation -= mousey;

        xrotation = Mathf.Clamp(xrotation, -90, 90);

    }
}
