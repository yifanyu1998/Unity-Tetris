using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallSpeed = 1;

    public bool allowRotation = true;
    public bool limitRotation = false;
    private bool shouldStop = false;
    private Game game;
    private void Awake()
    {
        game = FindObjectOfType<Game>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger collision");
        shouldStop = true;
    }

    void CheckUserInput()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 move = new Vector3(1, 0, 0);
            if (IsValidPosition(move)) {  
                transform.position += move;
                game.UpdateGrid(this);
            }


        }

        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 move = new Vector3(-1, 0, 0);
            if (IsValidPosition(move)) {  
                transform.position += move;
                game.UpdateGrid(this);

            }

        }

        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (allowRotation)
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);

                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);

                }

                Debug.Log(transform.rotation);
                if (!IsValidPositionNoMove())
                {
                    if (limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);

                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0,-90);

                    }


                }
                else
                { 
                    Debug.Log("it is valid");
                }
                game.UpdateGrid(this);


            }

        }

        else if((Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed) && !shouldStop)
        {

            Vector3 move = new Vector3(0, -1, 0);
            if (IsValidPosition(move)) {  
                transform.position += move;
                game.UpdateGrid(this);

            }
            else
            {
                enabled = false;
                game.DeleteFullRows();
                game.NextNode();
            }

            fall = Time.time;
        }
    }
    bool IsValidPosition(Vector3 posMove )
    {
        foreach(Transform m in transform)
        { 
            Vector2 pos  = game.Round(m.position+posMove);
           
            if(!game.IsInGrid(pos))
            { 
                return false;
            }
            if (game.GetTransform(pos) != null && game.GetTransform(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
    bool IsValidPositionNoMove()
    {
        foreach (Transform m in transform)
        {
            Vector2 pos = game.Round(m.position);
             
            if (!game.IsInGrid(pos))
            {
                return false;
            }
            if(game.GetTransform(pos) != null && game.GetTransform(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
