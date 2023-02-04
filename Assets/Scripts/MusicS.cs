using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicS : MonoBehaviour
{
    public Sprite musicOn;
    public Sprite musicOff;

   public Image sr;
    private void Awake()
    {
        this.RegisterListener(EventID.ToggleMusic, (param) => UpdateMusic());
    }
    private void Start()
    {
        UpdateMusic();
    }

    private void UpdateMusic()
    {
        sr.sprite = PlayerPrefs.GetInt("Music") == 1 ? musicOn : musicOff;
    }
}
