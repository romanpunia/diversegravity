using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerActivator : MonoBehaviour 
{
    public List<GameObject> Layers = new List<GameObject>();

    void Awake()
    {
        if (!PlayerPrefs.HasKey("DATA_LAYER"))
        {
            PlayerPrefs.SetInt("DATA_LAYER", 0);
            PlayerPrefs.Save();
        }

        int ActiveLayer = PlayerPrefs.GetInt("DATA_LAYER");
        for (int i = 0; i < Layers.Count; i++)
            Layers[i].SetActive(i == ActiveLayer);
		
		if (PlayerPrefs.HasKey ("DATA_LEVEL"))
        {
			if (PlayerPrefs.GetInt ("DATA_LEVEL") <= 0)
				PlayerPrefs.SetInt ("DATA_LEVEL", 1);
		}

        PlayerPrefs.SetInt("DATA_LAYER", 0);
		PlayerPrefs.Save();
    }
}