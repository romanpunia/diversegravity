using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public GameObject Target = null;
    public Vector3 Offset = new Vector3();
    public float ZoomSpeed = 1;
    private int LevelId = 1;
    private Player Script = null;
    private AudioSource Source = null;

    void Awake()
    {
        Source = GetComponent<AudioSource>();
        if (Source)
        {
            if (Source.clip)
            {
                if (PlayerPrefs.HasKey("DATA_MUSIC_PLAYBACK"))
                    Source.time = PlayerPrefs.GetFloat("DATA_MUSIC_PLAYBACK");

                if (PlayerPrefs.HasKey("DATA_MUSIC_VOLUME"))
                    Source.volume = PlayerPrefs.GetFloat("DATA_MUSIC_VOLUME");
                else
                    Source.volume = 0.01f;

                Source.loop = true;
                Source.Play();
            }
        }

        GL.Clear(true, true, Color.black);
        if (Target)
            Script = Target.GetComponent<Player>();

        string Name = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (!int.TryParse(Name, out LevelId))
        {
            Debug.LogWarning("Unknown level name: " + Name + ", should be %i");
            LevelId = 1;
        }
        else
            LevelId++;

        if (PlayerPrefs.HasKey("DATA_ZOOM"))
            transform.position = new Vector3(transform.position.x, transform.position.y, PlayerPrefs.GetFloat("DATA_ZOOM"));
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, -15);
    }
    public Player GetInstance()
    {
        return Script;
    }
    public void SaveState()
    {
        PlayerPrefs.SetFloat("DATA_ZOOM", transform.position.z);
        if (Source)
        {
            PlayerPrefs.SetFloat("DATA_MUSIC_PLAYBACK", Source.time);
            PlayerPrefs.SetFloat("DATA_MUSIC_VOLUME", Source.volume);
        }
        PlayerPrefs.Save();
    }
    void LateUpdate()
    {
        if (Script.IsFinished)
        {
            int GlobalLevelId = PlayerPrefs.GetInt("DATA_LEVEL");

            string Id = "DATA_LEVEL_POINT_" + (LevelId - 1);
            if (PlayerPrefs.HasKey(Id))
            {
                int Count = PlayerPrefs.GetInt(Id);
                if (PlayerPrefs.GetInt(Id) < Script.PointCount)
                {
                    Script.PointCount += PlayerPrefs.GetInt("DATA_POINT_COUNT");
                    PlayerPrefs.SetInt("DATA_POINT_COUNT", Script.PointCount - Count);
                }
            }
            else
            {
                PlayerPrefs.SetInt(Id, Script.PointCount);
                Script.PointCount += PlayerPrefs.GetInt("DATA_POINT_COUNT");
                PlayerPrefs.SetInt("DATA_POINT_COUNT", Script.PointCount);
            }

            if (LevelId > GlobalLevelId)
                GlobalLevelId = LevelId;

            if (Source)
            {
                PlayerPrefs.SetFloat("DATA_MUSIC_PLAYBACK", Source.time);
                PlayerPrefs.SetFloat("DATA_MUSIC_VOLUME", Source.volume);
            }

            PlayerPrefs.SetFloat("DATA_ZOOM", transform.position.z);
            PlayerPrefs.SetInt("DATA_LEVEL", GlobalLevelId);
            PlayerPrefs.SetInt("DATA_LAYER", 2);
            PlayerPrefs.Save();

            UnityEngine.SceneManagement.SceneManager.LoadScene("MENU");
        }
        else if (Script.IsDead)
        {
            if (Source)
            {
                PlayerPrefs.SetFloat("DATA_MUSIC_PLAYBACK", Source.time);
                PlayerPrefs.SetFloat("DATA_MUSIC_VOLUME", Source.volume);
            }
            PlayerPrefs.SetFloat("DATA_ZOOM", transform.position.z);
            PlayerPrefs.Save();

            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        if (Target)
            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z) + Offset;
    }
}