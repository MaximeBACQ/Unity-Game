using UnityEngine;

public class FadeOutImage : MonoBehaviour
{
    public float fadeDuration = 2f; // Durée de la transition de fondu
    private CanvasGroup canvasGroup;
    private float fadeTimer;

    private float alpha = 1f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        fadeTimer -= Time.deltaTime;    
        
        alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
        canvasGroup.alpha = alpha;

        if (fadeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    // Méthode pour démarrer le fondu
    public void StartFade()
    {
        fadeTimer = fadeDuration;
        gameObject.SetActive(true); // Réactiver l'objet
    }
}