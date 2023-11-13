using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] Button startWaveButton;

    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] CanvasController canvasController;

    int waveNumber = 0;

    void Awake() 
    {
        waveText.text = "Wave " + 1;
        canvasController.gameOverSurvivedWave.text = waveNumber.ToString();
        ActiveStartWaveButton();
    }

    public void StartCheckWaveClearedCoroutine()
    {
        StartCoroutine(CheckWaveCleared());
    }

    IEnumerator CheckWaveCleared()
    {
        while (true)
        {
            if(enemySpawner.CheckWaveCleared())
            {
                WaveCleared();
                yield return new WaitForSeconds(.5f);
            }
            DeactiveStartWaveButton();
            yield return new WaitForSeconds(1);
        }
    }

    void WaveCleared()
    {
        StopAllCoroutines();

        #region DestroyAllWeaponAfterWaveCleared
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        GameObject[] FireBalls = GameObject.FindGameObjectsWithTag("FireBall");
        foreach (var item in arrows)
        {
            Destroy(item);
        }
        foreach (var item in projectiles)
        {
            Destroy(item);
        }
        foreach (var item in FireBalls)
        {
            Destroy(item);
        }
        #endregion

        if(waveNumber == 12)
        {
            canvasController.ShowWin();
            return;
        }
        ActiveStartWaveButton();
    }

    public void StartWave()
    {
        DeactiveStartWaveButton();
        waveNumber++;
        waveText.text = "Wave " + waveNumber.ToString();
        canvasController.gameOverSurvivedWave.text = (waveNumber-1).ToString();
        if(waveNumber == 1) { enemySpawner.FirstWave(); return; }
        enemySpawner.AddEnemy(waveNumber*2);
        enemySpawner.StartWave();
    }

    void DeactiveStartWaveButton()
    {
        startWaveButton.interactable = false;
    }

    void ActiveStartWaveButton()
    {
        startWaveButton.interactable = true;
    }

}
