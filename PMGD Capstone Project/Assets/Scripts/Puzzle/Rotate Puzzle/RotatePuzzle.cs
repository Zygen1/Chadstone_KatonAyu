using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePuzzle : MonoBehaviour
{
    public GameObject[] parts;
    public Vector3[] partsRotation; // Menggunakan Vector3 untuk menyimpan rotasi Euler.
    public Vector3[] password;
    [SerializeField] int passwordIsDone;


    [Header("Debug")]
    [SerializeField] PuzzleStats puzzleStats;

    private void Awake()
    {
        puzzleStats = GetComponent<PuzzleStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        partsRotation = new Vector3[parts.Length]; // Inisialisasi array dengan panjang yang sesuai.
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            // Mengkonversi quaternion ke rotasi Euler (X, Y, Z).
            partsRotation[i] = parts[i].transform.localRotation.eulerAngles;
            partsRotation[i].z = Mathf.Round(partsRotation[i].z);
        }

        for(int i = 0; i < partsRotation.Length; i++)
        {
            if (partsRotation[i] != password[i])
            {
                Debug.Log("Password false");
                passwordIsDone = 0;
                break;
            }

            passwordIsDone++;
            passwordIsDone = Mathf.Clamp(passwordIsDone, 0, password.Length);
        }

        if(passwordIsDone >= password.Length)
        {
            puzzleStats.isDone = true;
            Debug.Log("Password true");
        }
    }
}
