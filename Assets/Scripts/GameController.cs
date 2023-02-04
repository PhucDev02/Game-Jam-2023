using UnityEngine;

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
    }
}