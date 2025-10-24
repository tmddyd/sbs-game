using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f; // 1�ʿ� 5ĭ �̵�
    private float topLimit;  // ȭ�� ���� ���

    void Start()
    {
        // ī�޶��� ���� ��� ���
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        topLimit = topEdge.y + 1f; // ������ ���� ��
    }

    void Update()
    {
        // ���� �̵�
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // ȭ�� ������ ������ ����
        if (transform.position.y > topLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
