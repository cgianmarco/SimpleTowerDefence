using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    public event Action<Unit> OnReachedDestination;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }






    IEnumerator FollowPath()
    {


        List<Tile> path = Environment.instance.GetComponent<PathManager>().path;
        transform.position = path[0].transform.position;


        for (int i = 1; i < path.Count; i++)
        {

            Tile start = path[i - 1];
            Tile end = path[i];


            Vector3 startPosition = start.transform.position;
            Vector3 endPosition = end.transform.position;


            float travelPercent = 0f;

            transform.LookAt(endPosition);


            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }


        }

        OnReachedDestination?.Invoke(GetComponent<Unit>());


        Destroy(gameObject);


    }
}
