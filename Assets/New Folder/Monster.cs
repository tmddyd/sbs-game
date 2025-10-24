using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 3f; // 1초에 3칸 아래로 이동
    private float bottomLimit;

    [Header("코인 설정")]
    public GameObject coinPrefab; // 생성할 코인 프리팹
    public int coinCount = 3; // 떨어질 코인 개수
    public float coinSpread = 1.0f; // 코인이 퍼지는 범위
    public float coinForce = 3f; // 코인 튀어나가는 힘

    void Start()
    {
        // 화면 아래쪽 경계 계산
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        bottomLimit = bottomEdge.y - 1f; // 약간 여유
    }

    void Update()
    {
        // 아래로 이동
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // 화면 아래로 나가면 삭제
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("attack"))
        {
            DropCoins();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("attack"))
        {
            DropCoins();
            Destroy(gameObject);
        }
    }

    void DropCoins()
    {
        if (coinPrefab == null) return;

        for (int i = 0; i < coinCount; i++)
        {
            // 코인 생성 위치 약간 랜덤
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-coinSpread, coinSpread), 0, 0);
            GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

            // Rigidbody2D가 있을 때 위로 튕겨나가게 함
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 무작위 방향으로 약간 포물선 형태의 힘을 가함
                Vector2 forceDir = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
                rb.AddForce(forceDir * coinForce, ForceMode2D.Impulse);

                // 회전 효과도 주고 싶다면 아래 한 줄 추가
                rb.AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);
            }
        }
    }
}
