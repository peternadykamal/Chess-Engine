using System.Collections.Generic;

namespace console_classes_testing
{
  public class Queen : SlideMovablePiece
  {
    public Queen(PieceColor color, ChessLocation location, ChessBoard board) : base(color, location, board)
    {
      Symbol = 'Q';
    }

    public override List<ChessLocation> GetValidMoves()
    {
      List<ChessLocation> validMoves = new List<ChessLocation>();

      // Check threats horizontally and vertically
      AddValidMovesInDirection(validMoves, 0, 1); // Right
      AddValidMovesInDirection(validMoves, 0, -1); // Left
      AddValidMovesInDirection(validMoves, 1, 0); // Down
      AddValidMovesInDirection(validMoves, -1, 0); // Up

      // Check threats diagonally
      AddValidMovesInDirection(validMoves, 1, 1); // Down-Right
      AddValidMovesInDirection(validMoves, 1, -1); // Down-Left
      AddValidMovesInDirection(validMoves, -1, 1); // Up-Right
      AddValidMovesInDirection(validMoves, -1, -1); // Up-Left

      return validMoves;
    }
  }
}
