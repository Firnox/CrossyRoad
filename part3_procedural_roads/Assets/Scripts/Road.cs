using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
  [SerializeField] private List<Rigidbody> vehicles;

  private int direction = 1;
  private float speed = 1f;
  private List<Rigidbody> spawnedVehicles = new();

  public HashSet<int> Init(float z) {
    // Place the obstacle at the location provided.
    transform.position = new Vector3(0, 0, z);

    // Choose which direction the vehicles go, -1 or +1.
    direction = 2 * Random.Range(0, 2) - 1;

    // Choose the speed, we make them faster as we progress.
    float minSpeed = Mathf.Lerp(1f, 5f, z / 500f);
    float maxSpeed = Mathf.Lerp(5f, 10f, z / 500f);
    speed = Random.Range(minSpeed, maxSpeed);

    // Choose which vehicle, how many, and how far apart they are.
    int idx = Random.Range(0, vehicles.Count);
    int vehicleCount = Random.Range(1, 5);
    float gap = Random.Range(2f, 6f);

    // Instantiate the vehicles.
    for (int i = 0; i < vehicleCount; i++) {
      Rigidbody vehicle = Instantiate(
        vehicles[idx], 
        new Vector3((i * gap) * -direction, 0.1f, z), 
        Quaternion.Euler(0f, 90 * direction, 0f), 
        transform
      );
      spawnedVehicles.Add(vehicle);
    }

    // The only obstacles are those outside the game area.
    return new() { -6, 6 };
  }

  private void FixedUpdate() {
    // Move vehicles
    foreach (Rigidbody vehicle in spawnedVehicles) {
      // Move along the road, us the RB movement so collisions are handled correctly.
      Vector3 moveAmount = new(speed * direction * Time.fixedDeltaTime, 0f, 0f);
      vehicle.MovePosition(vehicle.position + moveAmount);

      // Wrap around when they are off camera
      Vector3 pos = vehicle.position;
      if ((direction > 0) && (pos.x > 12)) {
        pos.x = -12;
        vehicle.position = pos;
      } else if ((direction < 0) && (pos.x < -12)) {
        pos.x = 12;
        vehicle.position = pos;
      }
    }
  }
}

