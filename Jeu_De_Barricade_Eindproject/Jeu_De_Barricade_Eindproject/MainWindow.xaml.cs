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

namespace Jeu_De_Barricade_Eindproject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private View.StartLevelOption levelOption;
        private View.LoadLevelOption loadLevel;

        private Controller.Game game;

        public MainWindow()
        {
            InitializeComponent();

            //Create a new Start Level Option view 
            levelOption = new View.StartLevelOption(this);
            mainGrid.Children.Add(levelOption);
            //Hide untill needed
            levelOption.Visibility = Visibility.Collapsed;

            //Create a new load level view 
            loadLevel = new View.LoadLevelOption(this);
            mainGrid.Children.Add(loadLevel);
            //Hide untill needed
            loadLevel.Visibility = Visibility.Collapsed;
            
            game = new Controller.Game(this);
        }

        //Method when the user left clicks on the upper grid
        private void grid_mouseleftdown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //Method to exit the application when label_exit is clicked
        private void label_exit_mousedown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        //Method to minimize the application when label_minimize is clicked
        private void label_minimize_mousedown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Method when start label is clicked
        private void label_start_click(object sender, MouseButtonEventArgs e)
        {
            ToggleImageOpacityAndButtonGrid();

            //Show the start level option view
            levelOption.Visibility = Visibility.Visible;
        }

        //Method when the load label is clicked
        private void label_laad_click(object sender, MouseButtonEventArgs e)
        {
            ToggleImageOpacityAndButtonGrid();

            //Update the list of saved games
            loadLevel.updateAllSavedGames();

            //Show the load level view
            loadLevel.Visibility = Visibility.Visible;
        }

        //When mainmenu label clicked it is collapsed and the board gets destroyd
        private void label_mainmenu_mousedown(object sender, MouseButtonEventArgs e)
        {

            label_mainmenu.Visibility = Visibility.Collapsed;
            label_save.Visibility = Visibility.Collapsed;

            if (game != null)
            {
                game.destoryMap();
            }
        }

        //Toggles the background opacity and the visibility of the 'button'-grid
        public void ToggleImageOpacityAndButtonGrid()
        {
            if (totalGrid.Background.Opacity != 1)
            {
                totalGrid.Background.Opacity = 1;
            }
            else
            {
                totalGrid.Background.Opacity = 0.10;
            }

            if (buttonGrid.Visibility == Visibility.Collapsed)
            {
                buttonGrid.Visibility = Visibility.Visible;
            }
            else
            {
                buttonGrid.Visibility = Visibility.Collapsed;
            }
        }

        //Start a new game which creates a new board
        public void startGame(int iRealPlayers, String sBoardType)
        {
            game.newBoard(iRealPlayers, sBoardType);
        }

        //Trigger the save game method in game
        private void label_save_mousedown(object sender, MouseButtonEventArgs e)
        {
            game.saveCurrentGame();
        }

        //Load a file.
        public void loadGame(String fileName)
        {
            game.loadBoard(fileName);
        }
    }
}
