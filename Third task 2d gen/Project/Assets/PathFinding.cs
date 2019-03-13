using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public List<int> moveList;


    private void Start()
    {
    }

    public void PathFindingCustom(int start,int end)
    {

        while (start != end)
        {
            if ((start / 5) < (end / 5))
            {
                start += 5;
            }
            else if ((start / 5) > (end / 5))
            {
                start -= 5;
            }
            else
            {
                if (start % 5 < end % 5)
                {
                    start++;
                }
                else if (start % 5 > end % 5)
                {
                    start--;
                }
            }

            Debug.Log(start);
            moveList.Add(start);
                

        }
    }
}
