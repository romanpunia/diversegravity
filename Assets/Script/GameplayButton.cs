using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayButton : MonoBehaviour
{
    public enum InputButton
    {
        NULL,
        LOAD_LEVEL,
        POINT_COUNT,
        RESET
    }
    public InputButton InputType = InputButton.NULL;
    public string LevelId = "MENU";
    public Color SelectColor = new Color(251.0f / 255.0f, 73.0f / 255.0f, 253.0f / 255.0f, 1);
    public View Camera = null;
    private AudioSource Source = null;
    private TextMesh Mesh = null;
    private Color AwakeColor = new Color();
    private bool MouseOver = false;

    void Awake()
    {
        Mesh = GetComponent<TextMesh>();
        Source = GetComponent<AudioSource>();
        if (Source)
            Source.volume = 0.25f;

        AwakeColor = Mesh.color;
    }
    void Update()
    {
        if (InputType != InputButton.POINT_COUNT)
        {
            if (!MouseOver)
                Mesh.color = AwakeColor;
            MouseOver = false;
        }
        else
            Mesh.text = "P: " + Camera.GetInstance().PointCount;
    }
    void OnMouseOver()
    {
        if (InputType == InputButton.POINT_COUNT)
            return;

        MouseOver = true;
        Mesh.color = SelectColor;
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(MenuSelection());
    }
    IEnumerator MenuSelection()
    {
        if (Source)
        {
            Source.Play();
            yield return new WaitForSeconds(Source.clip.length);
        }

        yield return new WaitForSeconds(0);
        switch (InputType)
        {
            case InputButton.LOAD_LEVEL:
                Camera.SaveState();
                UnityEngine.SceneManagement.SceneManager.LoadScene(LevelId);
                break;
            case InputButton.RESET:
                Camera.SaveState();
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                break;
        }
    }
}