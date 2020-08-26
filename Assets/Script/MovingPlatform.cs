using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 MinPosition = new Vector3();
    public Vector3 MaxPosition = new Vector3();
    public Vector3 MinRotation = new Vector3();
    public Vector3 MaxRotation = new Vector3();
    public Vector3 MinScale = new Vector3();
    public Vector3 MaxScale = new Vector3();
    public float PositionSpeed = 1;
    public float RotationSpeed = 1;
    public float ScaleSpeed = 1;
    public bool ContinuousRotation = false;
    private Rigidbody Body = null;
    private Vector3 SpecialPositionSpeed = new Vector3();
    private Vector3 SpecialScaleSpeed = new Vector3();

    void Awake()
    {
        Body = GetComponent<Rigidbody>();
        if (MinRotation.z == 0)
            MinRotation.z = 0.1f;

        if (MaxRotation.z == 0)
            MaxRotation.z = 0.1f;

        SpecialPositionSpeed = new Vector3(PositionSpeed, PositionSpeed, PositionSpeed);
        SpecialScaleSpeed = new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed);
    }
    void FixedUpdate()
    {
        if (MinPosition != MaxPosition)
            PositionLerp(Body.position, MinPosition, MaxPosition);

        if (MinRotation != MaxRotation)
        {
            Vector3 RotationA = Body.rotation.eulerAngles;
            RotationLerp(RotationA, MinRotation, MaxRotation);
        }

        if (MinScale != MaxScale)
            ScaleLerp(transform.localScale, MinScale, MaxScale);
    }
    void RotationLerp(Vector3 A, Vector3 Min, Vector3 Max)
    {
        if (ContinuousRotation && Min.z != Max.z)
        {
            Body.MoveRotation(Quaternion.Euler(A + new Vector3(0, 0, RotationSpeed * Time.fixedDeltaTime)));
            return;
        }

        if (Min.z < Max.z)
        {
            if (A.z >= Max.z && RotationSpeed > 0)
                RotationSpeed = -Mathf.Abs(RotationSpeed);

            if (A.z <= Min.z && RotationSpeed < 0)
                RotationSpeed = Mathf.Abs(RotationSpeed);
        }
        else if (Min.z > Max.z)
        {
            if (A.z >= Min.z && RotationSpeed > 0)
                RotationSpeed = -Mathf.Abs(RotationSpeed);

            if (A.z <= Max.z && RotationSpeed < 0)
                RotationSpeed = Mathf.Abs(RotationSpeed);
        }

        Body.MoveRotation(Quaternion.Euler(A + new Vector3(0, 0, RotationSpeed * Time.fixedDeltaTime)));
    }
    void PositionLerp(Vector3 A, Vector3 Min, Vector3 Max)
    {
        if (Min.x < Max.x)
        {
            if (A.x >= Max.x && SpecialPositionSpeed.x > 0)
                SpecialPositionSpeed.x = -Mathf.Abs(SpecialPositionSpeed.x);

            if (A.x <= Min.x && SpecialPositionSpeed.x < 0)
                SpecialPositionSpeed.x = Mathf.Abs(SpecialPositionSpeed.x);
        }
        else if (Min.x > Max.x)
        {
            if (A.x >= Min.x && SpecialPositionSpeed.x > 0)
                SpecialPositionSpeed.x = -Mathf.Abs(SpecialPositionSpeed.x);

            if (A.x <= Max.x && SpecialPositionSpeed.x < 0)
                SpecialPositionSpeed.x = Mathf.Abs(SpecialPositionSpeed.x);
        }
        else
            SpecialPositionSpeed.x = 0;

        if (Min.y < Max.y)
        {
            if (A.y >= Max.y && SpecialPositionSpeed.y > 0)
                SpecialPositionSpeed.y = -Mathf.Abs(SpecialPositionSpeed.y);

            if (A.y <= Min.y && SpecialPositionSpeed.y < 0)
                SpecialPositionSpeed.y = Mathf.Abs(SpecialPositionSpeed.y);
        }
        else if (Min.y > Max.y)
        {
            if (A.y >= Min.y && SpecialPositionSpeed.y > 0)
                SpecialPositionSpeed.y = -Mathf.Abs(SpecialPositionSpeed.y);

            if (A.y <= Max.y && SpecialPositionSpeed.y < 0)
                SpecialPositionSpeed.y = Mathf.Abs(SpecialPositionSpeed.y);
        }
        else
            SpecialPositionSpeed.y = 0;

        if (Min.z < Max.z)
        {
            if (A.z >= Max.z && SpecialPositionSpeed.z > 0)
                SpecialPositionSpeed.z = -Mathf.Abs(SpecialPositionSpeed.z);

            if (A.z <= Min.z && SpecialPositionSpeed.z < 0)
                SpecialPositionSpeed.z = Mathf.Abs(SpecialPositionSpeed.z);
        }
        else if (Min.z > Max.z)
        {
            if (A.z >= Min.z && SpecialPositionSpeed.z > 0)
                SpecialPositionSpeed.z = -Mathf.Abs(SpecialPositionSpeed.z);

            if (A.z <= Max.z && SpecialPositionSpeed.z < 0)
                SpecialPositionSpeed.z = Mathf.Abs(SpecialPositionSpeed.z);
        }
        else
            SpecialPositionSpeed.z = 0;

        Body.MovePosition(A + SpecialPositionSpeed * Time.fixedDeltaTime);
    }
    void ScaleLerp(Vector3 A, Vector3 Min, Vector3 Max)
    {
        if (Min.x < Max.x)
        {
            if (A.x >= Max.x && SpecialScaleSpeed.x > 0)
                SpecialScaleSpeed.x = -Mathf.Abs(SpecialScaleSpeed.x);

            if (A.x <= Min.x && SpecialScaleSpeed.x < 0)
                SpecialScaleSpeed.x = Mathf.Abs(SpecialScaleSpeed.x);
        }
        else if (Min.x > Max.x)
        {
            if (A.x >= Min.x && SpecialScaleSpeed.x > 0)
                SpecialScaleSpeed.x = -Mathf.Abs(SpecialScaleSpeed.x);

            if (A.x <= Max.x && SpecialScaleSpeed.x < 0)
                SpecialScaleSpeed.x = Mathf.Abs(SpecialScaleSpeed.x);
        }
        else
            SpecialScaleSpeed.x = 0;

        if (Min.y < Max.y)
        {
            if (A.y >= Max.y && SpecialScaleSpeed.y > 0)
                SpecialScaleSpeed.y = -Mathf.Abs(SpecialScaleSpeed.y);

            if (A.y <= Min.y && SpecialScaleSpeed.y < 0)
                SpecialScaleSpeed.y = Mathf.Abs(SpecialScaleSpeed.y);
        }
        else if (Min.y > Max.y)
        {
            if (A.y >= Min.y && SpecialScaleSpeed.y > 0)
                SpecialScaleSpeed.y = -Mathf.Abs(SpecialScaleSpeed.y);

            if (A.y <= Max.y && SpecialScaleSpeed.y < 0)
                SpecialScaleSpeed.y = Mathf.Abs(SpecialScaleSpeed.y);
        }
        else
            SpecialScaleSpeed.y = 0;

        if (Min.z < Max.z)
        {
            if (A.z >= Max.z && SpecialScaleSpeed.z > 0)
                SpecialScaleSpeed.z = -Mathf.Abs(SpecialScaleSpeed.z);

            if (A.z <= Min.z && SpecialScaleSpeed.z < 0)
                SpecialScaleSpeed.z = Mathf.Abs(SpecialScaleSpeed.z);
        }
        else if (Min.z > Max.z)
        {
            if (A.z >= Min.z && SpecialScaleSpeed.z > 0)
                SpecialScaleSpeed.z = -Mathf.Abs(SpecialScaleSpeed.z);

            if (A.z <= Max.z && SpecialScaleSpeed.z < 0)
                SpecialScaleSpeed.z = Mathf.Abs(SpecialScaleSpeed.z);
        }
        else
            SpecialScaleSpeed.z = 0;

        transform.localScale = A + SpecialScaleSpeed;
    }
}