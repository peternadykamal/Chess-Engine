using System.Collections.Generic;
using System.Linq;

namespace console_classes_testing
{
  public abstract class ChessPiece
  {
    public char Symbol { get; protected set; }
    public PieceColor Color { get; private set; }
    public ChessLocation Location { get; protected set; }
    private ChessLocation initialLocation;
    protected ChessBoard Board { get; private set; }
    // note that becuase the board is an object, it refrence type, which means if ChessBoard update
    // the board it will also be updated here
    public ChessPiece(PieceColor color, ChessLocation location, ChessBoard board)
    {
      this.Color = color;
      this.Location = location;
      this.initialLocation = location;
      this.Board = board;
    }
    public virtual bool HasMoved()
    {
      return !ReferenceEquals(Location, initialLocation);
    }
    public bool MoveTo(ChessLocation newLocation)
    {
      if (newLocation == null) return false;

      // to make sure the this piece is actually get updated on the board
      if (this == Board.GetPieceAtLocation(newLocation))
      {
        Location = newLocation;
        return true;
      }
      else
      {
        return Board.MovePiece(this.Location, newLocation, out string errorMessage);
      }
    }
    public bool WillMoveCauseCheck(ChessLocation to)
    {
      if (!MoveTo(to)) return false;
      King king = Color == PieceColor.Black ? Board.BlackKing : Board.WhiteKing;
      bool isKingInCheck = king.IsInCheck();
      Board.UndoLastMove();
      return isKingInCheck;
    }
    public virtual bool IsValidMove(ChessLocation from, ChessLocation to)
    {
      List<ChessLocation> validMoves = GetValidMoves();

      // Check if the destination is among the valid moves
      return validMoves.Contains(to);
    }
    public virtual bool IsThreateningKing()
    {
      List<ChessLocation> validMoves = GetValidMoves();

      // Check if any of the valid moves threatens the opponent's king
      return validMoves.Any(move =>
      {
        ChessPiece targetPiece = Board.GetPieceAtLocation(move);
        return targetPiece is King && targetPiece.Color != Color;
      });
    }
    public abstract List<ChessLocation> GetValidMoves();
  }
}
