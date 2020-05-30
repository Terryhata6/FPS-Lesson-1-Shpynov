using UnityEngine;

namespace Game
{
    public sealed class LevelMakerController : IInitialization
    {
        #region LevelController
        private LevelMaker _levelMaker;
        private int _height;
        private int _width;
        public Cell[,] _maze;
        private NavMeshRebaker _rebaker;
        #endregion
        #region IInitialization
        public void Initialization()
        {
            _levelMaker = Object.FindObjectOfType<LevelMaker>();
            _height = _levelMaker._height;
            _width = _levelMaker._width;
            _rebaker = Object.FindObjectOfType<NavMeshRebaker>();
            _maze = new Cell[_height,_width];
            for (int i = 0; i < 2; i++) //обрезает края массива(костыль)
            {
                for (int j = 0; j < 2; j++)
                {
                    _maze[i, j].isVisited = true;
                    _maze[_height - i - 2, _width - j - 2].isVisited = true;
                }
            }
            GenerateMaze();
        }
        #endregion
        #region Methods
        public void CreateMaze()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    switch (_maze[i, j].Type)
                    {
                        case CellTypes.BridgeLR:
                            _levelMaker.CreateBridge(i, j, CellTypes.BridgeLR);
                            break;
                        case CellTypes.BridgeUD:
                            _levelMaker.CreateBridge(i, j, CellTypes.BridgeUD);
                            break;
                        case CellTypes.Room:
                            _levelMaker.CreateRoom(i, j);
                            break;
                        case CellTypes.Void:
                            break;
                        default:
                            break;
                    }
                }
            }
            _rebaker.RebakeNavMesh();
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if(_maze[i,j].Type == CellTypes.Room)
                    _levelMaker.AddEnemyes(i,j);
                }
            }
        }

        /// <summary>
        /// Начинает генерацию лабиринта
        /// </summary>
        private void GenerateMaze()
        {
            _maze[2, 2].isVisited = true;
            _maze[2, 2].Type = CellTypes.Room;
            _maze[2, 3].Type = CellTypes.BridgeUD;
            Routine(2, 4);
        }
        /// <summary>
        /// Рекурсивно "копает" лабиринт
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void Routine(int x, int y)
        {
            bool up = true;
            bool down = true;
            bool left = true;
            bool right = true;
            _maze[x,y].isVisited = true;
            _maze[x,y].Type = CellTypes.Room;
            if (y - 2 > 0)
                up = _maze[x, y - 2].isVisited;
            if (y + 2 < _width)
                down = _maze[x, y + 2].isVisited;
            if (x + 2 < _height)
                right = _maze[x + 2, y].isVisited;
            if (x - 2 > 0)
                left = _maze[x - 2, y].isVisited;
            int _connections = 1;
            int _connectionLimit = Random.Range(2,4);
            while (up == false || down == false || right == false || left == false)
            {
                //int t = Random.Range(0, 4);
                switch (Random.Range(0, 4))
                {
                    case 0:
                        if (left == false) //left
                        {
                            left = true;
                            if(_connections < _connectionLimit) 
                            {
                                _maze[x - 1, y].Type = CellTypes.BridgeLR;
                                _connections++;
                            }                            
                            Routine(x - 2, y);
                        }                        
                        break;
                    case 1:
                        if (right == false) //right
                        {
                            right = true;
                            if (_connections < _connectionLimit)
                            {
                                _maze[x + 1, y].Type = CellTypes.BridgeLR;
                                _connections++;
                            }
                            Routine(x + 2, y);
                        }
                        break;
                    case 2:
                        if (up == false) //up
                        {
                            up = true;
                            if (_connections < _connectionLimit)
                            {
                                _maze[x, y - 1].Type = CellTypes.BridgeUD;
                                _connections++;
                            }
                            Routine(x, y - 2);
                        }
                        break;                        
                    case 3:
                        if (down == false) //down
                        {
                            down = true;
                            if (_connections < _connectionLimit)
                            {
                                _maze[x, y + 1].Type = CellTypes.BridgeUD;
                                _connections++;
                            }
                            Routine(x, y + 2);
                        }
                        break;
                    default:
                        break;                        
                }                
            }
        }
        #endregion
    }
}