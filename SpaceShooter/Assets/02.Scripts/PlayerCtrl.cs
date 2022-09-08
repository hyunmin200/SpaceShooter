using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //������Ʈ�� ĳ�� ó���� ����
    private Transform tr;
    //Animation ������Ʈ�� ������ ����
    private Animation anim;
    //�̵� �ӵ� ���� (public���� ����Ǿ� �ν����� �信 �����)
    public float moveSpeed = 10.0f;
    //ȸ�� �ӵ� ����
    public float turnSpeed = 80.0f;
    void Start()
    {
        //Transform ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        //�ִϸ��̼� ����
        anim.Play("Idle");
    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        //Debug.Log("h=" + h);
        //Debug.Log("v=" + v);

        //����ȭ ���͸� ����� �ڵ�
        //tr.position += Vector3.forward * 1;

        //�����Ӹ��� 10���־� �̵�
        //tr.Translate(Vector3.forward * 10);

        //�� �� 10���־� �̵�
        //tr.Translate(Vector3.forward * Time.deltaTime * 10);

        //Translate �Լ��� ����� �̵� ����
        //tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);
        //���� Ǯ�����ڸ�
        //tr.Translate({�̵��� ����} * Time.deltaTime * {����/���� ����} * {�ӵ�});

        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //Trans(�̵� ���� * �ӷ� * Time.deltaTime)
        //����ȭ ���͸� ������
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        //Vector3.up ���� �������� turnSpeed��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        //���ΰ� ĳ������ �ִϸ��̼� ����
        playerAnim(h, v);
    }

    void playerAnim(float h, float v)
    {
        //Ű���� �Է°��� �������� ������ �ִϸ��̼� ����
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f); //���� �ִϸ��̼� ����
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f); //���� �ִϸ��̼� ����
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunL", 0.25f); //������ �̵� �ִϸ��̼� ����
        }
        else
        {
            anim.CrossFade("Idle", 0.25f); //���� �� Idle �ִϸ��̼� ����
        }
    }
}
