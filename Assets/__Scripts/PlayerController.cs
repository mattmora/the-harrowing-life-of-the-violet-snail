using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ControlMode
    {
        Raft,
        Free
    }

    public ControlMode mode = ControlMode.Raft;

    public float raftLookSpeed = 10f;
    public float raftLookRange = 30f;

    public GameObject verticalLookObject;

    private float hInput, vInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if (mode == ControlMode.Raft) RaftUpdate();
    }

    void RaftUpdate()
    {
        float currentXRotation = verticalLookObject.transform.localEulerAngles.x;
        if (currentXRotation > 180f) currentXRotation -= 360f;
        float xLimit;
        if (vInput > 0) xLimit = raftLookRange;
        else xLimit = -raftLookRange;
        float xScalar = Mathf.Clamp01((xLimit - currentXRotation) / xLimit);

        float currentYRotation = transform.eulerAngles.y;
        if (currentYRotation > 180f) currentYRotation -= 360f;
        float yLimit;
        if (hInput > 0) yLimit = -raftLookRange;
        else yLimit = raftLookRange;
        float yScalar = Mathf.Clamp01((yLimit - currentYRotation) / yLimit);

        transform.Rotate(new Vector3(0f, -hInput * yScalar * raftLookSpeed * Time.deltaTime, 0f));
        verticalLookObject.transform.Rotate(new Vector3(-vInput * xScalar * raftLookSpeed * Time.deltaTime, 0f, 0f));
    }


}
