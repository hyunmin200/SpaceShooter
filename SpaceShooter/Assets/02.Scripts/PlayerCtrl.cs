using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //컴포넌트를 캐시 처리할 변수
    private Transform tr;
    //Animation 컴포넌트를 저장할 변수
    private Animation anim;
    //이동 속도 변수 (public으로 선언되어 인스펙터 뷰에 노출됨)
    public float moveSpeed = 10.0f;
    //회전 속도 변수
    public float turnSpeed = 80.0f;
    void Start()
    {
        //Transform 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        //애니메이션 실행
        anim.Play("Idle");
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        //Debug.Log("h=" + h);
        //Debug.Log("v=" + v);

        //정규화 백터를 사용한 코드
        //tr.position += Vector3.forward * 1;

        //프레임마다 10유닛씩 이동
        //tr.Translate(Vector3.forward * 10);

        //매 초 10유닛씩 이동
        //tr.Translate(Vector3.forward * Time.deltaTime * 10);

        //Translate 함수를 사용한 이동 로직
        //tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);
        //쉽게 풀이하자면
        //tr.Translate({이동할 방향} * Time.deltaTime * {전진/후진 변수} * {속도});

        //전후좌우 이동 방향 백터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //Trans(이동 방향 * 속력 * Time.deltaTime)
        //정규화 벡터를 적용함
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        //Vector3.up 축을 기준으로 turnSpeed만큼의 속도로 회전
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        //주인공 캐릭터의 애니메이션 설정
        playerAnim(h, v);
    }

    void playerAnim(float h, float v)
    {
        //키보드 입력값을 기준으로 동작할 애니메이션 실행
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f); //전진 애니메이션 실행
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f); //후진 애니메이션 실행
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunL", 0.25f); //오른쪽 이동 애니메이션 실행
        }
        else
        {
            anim.CrossFade("Idle", 0.25f); //정지 시 Idle 애니메이션 실행
        }
    }
}
