using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using WPFSyntaxTree.ViewModels;

namespace WPFSyntaxTree;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private async void OnLoad(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dlg = new();
        dlg.Filter = "C# Code (.cs)|*.cs";

        if (dlg.ShowDialog() == true)
        {
            string code = File.ReadAllText(dlg.FileName);

            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            SyntaxNode node = await tree.GetRootAsync();            

            Nodes.Add(new SyntaxNodeViewModel(node));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = default)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<SyntaxNodeViewModel> Nodes { get; } = new();

    private SyntaxNodeViewModel? _selectedNode;
    public SyntaxNodeViewModel? SelectedNode
    {
        get => _selectedNode;
        set
        {
            _selectedNode = value;
            OnPropertyChanged();
        }
    }

    private void OnSelectSyntaxNode(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        SelectedNode = e.NewValue as SyntaxNodeViewModel;
    }
}
