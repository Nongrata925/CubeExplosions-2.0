using System;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    
    public event Action<Cube> CubeDivided;
    public event Action<Cube> CubeDestroyed;

    private void OnEnable()
    {
        _inputReader.CubeClicked += HandleCube;
    }

    private void OnDisable()
    {
        _inputReader.CubeClicked -= HandleCube;
    }

    private void HandleCube(Cube cube)
    {
        if (cube.CanSplit())
        {
            cube.IncreaseGeneration();
            CubeDivided?.Invoke(cube);
        }
        else
        {
            CubeDestroyed?.Invoke(cube);
            Destroy(cube.gameObject);
        }
    }
}
