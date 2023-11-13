using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] float waitAfterButtonClick;

    [SerializeField] string gameSceneName;
    [SerializeField] Animator fadeAnimator;

    void Awake() 
    {
        if(!fadeAnimator.gameObject.activeSelf)    
            fadeAnimator.gameObject.SetActive(true);
    }

    public void PlayButtonEvent()
    {
        StartCoroutine(ForPlayButton());
    }

    public void QuitButtonEvent()
    {
        StartCoroutine(ForQuitButton());
    }

    void EnableFadeAnimation()
    {
        fadeAnimator.SetTrigger("Next");
    }

    IEnumerator ForPlayButton()
    {
        EnableFadeAnimation();
        yield return new WaitForSeconds(waitAfterButtonClick);
        SceneManager.LoadScene(gameSceneName);
        StopAllCoroutines();
    }

    IEnumerator ForQuitButton()
    {
        EnableFadeAnimation();
        yield return new WaitForSeconds(waitAfterButtonClick);
        Application.Quit();
        StopAllCoroutines();
    }

}
