using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace Jeu_De_Barricade_Eindproject.View
{
    public abstract class Level : UserControl
    {
        private Grid boardGrid;
        protected int pawnAmount;
        protected static int CellSize = 32;
        private int iMapWidth;
        private int iMapHeight;
        protected Controller.Game game;
        private Model.BarricadePawn[] aBarricadePawns;
        protected Image animatedWinImage1, animatedWinImage2;

        protected int iArrayBarricadePawns = 0;
        private List<Ellipse> blurLocations;

        private Model.Field[,] fields;

        private int[] aNoBarricades;

        private StreamResourceInfo sri;
        private Cursor customCursor;
        
        protected String[,] map;

        //properties
        public Grid BoardGrid
        {
            get { return boardGrid; }
            set { boardGrid = value; }
        }
        public int IMapWidth
        {
            get { return iMapWidth; }
            set { iMapWidth = value; }
        }
        public int IMapHeight
        {
            get { return iMapHeight; }
            set { iMapHeight = value; }
        }
        public Model.Field[,] Fields
        {
            get { return fields; }
            set { fields = value; }
        }
        protected Model.BarricadePawn[] ABarricadePawns
        {
            get { return aBarricadePawns; }
            set { aBarricadePawns = value; }
        }

        public int[] ANoBarricades
        {
            get { return aNoBarricades; }
            set { aNoBarricades = value; }
        }

        public Level(Controller.Game game)
        {
            this.game = game;

            initVariables();

            blurLocations = new List<Ellipse>();
            boardGrid.MouseDown += new MouseButtonEventHandler(boardGrid_MouseDown);

            Controller.MapConverter mapConverter = new Controller.MapConverter();
            fields = mapConverter.convertMap(map, iMapWidth, iMapHeight);
            drawFields();
        }

        //This method is used in subclasses to initialize the variables unique in each subclass.
        protected abstract void initVariables();

        protected void drawFields()
        {
            for (int iRow = 0; iRow < iMapHeight; iRow++)
            {
                for (int iColumn = 0; iColumn < iMapWidth; iColumn++)
                {
                    if (fields[iColumn, iRow] != null)
                    {
                        //Normal field
                        if (fields[iColumn, iRow].GetType() == typeof(Model.Field) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.RedStart) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.BlueStart) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.YellowStart) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.GreenStart))
                        {
                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Black);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);
                        }
                        //Barricade field
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.Barricade))
                        {
                            //Barricade field
                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.DarkRed);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);

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
                            boardGrid.Children.Add(r);

                            aBarricadePawns[iArrayBarricadePawns] = new Model.BarricadePawn(r, fields[iColumn, iRow]);
                            iArrayBarricadePawns++;
                        }
                        //Safespot field
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.SafeSpot))
                        {
                            Ellipse e = new Ellipse();
                            //A safespot is 2 px bigger
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.LightBlue);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);
                        }
                        //Finish field
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.Finish))
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
                            boardGrid.Children.Add(e);
                        }
                        //Blue base
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.BlueBase))
                        {

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Blue);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);

                            Ellipse e2 = new Ellipse();
                            e2.Width = 28;
                            e2.Height = 28;

                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource =
                                new BitmapImage(new Uri("pack://application:,,,/Image/pawn_blue.png"));
                            e2.Fill = myBrush;
                            e2.SetValue(Grid.ColumnProperty, iColumn);
                            e2.SetValue(Grid.RowProperty, iRow);
                            Panel.SetZIndex(e2, 2);
                            boardGrid.Children.Add(e2);

                            game.createPawn(fields[iColumn, iRow], e2, 3);

                        }
                        //Yellow base
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.YellowBase))
                        {

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Yellow);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);

                            Ellipse e2 = new Ellipse();
                            e2.Width = 28;
                            e2.Height = 28;

                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource =
                                new BitmapImage(new Uri("pack://application:,,,/Image/pawn_yellow.png"));
                            e2.Fill = myBrush;
                            e2.SetValue(Grid.ColumnProperty, iColumn);
                            e2.SetValue(Grid.RowProperty, iRow);
                            Panel.SetZIndex(e2, 2);
                            boardGrid.Children.Add(e2);

                            game.createPawn(fields[iColumn, iRow], e2, 2);
                        }
                        //Green base
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.GreenBase))
                        {

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Green);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);

                            Ellipse e2 = new Ellipse();
                            e2.Width = 28;
                            e2.Height = 28;

                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource =
                                new BitmapImage(new Uri("pack://application:,,,/Image/pawn_green.png"));
                            e2.Fill = myBrush;
                            e2.SetValue(Grid.ColumnProperty, iColumn);
                            e2.SetValue(Grid.RowProperty, iRow);
                            Panel.SetZIndex(e2, 2);
                            boardGrid.Children.Add(e2);

                            game.createPawn(fields[iColumn, iRow], e2, 1);
                        }
                        //Red base
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.RedBase))
                        {

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Red);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);

                            Ellipse e2 = new Ellipse();
                            e2.Width = 28;
                            e2.Height = 28;

                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource =
                                new BitmapImage(new Uri("pack://application:,,,/Image/pawn_red.png"));
                            e2.Fill = myBrush;
                            e2.SetValue(Grid.ColumnProperty, iColumn);
                            e2.SetValue(Grid.RowProperty, iRow);
                            Panel.SetZIndex(e2, 2);
                            boardGrid.Children.Add(e2);

                            game.createPawn(fields[iColumn, iRow], e2, 0);
                        }
                        //Line field
                        else if (fields[iColumn, iRow] is Model.LineField)
                        {
                            Rectangle r = new Rectangle();
                            r.Width = 5;
                            r.Height = 28;
                            r.Fill = new SolidColorBrush(Colors.Black);
                            r.SetValue(Grid.ColumnProperty, iColumn);
                            r.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(r);
                        }
                    }
                }
            }
        }

        private void boardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int mousePositionX = (int)Mouse.GetPosition(boardGrid).X;
            int mousePositionY = (int)Mouse.GetPosition(boardGrid).Y;

            int column = mousePositionX / CellSize;
            int row = mousePositionY / CellSize;

            if (fields[column, row] != null)
            {
                if (game.Barricade == null)
                {
                    game.fieldClick(fields[column, row]);
                }
                else
                {
                    game.moveBarricade(fields[column, row]);
                }
            }
            

        }

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
            boardGrid.Children.Add(blur);
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
                boardGrid.Children.Add(blur2);
                blurLocations.Add(blur2);
            }
        }

        public void removeBlurs()
        {
            foreach (Ellipse e in blurLocations)
            {
                e.Visibility = Visibility.Collapsed;
            }
            blurLocations.Clear();
        }

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
    }
}
