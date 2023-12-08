namespace console_classes_testing
{
  public class ChessLocation
  {
    public int Row { get; private set; }
    public int Column { get; private set; }

    // the reason why make the defualt constructor private and make TryCreate method resoponsible for creating ChessLocation
    // object is to enforce that it is possible to create instance of move only if column and row given is valid (without
    // throwing errors)
    private ChessLocation(int row, int column)
    {
      this.Row = row;
      this.Column = column;
    }

    // here is a use case for this method
    // ChessMove move;
    // if (ChessMove.TryCreate(8, a, out move))
    public static bool TryCreate(int row, char column, out ChessLocation location)
    {
      location = null;

      column = char.ToLower(column);

      if (column < 'a' || column > 'h' || row < 1 || row > 8)
        return false;

      int col = column - 'a';

      int adjustedRow = row - 1;

      location = new ChessLocation(adjustedRow, col);
      return true;
    }
    public static bool TryCreate(int indexRow, int indexColumn, out ChessLocation location)
    {
      location = null;

      if (indexColumn < 0 || indexColumn > 7 || indexRow < 0 || indexRow > 7)
        return false;

      location = new ChessLocation(indexRow, indexColumn);
      return true;
    }
    // here is a use case for this method
    // ChessMove move;
    // if (ChessMove.TryCreate("a8", out move))
    public static bool TryCreate(string input, out ChessLocation location)
    {
      location = null;

      if (input.Length != 2)
      {
        return false;
      }
      char column = input[0];
      char rowChar = input[1];
      int row;

      // if the rowChar can be parsed to an integer it's intger value will be placed in row variable
      // else it will return
      if (!int.TryParse(rowChar.ToString(), out row))
      {
        return false;
      }

      column = char.ToLower(column);

      if (column < 'a' || column > 'h' || row < 1 || row > 8)
        return false;

      int col = column - 'a';

      int adjustedRow = row - 1;

      location = new ChessLocation(adjustedRow, col);
      return true;
    }
    public static bool operator ==(ChessLocation location1, ChessLocation location2)
    {
      if (ReferenceEquals(location1, location2))
        return true;

      if (location1 is null || location2 is null)
        return false;

      return location1.Row == location2.Row && location1.Column == location2.Column;
    }

    public static bool operator !=(ChessLocation location1, ChessLocation location2)
    {
      return !(location1 == location2);
    }

    public override bool Equals(object obj)
    {
      if (obj is ChessLocation otherLocation)
      {
        return this == otherLocation;
      }

      return false;
    }

    public override int GetHashCode()
    {
      return (Row, Column).GetHashCode();
    }
  }
}
