using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupGame();
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
                int index = random.Next(animalEmojis.Count);
                string nextEmoji = animalEmojis[index];
                textBlock.Text = nextEmoji;
                animalEmojis.RemoveAt(index);
            }
        }

        TextBlock? lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock || lastTextBlockClicked is null) return;
            
            if (!findingMatch)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}
