using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f; // 1초에 5칸 이동
    private float topLimit;  // 화면 위쪽 경계

    void Start()
    {
        // 카메라의 위쪽 경계 계산
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        topLimit = topEdge.y + 1f; // 여유를 조금 줌
    }

    void Update()
    {
        // 위로 이동
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 화면 밖으로 나가면 삭제
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
