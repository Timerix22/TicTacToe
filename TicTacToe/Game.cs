using System;
using System.Collections;

namespace TicTacToe;

public class Game
{
    /*     0  1  2  ...
     *  0 [ ][ ][ ]
     *  1 [ ][ ][ ]
     *  2 [ ][ ][ ]
     *  ...
     */
    public char[][] GameField => _gameField;
    public char CurrentPlayerSign => _players[_nextPlayerIndex];
    
    public event Action<char>? PlayerWon;
    
    private char[][] _gameField;
    private char[] _players = { 'X', '0' };
    private int _nextPlayerIndex = 0;
    private int _winSequenceLength;
    private int _sideLength;
    
    public Game(int gameFieldSideSize, int winSequenceLength)
    {
        _winSequenceLength = winSequenceLength;
        _sideLength = gameFieldSideSize;
        _gameField = new char[gameFieldSideSize][];
        for (int y = 0; y < gameFieldSideSize; y++)
        {
            _gameField[y] = new char[gameFieldSideSize];
            for (int x = 0; x < gameFieldSideSize; x++)
                _gameField[y][x] = '\0';
        }
    }
    
    public void Turn(int cell_row, int cell_col)
    {
        if (_gameField[cell_row][cell_col] != '\0')
            throw new Exception("the cell was already used");
        _gameField[cell_row][cell_col] = CurrentPlayerSign;
        if (CheckWinCombination(cell_row, cell_col))
        {
            PlayerWon?.Invoke(CurrentPlayerSign);
            return;
        }
        
        _nextPlayerIndex++;
        if (_nextPlayerIndex >= _players.Length)
            _nextPlayerIndex = 0;
    }

    private bool CheckWinCombination(int cell_row, int cell_col)
    {
        int sequenceLength = 0;
        // up
        for (int row = cell_row; row > -1; row--)
        {
            if(_gameField[row][cell_col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }
        // down
        sequenceLength--;
        for (int row = cell_row; row < _sideLength; row++)
        {
            if(_gameField[row][cell_col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }
        sequenceLength = 0;
        
        // left
        for (int col = cell_col; col > -1; col--)
        {
            if(_gameField[cell_row][col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }
        // right
        sequenceLength--;
        for (int col = cell_col; col < _sideLength; col++)
        {
            if(_gameField[cell_row][col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }
        sequenceLength = 0;
        
        // up-left
        for (int row = cell_row, col = cell_col; row > -1 &&  col > -1; row--, col--)
        {
            if(_gameField[row][col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }
        // down-right
        sequenceLength--;
        for (int row = cell_row, col = cell_col; col < _sideLength && row < _sideLength; row++, col++)
        {
            if(_gameField[row][col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }
        sequenceLength = 0;
        
        
        // up-right
        for (int row = cell_row, col = cell_col; row > -1 &&  col < _sideLength; row--, col++)
        {
            if(_gameField[row][col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }
        // down-left
        sequenceLength--;
        for (int row = cell_row, col = cell_col; col > -1 && row < _sideLength; row++, col--)
        {
            if(_gameField[row][col] != CurrentPlayerSign)
                break;
            if (++sequenceLength == _winSequenceLength)
                return true;
        }

        return false;
    }
}