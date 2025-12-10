using UnityEngine;
using System.Collections.Generic;

public class Card : MonoBehaviour {
    public string cardName = "Unnamed Card";
    public string description = "No description";
    public SpriteRenderer cardImage;
    
    [HideInInspector]
    public Board board = null;
    [HideInInspector]
    public int cardType = 0; // 0: Piece, 1: Action
    
    void Update(){
        // if clicked, tell global manager
        if(Input.GetMouseButtonDown(0)){
        
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // check if mouse is over the card
            Collider2D collider = GetComponent<Collider2D>();
            
            if(collider == Physics2D.OverlapPoint(mousePos2D)){
                board.globalManager.ClickedCard(this);
            }
        }
    }

    public virtual List<Cell> GetHighlightedCells(int player){
        return new List<Cell>();
    }

    public virtual void UseCard(int player, Cell cell, CardManager cardManager){
        
    }
}

/*
Cards can be:
    - Piece cards: summon a piece to the board
    - Action cards: perform special actions
        - Spell cards: cast a spell with various effects
        - Buff cards: enhance a piece's abilities
*/
