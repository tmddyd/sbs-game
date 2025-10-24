// GameManager ��ũ��Ʈ
using UnityEngine;
using System.Collections;  // �� �� �� �߰�
using UnityEngine.SceneManagement; // �� ��ε带 ���� �ʿ�

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
        // ���� Ȱ��ȭ�� �� �̸� ��������
        string currentScene = SceneManager.GetActiveScene().name;

        // ���� �� �ٽ� �ε�
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }
}
