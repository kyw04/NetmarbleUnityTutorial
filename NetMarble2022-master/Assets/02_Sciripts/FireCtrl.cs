#pragma warning disable IDE0051, CS0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    AudioListener
    AudioSource
*/

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;

    public AudioClip fireSfx;
    private AudioSource audio;

    public MeshRenderer muzzleFlash;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();

        muzzleFlash.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Instantiate(생성할객체, 위치, 각도)
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        // Sound Play
        audio.PlayOneShot(fireSfx, 0.8f);

        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        // 텍스처의 Offset 변경
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        // 회전
        /*
            Quaternion 쿼터니언 (사원수) x, y, z, w

            Euler Rotation
            Gimbal Lock (짐벌락)

            Quaternion.LookRotation(벡터)
            Quaternion.Euler(x, y, z)
        */
        float angle = Random.Range(0.0f, 360.0f);
        Quaternion rot = Quaternion.Euler(Vector3.forward * angle);
        muzzleFlash.transform.localRotation = rot;

        // Scale
        float scale = Random.Range(1.0f, 2.5f);
        muzzleFlash.transform.localScale = Vector3.one * scale; // new Vector3(scale, scale, scale)

        // MuzzleFlash 활성화
        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.3f);

        // MuzzleFlash 비활성화
        muzzleFlash.enabled = false;
    }
}
