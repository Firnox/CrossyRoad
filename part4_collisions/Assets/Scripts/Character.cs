using UnityEngine;

public class Character : MonoBehaviour {
  [SerializeField] private GameManager gameManager;
  [SerializeField] private GameObject character;

  private void OnCollisionEnter(Collision collision) {
    // Only collide with vehicles if we're not already done so.
    if (collision.gameObject.CompareTag("Vehicle") && character.activeSelf) {
      // Hide the character model
      character.SetActive(false);

      // Tell the GameManager we've collided.
      gameManager.PlayerCollision();
    }
  }

  public void Reset() {
    // Re-enable the character model.
    character.SetActive(true);
  }

}

