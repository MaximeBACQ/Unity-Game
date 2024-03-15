using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject gameOverText;

    public void RetryFromStartButton(){
        Coin.instance.ResetCoin();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverCanvas.SetActive(false);
        gameOverText.SetActive(false);
    }
}
