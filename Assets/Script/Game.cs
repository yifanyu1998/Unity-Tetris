using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
 
    // Start is called before the first frame update
    void Start()
    {
        NextNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool IsFullAtRow(int y)
    {
        for(int x=0; x < gridWidth; x++)
        {
            if(grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    private void DeleteRow(int y)
    {
        for(int x =0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    private void MoveRowDown(int y)
    {
        for(int x = 0; x < gridWidth; x++)
        {
            if(grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }
    }
    public void MoveAllRowDown(int y)
    {
        for(int i = y; i < gridHeight; i++)
        {
            MoveRowDown(i);
        }
    }
    public void DeleteFullRows()
    {
        for(int y = 0; y < gridHeight; y++)
        {
            if (IsFullAtRow(y))
            {
                DeleteRow(y);
                MoveAllRowDown(y + 1);
                --y;
            }
        }
    }
    public void UpdateGrid(Tetromino tetronimo)
    {
        for (int y=0; y < gridHeight; y++)
        {
            for (int x=0; x < gridWidth; x++)
            {
                if(grid[x, y] != null && grid[x, y].parent == tetronimo.transform)
                {
                    grid[x, y] = null;
                }
            }
        }
        foreach(Transform t in tetronimo.transform)
        {
            Vector2 pos = Round(t.position);
            if(pos.y < gridHeight)
            {
                grid[(int)(pos.x), (int)(pos.y)] = t;
            }
        }

    }
    public Transform GetTransform(Vector2 pos)
    {
        if(pos.y >= gridHeight)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
    public void NextNode()
    {
        GameObject next = (GameObject)Instantiate(Resources.Load(RandomNode(),
        typeof(GameObject)), new Vector2((float)(gridWidth / 2.0), (float)gridHeight), Quaternion.identity);
    }

    public bool IsInGrid(Vector2 pos)
    {
         return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0 );
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }
    string RandomNode()
    {
        int randomInt = Random.Range(1, 8);
        string tetroName = "Tetromino_T";
        switch (randomInt)
        {
            case 1:
                tetroName = "Tetromino_T";
                break;
            case 2:
                tetroName = "Tetromino_Long";
                break;
            case 3:
                tetroName = "Tetromino_Square";
                break;
            case 4:
                tetroName = "Tetromino_J";
                break;
            case 5:
                tetroName = "Tetromino_L";
                break;
            case 6:
                tetroName = "Tetromino_S";
                break;
            case 7:
                tetroName = "Tetromino_Z";
                break;
        }
          return "Prefab/"+ tetroName;
    }

}
