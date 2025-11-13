using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] public static List<Transform> pathPoints;
    [SerializeField] private List<Transform> pointsInInspector;

    private void Awake()
    {
        pathPoints = new List<Transform>(pointsInInspector);
        
    }


}
