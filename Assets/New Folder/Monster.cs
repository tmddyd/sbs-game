using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 3f; // 1�ʿ� 3ĭ �Ʒ��� �̵�
    private float bottomLimit;

    [Header("���� ����")]
    public GameObject coinPrefab; // ������ ���� ������
    public int coinCount = 3; // ������ ���� ����
    public float coinSpread = 1.0f; // ������ ������ ����
    public float coinForce = 3f; // ���� Ƣ����� ��

    void Start()
    {
        // ȭ�� �Ʒ��� ��� ���
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        bottomLimit = bottomEdge.y - 1f; // �ణ ����
    }

    void Update()
    {
        // �Ʒ��� �̵�
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // ȭ�� �Ʒ��� ������ ����
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
            // ���� ���� ��ġ �ణ ����
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-coinSpread, coinSpread), 0, 0);
            GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

            // Rigidbody2D�� ���� �� ���� ƨ�ܳ����� ��
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ������ �������� �ణ ������ ������ ���� ����
                Vector2 forceDir = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
                rb.AddForce(forceDir * coinForce, ForceMode2D.Impulse);

                // ȸ�� ȿ���� �ְ� �ʹٸ� �Ʒ� �� �� �߰�
                rb.AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);
            }
        }
    }
}
