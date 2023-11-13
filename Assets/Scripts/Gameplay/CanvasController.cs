using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Button[] buttons;

    [SerializeField] string mainMenuName;

    [SerializeField] Animator speedUp;

    [SerializeField] Animator fadeImage;

    [Header("Canvas")]
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas winCanvas;

    public TextMeshProUGUI gameOverSurvivedWave;

    void Awake() 
    {
        DisableCanvas(pauseCanvas.gameObject);
        DisableCanvas(gameOverCanvas.gameObject);
        DisableCanvas(winCanvas.gameObject);
    }

    void DisableCanvas(GameObject canvas)
    {
        if(canvas.activeSelf)
            canvas.SetActive(false);
    }

    void EnableCanvas(GameObject canvas)
    {
        if(!canvas.activeSelf)
            canvas.SetActive(true);   
    }

    void EnableButtons()
    {
        foreach (var item in buttons)
        {
            item.interactable = true;
        }
    }

    void DisableButtons()
    {
        foreach (var item in buttons)
        {
            item.interactable = false;
        }
    }

    public void PauseGame()
    {
        EnableCanvas(pauseCanvas.gameObject);
        DisableButtons();
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        DisableCanvas(pauseCanvas.gameObject);
        EnableButtons();
    }

    public void GoMainMenu()
    {
        StartCoroutine(FadeImage());
    }

    public void ShowGameOver()
    {
        EnableCanvas(gameOverCanvas.gameObject);
        DisableButtons();
        Time.timeScale = 1;
    }

    public void ShowWin()
    {
        EnableCanvas(winCanvas.gameObject);
        DisableButtons();
        Time.timeScale = 1;
    }

    IEnumerator FadeImage()
    {
        Time.timeScale = 1;
        fadeImage.SetTrigger("Next");
        yield return new WaitForSeconds(.2f);
        DisableCanvas(winCanvas.gameObject);
        DisableCanvas(gameOverCanvas.gameObject);
        DisableCanvas(pauseCanvas.gameObject);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(mainMenuName);
    }

    public void SpeedUp()
    {
        speedUp.SetBool("Speed",!speedUp.GetBool("Speed"));
        if(speedUp.GetBool("Speed"))
            Time.timeScale = 2.5f;
        else if(!speedUp.GetBool("Speed"))
            Time.timeScale = 1;
    }

}   
