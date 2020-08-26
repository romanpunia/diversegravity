using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Vector3 Direction = new Vector3(0, 0, 0);
    public Vector3 DirectionUp = new Vector3(0, 0, 0);
    public Vector3 DirectionDown = new Vector3(0, 0, 0);
    public Vector3 DirectionLeft = new Vector3(0, 0, 0);
    public Vector3 DirectionRight = new Vector3(0, 0, 0);
    public Color AirColor = new Color(1, 1, 1, 1);
    public Color GroundColor = new Color(1, 1, 1, 1);
    public Color OnPointAddColor = new Color(1, 1, 1, 1);
    public float Speed = 25.0f;
    public float TorqueForce = 0;
    public float FinishSpeed = 6;
    public float FinishScale = 0.05f;
    public float PointTime = 1.0f;
    public int MaxSwitches = -1;
    public int MaxAirSwitches = -1;
    public int PointCount = 0;
    public bool UseDefaultGravity = true;
    public bool UseCustomGravity = false;
    public bool UseCollisionInversion = false;
    public bool UsePresetColor = true;
    public bool UseHVDirection = false;
    public bool IsActive = true;
    public bool IsFinished = false;
    public bool IsDead = false;
    public bool IsColliding = false;
    public bool CanColorBeChanged = true;
    public List<AudioClip> OnFall = new List<AudioClip>();
    public List<AudioClip> OnAddPoint = new List<AudioClip>();
    public AudioClip OnFinish = null;
    public AudioClip OnKill = null;
    public Rigidbody Body = null;
    public AudioSource Source = null;
    public MeshRenderer Renderer = null;
    private int SwitchCount = 0;
    private int AirSwitchCount = 0;
    private bool Crossed = false;

    void Start()
    {
        if (Renderer == null || Body == null)
            Awake();
    }
    void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        Body = GetComponent<Rigidbody>();
        Body.useGravity = false;
        gameObject.tag = "PhysX";

        if (!UseCustomGravity)
        {
            DirectionUp = new Vector3(0, Speed, 0);
            DirectionDown = new Vector3(0, -Speed, 0);
            DirectionLeft = new Vector3(-Speed, 0, 0);
            DirectionRight = new Vector3(Speed, 0, 0);
        }
        else
        {
            DirectionUp *= Speed;
            DirectionDown *= Speed;
            DirectionLeft *= Speed;
            DirectionRight *= Speed;
        }

        if (UseDefaultGravity)
            Direction = DirectionDown;
        else
            Direction *= Speed;

        Source = GetComponent<AudioSource>();
        OnCollisionExit(null);
    }
    void OnCollisionEnter(Collision Collision)
    {
        if (UsePresetColor)
            SetColor(GroundColor);

        if (Source && OnFall.Count > 0)
        {
            Source.clip = OnFall[Random.Range(0, OnFall.Count - 1)];
            Source.Play();
        }

        IsColliding = true;
        AirSwitchCount = 0;
    }
    void OnCollisionStay(Collision Collision)
    {
        if (UseCollisionInversion)
        {
            Direction = -Direction;
            Body.velocity -= Direction * 10.0f * Time.deltaTime;
        }

        IsColliding = true;
    }
    void OnCollisionExit(Collision Collision)
    {
        if (UsePresetColor)
            SetColor(AirColor);

        IsColliding = false;
    }
    void Update()
    {
        if (TorqueForce != 0.0f)
            Body.angularVelocity += new Vector3(0, 0, TorqueForce * Random.Range(-100, 100) / 100.0f);
        Body.velocity += Direction * Time.smoothDeltaTime;

        if (!IsActive)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 DirectionTo = new Vector3(Direction.x, DirectionUp.y, DirectionUp.z);
            if (UseHVDirection && Direction != DirectionTo)
                ChangeDirection(DirectionTo);
            else
                ChangeDirection(DirectionUp);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 DirectionTo = new Vector3(Direction.x, DirectionDown.y, DirectionDown.z);
            if (UseHVDirection && Direction != DirectionTo)
                ChangeDirection(DirectionTo);
            else
                ChangeDirection(DirectionDown);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (UseHVDirection)
                ChangeDirection(new Vector3(DirectionLeft.x, Direction.y, DirectionLeft.z));
            else
                ChangeDirection(DirectionLeft);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (UseHVDirection)
                ChangeDirection(new Vector3(DirectionRight.x, Direction.y, DirectionRight.z));
            else
                ChangeDirection(DirectionRight);
        }
    }
    public void PassLevel(GameObject Object)
    {
        StartCoroutine(OnLevelPass(Object));
    }
    public void Kill()
    {
        StartCoroutine(OnKillPass());
    }
    public void SetColor(Color Color)
    {
        if (!CanColorBeChanged)
            return;

        Renderer.material.color = Color;
        Renderer.material.SetColor("_EmissionColor", Color);
    }
    public void ChangeDirection(Vector3 New)
    {
        if (IsColliding && (SwitchCount < MaxSwitches || MaxSwitches < 0))
        {
            Direction = New;
            SwitchCount++;
            return;
        }

        if (AirSwitchCount < MaxAirSwitches || MaxAirSwitches < 0)
        {
            Direction = New;
            AirSwitchCount++;
            SwitchCount++;
        }
    }
    void SetToMaxScale(Vector3 Scale, float Speed)
    {
        float X = transform.localScale.x;
        if (X < Scale.x)
            X += Speed * Time.deltaTime;
        else X = Scale.x;

        float Y = transform.localScale.y;
        if (Y < Scale.y)
            Y += Speed * Time.deltaTime;
        else Y = Scale.y;

        float Z = transform.localScale.z;
        if (Z < Scale.z)
            Z += Speed * Time.deltaTime;
        else Z = Scale.z;

        transform.localScale = new Vector3(Mathf.Abs(X), Mathf.Abs(Y), Mathf.Abs(Z));
    }
    void SetToMinScale(Vector3 Scale, float Speed)
    {
        float X = transform.localScale.x;
        if (X > Scale.x)
            X -= Speed * Time.deltaTime;
        else X = Scale.x;

        float Y = transform.localScale.y;
        if (Y > Scale.y)
            Y -= Speed * Time.deltaTime;
        else Y = Scale.y;

        float Z = transform.localScale.z;
        if (Z > Scale.z)
            Z -= Speed * Time.deltaTime;
        else Z = Scale.z;

        transform.localScale = new Vector3(Mathf.Abs(X), Mathf.Abs(Y), Mathf.Abs(Z));
    }
    IEnumerator OnLevelPass(GameObject Object)
    {
        if (Source && OnFinish && !Crossed)
        {
            Source.clip = OnFinish;
            Source.Play();
            Crossed = true;
        }

        while (transform.localScale.magnitude / 3 > FinishScale)
        {
            Vector3 Scale = new Vector3(FinishScale, FinishScale, FinishScale);
            SetToMinScale(Scale, FinishSpeed);
            Body.velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        while (Source.isPlaying)
        {
            SetColor(new Color(0, 0, 0, 0));
            Body.velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);
        IsActive = (Object == null);
        IsFinished = !IsActive;
    }
    IEnumerator OnKillPass()
    {
        if (Source && OnFinish && !Crossed)
        {
            Source.clip = OnKill;
            Source.Play();
            Crossed = true;
        }

        while (transform.localScale.magnitude / 3 > FinishScale)
        {
            Vector3 Scale = new Vector3(FinishScale, FinishScale, FinishScale);
            SetToMinScale(Scale, FinishSpeed);
            Body.velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        while (Source.isPlaying)
        {
            SetColor(new Color(0, 0, 0, 0));
            Body.velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);
        IsActive = false;
        IsFinished = false;
        IsDead = true;
        Direction = new Vector3();
        Body.velocity = Direction;
    }
}