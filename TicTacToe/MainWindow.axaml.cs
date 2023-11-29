using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TicTacToe;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private void RestartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        int size = Convert.ToInt32(SizeTextBox.Text);
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
                var c = new Cell();
                c.SetValue(Grid.ColumnProperty, x);
                c.SetValue(Grid.RowProperty, y);
                GameGrid.Children.Add(c);
                
            }
    }
}