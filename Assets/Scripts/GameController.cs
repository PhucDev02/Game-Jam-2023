using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public bool isHumanTurn;
    public TextMeshProUGUI winner;
    public GameObject gameOverPanel;
    private void Start()
    {
        isHumanTurn = true;
    }
    public void SwitchTurn()
    {
        isHumanTurn = !isHumanTurn;
        if(Board.Instance.IsHumanAlive()==false)
        {
            GameOver(false);
        }
    }
    public void GameOver(bool humanWin)
    {
        AudioManager.Instance.Play("CompleteGame");

        gameOverPanel.SetActive(true);
        if(humanWin)
        {
            winner.text = "Human win !";
        }
        else
        {
            winner.text = "Animal win !";
        }
    }
    public void Reload()
    {
        DOTween.KillAll();
        LoadSceneEffect.Instance.PlayLoadSceneEffect("Main");
    }

    public void GoHome()
    {
        DOTween.KillAll();
        LoadSceneEffect.Instance.PlayLoadSceneEffect("Home");
    }

    public void PlayGame()
    {
        LoadSceneEffect.Instance.PlayLoadSceneEffect("Main");
    }
}