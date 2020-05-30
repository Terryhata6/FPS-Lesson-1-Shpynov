namespace Game
{
    public struct Cell
    {
        public int X;
        public int Y;
        public bool isVisited;
        //public int Connections;
        public CellTypes Type;


        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            isVisited = false;
            //Connections = 0;
            Type = CellTypes.Void;
        }
    }
}
