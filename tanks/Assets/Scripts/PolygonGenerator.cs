﻿using System.Collections.Generic;
using UnityEngine;

public class PolygonGenerator : MonoBehaviour
{
    public List<Vector3> newVertices = new List<Vector3>();
    public List<int> newTriangles = new List<int>();
    public List<Vector2> newUV = new List<Vector2>();

    private Mesh mesh;

    private float tUnit = 0.25f;

    private int terrainSize = 80;

    private static Vector2 tBlue1 = new Vector2(0, 0);
    private static Vector2 tGrass1 = new Vector2(0, 1);
    private static Vector2 tGrass2 = new Vector2(0, 2);
    private static Vector2 tClear = new Vector2(0, 3);

    private static Vector2 tStone = new Vector2(1, 0);
    private static Vector2 tOrange = new Vector2(1, 1);
    private static Vector2 tRed = new Vector2(1, 2);
    private static Vector2 tSea = new Vector2(1, 3);

    private static Vector2 tPurple = new Vector2(2, 0);
    private static Vector2 tGreen = new Vector2(2, 1);
    private static Vector2 tYellow = new Vector2(2, 2);
    private static Vector2 tBlue = new Vector2(2, 3);

    private static Vector2 tWhite = new Vector2(3, 0);
    private static Vector2 tGrey1 = new Vector2(3, 1);
    private static Vector2 tGrey2 = new Vector2(3, 2);
    private static Vector2 tBlack = new Vector2(3, 3);

    private static Vector2[] tiles = new Vector2[] { tBlue1, tGrass1, tGrass2, tClear, tStone, tOrange, tRed, tSea, tPurple, tGreen, tYellow, tBlue, tWhite, tGrey1, tGrey2, tBlack, tClear };

    public byte[,] blocks;
    private int squareCount;

    private string[] tileMap = new string[] {
        "61111111111111111111111111111111666666111111111111111111111111111111111111111111",
        "61111111111111111111111111111111666666666611111111111111111111111111111111111111",
        "61111111111111111111111111111111166666111111111111111111111111111111111111111111",
        "61111111111111111111111111111111111116666111666611111111111111111111111111111111",
        "61111111111111111111111111111111111111666666661111111111111111111111111111111111",
        "61111111111111111111111111111111111166666666611111111111111111111111111111111111",
        "61111111111111111111111666666666666666666666666611666666666666666666666666611111",
        "61111111111111111111111111666666666666666666666666666666666666666666661111111111",
        "61111111111111111111111111111166666666666666666666666661111111111111111111111111",
        "6111111111111111111111111111dddddddddddddddd111111111111111111111111111111111111",
        "611111111111111111111111111111dddddddddd1111111111111111111111111111111111111111",
        "6111111111111111111111111111111ddddd11111111111111111111111111111111111111111111",
        "61111111111111111111111111111161611111111111111111111111111111111111111111111111",
        "61111111111111111111111111111161611111111111111111111111111111111111111111111111",
        "61111111111111111111111111111161611111111111111111111111111111111111111111111111",
        "61111111111111111111111666666661611111111111111111111111111111111111111111111111",
        "61111111111111111111111111666666611111116666661111111111111111111111111111111111",
        "61111111111111111111111116666161611111111116666611111111111111111111111111111111",
        "61111111111111111111111111111161611111116666611111111111111111111111111111111111",
        "61111111111111111111111111111161611116666611111111111111111111111111111111111111",
        "61111111111111111111111111111166666666111111111111111111111111111111111111111111",
        "61111111111111111111111111111166666666111111111111111111111111111111111111111111",
        "61111111111111111111111111111116666666111111111111111111111111111111111111111111",
        "61111111111111111111111111111111666666111111111111111111111111111111111111111111",
        "61111111111111111111111111111111666666666611111111111111111111111111111111111111",
        "61111111111111111111111111111111166666111111111111111111111111111111111111111111",
        "61111111111111111111111111111111111666666111111111111111111111111111111111111111",
        "61111111111111111111111111111111111116666111666611111111111111111111111111111111",
        "61111111111111111111111111111111111111666666661111111111111111111111111111111111",
        "61111111111111111111111111111111111166666666611111111111111111111111111111111111",
        "61111111111111111111111666666666666666666666666611666666666666666666666666611111",
        "61111111111111111111111111666666666666666666666666666666666666666666661111111111",
        "61111111111111111111111111111166666666666666666666666661111111111111111111111111",
        "61111111111111111111111111111111111166666666666666666666666661111116111111111111",
        "61111111111111111111111111111666666666666666666666666611111161111111111111111111",
        "61111111999999999999999999999999996666666666666666666666666161111111111111111111",
        "61111111999999999999996666666666666666666666666116661111111111111111111111111111",
        "61111111999999999999999999999999999999666666666666666666666666661111111111111111",
        "61111111999999666666666666666666666666666666666666666666666666666666666666666666",
        "66666666666666666666666666666666666666666666666666666666666666666666666666666666",
        "66666666666666666666666666666666666666666666666666666666666666666666666666666666",
        "66666666666666666666666666666666669999999999999999999999999999999999999999111111",
        "11111111999999999999888888886666699999999999999999999999999999999999999999111111",
        "11111111999999999888888888886666688899888998889999999999999999999999999999111111",
        "11111111999999999999888888886666699999999999999999999999999999999999999999111111",
        "11111111999999999999999999988888888666669999999999999999999999999999999999111111",
        "11111111999999999999998888888866666999999999999999999999999999999999999999111111",
        "11111111999999999999999999999999888888886666699999999999999999999999999999111111",
        "11111111999999999999999888888886666699999999866666888998889999999999999999111111",
        "11111111999999999999999999999888888886666686666688899888996999999999999999111111",
        "11111111999999999999999999988888888666669992222222222222222222222222999999111111",
        "11111111999999999999999999999999999999866666888228882222222222222222999999111111",
        "11111111999999999999999999999999866666888998882222222222222222222222999999111111",
        "11111111999999999999999999999999999986666688888888666662222222222222999999111111",
        "111111119999999999999999999999988888666661122222222245c2222666626222111111111111",
        "11111111999999999999999998888866666888886666622222255552222222666622111111111111",
        "11111111992222222222222222299999888886666612222222255552222226666221111111131111",
        "11111111112222222222222222211111111118888866666222255555222222662622111111111111",
        "11111111112222222222222222211111111888886666622222222222222222622662111111111111",
        "11111111112222222222222222211111888886666612222222222222222226662222111111111111",
        "11111111112222222222222222211111111188888666662222222222222222666222111111111111",
        "11111111112222222222222222211188888666661112222222222222222222262222111111111111",
        "11111111112222222222222222211111111188888666662222222222222226666622111111111111",
        "11111111112222222222222222211111888888886666622222222222222222222222111111111111",
        "11111111112222222222222222211111888888886666622222222222222222222222111111111111",
        "11111111112222222222222222211111888888886666622222222222222222222222999999999999",
        "11111111112222222222222222211111888888886666622222222222222222222222999999999999",
        "11111111112222222222222222211111888888886666611111111111111999999999999999999999",
        "11111111112222222222222222211111888888886666611111111111111999999999999999999999",
        "11111111111111111111111111111111888888886666611111111111111999999999999999999999",
        "11111111111111111111111111111111888888886666611111111111111999999999999999999999",
        "11111111111111122222222222222222888888886666622222222211111999999999999999999999",
        "11111111111111122222222222222222888888886666622222222211111999999999999999999999",
        "11111111111111122222222228888866666888886666622222222211111999666699999999999999",
        "11111111111111122222222228888866666888886666622222222211111999666699999999999999",
        "11111111111111122222222228888866666888886666622222222211111999666699999999999999",
        "11111111111111122222222228888866666888886666622222222211111999666699999999999999",
        "11111111111111122222222228888866666888886666622222222211111999666699999999999999",
        "11111111111111122222222228888866666888886666622222222211111999666699999999999999",
        "11111111111111122222222222222222222228888866666222222211111999669699999999999999",
    };

    // Use this for initialization
    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        GenTerrain();
        BuildMesh();
        UpdateMesh();
    }

    private void Update()
    {
    }

    private void GenTerrain()
    {
        blocks = new byte[terrainSize, terrainSize];

        int row = 0;
        for (int i = tileMap.Length - 1; i >= 0; i--)
        {
            string t = tileMap[i];
            int col = 0;
            foreach (char c in t)
            {
                blocks[col, row] = System.Convert.ToByte("" + c, 16);
                col += 1;
            }
            row += 1;
        }
    }

    private void BuildMesh()
    {
        for (int px = 0; px < blocks.GetLength(0); px++)
        {
            for (int py = 0; py < blocks.GetLength(1); py++)
            {
                GenSquare(px, py, tiles[blocks[px, py]]);
            }
        }
    }

    private void GenSquare(int x, int y, Vector2 texture)
    {
        newVertices.Add(new Vector3(x, y, 0));
        newVertices.Add(new Vector3(x + 1, y, 0));
        newVertices.Add(new Vector3(x + 1, y - 1, 0));
        newVertices.Add(new Vector3(x, y - 1, 0));

        newTriangles.Add(squareCount * 4);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 3);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 2);
        newTriangles.Add((squareCount * 4) + 3);

        newUV.Add(new Vector2(tUnit * texture.x, tUnit * texture.y + tUnit));
        newUV.Add(new Vector2(tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
        newUV.Add(new Vector2(tUnit * texture.x + tUnit, tUnit * texture.y));
        newUV.Add(new Vector2(tUnit * texture.x, tUnit * texture.y));

        squareCount++;
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        newVertices.Clear();
        newTriangles.Clear();
        newUV.Clear();
        squareCount = 0;
    }
}