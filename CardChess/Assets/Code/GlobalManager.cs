using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GlobalManager : MonoBehaviour{
    
    public Board board = null;
    [HideInInspector]
    public CardManager cardManager = null;
    public SpriteRenderer turnIndicator = null;
    public int turn = 0, turnCount = 0;
    public bool isPaused = false;
    public PauseMenu pauseMenu;
    public GameObject evoScreen;
    [HideInInspector]
    public GameObject lastScreen = null;
    [Header("Evolution UI")]
    public Transform evoButtonContainer; // parent with GridLayoutGroup
    public GameObject evoButtonPrefab;

private Piece evolvingPiece;


    void Start(){
        cardManager = GetComponent<CardManager>(); // same object as card manager
        board.globalManager = this;
        turn = 0;
    }

    void Update(){ }

    public void NextTurn(){ //reset turn variables
        turn ^= 1; // switch turn
        turnCount += 1;
        board.UpdateTurnState();
        cardManager.NextTurn();
        if(turnIndicator) turnIndicator.color = turn == 0 ? Color.white : Color.gray;
    }

    public void ActionComplete(int actionsUsed=-1){
        int currentPlayer = turn;
        int opponent = turn ^ 1;

        // after action, before switching turn
        if (board.IsCheckmate(opponent))
        {
            GameOver(currentPlayer);
            return;
        }
            // Check for stalemate
        if (board.IsStalemate(opponent))
        {
            Debug.Log("Stalemate!");
            // decide behavior: draw, restart, etc.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        NextTurn();
    }

    private Card selectedCard = null;
    private Cell selectedCell = null;

    public void ClickedCard(Card card){
        if(isPaused) return;
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
    public void GameOver(int winningPlayer){
        Debug.Log("Game Over! Player " + winningPlayer + " wins.");

        // Optional: disable input
        enabled = false;

        // Optional: visual feedback
        if(turnIndicator){
            turnIndicator.color = Color.red;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // TODO:
        // - show UI popup
        // - restart button
        // - block further moves
    }
    public void ClickedCell(Cell cell){
        if(isPaused) return;
        // select this cell
        if(selectedCell == null && selectedCard == null){
            if(cell.piece != null && cell.piece.player == turn){
                selectedCell = cell;
                board.AddHighlight(cell, turn);
            }
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

                if (piece && !board.WouldMoveCauseCheck(piece, cell)){
                    piece.MoveToCell(cell);
                }
                else{
                    board.ClearHighlights();
                    selectedCell = null;
                    return;
                }
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

    public void HandleEvolution(Piece piece)
    {
        Debug.Log("Handling evolution for piece at " + piece.pieceType);

        if (piece.evolutions == null || piece.evolutions.Count == 0)
            return;

        PauseGame();

        evolvingPiece = piece;
        lastScreen = pauseMenu.atual;

        evoScreen.SetActive(true);

        // Clear old buttons
        foreach (Transform child in evoButtonContainer)
            Destroy(child.gameObject);

        RectTransform prefabRT = evoButtonPrefab.GetComponent<RectTransform>();
        float buttonSize = prefabRT.rect.width;

        float spacing = 1.1f * buttonSize;
        float half = (piece.evolutions.Count - 1) / 2f;

        Vector2 pos = new Vector2(-half * spacing, 0f);

        foreach (GameObject evoPrefab in piece.evolutions)
        {
            GameObject btnObj = Instantiate(evoButtonPrefab, evoButtonContainer);

            EvoButton btn = btnObj.GetComponent<EvoButton>();
            btn.Init(this, piece, evoPrefab);

            RectTransform btnRT = btnObj.GetComponent<RectTransform>();
            btnRT.anchoredPosition = pos;

            pos.x += spacing;
        }

    }

    public void ConfirmEvolution(Piece oldPiece, GameObject evolutionPrefab){
        evoScreen.SetActive(false);
        UnpauseGame();
        board.ChangePiece(oldPiece, evolutionPrefab);
    }

    public void PauseGame(){ isPaused = true; }
    public void UnpauseGame(){ isPaused = false; }
}
