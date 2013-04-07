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
using System.Windows.Resources;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace Jeu_De_Barricade_Eindproject.View
{
    /// <summary>
    /// Interaction logic for ViewLevel.xaml
    /// </summary>
    public partial class ViewLevel : UserControl
    {
        private Model.ModelLevel levelModel;
        private Controller.Game gameController;

        private StreamResourceInfo sri;
        private Cursor customCursor;

        private List<Ellipse> blurLocations = new List<Ellipse>();

        private int iAmountOfBarricades = 0;


        public int[] ANoBarricades
        {
            get { return levelModel.ANoBarricades; }
            set { levelModel.ANoBarricades = value; }
        }

        public Model.ModelLevel LevelModel
        {
            get { return levelModel; }
        }


        public ViewLevel(Controller.Game game, Model.ModelLevel levelModel)
        {
            InitializeComponent();

            //Set the gameController controller
            gameController = game;

            //Set the right model
            this.levelModel = levelModel;

            //Create the right amount of rows and columns
            setGrid();

            //Set the view to the right hight and width
            this.Width = levelModel.IWidth;
            this.Height = levelModel.IHeight;

            //Add a mousedown event to the grid
            mainGrid.MouseDown += new MouseButtonEventHandler(mainGrid_MouseDown);

            //Draw all the fields
            drawFields();

            //Draw the decoration images on the grid
            drawImages();
        }

        //Create the right amount of rows and columns
        private void setGrid()
        {
            for (int i = 0; i < levelModel.IMapHeight; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(levelModel.ICellSize);
                mainGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < levelModel.IMapWidth; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(levelModel.ICellSize);
                mainGrid.ColumnDefinitions.Add(col);
            }
        }

        //Draw the field
        private void drawFields()
        {
            for (int iRow = 0; iRow < levelModel.IMapHeight; iRow++)
            {
                for (int iColumn = 0; iColumn < levelModel.IMapWidth; iColumn++)
                {
                    if (levelModel.Fields[iColumn, iRow] != null)
                    {
                        //Normal field
                        if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.Field) ||
                            levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.RedStart) ||
                            levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.BlueStart) ||
                            levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.YellowStart) ||
                            levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.GreenStart))
                        {
                            mainGrid.Children.Add(createEllipse(28, Colors.Black, iColumn, iRow));
                        }
                        //Barricade field
                        else if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.Barricade))
                        {
                            //Barricade field
                            mainGrid.Children.Add(createEllipse(28, Colors.DarkRed, iColumn, iRow));

                            //Barricade
                            Rectangle r = new Rectangle();
                            r.Width = 28;
                            r.Height = 26;
                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/barricade_pawn.png"));
                            r.Fill = myBrush;
                            r.SetValue(Grid.ColumnProperty, iColumn);
                            r.SetValue(Grid.RowProperty, iRow);
                            Panel.SetZIndex(r, 5);
                            mainGrid.Children.Add(r);

                            levelModel.ABarricades[iAmountOfBarricades] = new Model.BarricadePawn(r, levelModel.Fields[iColumn, iRow]);
                            iAmountOfBarricades++;
                        }
                        //Safespot field
                        else if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.SafeSpot))
                        {
                            mainGrid.Children.Add(createEllipse(28, Colors.LightBlue, iColumn, iRow));
                        }
                        //Finish field
                        else if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.Finish))
                        {
                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;

                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource =
                                new BitmapImage(new Uri("pack://application:,,,/Image/finish.png"));
                            e.Fill = myBrush;
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            mainGrid.Children.Add(e);
                        }
                        //Blue base
                        else if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.BlueBase))
                        {
                            //Blue base
                            mainGrid.Children.Add(createEllipse(28, Colors.Blue, iColumn, iRow));

                            //Blue pawn
                            Ellipse e = createEllipsePawn(28, "pawn_blue", iColumn, iRow, 2);
                            mainGrid.Children.Add(e);

                            gameController.createPawn(levelModel.Fields[iColumn, iRow], e, 3);

                        }
                        //Yellow base
                        else if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.YellowBase))
                        {
                            //Draw yellow base
                            mainGrid.Children.Add(createEllipse(28, Colors.Yellow, iColumn, iRow));

                            //Draw yellow pawn
                            Ellipse e = createEllipsePawn(28, "pawn_yellow", iColumn, iRow, 2);
                            mainGrid.Children.Add(e);

                            gameController.createPawn(levelModel.Fields[iColumn, iRow], e, 2);
                        }
                        //Green base
                        else if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.GreenBase))
                        {
                            //Draw green base
                            mainGrid.Children.Add(createEllipse(28, Colors.Green, iColumn, iRow));

                            //Draw green pawn
                            Ellipse e = createEllipsePawn(28, "pawn_green", iColumn, iRow, 2);
                            mainGrid.Children.Add(e);

                            gameController.createPawn(levelModel.Fields[iColumn, iRow], e, 1);
                        }
                        //Red base
                        else if (levelModel.Fields[iColumn, iRow].GetType() == typeof(Model.RedBase))
                        {
                            //Draw red base
                            mainGrid.Children.Add(createEllipse(28, Colors.Red, iColumn, iRow));

                            //Draw red pawn
                            Ellipse e = createEllipsePawn(28, "pawn_red", iColumn, iRow, 2);
                            mainGrid.Children.Add(e);

                            gameController.createPawn(levelModel.Fields[iColumn, iRow], e, 0);
                        }
                        //Line field
                        else if (levelModel.Fields[iColumn, iRow] is Model.LineField)
                        {
                            Rectangle r = new Rectangle();
                            r.Width = 5;
                            r.Height = 28;
                            r.Fill = new SolidColorBrush(Colors.Black);
                            r.SetValue(Grid.ColumnProperty, iColumn);
                            r.SetValue(Grid.RowProperty, iRow);
                            mainGrid.Children.Add(r);
                        }
                    }
                }
            }
        }

        //Create and return an ellipse - USED IN DRAW MAP
        private Ellipse createEllipse(int size, System.Windows.Media.Color color, int column, int row )
        {
            Ellipse e = new Ellipse();
            e.Width = size;
            e.Height = size;
            e.Fill = new SolidColorBrush(color);
            e.SetValue(Grid.ColumnProperty, column);
            e.SetValue(Grid.RowProperty, row);

            return e;
        }

        //Create and return an ellipse of a pawn - USED IN DRAW MAP
        private Ellipse createEllipsePawn(int size, String image, int column, int row, int zindex)
        {
            Ellipse e = new Ellipse();
            e.Width = size;
            e.Height = size;

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri("pack://application:,,,/Image/" + image + ".png"));
            e.Fill = myBrush;
            Panel.SetZIndex(e, zindex);
            e.SetValue(Grid.ColumnProperty, column);
            e.SetValue(Grid.RowProperty, row);

            return e;
        }

        //Mousedown event on the main grid
        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int mousePositionX = (int)Mouse.GetPosition(mainGrid).X;
            int mousePositionY = (int)Mouse.GetPosition(mainGrid).Y;

            int column = mousePositionX / levelModel.ICellSize;
            int row = mousePositionY / levelModel.ICellSize;

            if (levelModel.Fields[column, row] != null)
            {
                if (gameController.Barricade == null)
                {
                    gameController.fieldClick(levelModel.Fields[column, row]);
                }
                else
                {
                    gameController.moveBarricade(levelModel.Fields[column, row]);
                }
            }
            
        }

        //Show circles on the position of the pawn, and all locations it can move to. 
        public void createBlurs(Model.Field[] blurPositions, Model.Field pawnPosition)
        {
            removeBlurs();

            //Create blur ellipse for selecting a pawn.
            Ellipse blur;
            blur = new Ellipse();
            blur.Width = 28;
            blur.Height = 28;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/blur.png"));
            blur.Fill = myBrush;
            blur.SetValue(Grid.ColumnProperty, pawnPosition.X);
            blur.SetValue(Grid.RowProperty, pawnPosition.Y);
            Panel.SetZIndex(blur, 4);
            mainGrid.Children.Add(blur);
            blurLocations.Add(blur);

            //Create blur ellipse for positions the pawn can move to.
            foreach (Model.Field blurPosition in blurPositions)
            {
                Ellipse blur2;
                blur2 = new Ellipse();
                blur2.Width = 28;
                blur2.Height = 28;
                ImageBrush myBrush2 = new ImageBrush();
                myBrush2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/blur.png"));
                blur2.Fill = myBrush2;
                blur2.SetValue(Grid.ColumnProperty, blurPosition.X);
                blur2.SetValue(Grid.RowProperty, blurPosition.Y);
                Panel.SetZIndex(blur2, 3);
                mainGrid.Children.Add(blur2);
                blurLocations.Add(blur2);
            }
        }

        //Remove all of the blurs.
        public void removeBlurs()
        {
            foreach (Ellipse e in blurLocations)
            {
                e.Visibility = Visibility.Collapsed;
            }
            blurLocations.Clear();
        }

        //Make the barricade image follow the cursor untill it's placed
        public void setCursor(String cursor, Model.Field field)
        {
            switch (cursor)
            {
                case "barricade":
                    field.Barricade.Image.Visibility = Visibility.Collapsed;

                    sri = Application.GetResourceStream(new Uri("pack://application:,,,/Image/barricade_cur.cur"));
                    customCursor = new Cursor(sri.Stream);
                    this.Cursor = customCursor;
                    break;
                //Red player
                case "0":
                    sri = Application.GetResourceStream(new Uri("pack://application:,,,/Image/pawn_red_cur.cur"));
                    customCursor = new Cursor(sri.Stream);
                    this.Cursor = customCursor;
                    break;
                //Green player
                case "1":
                    sri = Application.GetResourceStream(new Uri("pack://application:,,,/Image/pawn_green_cur.cur"));
                    customCursor = new Cursor(sri.Stream);
                    this.Cursor = customCursor;
                    break;
                //Yellow player
                case "2":
                    sri = Application.GetResourceStream(new Uri("pack://application:,,,/Image/pawn_yellow_cur.cur"));
                    customCursor = new Cursor(sri.Stream);
                    this.Cursor = customCursor;
                    break;
                //Blue player
                case "3":
                    sri = Application.GetResourceStream(new Uri("pack://application:,,,/Image/pawn_blue_cur.cur"));
                    customCursor = new Cursor(sri.Stream);
                    this.Cursor = customCursor;
                    break;
                case "arrow":
                    if (field.Barricade != null)
                    {
                        field.Barricade.Image.Visibility = Visibility.Visible;
                    }

                    this.Cursor = Cursors.Arrow;
                    break;
            }
        }

        //Show the win animation for a certain player color. 
        public void showWinAnimation(int player)
        {
            switch (player)
            {
                case 0:
                    ImageBehavior.SetAnimatedSource(animatedWinImage1, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_red.gif")));
                    ImageBehavior.SetAnimatedSource(animatedWinImage2, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_red.gif")));
                    break;
                case 1:
                    ImageBehavior.SetAnimatedSource(animatedWinImage1, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_green.gif")));
                    ImageBehavior.SetAnimatedSource(animatedWinImage2, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_green.gif")));
                    break;
                case 2:
                    ImageBehavior.SetAnimatedSource(animatedWinImage1, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_yellow.gif")));
                    ImageBehavior.SetAnimatedSource(animatedWinImage2, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_yellow.gif")));
                    break;
                case 3:
                    ImageBehavior.SetAnimatedSource(animatedWinImage1, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_blue.gif")));
                    ImageBehavior.SetAnimatedSource(animatedWinImage2, new BitmapImage(new Uri("pack://application:,,,/Image/winAnimation_blue.gif")));
                    break;
            }

            animatedWinImage1.Visibility = Visibility.Visible;
            animatedWinImage2.Visibility = Visibility.Visible;
            ImageBehavior.GetAnimationController(animatedWinImage1).Play();
            ImageBehavior.GetAnimationController(animatedWinImage2).Play();
        }

        //Draws the right decoration images on the right map
        private void drawImages()
        {
            if (levelModel.GetType() ==  typeof(Model.ModelLevelSlow))
            {
                Image i = new Image();
                i.Source = new BitmapImage(new Uri("pack://application:,,,/Image/trees.png"));
                i.HorizontalAlignment = HorizontalAlignment.Left;
                i.Margin = new Thickness(30, -170, 0, 0);
                i.Width = 140;
                i.Height = 50;
                i.Visibility = Visibility.Visible;
                totalGrid.Children.Add(i);

                Image i2 = new Image();
                i2.Source = new BitmapImage(new Uri("pack://application:,,,/Image/house.png"));
                i2.HorizontalAlignment = HorizontalAlignment.Right;
                i2.Margin = new Thickness(0, -190, 30, 0);
                i2.Width = 140;
                i2.Height = 70;
                i2.Visibility = Visibility.Visible;
                totalGrid.Children.Add(i2);
            }
            else
            {
                Image i = new Image();
                i.Source = new BitmapImage(new Uri("pack://application:,,,/Image/trees.png"));
                i.Margin = new Thickness(160, 191, 158, 348);
                i.Width = 100;
                i.Height = 35;
                i.Visibility = Visibility.Visible;
                totalGrid.Children.Add(i);
            }
        }
    }
}
