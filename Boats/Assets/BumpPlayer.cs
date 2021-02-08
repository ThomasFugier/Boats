using UnityEngine;
using System.Collections;

public class BumpPlayer : MonoBehaviour
{
	private GameObject obj;
	private Rigidbody rigidbodyComp;
	private float time = 0;
	private Transform trans;
	[SerializeField]
	private float MinCoef = 2.0f;
	[SerializeField]
	private float MinValue = 3;
	[SerializeField]
	private float bumpMinimal;
	[SerializeField]
	private float bumpMax;
	[SerializeField]
	private float facteurSuperior;
	[SerializeField]
	private float facteurInferior;
	[SerializeField]
	private float facteurSuperiorMax;
	[SerializeField]
	private float facteurInferiorMax;
	[SerializeField]
	[Tooltip("Coefficient of propulsion per of character propulsion force")]
	private float facteurEgg;
	[SerializeField]
	private float FacteurMinEgg;
	[SerializeField]
	private float Timer = 0.5f;

	void Start()
	{
		rigidbodyComp = GetComponent<Rigidbody>();
		trans = GetComponent<Transform>();
		obj = gameObject;
	}

	void Update()
	{
		if (obj == this.gameObject)
			return;

		time += Time.deltaTime;

		if (time > Timer)
			obj = gameObject;
	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
        {
			Vector3 direction = collision.transform.position - trans.position;
			direction.Normalize();

			Debug.Log("Je vais te pousser vers " + direction);
			Rigidbody rigidbodyCompCol = collision.gameObject.transform.parent.GetComponent<Rigidbody>();

			float me = rigidbodyComp.velocity.magnitude < MinCoef ? MinCoef : rigidbodyComp.velocity.magnitude;
			float other = rigidbodyCompCol.velocity.magnitude < MinCoef ? MinCoef : rigidbodyCompCol.velocity.magnitude;


			float coefMagnitude = me - other;
			Debug.Log("mon coef de magnitude apres reduction " + coefMagnitude);

			Vector3 forceCharacter = direction * rigidbodyComp.velocity.magnitude;
			Vector3 forceCharacterOther = -direction * rigidbodyCompCol.velocity.magnitude;
			Debug.Log("Magnitude de la force dans laquelle je vais te pousser " + forceCharacter.magnitude);


			float forceCharsup;
			float forceCharinf;

			if (forceCharacter.magnitude < MinValue)
			{
				forceCharacter = direction * bumpMinimal;
				forceCharsup = facteurSuperior;
				forceCharinf = facteurInferior;
			}
			else
			{
				forceCharacter = direction * bumpMax;
				forceCharsup = facteurSuperiorMax;
				forceCharinf = facteurInferiorMax;
			}

			rigidbodyComp.AddForce(Vector3.zero, ForceMode.VelocityChange);

			if (coefMagnitude > 0)
            {
				rigidbodyCompCol.AddForce(forceCharacter * forceCharsup, ForceMode.Impulse);
				Debug.Log("ici");
			}
				
			else
            {
				rigidbodyCompCol.AddForce(forceCharacter * forceCharinf, ForceMode.Impulse);
				Debug.Log("ici2");
			}
				


			obj = collision.gameObject;
			time = 0;
		}
	
		
	}
}
