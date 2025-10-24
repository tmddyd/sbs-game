using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove2D : MonoBehaviour
{
    public float speed = 2f; // 1초에 2칸 이동
    private float minX, maxX;
    private float halfWidth;

    [Header("미사일 관련")]
    public GameObject missilePrefab;   // 기본 미사일
    public GameObject missile2Prefab;  // 점수 5 이상일 때 사용할 미사일
    public Transform firePoint;        // 발사 위치

    [Header("UI 관련")]
    public GameObject gameOverUI;
    public Text scoreText;

    private int score = 0;

    void Start()
    {
        // 카메라 경계 계산
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        // 콜라이더 크기 가져오기
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            halfWidth = col.bounds.extents.x;
        else
            halfWidth = 0.5f;

        minX = leftEdge.x + halfWidth;
        maxX = rightEdge.x - halfWidth;

        // UI 초기 상태
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        UpdateScoreText();
    }

    void Update()
    {
        // 좌우 이동
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(moveInput * speed * Time.deltaTime, 0, 0);
        transform.position += move;

        // 화면 밖 제한
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;

        // 미사일 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireMissile();
        }
    }

    void FireMissile()
    {
        // ✅ 점수에 따라 다른 미사일 발사
        if (score >= 5 && missile2Prefab != null)
        {
            Instantiate(missile2Prefab, firePoint.position, Quaternion.identity);
        }
        else
        {
            Instantiate(missilePrefab, firePoint.position, Quaternion.identity);
        }
    }

    // 충돌 감지
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            FindObjectOfType<GameManager>().ShowGameOverUI(1f);
            Destroy(gameObject);
        }
        else if (other.CompareTag("coin"))
        {
            AddScore(1);
            Destroy(other.gameObject);
        }
    }

    void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "점수 : " + score.ToString();
    }
}
