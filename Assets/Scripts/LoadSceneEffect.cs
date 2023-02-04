using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;
public class LoadSceneEffect : MonoBehaviour
{
    public static LoadSceneEffect Instance;
    public float time = 1;
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        fadeSprite.DOFade(1, 0f);
        fadeSprite.DOFade(0, time);
    }
    public SpriteRenderer fadeSprite;
    public void PlayLoadSceneEffect(string scene)
    {
        fadeSprite.DOFade(1, time).OnComplete(()=>
        {
            SceneManager.LoadScene(scene);
        });
    }

}
