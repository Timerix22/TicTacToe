using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TicTacToe;

public partial class MainWindow : Window
{
    private Game _game = new(3, 3);

    public MainWindow()
    {
        InitializeComponent();
        RestartButton_OnClick(null, null);
    }

    private void RestartButton_OnClick(object? sender, RoutedEventArgs? e)
    {
        int size = Convert.ToInt32(SizeTextBox.Text);
        _game = new Game(size, size);
        _game.PlayerWon += ShowWin;
        _game.PlayerChanged += playerChar => CurrentPlayerText.Text = playerChar.ToString();
        DrawGame();
    }

    private void ShowWin(char playerChar)
    {
        GameGrid.IsVisible = false;
        WinPanel.IsVisible = true;
        WinText.Text = $"Player {playerChar} won!!1!";
    }
    
    private void DrawGame()
    {
        GameGrid.IsVisible = true;
        WinPanel.IsVisible = false;
        int size = _game.GameField.Length;
        GameGrid.Children.Clear();
        GameGrid.RowDefinitions = new RowDefinitions();
        for(int y=0; y<size; y++)
            GameGrid.RowDefinitions.Add(new RowDefinition());
        GameGrid.ColumnDefinitions = new ColumnDefinitions();
        for(int x=0; x<size; x++)
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
        for(int y=0; y<size; y++)
        for (int x = 0; x < size; x++)
        {
            var cellView = new Cell { Game = _game };
            cellView.SetValue(Grid.ColumnProperty, x);
            cellView.SetValue(Grid.RowProperty, y);
            char cellValue = _game.GameField[y][x];
            if (cellValue != '\0')
                cellView.Text.Content = cellValue.ToString();
            GameGrid.Children.Add(cellView);
        }
    }

}