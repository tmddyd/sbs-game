using UnityEngine;

public class MoveDownLoop : MonoBehaviour
{
    public float speed = 2f;     // 1초에 2칸 이동
    public float lowerLimit = -10f;
    public float upperPosition = 10f;

    void Update()
    {
        // 매 프레임마다 y축으로 아래 이동
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // y 좌표가 -10 이하로 내려가면 순간이동
        if (transform.position.y <= lowerLimit)
        {
            Vector3 newPos = transform.position;
            newPos.y = upperPosition;
            transform.position = newPos;
        }
    }
}
