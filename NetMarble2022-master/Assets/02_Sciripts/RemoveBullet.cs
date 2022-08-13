using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    // 충돌 콜백함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
            // 충돌 정보
            ContactPoint cp = coll.GetContact(0);
            // 충돌 좌표
            Vector3 pos = cp.point;
            // 법선 벡터 (Normal Vector)
            Vector3 _normal = -cp.normal;

            // Spark Effect 생성
            GameObject obj = Instantiate(sparkEffect, pos, Quaternion.LookRotation(_normal));
            Destroy(obj, 0.4f);
        }
    }
}
