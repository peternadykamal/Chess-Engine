using System.Collections.Generic;

namespace console_classes_testing
{
  public class ChessBoard
  {
    protected ChessPiece[,] board;
    public King WhiteKing { get; private set; }
    public King BlackKing { get; private set; }

    private List<ChessMove> moveHistory;
    public List<ChessPiece> Graveyard
    {
      get
      {
        List<ChessPiece> graveyard = new List<ChessPiece>();

        foreach (ChessMove move in moveHistory)
        {
          if (move.CapturedPiece != null)
          {
            graveyard.Add(move.CapturedPiece);
          }
        }

        return graveyard;
      }
    }

    public ChessBoard()
    {
      board = new ChessPiece[8, 8];
      InitializeBoard();
      moveHistory = new List<ChessMove>();
    }
    private void InitializeBoard()
    {
      BoardInitializer boardInitializer = new BoardInitializer(this);
      List<ChessPiece> IntialChessPieces = boardInitializer.GetIntialChessPieces();

      foreach (ChessPiece piece in IntialChessPieces)
      {
        board[piece.Location.Row, piece.Location.Column] = piece;
      }

      WhiteKing = boardInitializer.WhiteKing;
      BlackKing = boardInitializer.BlackKing;

      // this is just for testing
      //ChessLocation whitePawnLocation;
      //ChessLocation.TryCreate(7, 'f', out whitePawnLocation);
      //board[whitePawnLocation.Row, whitePawnLocation.Column] = new Pawn(PieceColor.White, whitePawnLocation, this);
    }
    public ChessPiece GetPieceAtLocation(ChessLocation location)
    {
      return board[location.Row, location.Column];
    }

    public ChessPiece GetPieceAtLocation(int row, int column)
    {
      ChessLocation.TryCreate(row, column, out ChessLocation location);

      if (location == null)
        return null;

      return board[location.Row, location.Column];
    }
    public List<ChessPiece> GetPiecesByColor(PieceColor color)
    {
      List<ChessPiece> opponentPieces = new List<ChessPiece>();

      for (int row = 0; row < 8; row++)
      {
        for (int col = 0; col < 8; col++)
        {
          ChessPiece piece = board[row, col];

          if (piece != null && piece.Color == color)
          {
            opponentPieces.Add(piece);
          }
        }
      }

      return opponentPieces;
    }
    public bool MovePiece(ChessLocation from, ChessLocation to, out string errorMessage)
    {
      errorMessage = null;

      if (from == null || to == null)
      {
        errorMessage = "Invalid source or destination location.";
        return false;
      }

      ChessPiece piece = board[from.Row, from.Column];
      if (piece == null)
      {
        errorMessage = "No piece selected.";
        return false;
      }

      if (piece is King && (piece as King).IsValidCastling(to))
      {
        bool CastlingSuccessfully = HandleCastlingMove(from, to, piece);
        return CastlingSuccessfully;
      }

      if (!piece.IsValidMove(from, to))
      {
        errorMessage = "Invalid move for the selected piece.";
        return false;
      }

      ChessPiece capturedPiece = board[to.Row, to.Column];
      board[from.Row, from.Column] = null;
      board[to.Row, to.Column] = piece;
      piece.MoveTo(to);

      moveHistory.Add(new ChessMove(from, to, capturedPiece));

      return true;
    }

    private bool HandleCastlingMove(ChessLocation from, ChessLocation to, ChessPiece piece)
    {
      // Update the king's location
      board[from.Row, from.Column] = null;
      board[to.Row, to.Column] = piece;
      ChessMove kingMove = piece.MoveTo(to) ? new ChessMove(from, to, null) : null;
      ChessMove rookMove = HandleCastlingRookMovement(from, to);

      if (kingMove == null || rookMove == null)
        return false;

      moveHistory.Add(new ChessCastlingMove(kingMove.From, kingMove.To, rookMove.From, rookMove.To, null));
      return true;
    }

    public void UndoLastMove()
    {
      if (moveHistory.Count == 0)
        return;

      ChessMove lastMove = moveHistory[moveHistory.Count - 1];
      ChessPiece movedPiece = board[lastMove.To.Row, lastMove.To.Column];

      board[lastMove.From.Row, lastMove.From.Column] = movedPiece;
      movedPiece.MoveTo(lastMove.From);

      board[lastMove.To.Row, lastMove.To.Column] = lastMove.CapturedPiece;

      if (lastMove is ChessCastlingMove)
      {
        ChessMove rookMove = (lastMove as ChessCastlingMove).RookMove;
        ChessPiece rookPiece = board[rookMove.To.Row, rookMove.To.Column];

        board[rookMove.From.Row, rookMove.From.Column] = rookPiece;
        rookPiece.MoveTo(rookMove.From);

        board[rookMove.To.Row, rookMove.To.Column] = rookMove.CapturedPiece;
      }

      // Remove the move from the history
      moveHistory.RemoveAt(moveHistory.Count - 1);
    }
    public bool IsCheckmate(PieceColor currentPlayer)
    {
      King currentKing = (currentPlayer == PieceColor.White) ? WhiteKing : BlackKing;

      // Check if the king is in check
      if (!currentKing.IsInCheck())
      {
        return false; // The king is not in check, so it's not checkmate
      }

      // Check for legal moves for each piece of the current player
      foreach (ChessPiece piece in GetPiecesByColor(currentPlayer))
      {
        foreach (ChessLocation destination in piece.GetValidMoves())
        {
          // Try making the move
          string errorMessage;
          if (MovePiece(piece.Location, destination, out errorMessage))
          {
            // Check if the king is still in check after the move
            if (!currentKing.IsInCheck())
            {
              // Undo the move
              UndoLastMove();
              return false; // The king is not in checkmate
            }

            // Undo the move
            UndoLastMove();
          }
        }
      }

      // If no legal moves can remove the check, it's checkmate
      return true;
    }
    private ChessMove HandleCastlingRookMovement(ChessLocation kingMovefrom, ChessLocation kingMoveto)
    {
      // this method return chessMove contain the move of the Rook
      ChessMove rookMove = null;

      // Move the rook accordingly in kingside castling
      if (kingMoveto.Column - kingMovefrom.Column == 2)
      {
        ChessPiece kingsideRook = board[kingMoveto.Row, kingMoveto.Column + 1];
        board[kingMoveto.Row, kingMoveto.Column + 1] = null;
        board[kingMoveto.Row, kingMoveto.Column - 1] = kingsideRook;
        if (ChessLocation.TryCreate(kingMoveto.Row, kingMoveto.Column - 1, out ChessLocation kingsideRookLocation))
        {
          rookMove = new ChessMove(kingsideRook.Location, kingsideRookLocation, null);
          return kingsideRook.MoveTo(kingsideRookLocation) ? rookMove : null; // if move done succefully return rookMove
        }
      }
      // Move the rook accordingly in queenside castling
      else if (kingMoveto.Column - kingMovefrom.Column == -2)
      {
        ChessPiece queensideRook = board[kingMoveto.Row, kingMoveto.Column - 2];
        board[kingMoveto.Row, kingMoveto.Column - 2] = null;
        board[kingMoveto.Row, kingMoveto.Column + 1] = queensideRook;
        if (ChessLocation.TryCreate(kingMoveto.Row, kingMoveto.Column + 1, out ChessLocation queensideRookLocation))
        {
          rookMove = new ChessMove(queensideRook.Location, queensideRookLocation, null);
          return queensideRook.MoveTo(queensideRookLocation) ? rookMove : null; // if move done succefully return rookMove
        }
      }

      return null;
    }
  }
}