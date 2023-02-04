using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundS : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.sprite = AudioManager.Instance.musicOn ? soundOn : soundOff;
    }
}
