using UnityEngine;

public class LaneRunner : MonoBehaviour
{
    [Header("Lane Settings")]
    public float leftLaneX = -3f;
    public float middleLaneX = 0f;
    public float rightLaneX = 3f;
    public float laneChangeSpeed = 10f;

    [Header("Forward Movement")]
    public float forwardSpeed = 10f;

    [Header("Ground Settings")]
    public float fixedY = 0.5f;       // The height your player should stay at

    [Header("Looping Settings")]
    public float resetZ = 200f;       // After reaching this Z, restart
    public float startZ = 0f;         // Restart point

    private int currentLane = 1;
    private float[] laneXPositions;

    private void Awake()
    {
        laneXPositions = new float[3] { leftLaneX, middleLaneX, rightLaneX };
    }

    private void Update()
    {
        HandleInput();
        MoveForward();
        SlideToLane();
        KeepOnGround();
        LoopLevel();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Clamp(currentLane - 1, 0, 2);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Clamp(currentLane + 1, 0, 2);
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    private void SlideToLane()
    {
        float targetX = laneXPositions[currentLane];

        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = pos;
    }

    private void KeepOnGround()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;        // FORCES player to stay grounded
        transform.position = pos;
    }

    private void LoopLevel()
    {
        if (transform.position.z >= resetZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;    // Reset to beginning
            transform.position = pos;
        }
    }
}
