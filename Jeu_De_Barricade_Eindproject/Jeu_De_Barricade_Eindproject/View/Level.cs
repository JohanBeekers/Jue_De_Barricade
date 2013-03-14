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
using System.Windows.Shapes;

namespace Jeu_De_Barricade_Eindproject.View
{
    public abstract class Level : UserControl
    {
        private Grid boardGrid;
        protected int pawnAmount;
        protected static int CellSize = 32;
        protected int iMapWidth;
        protected int iMapHeight;
        protected Controller.Game game;
        private Model.BarricadePawn[] aBarricadePawns;

        protected int iArrayBarricadePawns = 0;
        private Ellipse blur;

        private Model.Field[,] fields;

        protected String[,] map;

        //properties
        public Grid BoardGrid
        {
            get { return boardGrid; }
            set { boardGrid = value; }
        }
        public Ellipse Blur
        {
            get { return blur; }
            set { blur = value; }
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

        public Level(Controller.Game game)
        {
            this.game = game;

            initVariables();

            boardGrid.MouseDown += new MouseButtonEventHandler(boardGrid_MouseDown);

            //Create blur ellipse for selecting a pawn.
            blur = new Ellipse();
            blur.Width = 28;
            blur.Height = 28;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/blur.png"));
            blur.Fill = myBrush;

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
                            Ellipse e2 = new Ellipse();
                            e2.Width = 28;
                            e2.Height = 28;

                            ImageBrush myBrush = new ImageBrush();
                            myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/barricade_pawn.png"));
                            e2.Fill = myBrush;
                            e2.SetValue(Grid.ColumnProperty, iColumn);
                            e2.SetValue(Grid.RowProperty, iRow);
                            Panel.SetZIndex(e2, 1);
                            boardGrid.Children.Add(e2);

                            aBarricadePawns[iArrayBarricadePawns] = new Model.BarricadePawn(e2, fields[iColumn, iRow]);
                            iArrayBarricadePawns++;
                        }
                        //Safespot field
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.SafeSpot))
                        {
                            Ellipse e = new Ellipse();
                            //A safespot is 2 px bigger
                            e.Width = 30;
                            e.Height = 30;
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

            if(game.Barricade == null)
            {
                game.selectPawn(column, row);
            }
            else
            {
                game.moveBarricade(column, row);
            }
            

        }

    }
}
