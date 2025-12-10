using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {
    [HideInInspector]
    public Board board = null;
    [HideInInspector]
    public GlobalManager globalManager = null;

    public int maxCardsInDeck = 4;
    public int maxCardsPerRowInDeck = 2;
    public GameObject deckArea;
    public List<GameObject> handAreas = new List<GameObject>();
    public List<int> maxCardsInHandPerPlayer = new List<int>(){5,5};

    // stack of cards
    public List<Card> cardDeck = new List<Card>();
    public List<Card>[] playerHands = new List<Card>[2];
    public List<Card> discardPile = new List<Card>();

    public int MaxCardsPerTurn = 1;
    int cardsGotThisTurn = 0;

    void Start(){
        globalManager = GetComponent<GlobalManager>();
        board = globalManager.board;

        if(handAreas.Count < 2){
            // create hand areas
            for(int i = handAreas.Count; i < 2; i++){
                GameObject handArea = new GameObject("Player" + i + "HandArea");
                handArea.transform.parent = this.transform;
                handAreas.Add(handArea);
            }
        }

        // seek over deckArea if has cards in children and add to cardDeck
        cardDeck.AddRange(deckArea.GetComponentsInChildren<Card>(true));
        // check if is not null

        // remove null cards
        cardDeck.RemoveAll(card => card == null);
        // remove duplicates
        HashSet<Card> uniqueCards = new HashSet<Card>(cardDeck);
        cardDeck = new List<Card>(uniqueCards);
        Debug.Log("Deck has " + cardDeck.Count + " unique cards after removing nulls and duplicates.");

        foreach(Card card in cardDeck){
            if(card == null) Debug.LogError("Card in deck is null!");
        }

        playerHands[0] = new List<Card>();
        playerHands[1] = new List<Card>();

        // def every card board
        foreach(Card card in cardDeck) card.board = board;
        foreach(Card card in playerHands[0]) card.board = board;
        foreach(Card card in playerHands[1]) card.board = board;

        ShuffleDeck();
        PlaceCardsInDeckArea();
        PlaceCardsInHandArea(0);
        PlaceCardsInHandArea(1);
    }

    void Update(){
    }

    public void DiscardCard(Card card, int player){
        if(playerHands[player].Contains(card)){
            discardPile.Add(card);
            playerHands[player].Remove(card);
            card.gameObject.SetActive(false);
            PlaceCardsInHandArea(player);
        }
    }

    public void UseCard(Card card, int player, Cell cell){
        card.UseCard(player, cell, this);
        DiscardCard(card, player);
    }
    
    public bool AddCardToHand(Card card, int player){
        if(cardsGotThisTurn >= MaxCardsPerTurn) return false;
        if(playerHands[player].Count >= maxCardsInHandPerPlayer[player]) return false;

        playerHands[player].Add(card);
        cardDeck.Remove(card);
        cardsGotThisTurn += 1;

        PlaceCardsInDeckArea();
        PlaceCardsInHandArea(player);
        return true;
    }

    public void RemoveFromHand(int player, int amountToClear = -1){
        if(amountToClear == -1) amountToClear = playerHands[player].Count;
        else amountToClear = Mathf.Min(amountToClear, playerHands[player].Count);
        // random list of cards to remove from hand
        for(int i = 0; i < amountToClear; i++){
            int randomIndex = Random.Range(0, playerHands[player].Count);
            Card cardToRemove = playerHands[player][randomIndex];
            DiscardCard(cardToRemove, player);
        }
    }

    public void NextTurn(){ //reset turn variables
        cardsGotThisTurn = 0;
    }

    public void ShuffleDeck(){
        for (int i = 0; i < cardDeck.Count; i++){
            Card temp = cardDeck[i];
            int randomIndex = Random.Range(i, cardDeck.Count);
            cardDeck[i] = cardDeck[randomIndex];
            cardDeck[randomIndex] = temp;
        }
    }

    public void PlaceCardsInDeckArea(){
        float cardsSize = 0;
        float cardHeight = 0;
        if(cardDeck.Count > 0){
            cardsSize = cardDeck[0].GetComponent<SpriteRenderer>().bounds.size.x;
            cardHeight = cardDeck[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }
        foreach(Card card in cardDeck) card.gameObject.SetActive(false);

        Vector3 cardPos = deckArea.transform.position;
        int addedCards = 0;
        foreach(Card card in cardDeck){
            card.gameObject.SetActive(true);
            card.transform.parent = deckArea.transform;
            card.transform.position = cardPos;
            cardPos.x += cardsSize * 1.1f;
            
            addedCards += 1;
            if(addedCards >= maxCardsInDeck) break;
            if(addedCards % maxCardsPerRowInDeck == 0){
                cardPos.x = deckArea.transform.position.x;
                cardPos.y -= cardHeight * 1.1f;
            }
        }
    }

    public void PlaceCardsInHandArea(int player){
        float cardsSize = 0;
        float cardHeight = 0;
        if(cardDeck.Count > 0){
            cardsSize = cardDeck[0].GetComponent<SpriteRenderer>().bounds.size.x;
            cardHeight = cardDeck[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }

        Vector3 cardPos = handAreas[player].transform.position;
        int addedCards = 0;
        foreach(Card card in playerHands[player]){
            card.transform.parent = handAreas[player].transform;
            card.transform.position = cardPos;
            cardPos.x += cardsSize * 1.1f;
            
            addedCards += 1;
            if(addedCards % maxCardsPerRowInDeck == 0){
                cardPos.x = deckArea.transform.position.x;
                cardPos.y -= cardHeight * 1.1f;
            }
        }

    }
}
