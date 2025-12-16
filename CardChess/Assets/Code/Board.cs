using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    public int H = 8;
    public int W = 8;
    public GameObject cellPrefab;
    
    [HideInInspector]
    public Cell[,] cells;
    
    [HideInInspector]
    public List<Piece> pieces;

    [HideInInspector]
    public GlobalManager globalManager;

    // list of prefabs with initial positions and color // show in inspector    [System.Serializable]
    [System.Serializable]
    public class InitialPiece {
        public GameObject prefab;
        public int x, y, player;
        public InitialPiece(GameObject prefab, int x, int y, int player){ this.prefab = prefab; this.x = x; this.y = y; this.player = player; }
        // to string
        public override string ToString(){ return prefab.name + "(x=" + x + ", y=" + y + ", player=" + player + ")"; }
    }

    [Header("Initial pieces (set in inspector)")]
    public List<InitialPiece> initialPieces = new List<InitialPiece>();
    // piece prefabs
    public GameObject kingPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        pieces = new List<Piece>();
        this.transform.position = Vector3.zero;
        RecreateBoard();

        // check if king exists in initial pieces, else add them
        bool kingWhiteExists = false;
        bool kingBlackExists = false;
        foreach(InitialPiece ip in initialPieces){
            ip.x = (ip.x + H) % H;
            ip.y = (ip.y + W) % W;
            AddPiece(ip.prefab, ip.x, ip.y, ip.player);
            if(ip.prefab == kingPrefab){
                if(ip.player == 0) kingWhiteExists = true;
                if(ip.player == 1) kingBlackExists = true;
            }
        }
        
        // seek over chieldren of board for pieces as well
        foreach(Transform child in transform){
            foreach(Transform grandChild in child){
                Piece grandPiece = grandChild.GetComponent<Piece>();
                if(grandPiece != null){
                    AddPiece(grandPiece, (int)grandPiece.transform.position.x, (int)grandPiece.transform.position.y, grandPiece.player);
                    Debug.Log("Found piece on board: " + grandPiece.pieceType + " at " + grandPiece.transform.position.x + "," + grandPiece.transform.position.y);
                    if(grandPiece.pieceType == "King"){
                        if(grandPiece.player == 0) kingWhiteExists = true;
                        if(grandPiece.player == 1) kingBlackExists = true;
                    }
                }
            }
        }

        if(!kingWhiteExists) AddPiece(kingPrefab, 4, 0, 0);
        if(!kingBlackExists) AddPiece(kingPrefab, 4, H-1, 1);

        RescaleBoard();
    }

    // Update is called once per frame
    void Update(){ }

    // add piece to the board by prefab
    public bool AddPiece(GameObject piecePrefab, int x, int y, int player){
        if(x < 0 || x >= H || y < 0 || y >= W) return false;
        if(cells[x,y].piece != null) return false;

        GameObject pieceObj = Instantiate(piecePrefab);
        pieceObj.transform.parent = this.transform;

        Piece piece = pieceObj.GetComponent<Piece>();
        piece.player = player;
        piece.MoveToCell(cells[x,y]);
        piece.hasMoved = false;
        pieces.Add(piece);

        return true;
    }

    public bool AddPiece(Piece piece, int x, int y, int player){
        // if(x < 0 || x >= H || y < 0 || y >= W) return false;
        x = (x + H) % H;
        y = (y + W) % W;
        if(cells[x,y].piece != null) return false;

        piece.player = player;
        piece.MoveToCell(cells[x,y]);
        piece.hasMoved = false;
        pieces.Add(piece);

        return true;
    }

    public void RemovePiece(Piece piece){
        if(pieces.Contains(piece)){
            pieces.Remove(piece);
            piece.cell.piece = null;
            Destroy(piece.gameObject); 
        }
    }

    public void RecreateBoard(){
        cells = new Cell[H,W];

        // create new cells
        for(int i=0; i<H; i++){
            for(int j=0; j<W; j++){
                if(cells[i,j] == null){
                    GameObject cellObj = Instantiate(cellPrefab);
                    cellObj.transform.parent = this.transform;
                    cells[i,j] = cellObj.GetComponent<Cell>();
                    
                    if((i + j) % 2 == 0){
                        cells[i,j].backgroundImage.color = Color.white;
                    } else {
                        cells[i,j].backgroundImage.color = Color.gray;
                    }
                    
                    cells[i,j].x = i;
                    cells[i,j].y = j;
                }
                cells[i,j].board = this;
                cells[i,j].transform.position = new Vector3(i, j, 0);
                
                // if end, mark for pawn evolution
                if(i == H-1) cells[i,j].canEvolveWhite = true;
                if(i == 0  ) cells[i,j].canEvolveBlack = true;
            }
        }
    }
    public void RescaleBoard(){
        float newScale = Mathf.Min(8f/H, 8f/W);
        transform.localScale = new Vector3(newScale, newScale, 1);
    }

    public bool IsInsideBoard(int x, int y){ return x >= 0 && x < H && y >= 0 && y < W; }

    // get empty cells
    public List<Cell> GetEmptyCells(){
        List<Cell> emptyCells = new List<Cell>();
        for(int i=0; i<H; i++){
            for(int j=0; j<W; j++){
                if(cells[i,j].piece == null) 
                    emptyCells.Add(cells[i,j]);
            }
        }
        return emptyCells;
    }

    private List<Cell> highlightedCells = new List<Cell>();

    public void AddHighlight(Cell cell, int player){
        ClearHighlights();
        
        highlightedCells.Add(cell);
        cell.HighlightCell();

        if(cell.piece == null || cell.piece.player != player ) return;

        List<Cell> moves = cell.piece.ListOfMoves(this);
        moves.AddRange(cell.piece.ListOfAttacks(this)); // add atacks as well
        moves = new List<Cell>(new HashSet<Cell>(moves)); // unique

        foreach(Cell move in moves){
            highlightedCells.Add(move);
            move.HighlightCell();
        }
    }

    public void AddHighlights(List<Cell> cellsToHighlight){
        ClearHighlights();
        
        foreach(Cell cell in cellsToHighlight){
            highlightedCells.Add(cell);
            cell.HighlightCell();
        }
    }

    public void ClearHighlights(){
        foreach(Cell cell in highlightedCells) cell.ClearHighlight();
        highlightedCells.Clear();
    }

    public bool IsCellHighlighted(Cell cell){
        return highlightedCells.Contains(cell);
    }

    public Cell GetCell(int x, int y){
        if(!IsInsideBoard(x,y)) return null;
        return cells[x,y];
    }

    public Piece GetKing(int player){
        foreach(Piece piece in pieces)
            if(piece.player == player && piece.pieceType == "King")
                return piece;
        return null;
    }
}
