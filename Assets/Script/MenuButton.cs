using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public enum InputButton
    {
        CONTINUE,
        NEW_GAME,
        LAYER_SWITCH,
        LEVEL_LOAD,
        LEVEL_SELECT,
        NULL
    }
    public InputButton InputType = InputButton.NULL;
    public GameObject SwitchableLayer = null;
    public float Delay = 0.025f;
    public bool UseReplacement = true;
    public string LevelId = "MENU";
    public Color SelectColor = new Color(251.0f / 255.0f, 73.0f / 255.0f, 253.0f / 255.0f, 1);
    public Color NextColor = new Color();
    private AudioSource Source = null;
    private TextMesh Mesh = null;
    private Color AwakeColor = new Color();
    private bool MouseOver = false;
    private float Time = 0;
    private int Types = 0;
    private char Spec = '_';
    private int Level = 1;
    private int GlobalLevel = 1;

    void Awake()
    {
        Source = GetComponent<AudioSource>();
        if (Source)
            Source.volume = 0.25f;

        Mesh = gameObject.GetComponent<TextMesh>();
        AwakeColor = Mesh.color;

        GlobalLevel = PlayerPrefs.GetInt("DATA_LEVEL");
        if (InputType == InputButton.LEVEL_SELECT)
        {
            if (!int.TryParse(Mesh.text, out Level))
                return;

            if (Level < GlobalLevel)
                AwakeColor = NextColor;
        }
    }
    void Update()
    {
        if (InputType == InputButton.CONTINUE && gameObject.activeSelf)
        {
            if (GlobalLevel > 120)
                gameObject.SetActive(false);
        }

        if (MouseOver)
        {
            if (UseReplacement)
            {
                if (Time <= 0)
                {
                    if (Mesh.text[Mesh.text.Length - 1] != Spec)
                        Mesh.text += Spec;
                    else
                        Mesh.text = Mesh.text.Remove(Mesh.text.Length - 1);
                    Time = 1;
                }
                Time -= Delay;
            }
        }
        else
        {
            if (Mesh.text[Mesh.text.Length - 1] == '_' || Mesh.text[Mesh.text.Length - 1] == '?')
                Mesh.text = Mesh.text.Remove(Mesh.text.Length - 1);
            Mesh.color = AwakeColor;
        }
        MouseOver = false;
    }
    void OnMouseOver()
    {
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
            case InputButton.CONTINUE:
                if (PlayerPrefs.HasKey("DATA_LEVEL"))
                {
                    string Scene = "" + GlobalLevel;
                    if (Application.CanStreamedLevelBeLoaded(Scene))
                        UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
                }
                break;
            case InputButton.NEW_GAME:
                Types++;
                if (Mesh.text[Mesh.text.Length - 1] == '?' || Mesh.text[Mesh.text.Length - 1] == '_')
                    Mesh.text = Mesh.text.Remove(Mesh.text.Length - 1);
                Spec = '?';

                if (Types >= 2)
                {
                    Types = 0;

                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.SetInt("DATA_LEVEL", 1);
                    PlayerPrefs.Save();
                    UnityEngine.SceneManagement.SceneManager.LoadScene("1");
                }
                break;
            case InputButton.LAYER_SWITCH:
                SwitchableLayer.SetActive(true);

                if (gameObject.transform.root)
                    gameObject.transform.root.gameObject.SetActive(false);
                else
                    gameObject.SetActive(false);
                break;
            case InputButton.LEVEL_LOAD:
                if (Source)
                    Source.Play();

                UnityEngine.SceneManagement.SceneManager.LoadScene(LevelId);
                break;
            case InputButton.LEVEL_SELECT:
                int Level = 1;
                if (!int.TryParse(Mesh.text, out Level))
                    break;

                if (!PlayerPrefs.HasKey("DATA_LEVEL"))
                    break;

                if (Level <= GlobalLevel)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(Mesh.text);
                break;
        }
    }
}