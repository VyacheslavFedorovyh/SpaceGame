using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodScript : MonoBehaviour
{
	public GameObject asteroidExplosion;
	public GameObject playerExplosion;

	float speed;
	float xSpeed;

	public float xSpread; // 0..1
	public float minSpeed, maxSpeed;
	public float rotationSpeed;

	Rigidbody asteroid;

	void Start()
	{
		asteroid = GetComponent<Rigidbody>();
		asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;

		speed = Random.Range(minSpeed, maxSpeed);
		xSpeed = speed * Random.Range(-xSpread, xSpread);

		asteroid.velocity = new Vector3(xSpeed, 0, -speed);
		asteroid.transform.localScale *= Random.Range(1f, 2.2f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Asteroid")
			return;

		if (other.tag == "GameBoundary")
		{
			if (GameController.score > 100)
				GameController.score -= 5;

			return;
		}

		if (other.tag == "LaserShot")
			GameController.score += 50;

		if (other.tag == "Player" || other.tag == "Enemy")
			Instantiate(playerExplosion, other.transform.position, Quaternion.identity);

		Instantiate(asteroidExplosion, transform.position, Quaternion.identity);

		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
