using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.Interaction.PhysicsHands.Example
{
    public class CubeSpawner : MonoBehaviour
    {
        private List<GameObject> _cubes = new List<GameObject>();
        public GameObject block;

        public void SpawnCube1()
        {
            GameObject cube = Instantiate(block);
            cube.transform.SetParent(transform);
            cube.AddComponent<Rigidbody>();
            _cubes.Add(cube);
        }

        public void DestroyCubes()
        {
            foreach (var cube in _cubes)
            {
                Destroy(cube);
            }
            _cubes.Clear();
        }
    }
}