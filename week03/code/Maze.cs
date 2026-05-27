/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are boolean are represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  If there is no wall,
/// then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    // The direction array stored at each (x,y) is [left, right, up, down].
    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int UP = 2;
    private const int DOWN = 3;

    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        if (!CanMove(LEFT))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currX -= 1;
    }

    /// <summary>
    /// Check to see if you can move right.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        if (!CanMove(RIGHT))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currX += 1;
    }

    /// <summary>
    /// Check to see if you can move up.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        if (!CanMove(UP))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currY -= 1;
    }

    /// <summary>
    /// Check to see if you can move down.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
        if (!CanMove(DOWN))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currY += 1;
    }

    // Helper: look up the current cell in the map and check whether the
    // requested direction is allowed. A missing cell (off the map) is
    // treated as a wall.
    private bool CanMove(int direction)
    {
        if (!_mazeMap.TryGetValue((_currX, _currY), out var moves))
        {
            return false;
        }
        return moves[direction];
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}