using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new();
        int tenthsOfSecondsElapsed;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetupGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = $"{timeTextBlock.Text} - Play again?";
            }
        }

        private void SetupGame()
        {
            List<string> animalEmojis = new()
            {
                "🐵", "🐵",
                "🦍", "🦍",
                "🐶", "🐶",
                "🐕", "🐕",
                "🐩", "🐩",
                "🐺", "🐺",
                "🦊", "🦊",
                "🐱", "🐱",
            };

            Random random = new();

            foreach (var textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "timeTextBlock") continue;

                int index = random.Next(animalEmojis.Count);
                string nextEmoji = animalEmojis[index];
                textBlock.Text = nextEmoji;
                textBlock.Visibility = Visibility.Visible;
                animalEmojis.RemoveAt(index);
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock? lastTextBlockClicked;
        bool findingMatch = false;
        int matchesFound;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock) return;

            if (!findingMatch) // non selected
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked?.Text) // if match
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound++;
            }
            else // wrong selected
            {
                if (lastTextBlockClicked is null) return;
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetupGame();
            }
        }
    }
}
