using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundS : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;
    public Image sr;


    private void Awake()
    {
        this.RegisterListener(EventID.ToggleSound, (param) => UpdateSound());
    }
    private void Start()
    {
        UpdateSound();
    }

    private void UpdateSound()
    {
        sr.sprite = PlayerPrefs.GetInt("Sound")==1 ? soundOn : soundOff;
    }
}
