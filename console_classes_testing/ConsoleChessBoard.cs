using System;

namespace console_classes_testing
{
  public class ConsoleChessBoard : ChessBoard
  {
    public void DisplayBoard()
    {
      Console.WriteLine("   a  b  c  d  e  f  g  h");
      Console.WriteLine("   ----------------------");
      for (int row = 0; row < 8; row++)
      {
        Console.Write($"{row + 1}| ");
        for (int col = 0; col < 8; col++)
        {
          ChessPiece piece = board[row, col];
          if (piece != null)
          {
            char colorPrefix = (piece.Color == PieceColor.White) ? 'w' : 'b';
            Console.Write($"{colorPrefix}{piece.Symbol} ");
          }
          else
          {
            Console.Write(".  ");
          }
        }
        Console.WriteLine();
      }
    }
    public bool MovePiece(ChessLocation from, ChessLocation to)
    {
      string errorMessage;
      bool moveSuccessful = base.MovePiece(from, to, out errorMessage);

      if (moveSuccessful)
      {
        Console.WriteLine("Move successful!");
      }
      else
      {
        Console.WriteLine($"Invalid move: {errorMessage}");
      }
      return moveSuccessful;
    }
  }
}
// here is a list of features that are available in the chess engine
// 1- checkmate
// 2- castling
// 3- pawn promotion
// 4- en passant
// 5- undo move
// 6- check
// 9- draw by threefold repetition
// 10- draw by insufficient material
// 14- draw by impossibility of checkmate
// 15- having a history of moves
// 16- having a graveyard of captured pieces
// 17- chesk if a move will cause a check on the king
// 18- having a method to get all valid moves for a piece
