using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Jeu_De_Barricade_Eindproject.View
{
    public abstract class Level : UserControl
    {
        protected Grid boardGrid;
        protected int pawnAmount;
        protected int iMapWidth;
        protected int iMapHeight;
        protected Model.Field[] aPlayerRedStartFields, aPlayerGreenStartFields, aPlayerYellowStartFields, aPlayerBlueStartFields;
        protected int iRedStartFields = 0, iGreenStartFields = 0, iYellowStartFields = 0, iBlueStartFields = 0;

        protected Model.Field[,] fields;

        protected String[,] map;

        //properties
        public Model.Field[] APlayerRedStartFields
        {
            get { return aPlayerRedStartFields; }
        }
        public Model.Field[] APlayerGreenStartFields
        {
            get { return aPlayerRedStartFields; }
        }
        public Model.Field[] APlayerYellowStartFields
        {
            get { return aPlayerRedStartFields; }
        }
        public Model.Field[] APlayerBlueStartFields
        {
            get { return aPlayerRedStartFields; }
        }

        public Level()
        {
            initVariables();

            aPlayerRedStartFields = new Model.Field[pawnAmount];
            aPlayerGreenStartFields = new Model.Field[pawnAmount];
            aPlayerYellowStartFields = new Model.Field[pawnAmount];
            aPlayerBlueStartFields = new Model.Field[pawnAmount];

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
                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.DarkRed);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);
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
                            aPlayerBlueStartFields[iBlueStartFields] = fields[iColumn, iRow];
                            iBlueStartFields++;

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Blue);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);
                        }
                        //Yellow base
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.YellowBase))
                        {
                            aPlayerYellowStartFields[iYellowStartFields] = fields[iColumn, iRow];
                            iYellowStartFields++;

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Yellow);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);
                        }
                        //Green base
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.GreenBase))
                        {
                            aPlayerGreenStartFields[iGreenStartFields] = fields[iColumn, iRow];
                            iGreenStartFields++;

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Green);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);
                        }
                        //Red base
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.RedBase))
                        {
                            aPlayerRedStartFields[iRedStartFields] = fields[iColumn, iRow];
                            iRedStartFields++;

                            Ellipse e = new Ellipse();
                            e.Width = 28;
                            e.Height = 28;
                            e.Fill = new SolidColorBrush(Colors.Red);
                            e.SetValue(Grid.ColumnProperty, iColumn);
                            e.SetValue(Grid.RowProperty, iRow);
                            boardGrid.Children.Add(e);
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

    }
}
