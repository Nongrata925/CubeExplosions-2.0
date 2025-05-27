using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeHandler _cubeHandler;
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    private void OnEnable()
    {
        _spawnerCubes.CubeSpawned += PushNewCubes;
        _cubeHandler.CubeDestroyed += Explode;
    }

    private void OnDisable()
    {
        _spawnerCubes.CubeSpawned -= PushNewCubes;
        _cubeHandler.CubeDestroyed -= Explode;
    }
    
    private void PushNewCubes(Cube[] cubes, Cube parent)
    {
        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(_force, parent.transform.position, _radius);
            cube.ReduceSplitChance(parent.Generation);
        }
    }

    private void Explode(Cube parent)
    {
        float sizeDivider = 1f;
        float force = 20f;
        float radius = 4f;
        
        float forceMultiplier = 2f;
        float radiusMultiplier = 1.5f;
        
        float sizeFactor = sizeDivider / parent.transform.localScale.x;
        float explosionForce = force * sizeFactor * forceMultiplier;
        float explosionRadius = radius * sizeFactor * radiusMultiplier;
        
        Collider[] explodableObjects = Physics.OverlapSphere(parent.transform.position, explosionRadius);

        foreach (Collider explodableObject in explodableObjects)
        {
            if (explodableObject.TryGetComponent<Cube>(out Cube cube))
            {
                cube.Rigidbody.AddExplosionForce(explosionForce, parent.transform.position, explosionRadius);
            }
        }
    }
}