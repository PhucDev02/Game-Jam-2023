using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public float timeToAppearance;
    public GameObject howToPlayPanel;
    public GameObject menuPanel;
    public void ClickPlay()
    {
        LoadSceneEffect.Instance.PlayLoadSceneEffect("Main");
    }
    public void ClickSound()
    {
        AudioManager.Instance.ClickSound();
    }
    public void ToggleSound()
    {
        AudioManager.Instance.ToggleSound();
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ClickHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        howToPlayPanel.GetComponent<Image>().DOFade(.3f, timeToAppearance);
        howToPlayPanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(Vector2.right * -1000f, 0);
        howToPlayPanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, timeToAppearance).SetEase(Ease.OutQuart);
    }

    public void ClickBackHowToPlay()
    {
        howToPlayPanel.GetComponent<Image>().DOFade(0, timeToAppearance).SetEase(Ease.InQuart);
        howToPlayPanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(Vector2.right * -1000f, timeToAppearance).SetEase(Ease.OutQuart).OnComplete(
            () =>
            {
                howToPlayPanel.SetActive(false);
            });
    }

    public void ClickMenu()
    {
        menuPanel.SetActive(true);
        menuPanel.GetComponent<Image>().DOFade(.3f, timeToAppearance);
        menuPanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(Vector2.right * 1000f, 0);
        menuPanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, timeToAppearance)
            .SetEase(Ease.OutQuart);
    }

    public void ClickBackMenu()
    {
        menuPanel.GetComponent<Image>().DOFade(0, timeToAppearance).SetEase(Ease.InQuart);
        menuPanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(Vector2.right * 1000f, timeToAppearance).SetEase(Ease.OutQuart).OnComplete(
            () =>
            {
                menuPanel.SetActive(false);
            });
    }
}