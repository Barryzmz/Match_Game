using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Match_Game;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // 記錄玩家按的第一個動物
    TextBlock lastTextBlockClicked;
    // 記錄玩家是否已經按了第一個動物
    bool findingMatch = false;
    public MainWindow()
    {
        InitializeComponent();
        SetUpGame();
    }

    private void SetUpGame()
    {
        List<string> animalEmoji = new List<string>()
        {
            "🐘", "🐘",
            "🦅", "🦅",
            "🦘", "🦘",
            "🦑", "🦑",
            "🦁", "🦁",
            "🐨", "🐨",
            "🐒", "🐒",
            "🐳", "🐳",
        };
        Random random = new Random();
        foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
        {
            int index = random.Next(animalEmoji.Count);
            string nextEmoji = animalEmoji[index];
            textBlock.Text = nextEmoji;
            animalEmoji.RemoveAt(index);
        }
    }

    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock textBlock = sender as TextBlock;
        if (findingMatch == false)
        {
            // 第一次按，將第一個動物隱藏，並記錄TextBox
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
        }
        else if (textBlock.Text == lastTextBlockClicked.Text)
        {
            // 配對成功，並也把第二個動物隱藏，然後重設findingMatch 
            textBlock.Visibility = Visibility.Hidden;
            findingMatch = false;
        }
        else
        {
            // 配對失敗，把第一個動物顯示，然後重設findingMatch 
            lastTextBlockClicked.Visibility = Visibility.Visible;
            findingMatch = false;
        }
    }
}