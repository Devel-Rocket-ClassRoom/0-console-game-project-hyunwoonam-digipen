using System;
using System.Collections.Generic;
using System.Text;

public class MazeGenerator
{
    private int width;
    private int height;

    private int cellWidth;
    private int cellHeight;

    private int[,] map;
    private Random rand = new Random();


    MazeGenerator(int w, int h)
    {
        width = w % 2 == 0 ? w + 1 : w;
        height = h % 2 == 0 ? h + 1 : h;

        cellHeight = (height - 1) / 2;
        cellWidth = (width - 1) / 2;

        map = new int[width, height];
    }

    public void Generate()
    {
        //전체 그리드 벽으로
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = 1;
            }
        }

        int[] currRow = new int[cellWidth];

        int nextSetId = 1;

        for (int y = 0; y < cellHeight; y++)
        {
            int currCol = y * 2 + 1;

            //방파기
            for (int x = 0; x < cellWidth; x++)
            {
                map[currCol, x * 2 + 1] = 0;
            }
        }

        for (int y = 0; y < cellHeight; y++)
        {
            int currCol = y * 2 + 1;

            //첫 셀 행의 고유 집합 번호 부여
            for (int i = 0; i < cellWidth; i++)
            {
                currRow[i] = nextSetId++;
            }
            

            //수평 연결
            for (int x = 0; x < cellWidth - 1; x++)
            {
                if (currRow[x] == currRow[x + 1])
                {
                    continue;
                }

                if (rand.Next(2) == 0)
                {
                    map[currCol, x * 2 + 2] = 0;

                    int oldSet = currRow[x + 1];
                    int newSet = currRow[x];

                    for (int i = 0; i < cellWidth; i++)
                    {
                        if (currRow[i] == oldSet)
                        {
                            currRow[i] = newSet;
                        }
                    }
                }
            }

            //수직 연결

            if (y < cellHeight -1)
            {

            }

        }

        

    }


}
