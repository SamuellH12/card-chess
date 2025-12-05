using UnityEngine;
using System.Collections.Generic;

public class GlobalManager : MonoBehaviour{
    
    public Board board = null;
    int turn = 0, turnCount = 0;
    int cardsGotThisTurn = 0;

    private Cell selectedCell = null;
    private List<Cell> highlightedCells = new List<Cell>();
    
    // stack of cards
    public List<Card> cardDeck = new List<Card>();
    public List<Card>[] playerHands = new List<Card>[2];
    public List<Card> discardPile = new List<Card>();

    void Start(){
        board.globalManager = this;
        turn = 0;
        
        // def every card board
        foreach(Card card in cardDeck) card.board = board;

        playerHands[0] = new List<Card>();
        playerHands[1] = new List<Card>();

        foreach(Card card in playerHands[0]) card.board = board;
        foreach(Card card in playerHands[1]) card.board = board;
    }

    void Update(){ }

    public void NextTurn(){ //reset turn variables
        turn ^= 1; // switch turn
        turnCount += 1;
        cardsGotThisTurn = 0;
    }

    private Card selectedCard = null;
    public void ClickedCard(Card card){
        if(cardDeck.Contains(card)){
            if(cardsGotThisTurn >= 1) return; // only 1 card per turn
            AddCardToHand(card, turn);
            return;
        }
        if(!playerHands[turn].Contains(card)) return;
        
        selectedCard = card;
        ClearHighlights();
        
        if(card.cardType == 0){ // Piece card
            foreach(Cell cell in board.GetEmptyCells()){ // if cell is empty and is the first or second row for the player
                if((turn == 0 && cell.y <= 1) || (turn == 1 && cell.y >= board.H - 2)){
                    highlightedCells.Add(cell);
                    cell.HighlightCell();
                }
            }
            if(highlightedCells.Count == 0) selectedCard = null; // no valid cells
        }

    }

    public void ClickedCell(Cell cell){
        
        // select this cell
        if(selectedCell == null && selectedCard == null){
            selectedCell = cell;
            AddHighlight(cell);
            return;
        }

        // deselect
        if(selectedCell == cell && selectedCard == null){
            ClearHighlights();
            selectedCell = null;
            return;
        }

        // if clicked cell is highlighted, move piece
        if(highlightedCells.Contains(cell)){

            if(selectedCard){

                if(selectedCard.cardType == 0){ // Piece card
                    PieceCard pieceCard = (PieceCard)selectedCard;
                    pieceCard.SummonPiece(cell, turn);

                    // remove card from hand
                    discardPile.Add(selectedCard);
                    playerHands[turn].Remove(selectedCard);
                    Destroy(selectedCard.gameObject);
                }
                
                ClearHighlights();
                selectedCell = null;
                selectedCard = null;
                return;
            }

            if(selectedCell.piece){
                Piece piece = selectedCell.piece;

                if(piece) piece.MoveToCell(cell);

                ClearHighlights();
                selectedCell = null;
                NextTurn();
            }

            return;
        }

        // clicked cell is not highlighted, clear selection
        ClearHighlights();
        selectedCell = null;
    }

    void AddHighlight(Cell cell){
        Debug.Log("Highlighting cell " + cell.x + "," + cell.y);
        ClearHighlights();
        
        highlightedCells.Add(cell);
        cell.HighlightCell();

        if(cell.piece == null || cell.piece.player != turn ) return;

        List<Cell> moves = cell.piece.ListOfMoves(board);
        moves.AddRange(cell.piece.ListOfAttacks(board)); // add atacks as well
        moves = new List<Cell>(new HashSet<Cell>(moves)); // unique

        foreach(Cell move in moves){
            highlightedCells.Add(move);
            move.HighlightCell();
        }
    }

    void ClearHighlights(){
        foreach(Cell cell in highlightedCells) cell.ClearHighlight();
        highlightedCells.Clear();
    }

    public void HandleEvolution(Piece piece){
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

    public void AddCardToHand(Card card, int player){
        playerHands[player].Add(card);
        cardDeck.Remove(card);
        cardsGotThisTurn += 1;
    }

}
