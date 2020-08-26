using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    public enum DirectionType
    {
        DIRECTION_UP,
        DIRECTION_DOWN,
        DIRECTION_LEFT,
        DIRECTION_RIGHT,
        ZOOM_UP,
        ZOOM_DOWN,
        VOLUME_UP,
        VOLUME_DOWN
    }
    public DirectionType Direction;
    public View View = null;
    public AudioSource Source = null;
    private Player Player = null;

    void Awake()
    {
        if (Source)
        {
            if (PlayerPrefs.HasKey("DATA_MUSIC_VOLUME"))
                Source.volume = PlayerPrefs.GetFloat("DATA_MUSIC_VOLUME");

            if (PlayerPrefs.HasKey("DATA_MUSIC_PLAYBACK"))
                Source.time = PlayerPrefs.GetFloat("DATA_MUSIC_PLAYBACK");
        }

        if (View)
            Player = View.Target.GetComponent<Player>();
    }
    void OnMouseOver()
    {
        if (View != null && Player != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (Direction)
                {
                    case DirectionType.DIRECTION_DOWN:
                        Player.ChangeDirection(Player.DirectionDown);
                        break;
                    case DirectionType.DIRECTION_UP:
                        Player.ChangeDirection(Player.DirectionUp);
                        break;
                    case DirectionType.DIRECTION_LEFT:
                        Player.ChangeDirection(Player.DirectionLeft);
                        break;
                    case DirectionType.DIRECTION_RIGHT:
                        Player.ChangeDirection(Player.DirectionRight);
                        break;
                }
            }

            if (Input.GetMouseButton(0))
            {
                switch (Direction)
                {
                    case DirectionType.ZOOM_DOWN:
                        if (View.transform.position.z < -10)
                            View.transform.position = new Vector3(View.transform.position.x, View.transform.position.y, View.transform.position.z + View.ZoomSpeed * Time.deltaTime);
                        break;
                    case DirectionType.ZOOM_UP:
                        if (View.transform.position.z > -50)
                            View.transform.position = new Vector3(View.transform.position.x, View.transform.position.y, View.transform.position.z - View.ZoomSpeed * Time.deltaTime);
                        break;
                }
            }
        }

        if (Input.GetMouseButton(0) && Source)
        {
            switch (Direction)
            {
                case DirectionType.VOLUME_DOWN:
                    if (Source.volume > 0)
                    {
                        Source.volume -= Time.deltaTime;
                        PlayerPrefs.SetFloat("DATA_MUSIC_VOLUME", Source.volume);
                    }
                    break;
                case DirectionType.VOLUME_UP:
                    if (Source.volume < 1)
                    {
                        Source.volume += Time.deltaTime;
                        PlayerPrefs.SetFloat("DATA_MUSIC_VOLUME", Source.volume);
                    }
                    break;
            }
        }
    }
}