using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeWordsPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] words;
    [SerializeField] bool passwordCorrect;

    PuzzleStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PuzzleStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //Menukar objek
        for(int i = 0; i < words.Length; i++)
        {
            WordObject wordsObject = words[i].GetComponent<WordObject>();

            if (wordsObject.isMove)
            {

                //menukar posisi objek
                if (i == 0)
                {
                    Vector3 tempPos = words[i].transform.position;
                    words[i].transform.position = words[words.Length - 1].transform.position;
                    words[words.Length - 1].transform.position = tempPos;
                }
                else
                {
                    Vector3 tempPos = words[i].transform.position;
                    words[i].transform.position = words[i-1].transform.position;
                    words[i-1].transform.position = tempPos;
                }

                //Menukar index
                if (i == 0)
                {
                    int lastIndex = words.Length - 1;
                    GameObject tempObject = words[i];
                    words[i] = words[lastIndex];
                    words[lastIndex] = tempObject;
                }
                else
                {
                    int targetIndex = i - 1;
                    GameObject tempObject = words[i];
                    words[i] = words[targetIndex];
                    words[targetIndex] = tempObject;
                }

                wordsObject.isMove = false;
            }
        }


        //Check Password
        //bool passwordCorrect = true; // Flag untuk menunjukkan apakah kata sandi benar atau salah

        for (int i = 0; i < words.Length; i++)
        {
            WordObject wordObject = words[i].GetComponent<WordObject>();
            if (wordObject.order != (i + 1))
            {
                passwordCorrect = false;
                break; // Jika ada yang salah, hentikan loop
            }
            passwordCorrect = true;
        }

        if (passwordCorrect)
        {
            stats.isDone = true;
            Debug.Log("SELESAI");
        }
        else
        {
            stats.isDone = false;
        }

    }
}
