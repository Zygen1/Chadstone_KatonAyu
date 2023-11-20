using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PiecePuzzleSlideBox : MonoBehaviour
{
    /*[Header("Atribut")]
    [SerializeField] float moveSpeed;*/

    [Header("Debug")]
    [SerializeField] bool isCanMove;
    [SerializeField] Vector2 lastPos;

    PuzzleStats stats;
    Puzzle_SlideBox puzzleSlideBox;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponentInParent<PuzzleStats>();
        puzzleSlideBox = GetComponentInParent<Puzzle_SlideBox>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < puzzleSlideBox.allowMovepieces.Length; i++)
        {
            if(Vector2.Distance(transform.position, puzzleSlideBox.allowMovepieces[i].transform.position) < 0.1f)
            {
                //Debug.Log("Bisa gerak");
                isCanMove = true;
                break;
            }
            else if (Vector2.Distance(transform.position, puzzleSlideBox.allowMovepieces[i].transform.position) > 0.1f)
            {
                //Debug.Log("GK");
                isCanMove = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if(!stats.isDone)
        {
            if (isCanMove)
            {
                lastPos = transform.position;
                transform.position = puzzleSlideBox.emptyPiece.transform.position;
                puzzleSlideBox.emptyPiece.transform.position = lastPos;
                Debug.LogWarning("Move piece");
            }
        }
    }
}
