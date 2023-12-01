using System.Net.Mime;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace TicTacToe;

public partial class Cell : UserControl
{
    public required Game Game;
    
    public Cell()
    {
        InitializeComponent();
    }

    private void Text_OnClick(object? sender, RoutedEventArgs e)
    {
        if(Text.Content != null)
           return;
        Text.Content = Game.CurrentPlayerSign;
        Game.DoTurn(GetValue(Grid.RowProperty), GetValue(Grid.ColumnProperty));
        IsEnabled = false;
        Foreground = Brushes.White;
    }
}