using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private Lane leftLane;
    [SerializeField]
    private Lane rightLane;

    public bool HasLeftLane
    {
        get { return leftLane != null; }
    }

    public bool HasRightLane
    {
        get { return rightLane != null; }
    }

    public float LanePosition
    {
        get { return transform.position.x; }
    }

    public float LeftLanePosition
    {
        get { return leftLane.LanePosition; }
    }

    public float RightLanePosition
    {
        get { return rightLane.LanePosition; }
    }

    public Lane LeftLane
    {
        get { return leftLane; }
    }

    public Lane RightLane
    {
        get { return rightLane; }
    }
}
