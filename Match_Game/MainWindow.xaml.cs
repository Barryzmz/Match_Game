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
using System.Windows.Threading;

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
    DispatcherTimer timer = new DispatcherTimer();
    int tenthsOfSecondsElapsed;
    int matchesFound;
    public MainWindow()
    {
        InitializeComponent();
        timer.Interval = TimeSpan.FromSeconds(.1);
        timer.Tick += Timer_Tick;
        SetUpGame();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        tenthsOfSecondsElapsed++;
        timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        if(matchesFound == 8)
        {
            timer.Stop();
            timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
        }
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
            if(textBlock.Name != "timeTextBlock")
            {
                textBlock.Visibility = Visibility.Visible;
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }
        timer.Start();
        tenthsOfSecondsElapsed = 0;
        matchesFound = 0;
    }

    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock textBlock = sender as TextBlock;
        if (findingMatch == false)
        {
            // 第一次按，將第一個動物隱藏，並記錄TextBlock
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
        }
        else if (textBlock.Text == lastTextBlockClicked.Text)
        {
            // 配對成功，並也把第二個動物隱藏，然後重設findingMatch 
            matchesFound++;
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

    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (matchesFound == 8)
        {
            SetUpGame();
        }
    }
}