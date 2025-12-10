using UnityEngine;
using System.Collections.Generic;

public class GlobalManager : MonoBehaviour{
    
    public Board board = null;
    [HideInInspector]
    public CardManager cardManager = null;
    public SpriteRenderer turnIndicator = null;
    int turn = 0, turnCount = 0;

    void Start(){
        cardManager = GetComponent<CardManager>(); // same object as card manager
        board.globalManager = this;
        turn = 0;
    }

    void Update(){ }

    public void NextTurn(){ //reset turn variables
        turn ^= 1; // switch turn
        turnCount += 1;
        cardManager.NextTurn();
        if(turnIndicator) turnIndicator.color = turn == 0 ? Color.white : Color.gray;
    }

    public void ActionComplete(int actionsUsed=-1){
        NextTurn();
    }

    private Card selectedCard = null;
    private Cell selectedCell = null;

    public void ClickedCard(Card card){
        if(selectedCard == card){
            selectedCard = null;
            board.ClearHighlights();
            return;
        }
        if(cardManager.cardDeck.Contains(card)){
            if(cardManager.AddCardToHand(card, turn))
                ActionComplete(3);
            return;
        }
        if(!cardManager.playerHands[turn].Contains(card)) return;
        
        selectedCard = card;
        
        board.ClearHighlights();
        List<Cell> highlightedCells = card.GetHighlightedCells(turn);
        
        if(highlightedCells.Count == 0) selectedCard = null; // no valid cells
        else board.AddHighlights(highlightedCells);
    }

    public void ClickedCell(Cell cell){
        
        // select this cell
        if(selectedCell == null && selectedCard == null){
            selectedCell = cell;
            board.AddHighlight(cell, turn);
            return;
        }

        // deselect
        if(selectedCell == cell && selectedCard == null){
            board.ClearHighlights();
            selectedCell = null;
            return;
        }

        // if clicked cell is highlighted, move piece
        if(board.IsCellHighlighted(cell)){

            if(selectedCard){
                cardManager.UseCard(selectedCard, turn, cell);
                board.ClearHighlights();
                selectedCell = null;
                selectedCard = null;
                ActionComplete(2);
                return;
            }

            if(selectedCell.piece){
                Piece piece = selectedCell.piece;

                if(piece) piece.MoveToCell(cell);

                board.ClearHighlights();
                selectedCell = null;
                ActionComplete(1);
            }

            return;
        }

        // clicked cell is not highlighted, clear selection
        board.ClearHighlights();
        selectedCell = null;
        selectedCard = null;
    }

    public void HandleEvolution(Piece piece){
        Debug.Log("Handling evolution for piece at " + piece.pieceType);
        Debug.Log("Evolutions available: " + piece.evolutions.Count);
        if(piece.evolutions.Count == 0) return;

        // for simplicity, just evolve to the first option
        GameObject evolutionPrefab = piece.evolutions[0];

        // create new piece
        GameObject newPieceObj = Instantiate(evolutionPrefab);
        newPieceObj.transform.parent = board.transform;

        Piece newPiece = newPieceObj.GetComponent<Piece>();
        newPiece.player = piece.player;
        newPiece.MoveToCell(piece.cell);

        // remove old piece
        board.pieces.Remove(piece);
        Destroy(piece.gameObject);

        board.pieces.Add(newPiece);

        Debug.Log("Piece evolved for player " + newPiece.player + " at cell " + newPiece.cell.x + "," + newPiece.cell.y);
    }
}
