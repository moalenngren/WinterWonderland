using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition()
    {
        startPos.position = new Vector3(100, 0, 0);
    }


    //Get start Pos Public
    public Vector3 GetStartPos()
    {
        return startPos.position;
    }

    public Vector3 GetEndPos()
    {
        return endPos.position;
    }

    //Set start Pos Public
    public void SetStartPos(Vector3 pos)
    {
        startPos.position = pos;
    }

    public void SetEndPos(Vector3 pos)
    {
        endPos.position = pos;
    }
}
