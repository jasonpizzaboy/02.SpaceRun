using UnityEngine;
using System.Collections;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들, 배열로 선언
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가


    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    public void OnEnable() {                         //실행순서 : awake(1번) - onenable(여러번 - 초기화 처리) - start   - fixedupdate - ontrigger - oncollision-
        // 발판을 리셋하는 처리                            //리소스, 스타트보다 먼저 실행될 것들 - awake , 풀링에서 초기화 - onenable, 나머지 - start

        stepped = false;                              //풀링을 불러올때, 새로 생기면 false로 초기화

        for(int i=0;i<obstacles.Length;i++)           //obstacles 배열의 length 길이를 기록함.
        {
            if(Random.Range(0,3)==0)                 // random.range(범위) - 랜덤으로 부르는 것, 마지막수를 제외한 나머지가 불러짐
            {                                       //3번중에(0,1,2) 에서 0이 나올때, 1/3확률로
                obstacles[i].SetActive(true);        //참으로 활성화
            }

            else
            {
                obstacles[i].SetActive(false);        //나머지는 거짓을 활성화.
            }
        }
    }
   

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리

        if (collision.collider.tag == "Player" && !stepped)     //충돌시 콜라이더에 묻은 정보의 태그가 플레이어이고, 스텝이 true일때  
        {
            stepped = true;                                    //스텝을 true로 변환
            GameManager.instance.AddScore(1);                  //GameManager스크립트의 instance 변수의 AddScore함수에서 점수1증ㄱ
        }

    }
}