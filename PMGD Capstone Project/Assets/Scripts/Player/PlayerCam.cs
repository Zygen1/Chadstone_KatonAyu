using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothSpeed = 5.0f; // Kecepatan perpindahan kamera

    private Vector3 offset; // Jarak awal antara kamera dan pemain

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position; // Menghitung jarak awal
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        // Lerp digunakan untuk mengubah posisi kamera secara mulus
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
    }
}
