using System;
using System.Collections.Generic;
using System.Text;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }
}

// 정점 정보 (NodeInfo)
public class NodeInfo
{
    public int G { get; private set; } // 시작점 비용
    public int H { get; private set; } // 도착점 비용

    // f = g + h
    public int F => G + H;

    public Point NowP { get; private set; } // 현재 노드의 위치
    public NodeInfo ParentInfo { get; private set; } // 부모 노드

    public NodeInfo(Point point, NodeInfo parent, int g, Point h)
    {
        NowP = point;
        ParentInfo = parent;
        G = g;
        Heuristic(h);
    }

    //  시작점 비용 설정
    public void setG(int g)
    {
        G = g;
    }

    // H 휴리스틱
    private void Heuristic(Point endNode)
    {
        int manhattan = Math.Abs(endNode.X - NowP.X) + Math.Abs(endNode.Y - NowP.Y);
        H = G * manhattan; 
    }
}

// 길찾기 (PathFinding)
public class PathFinding
{
    public List<NodeInfo> Open { get; set; } = new List<NodeInfo>();   //이미 F비용을 계산한 모는 노드를 저장
    public List<NodeInfo> Closed { get; set; } = new List<NodeInfo>(); //이미 평가된 노드 집합
    public List<Point> CurrentPoints { get; set; } = new List<Point>();// 가장 낮은 f 비용을 가진 열린 목록의 노드

    private int[,] mapdata; //임시 맵

    private Point[] direction = new Point[4]; // 4방향의 정보를 담고 있음

    public PathFinding(int[,] map)
    {
        mapdata = map;

        // 4방향 (위, 아래, 왼쪽, 오른쪽)
        direction[0] = new Point(0, -1);
        direction[1] = new Point(0, 1);
        direction[2] = new Point(-1, 0);
        direction[3] = new Point(1, 0);
    }

    public List<Point> GetPath(Point start, Point end)
    {
        // 초기화
        Open.Clear();
        Closed.Clear();
        CurrentPoints.Clear();

        //현재 노드가 부모노드가 됨
        NodeInfo parent = new NodeInfo(start, null, 0, end); // (시작점, 자기 자신 초기화, 끝점)
        Closed.Add(parent);

        // 재귀 탐색 시작
        NodeInfo pathFind = FindLoop(parent, end);

        if (pathFind != null)
        {
            while (pathFind.ParentInfo != null)
            {
                // end 부터 start까지 부모를 불러와 넣어줌
                CurrentPoints.Insert(
                    0, //begin()
                    pathFind.NowP); // end 부터 start까지 부모를 불러와 넣어줌 end -> 그 이전 점 -> ... -> start
                pathFind = pathFind.ParentInfo;
            }
        }

        return CurrentPoints;
    }

    //재귀
    public NodeInfo FindLoop(NodeInfo parent, Point end)
    {
        // foreach neighbout of the current node
        for (int i = 0; i < 4; i++)
        {
            // 4방향
            Point childPoint = parent.NowP + direction[i];

            if (Exception(childPoint))
            {
                NodeInfo child = new NodeInfo(childPoint, parent, parent.G + 1, end);
                // 중복 오픈리스트, 클로즈리스트 검사
                CheckList(child);
            }
        }

        // 리스트가 비어있을 때 인덱스로 접근하면 에러
        if (Open.Count == 0)
        {
            return null;
        }

        // node in open with the lowest fcost
        NodeInfo current = Open[0];
        foreach (var it in Open)
        {
            if (current.F >= it.F)
            {
                current = it;
            }
        }

        // remove current from open
        Open.Remove(current);
        // add current to closed
        Closed.Add(current);

        // if current is the target node
        if (OverlapCheck(current.NowP, end))
        {
            //Console.WriteLine("find");
            return current;
        }

        return FindLoop(current, end);
    }

    //  타겟 노드 도착 체크
    public bool OverlapCheck(Point c, Point t)
    {
        if (c.X == t.X && c.Y == t.Y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 맵의 장애물 및 경계 처리
    public bool Exception(Point point)
    {
        // 벽 검사
        if (mapdata[point.Y, point.X] == 1)
        {
            return false;
        }
        return true;
    }

    // 원본 로직: neighbour is in closed
    public bool CheckList(NodeInfo c)
    {
        // neighbout is in closed skip to the next neighbour
        foreach (var it in Closed)
        {
            if (OverlapCheck(it.NowP, c.NowP))
            {
                return true;
            }
        }

        // if new path to neighbour is shorter or neighbour is not in open
        // set fcost of neighbour
        // set parent of neighbour to current

        foreach (var it in Open)
        {
            if(OverlapCheck(it.NowP, c.NowP))
            {
                if (it.F > c.F)
                {
                    Open.Remove(it);
                    Open.Add(c);
                    return true;
                }
                return true;
            }
        }

        // if neighbour is not in open add neighbour to open
        Open.Add(c);
        return true;
    }
}