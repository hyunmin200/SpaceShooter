using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //폭발 효과 파티클을 연결할 변수
    public GameObject expEffect;
    //무작위로 적용할 텍스처 배열
    public Texture[] textures;
    //하위에 있는 Mesh Renderer 컴포넌트를 저장할 변수
    private new MeshRenderer renderer;

    //컴포넌트를 저장할 변수
    private Transform tr;
    private Rigidbody rb;

    //총알 맞은 횟수를 누적시킬 변수
    private int hitCount = 0;
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        //하위에 있는 MeshRenderer 컴포넌트를 추출
        renderer = GetComponentInChildren<MeshRenderer>();

        //난수 발생
        int idx = Random.Range(0, textures.Length);
        //텍스쳐 지정
        renderer.material.mainTexture = textures[idx];
    }

    //충돌 시 발생하는 콜백 함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //총알 맞은 횟수를 증가시키고 3회 이상이면 폭발 처리
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        //폭발 효과 파티클 생성
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        //폭발 효과 파티클 5초 후 제거
        Destroy(exp, 5.0f);

        //Rigidbody 컴포넌트의 mass를 1.0로 수정해 무게를 가볍게 함
        rb.mass = 1.0f;
        //위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * 1500.0f);

        //3초 후에 드럼통 제거
        Destroy(gameObject, 3.0f);
    }
}
