using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] TheGrid { get; set; }
        public Board(int s)
        {
            //initial board size is defined by s
            Size  = s;

            //create a new 2D array of type cell
            TheGrid = new Cell[Size, Size];

            //fill the 2D array with new cells
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[i, j] = new Cell(i,j);
                }
            }
        }
        public bool isSafe(int x, int y)
        {
            return (x >= 0 && x < Size && y >= 0 && y < Size);
        }
        public void MarkNextLegalMoves( Cell currentCell, string chessPiece)
        {
            //step 1 - clear all previous legal moves
            for (int i = 0;i < Size;i++)
            {
                for (int j = 0;j<Size;j++)
                {
                    TheGrid[i, j].LegalNextMove = false;
                    TheGrid[i,j].CurrentlyOccupied = false;
                }
            }

            //step 2 - find all legal moves and mark the cells as "legal"
            switch(chessPiece)
            {
                case "Knight":
                    HandleKnightMoves(currentCell);
                    break;
                case "Rook":
                    HandleRookMoves(currentCell);
                    break;
                case "King":
                    HandleKingMoves(currentCell);
                    break;
                case "Bishop":
                    HandleBishopMoves(currentCell);
                    break;
                case "Queen":
                    HandleQueenMoves(currentCell);
                    break;
                default:
                    break;
            }
            TheGrid[currentCell.RowNumber, currentCell.ColumnNumber].CurrentlyOccupied = true;
        }
        void HandleKingKnight(Cell currentCell, int[] rowOffset, int[] columnOffset)
        {
            for (int i = 0; i < 8; i++)
            {
                int targetRow = currentCell.RowNumber + rowOffset[i];
                int targetColumn = currentCell.ColumnNumber + columnOffset[i];

                if (isSafe(targetRow, targetColumn))
                    TheGrid[targetRow, targetColumn].LegalNextMove = true;
            }
        }
        void HandleBishopRook(Cell currentCell, int[] rowOffset, int[] columnOffset)
        {
            for (int i = 0; i < 4; i++)
            {
                int targetRow = currentCell.RowNumber + rowOffset[i];
                int targetColumn = currentCell.ColumnNumber + columnOffset[i];

                while (isSafe(targetRow, targetColumn))
                {
                    TheGrid[targetRow, targetColumn].LegalNextMove = true;
                    targetRow += rowOffset[i];
                    targetColumn += columnOffset[i];
                }
            }
        }
        void HandleKnightMoves(Cell currentCell)
        {
            int[] rowOffset = { 2, 2, 1, 1, -1, -1, -2, -2 };
            int[] columnOffset = { 1, -1, 2, -2, -2, 2, -1, 1 };
            HandleKingKnight(currentCell, rowOffset, columnOffset);
        }
        void HandleKingMoves(Cell currentCell)
        {
            int[] rowOffset = { -1, - 1, - 1, 0, 0, 1, 1, 1 };
            int[] columnOffset = { -1, 0, 1, -1, 1, -1, 0, 1 };
            HandleKingKnight(currentCell, rowOffset, columnOffset);
        }
        void HandleRookMoves(Cell currentCell)
        {
            int[] rowOffset = { 1, -1, 0, 0 };
            int[] columnOffset = { 0, 0, 1, -1 };
            HandleBishopRook(currentCell, rowOffset, columnOffset);
        }
        void HandleBishopMoves(Cell currentCell)
        {
            int[] rowOffset = { -1, -1, 1, 1 };
            int[] columnOffset = { -1, 1, -1, 1 };
            HandleBishopRook(currentCell, rowOffset, columnOffset);
            
        }
        void HandleQueenMoves(Cell currentCell)
        {
            HandleBishopMoves(currentCell);
            HandleRookMoves(currentCell);
        }
    }
}
