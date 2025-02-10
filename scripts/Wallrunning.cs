using UnityEngine;

public class Wallrunning : MonoBehaviour
{

    [SerializeField] Transform Orientation;

    [Header("Wall Running")]
    [SerializeField] float WallDistanec = .5f, MinimumJumpHeigh = 1.5f;

    bool WallOnLeft;
    bool WallOnRight;

    bool CanWallRun()
     {
        return !Physics.Raycast(transform.position, Vector3.down,MinimumJumpHeigh);
     }

    void CheckWall()
    {
        WallOnLeft = Physics.Raycast(transform.position,-Orientation.right,WallDistanec);
        WallOnRight = Physics.Raycast(transform.position,Orientation.right,WallDistanec);
    }

    private void Update()
    {
        CheckWall();
        if(CanWallRun() )
        {
            if( WallOnLeft )
            {
                Debug.Log("Wall on left");
            }

            else if (WallOnRight)
            {
                Debug.Log("Wall on right");
            }
        }
    }
}
