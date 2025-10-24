using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnInterval = 3f; // 생성 간격
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMonsters(); // 여러 마리 생성
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnMonsters()
    {
        // 화면 경계 계산
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));

        float screenWidth = rightEdge.x - leftEdge.x;
        float sectionWidth = screenWidth / 5f; // 화면을 5등분

        // 🎯 이번에 생성할 몬스터 수 (1~5 랜덤)
        int monsterCount = Random.Range(1, 6);

        // 랜덤한 위치들을 뽑기 위한 리스트
        System.Collections.Generic.List<int> availableSections = new System.Collections.Generic.List<int>() { 0, 1, 2, 3, 4 };

        for (int i = 0; i < monsterCount; i++)
        {
            // 남은 섹션 중 하나 랜덤 선택
            int randomIndex = Random.Range(0, availableSections.Count);
            int randomSection = availableSections[randomIndex];

            // 해당 섹션을 중복 방지를 위해 제거
            availableSections.RemoveAt(randomIndex);

            // 섹션 중앙 X좌표 계산
            float spawnX = leftEdge.x + sectionWidth * randomSection + sectionWidth / 2f;
            Vector3 spawnPos = new Vector3(spawnX, topEdge.y + 1f, 0);

            // 몬스터 생성
            Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
        }
    }
}
