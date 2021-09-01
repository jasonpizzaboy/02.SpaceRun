using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.0f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들                       //인덱스는 0-2까지 생성
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성

        platforms = new GameObject[count];           //아직은 껍데기(주소/빈방)만 있고, instansiate 되지 않은 상황! 알맹이는 없음

        for(int i=0;i<count;i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity); //instantiate(프리펩, 풀포지션, 쿼터니언 아이덴티티 - 회전값이 없음) = 빈방에 내용 채우는 과정!
        }

        lastSpawnTime = 0f;                             //마지막 배치시점 초기화
        timeBetSpawn = 0f;                              //다음번 배치까지의 시간간격을 0으로 초기화
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치

        if(GameManager.instance.isGameover)
        {
            return;
        }

        if(Time.time>=lastSpawnTime+timeBetSpawn)                                   //Time.time 게임시작후 흐른 시간 / 마지막 만든시간, 생성시간보다 크면 
        {
            lastSpawnTime = Time.time;                                              //마지막으로 만든 시간 = 현재시간
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);          //최대 최솟값 사이에서 랜덤으로 생성시간 입력


            float ypos = Random.Range(yMin, yMax);                                  //y값을 최대/최솟값 사이에서 랜덤으로 생성

            platforms[currentIndex].SetActive(false);                               //플랫폼 sc에서 onenabled 부르기 위한 코드
            platforms[currentIndex].SetActive(true);

            platforms[currentIndex].transform.position = new Vector2(xPos, ypos);   //플랫폼 포지션을 새로운 x,y에 위치시킴
            currentIndex++;                                                         //플랫폼을 +1해줌, 다음 플랫폼 출력

            if(currentIndex>=count)
            {
                currentIndex = 0;                                                   //플랫폼 번호가 카운트 이상이면 다시 0번 플랫폼으로 회ㄱ
            }
        }
    }
}