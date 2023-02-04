using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicS : MonoBehaviour
{
    public Sprite musicOn;
    public Sprite musicOff;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.sprite = AudioManager.Instance.musicOn ? musicOn : musicOff;
    }
}
