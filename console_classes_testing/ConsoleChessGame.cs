using System;

namespace console_classes_testing
{
  public class ConsoleChessGame
  {
    private ConsoleChessBoard board;
    private PieceColor currentPlayer;
    private King CurrentKing
    {
      get
      {
        return currentPlayer == PieceColor.Black ? board.BlackKing : board.WhiteKing;
      }
    }
    public ConsoleChessGame()
    {
      board = new ConsoleChessBoard();
      currentPlayer = PieceColor.White;
    }

    public void Play()
    {
      while (true)
      {
        board.DisplayBoard();

        Console.WriteLine($"{currentPlayer}'s turn");
        Console.Write("select the piece you want to move (e.g., 'e2'): ");
        string fromInput = ReadLine();
        Console.Write("select the piece where you want to place it (e.g., 'e2'): ");
        string toInput = ReadLine();
        Console.Clear();

        if (!ChessLocation.TryCreate(fromInput, out ChessLocation from) || !ChessLocation.TryCreate(toInput, out ChessLocation to))
        {
          Console.WriteLine("Invalid input: Please enter moves in the format 'e2'.");
          continue;
        }

        ChessPiece piece = board.GetPieceAtLocation(from);
        if (piece == null)
        {
          Console.WriteLine("Invalid selection: No piece at the specified location.");
          continue;
        }

        if (piece.Color != currentPlayer)
        {
          Console.WriteLine("Invalid selection: The chosen piece doesn't belong to the current player.");
          continue;
        }

        if (piece.WillMoveCauseCheck(to))
        {
          Console.WriteLine("Invalid move: The move would leave your king in check.");
          continue;
        }

        bool moveSuccessful = board.MovePiece(from, to);
        if (moveSuccessful)
          SwitchPlayers();

        // after the switch we check if the current move cause a check on the other king
        if (CurrentKing.IsInCheck())
        {
          Console.WriteLine("Check!");
          if (board.IsCheckmate(currentPlayer))
          {
            Console.WriteLine($"Checkmate! {currentPlayer} wins!");
            Exit();
          }
        }
      }
    }
    private void SwitchPlayers()
    {
      currentPlayer = (currentPlayer == PieceColor.White) ? PieceColor.Black : PieceColor.White;
    }
    static void CheckForExit(string input)
    {
      input = input.Trim().ToLower();
      if (input == "exit")
      {
        Exit();
      }
    }
    static void Exit()
    {
      Console.WriteLine("Program exited.");
      // this method on system namespace that exit the whole program at any point
      Environment.Exit(0);
    }
    static string ReadLine()
    {
      string input = Console.ReadLine();
      CheckForExit(input);
      return input;
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
