using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _maxSplitChance = 100f;
    private float _minSplitChance = 1.0f;
    private float _currentSplitChance;

    private int _chanceDivider = 2;
    private int _generation;

    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public int Generation => _generation;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
        _currentSplitChance = _maxSplitChance;
    }

    public bool CanSplit() =>
        Random.Range(_minSplitChance, _maxSplitChance) <= _currentSplitChance;


    public void ReduceSplitChance(int generation)
    {
        _generation = generation;
        _currentSplitChance = _maxSplitChance / Mathf.Pow(_chanceDivider, _generation);
    }

    public void IncreaseGeneration() =>
        _generation++;
}