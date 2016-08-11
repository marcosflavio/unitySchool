using UnityEngine;
using System.Collections;

public class armadilha : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D coll){

		//Estou criando um objeto prefab temporario
		// ele recebe o meu objeto pregab com o método Instantiate
		//Instantiate instancia o objeto passado por parâmetro

		if (coll.gameObject.tag.Equals ("Player")) {
			GameObject tempPrefab = Instantiate (prefab) as GameObject;
		}




	}
}
