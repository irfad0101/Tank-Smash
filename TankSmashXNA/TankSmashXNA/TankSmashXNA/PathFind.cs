using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//***using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework;

/****class Pathfinder
{
    // Movements is an array of various directions.
    Point[] _movements;

    // Squares is an array of square objects.
    CompleteSquare[,] _squares = new CompleteSquare[15, 15];
    public CompleteSquare[,] Squares
    {
        get { return _squares; }
        set { _squares = value; }
    }

    public Pathfinder()
    {
        InitMovements(4);
        ClearSquares();
    }

    public void ReadMap(string fileName)
    {
        // Read in a map file.
        using (StreamReader reader = new StreamReader(fileName))
        {
            int lineNum = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                char[] parts = line.ToCharArray();
                for (int i = 0; i < parts.Length && i < 15; i++)
                {
                    _squares[i, lineNum].FromChar(parts[i]);
                }
                lineNum++;
            }
        }
    }

    public void InitMovements(int movementCount)
    {
        // Just do some initializations.
        if (movementCount == 4)
        {
            _movements = new Point[]
	    {
		new Point(0, -1),
		new Point(1, 0),
		new Point(0, 1),
		new Point(-1, 0)
	    };
        }
        else
        {
            _movements = new Point[]
	    {
		new Point(-1, -1),
		new Point(0, -1),
		new Point(1, -1),
		new Point(1, 0),
		new Point(1, 1),
		new Point(0, 1),
		new Point(-1, 1),
		new Point(-1, 0)
	    };
        }
    }

    public void ClearSquares()
    {
        // Reset every square.
        foreach (Point point in AllSquares())
        {
            _squares[point.X, point.Y] = new CompleteSquare();
        }
    }

    public void ClearLogic()
    {
        // Reset some information about the squares.
        foreach (Point point in AllSquares())
        {
            int x = point.X;
            int y = point.Y;
            _squares[x, y].DistanceSteps = 10000;
            _squares[x, y].IsPath = false;
        }
    }

    public void DrawBoard(BoardControl boardControl1)
    {
        foreach (Point point in AllSquares())
        {
            int x = point.X;
            int y = point.Y;
            int num = _squares[x, y].DistanceSteps;
            Color setColor = Color.Gainsboro;
            SquareContent content = _squares[x, y].ContentCode;

            if (content == SquareContent.Empty)
            {
                if (_squares[x, y].IsPath == true)
                {
                    setColor = Color.LightBlue;
                }
                else
                {
                    setColor = Color.White;
                }
            }
            else
            {
                if (content == SquareContent.Hero)
                {
                    setColor = Color.Coral;
                }
                else if (content == SquareContent.Monster)
                {
                    setColor = Color.Coral;
                }
                else
                {
                    setColor = Color.Gray;
                }
            }
            boardControl1.SetHighlight(setColor, x, y);
        }
        boardControl1.Invalidate();
    }

    public void Pathfind()
    {
        // Find path from hero to monster.
        // First, get coordinates of hero.
        Point startingPoint = FindCode(SquareContent.Hero);
        int heroX = startingPoint.X;
        int heroY = startingPoint.Y;
        if (heroX == -1 || heroY == -1)
        {
            return;
        }
        // Hero starts at distance of 0.
        _squares[heroX, heroY].DistanceSteps = 0;

        while (true)
        {
            bool madeProgress = false;

            // Look at each square on the board.
            foreach (Point mainPoint in AllSquares())
            {
                int x = mainPoint.X;
                int y = mainPoint.Y;

                // If the square is open, look through valid moves given
                // the coordinates of that square.
                if (SquareOpen(x, y))
                {
                    int passHere = _squares[x, y].DistanceSteps;

                    foreach (Point movePoint in ValidMoves(x, y))
                    {
                        int newX = movePoint.X;
                        int newY = movePoint.Y;
                        int newPass = passHere + 1;

                        if (_squares[newX, newY].DistanceSteps > newPass)
                        {
                            _squares[newX, newY].DistanceSteps = newPass;
                            madeProgress = true;
                        }
                    }
                }
            }
            if (!madeProgress)
            {
                break;
            }
        }
    }

    static private bool ValidCoordinates(int x, int y)
    {
        // Our coordinates are constrained between 0 and 14.
        if (x < 0)
        {
            return false;
        }
        if (y < 0)
        {
            return false;
        }
        if (x > 14)
        {
            return false;
        }
        if (y > 14)
        {
            return false;
        }
        return true;
    }

    private bool SquareOpen(int x, int y)
    {
        // A square is open if it is not a wall.
        switch (_squares[x, y].ContentCode)
        {
            case SquareContent.Empty:
                return true;
            case SquareContent.Hero:
                return true;
            case SquareContent.Monster:
                return true;
            case SquareContent.Wall:
            default:
                return false;
        }
    }

    private Point FindCode(SquareContent contentIn)
    {
        // Find the requested code and return the point.
        foreach (Point point in AllSquares())
        {
            if (_squares[point.X, point.Y].ContentCode == contentIn)
            {
                return new Point(point.X, point.Y);
            }
        }
        return new Point(-1, -1);
    }

    public void HighlightPath()
    {
        // Mark the path from monster to hero.
        Point startingPoint = FindCode(SquareContent.Monster);
        int pointX = startingPoint.X;
        int pointY = startingPoint.Y;
        if (pointX == -1 && pointY == -1)
        {
            return;
        }

        while (true)
        {
            // Look through each direction and find the square
            // with the lowest number of steps marked.
            Point lowestPoint = Point.Empty;
            int lowest = 10000;

            foreach (Point movePoint in ValidMoves(pointX, pointY))
            {
                int count = _squares[movePoint.X, movePoint.Y].DistanceSteps;
                if (count < lowest)
                {
                    lowest = count;
                    lowestPoint.X = movePoint.X;
                    lowestPoint.Y = movePoint.Y;
                }
            }
            if (lowest != 10000)
            {
                // Mark the square as part of the path if it is the lowest number.
                // Set the current position as the square with that number of steps.
                _squares[lowestPoint.X, lowestPoint.Y].IsPath = true;
                pointX = lowestPoint.X;
                pointY = lowestPoint.Y;
            }
            else
            {
                break;
            }

            if (_squares[pointX, pointY].ContentCode == SquareContent.Hero)
            {
                // We went from monster to hero, so we're finished.
                break;
            }
        }
    }

    private static IEnumerable<Point> AllSquares()
    {
        // Return every point on the board in order.
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                yield return new Point(x, y);
            }
        }
    }

    private IEnumerable<Point> ValidMoves(int x, int y)
    {
        // Return each valid square we can move to.
        foreach (Point movePoint in _movements)
        {
            int newX = x + movePoint.X;
            int newY = y + movePoint.Y;

            if (ValidCoordinates(newX, newY) &&
            SquareOpen(newX, newY))
            {
                yield return new Point(newX, newY);
            }
        }
    }


    class CompleteSquare
    {
        SquareContent _contentCode = SquareContent.Empty;
        public SquareContent ContentCode
        {
            get { return _contentCode; }
            set { _contentCode = value; }
        }

        int _distanceSteps = 100;
        public int DistanceSteps
        {
            get { return _distanceSteps; }
            set { _distanceSteps = value; }
        }

        bool _isPath = false;
        public bool IsPath
        {
            get { return _isPath; }
            set { _isPath = value; }
        }

        public void FromChar(char charIn)
        {
            // Use a switch statement to parse characters.
            switch (charIn)
            {
                case 'W':
                    _contentCode = SquareContent.Wall;
                    break;
                case 'H':
                    _contentCode = SquareContent.Hero;
                    break;
                case 'M':
                    _contentCode = SquareContent.Monster;
                    break;
                case ' ':
                default:
                    _contentCode = SquareContent.Empty;
                    break;
            }
        }
    }

    enum SquareContent
    {
        Empty,
        Monster,
        Hero,
        Wall
    };

}*****/