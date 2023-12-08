namespace console_classes_testing
{
  internal class Program
  {
    static void Main(string[] args)
    {
      ConsoleChessGame game = new ConsoleChessGame();
      game.Play();
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
