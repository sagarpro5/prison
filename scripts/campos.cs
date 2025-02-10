using UnityEngine;

public class campos : MonoBehaviour
{
    public Transform camposition;

    // Update is called once per frame
    void Update()
    {
        transform.position = camposition.position;
    }
}
