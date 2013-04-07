using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_De_Barricade_Eindproject.Model
{
    public abstract class ModelLevel
    {
        protected String[,] map;

        protected Model.Field[,] fields;

        protected int[] aNoBarricades;
        protected Model.BarricadePawn[] aBarricades;
        
        protected int pawnAmount;
        protected int iMapWidth;
        protected int iMapHeight;
        protected int iWidth;
        protected int iHeight;
        protected int iCellSize = 32;

        protected int iArrayBarricadePawns = 0;

        public String[,] Map
        {
            get { return map; }
        }

        public Model.Field[,] Fields
        {
            get { return fields; }
        }


        public int[] ANoBarricades
        {
            get { return aNoBarricades; }
            set { aNoBarricades = value; }
        }

        public Model.BarricadePawn[] ABarricades
        {
            get { return aBarricades; }
            set { aBarricades = value; }
        }
        
        public int PawnAmount
        {
            get { return pawnAmount; }
        }

        public int IMapWidth
        {
            get { return iMapWidth; }
        }
        
        public int IMapHeight
        {
            get { return iMapHeight; }
        }

        public int IWidth
        {
            get { return iWidth; }
        }

        public int IHeight
        {
            get { return iHeight; }
        }

        public int ICellSize
        {
            get { return iCellSize; }
        }

        public int IArrayBarricadePawns
        {
            get { return iArrayBarricadePawns; }
            set { iArrayBarricadePawns = value; }
        }

        //Convert the 2d String array to a 2d array of fields.
        protected void fillViewArray()
        {
            //Initialize and fill the array with view components
            Controller.MapConverter mapConverter = new Controller.MapConverter();
            fields = mapConverter.convertMap(map, iMapWidth, iMapHeight);
        }
    }
}
