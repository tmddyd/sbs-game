using UnityEngine;

public class MoveDownLoop : MonoBehaviour
{
    public float speed = 2f;     // 1�ʿ� 2ĭ �̵�
    public float lowerLimit = -10f;
    public float upperPosition = 10f;

    void Update()
    {
        // �� �����Ӹ��� y������ �Ʒ� �̵�
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // y ��ǥ�� -10 ���Ϸ� �������� �����̵�
        if (transform.position.y <= lowerLimit)
        {
            Vector3 newPos = transform.position;
            newPos.y = upperPosition;
            transform.position = newPos;
        }
    }
}
