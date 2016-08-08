﻿using UnityEngine;
using System.Collections;

public class estudo : MonoBehaviour {

	//Criei um componente do tipo Animator
	public Animator anim;
	private bool 	walk; //Para acessar o parâmentro walk do componente Walk do Animator na Unity
	private float	horizontal; // -1 a 1s
	private float 	velocidadeDoRigid = 2; //velocidade em que o personagem pode andar

	//Criar um objeto do tipo RigidBody para poder manipular o rigidBody do nosso personagem
	public Rigidbody2D playerRigidBody;


	//booleana para saber pra onde o personagem está olhando
	public bool olhandoDireita = true; //se estiver olhando para direita é true, cc false.

	public Transform trasnform;


	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		changeAnimation ();
		controlarCorpoRigido ();

	}



	public void controlarCorpoRigido(){
		//Quando na descrição de um componente aparecer : get;set, voce utiliza o =;
		this.playerRigidBody.velocity = new Vector2(horizontal * velocidadeDoRigid,0); // x, y (x para horizontal, y para vertical)
	}



	public void changeAnimation(){
		//Verificar se nosso personagem está andando pra esq ou dir ou parado
		horizontal = Input.GetAxisRaw("Horizontal"); //Pego o axes de nome Horizontal.

		if (horizontal == 0) {
			walk = false;
		} else if (horizontal < 0 && olhandoDireita) {

			virarPersonagem ();
			walk = true;
		} else if (horizontal > 0 && !olhandoDireita) {
			virarPersonagem ();
			walk = true;
		} else if (horizontal > 0) {
			walk = true;
		}
		else if (horizontal < 0) {
			walk = true;
		}

		// para que o walk do script jogue o valor no walk do parametro no anmator
		anim.SetBool("walk", walk);
	}

	//Função para virar o personagem.
	public void virarPersonagem(){

		//Quando esta função for chamada eu tenho que inverter o valor do booleano(olhandoDireita)

		olhandoDireita = !olhandoDireita;
		//Criamos um vecto3 para pegar os 3 componentes do transform (scale x,y e z)
		Vector3 theScale = transform.localScale;
		//agora, nosso scale na posĩção x é negativado, para ele inverter e virar o personagem!
		theScale.x = theScale.x * -1; 
		transform.localScale = theScale; //depois atribuimos o valor dele ao valor do nosso transform!
	}
}
