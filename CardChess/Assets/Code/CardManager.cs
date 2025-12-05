using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {
    [HideInInspector]
    public Board board = null;
    [HideInInspector]
    public GlobalManager globalManager = null;

    // stack of cards
    public List<Card> cardDeck = new List<Card>();
    public List<Card>[] playerHands = new List<Card>[2];
    public List<Card> discardPile = new List<Card>();

    public int MaxCardsPerTurn = 1;
    int cardsGotThisTurn = 0;

    void Start(){
        globalManager = GetComponent<GlobalManager>();
        board = globalManager.board;

        // def every card board
        foreach(Card card in cardDeck) card.board = board;

        playerHands[0] = new List<Card>();
        playerHands[1] = new List<Card>();

        foreach(Card card in playerHands[0]) card.board = board;
        foreach(Card card in playerHands[1]) card.board = board;
    }

    void Update(){
    }

    public void UseCard(Card card, int player, Cell cell){
        if(card.cardType == 0){ // Piece card
            PieceCard pieceCard = (PieceCard)card;
            pieceCard.SummonPiece(cell, player);

            // remove card from hand
            discardPile.Add(card);
            playerHands[player].Remove(card);
            Destroy(card.gameObject);
        }
    }
    
    public void AddCardToHand(Card card, int player){
        if(cardsGotThisTurn >= MaxCardsPerTurn) return;

        playerHands[player].Add(card);
        cardDeck.Remove(card);
        cardsGotThisTurn += 1;
    }

    public void NextTurn(){ //reset turn variables
        cardsGotThisTurn = 0;
    }
}
