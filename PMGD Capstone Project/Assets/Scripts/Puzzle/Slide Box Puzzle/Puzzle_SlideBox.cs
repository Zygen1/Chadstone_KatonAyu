using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PuzzleStats))]
public class Puzzle_SlideBox : MonoBehaviour
{

    [SerializeField] GameObject[] pieces;
    [SerializeField] Vector2[] piecesPos;
    public GameObject emptyPiece;
    public GameObject[] allowMovepieces;
    public Vector2[] password;
    [SerializeField] int passwordIsDone;

    [Header("Debug")]
    [SerializeField] Vector2 emptyPiecePos;
    [SerializeField] PuzzleStats puzzleStats;

    private void Awake()
    {
        puzzleStats = GetComponent<PuzzleStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        for(int i = 0; i < pieces.Length; i++)
        {
            piecesPos[i] = pieces[i].transform.localPosition;
        }

        emptyPiecePos = emptyPiece.transform.localPosition;


        //check password
        for (int i = 0; i < piecesPos.Length; i++)
        {
            if (piecesPos[i] != password[i])
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
