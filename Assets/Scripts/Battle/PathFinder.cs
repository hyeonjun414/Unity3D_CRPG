using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Constants
{
    public const int INF = 99999;
}
public class PathNode
{
    public bool isActive;
    public PathNode prevNode;
    public Vector2 point;
    public int g, h, f;

    public PathNode()
    {
        isActive = true;
        prevNode = null;
        g = Constants.INF;
        h = Constants.INF;
        f = Constants.INF;
    }
    public PathNode(PathNode _prevNode, Vector2 _point, int _g, int _h)
    {
        isActive = true;
        prevNode = _prevNode;
        point = _point;
        g = _g;
        h = _h;
        f = g + h;
    }
}

public class PathFinder : MonoBehaviour
{
    BattleTile[,] map;
    public int mapSize;
    public List<Vector2> dir = new List<Vector2>();

    void Awake()
    {
        AddDir();
    }

    public void AddDir()
    {
        dir.Add(new Vector2(-1, 0));
        dir.Add(new Vector2(1, 0));
        dir.Add(new Vector2(0, 1));
        dir.Add(new Vector2(0, -1));
        dir.Add(new Vector2(-1, -1));
        dir.Add(new Vector2(-1, 1));
        dir.Add(new Vector2(1, -1));
        dir.Add(new Vector2(1, 1));
    }

    public List<Vector2> ExcutePathFind(Vector2 _start, Vector2 _end, BattleStage bs)
    {
        map = bs.battleMap;
        mapSize = bs.mapSize;
        
        return Astar(_start, _end);
    }
    // 한 정점을 기준으로 도착점까지의 h값을 추산하여 반환하는 함수
    public int GetH(Vector2 start, Vector2 end)
    {
        int xSize = (int)Mathf.Abs(start.x - end.x);
        int ySize = (int)Mathf.Abs(start.y - end.y);
        int line = Mathf.Abs(xSize - ySize);
        int cross = xSize > ySize ? xSize - line : ySize - line;

        return 10 * line + 14 * cross;
    }
    // 해당 좌표의 정점이 리스트에 존재하는지 확인하는 함수
    public PathNode GetNodeFromList(LinkedList<PathNode> _list, Vector2 _point)
    {
        PathNode resultNode = null;
        foreach (PathNode node in _list)
        {
            if (node.point == _point)
                resultNode = node;
        }
        return resultNode;
    }
    // 오픈 리스트에서 f값이 가장 작은 정점을 반환하는 함수
    public PathNode NodeWithLowestF(LinkedList<PathNode> _list)
    {
        PathNode Node = null;
        int minF = Constants.INF;
        int minH = Constants.INF;
        foreach (PathNode node in _list)
        {
            if ((node.f < minF || (node.f == minF && node.h < minH)) &&
                node.isActive)
            {
                minF = node.f;
                minH = node.h;
                Node = node;
            }
        }
        return Node;
    }
    public List<Vector2> Astar(Vector2 start, Vector2 end)
    {
        LinkedList<PathNode> openList = new LinkedList<PathNode>();
        openList.AddLast(new PathNode(null, start, 0, GetH(start, end)));

        while (true)
        {
            PathNode openNode = NodeWithLowestF(openList);

            if(openNode == null)
            {
                print("경로를 찾을 수 없습니다.");
                return new List<Vector2>();
            }

            if (openNode.point == end)
            {
                PathNode tempNode = openNode;
                List<Vector2> tempList = new List<Vector2>();
                while (null != tempNode)
                {
                    tempList.Insert(0,map[(int)tempNode.point.x, (int)tempNode.point.y].tilePos);
                    tempNode = tempNode.prevNode;
                }
                return tempList;
            }
            for (int i = 0; i < dir.Count; i++)
            {
                if (openNode.point.y + dir[i].y >= 0 &&
                    openNode.point.y + dir[i].y < mapSize &&
                    openNode.point.x + dir[i].x < mapSize &&
                    openNode.point.x + dir[i].x >= 0)
                {
                    int childY = (int)(openNode.point.y + dir[i].y);
                    int childX = (int)(openNode.point.x + dir[i].x);

                    PathNode tempNode = GetNodeFromList(openList, map[childX, childY].tilePos);
                    // 못가는 노드면 다음 탐색
                    if (map[childX, childY].state == TileState.STAY && map[childX,childY].tilePos != end) continue;

                    // 이미 오픈리스트에 있는 노드이면
                    if (GetNodeFromList(openList, map[childX, childY].tilePos) != null &&
                        !tempNode.isActive) continue;

                    int newG = 10;
                    int newH = GetH(map[childX, childY].tilePos, end);
                    int newF = newG + newH;

                    if (tempNode == null)
                    {
                        openList.AddLast(new PathNode(openNode, map[childX, childY].tilePos, newG, newH));
                    }
                    else if (tempNode.f > newF)
                    {
                        tempNode.g = newG;
                        tempNode.h = newH;
                        tempNode.f = newF;
                        tempNode.prevNode = openNode;
                    }
                }
            }
            openNode.isActive = false;
        }
    }
}