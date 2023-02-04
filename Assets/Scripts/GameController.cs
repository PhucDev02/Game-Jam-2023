using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
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
        if(humanWin)
        {
            Logger.Log("Human win");
        }
        else
        {
            Logger.Log("Animal Win");
        }
    }
    public void Reload()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Main");
    }

    public void GoHome()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Home");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }
}