using UnityEngine;
using System.Collections.Generic;

public class PieceCard : Card
{
    // Piece prefab to summon
    public GameObject piecePrefab;

    void Start(){
        // get the sprite renderer from the piece prefab and set as card image
        SpriteRenderer pieceSprite = piecePrefab.GetComponent<SpriteRenderer>();
        cardImage.sprite = pieceSprite.sprite;
        cardType = 0; // Piece card
    }

    public void SummonPiece(Cell cell, int player){
        board.AddPiece(piecePrefab, cell.x, cell.y, player);
    }
}
