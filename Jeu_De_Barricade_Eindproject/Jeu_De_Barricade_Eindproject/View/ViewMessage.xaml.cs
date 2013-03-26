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
using System.Windows.Shapes;

namespace Jeu_De_Barricade_Eindproject.View
{
    /// <summary>
    /// Interaction logic for ViewMessage.xaml
    /// </summary>
    public partial class ViewMessage : Window
    {
        private Model.ModelLevel levelModel;
        private Controller.Player[] aPlayers;

        private String[,] viewMap;

        private int iPlayerNumber = 0;
        private String sTotal;


        public ViewMessage(Model.ModelLevel levelModel, Controller.Player[] aPlayers)
        {
            InitializeComponent();

            this.levelModel = levelModel;
            this.aPlayers = aPlayers;

            viewMap = new String[levelModel.IMapHeight, levelModel.IMapWidth];
        }

        public void generateViewMap()
        {
            //Loop through the orginial map
            for (int iRow = 0; iRow < levelModel.IMapHeight; iRow++)
            {
                for (int iColumn = 0; iColumn < levelModel.IMapWidth; iColumn++)
                {
                    if (levelModel.Map[iRow, iColumn].Equals(" "))
                    {
                        viewMap[iRow, iColumn] = "   ";
                    }
                    else if (!levelModel.Map[iRow, iColumn].Equals(" "))
                    {
                        viewMap[iRow, iColumn] = "(o)";
                    }
                }
            }

            //Loop through all barricades
            foreach (Model.BarricadePawn barricade in levelModel.ABarricades)
            {
                viewMap[barricade.CurrentLocation.Y, barricade.CurrentLocation.X] = "(#)";
            }

            //Loop through all player and pawns
            foreach (Controller.Player player in aPlayers)
            {
                String sPlayer = "";

                switch (iPlayerNumber)
                {
                    case 0:
                        sPlayer = "(r)";
                        break;
                    case 1:
                        sPlayer = "(g)";
                        break;
                    case 2:
                        sPlayer = "(y)";
                        break;
                    case 3:
                        sPlayer = "(b)";
                        break;
                }


                foreach (Controller.Pawn pawn in player.APawns)
                {
                    viewMap[pawn.CurrentLocation.Y, pawn.CurrentLocation.X] = sPlayer;
                }

                iPlayerNumber++;
            }

            iPlayerNumber = 0;
        }

        public void show()
        {
            sTotal = "";
            generateViewMap();

            for (int iRow = 0; iRow < levelModel.IMapHeight; iRow++)
            {
                String sRow = "";

                for (int iColumn = 0; iColumn < levelModel.IMapWidth; iColumn++)
                {
                    sRow += viewMap[iRow, iColumn] + " ";
                }

                sTotal += sRow + "\n";
            }

            labelMap.Content = sTotal;

            this.Width = 520;
            this.Height = 305;
        }
    }
}
