using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int m_width;
    private int m_height;
    private int[,] m_gridArray;

    public Grid(int width, int height)
    {
        this.m_width = width;
        this.m_height = height;
        m_gridArray = new int[width, height];

        for (int x = 0; x < m_gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < m_gridArray.GetLength(1); y++)
            {
                Debug.Log(x + ", " + y);
            }
        }

    }








}
