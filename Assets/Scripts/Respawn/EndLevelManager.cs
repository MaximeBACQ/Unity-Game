using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelManager : MonoBehaviour
{
    public GameObject endLevelCanvas;
    public GameObject endLevelText;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public void RetryButton(){
        // Method to be called when the retry button is pressed
        Coin.instance.ResetCoin();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene to restart the level
        // Hides the end level UI elements
        endLevelCanvas.SetActive(false);
        endLevelText.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }
}
