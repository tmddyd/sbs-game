// GameManager 스크립트
using UnityEngine;
using System.Collections;  // ← 이 줄 추가
using UnityEngine.SceneManagement; // 씬 재로드를 위해 필요

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void ShowGameOverUI(float delay)
    {
        StartCoroutine(EnableUIAfterDelay(delay));
    }

    IEnumerator EnableUIAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
     public void RestartGame()
    {
        // 현재 활성화된 씬 이름 가져오기
        string currentScene = SceneManager.GetActiveScene().name;

        // 같은 씬 다시 로드
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }
}
