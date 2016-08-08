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
	public bool olhandoDireita; //se estiver olhando para direita é true, cc false.

	//Objeto Transform para manipularmos no nosso Personagem
	public Transform trasnform;

	//Variável para aplicar uma força no salto do nosso personagem.
	private float forcaPulo = 200;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		changeAnimation ();
		controlarCorpoRigido ();
		pular ();

	}



	public void controlarCorpoRigido(){
		//Quando na descrição de um componente aparecer : get;set, voce utiliza o =;
		this.playerRigidBody.velocity = new Vector2(horizontal * velocidadeDoRigid, playerRigidBody.velocity.y); // x, y (x para horizontal, y para vertical)
		//playerRigidBody.velocity.y ou seja, manterá a velocidade do y.
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


	//Função para o personagem pular
	public void pular(){

		//GetButtonDown significa que eu apertei um determinado botão
		//GetButtonUp significa que eu soltei um determinado botão
		//GetButtonDown significa que eu estou pressionando ou soltando um determinado botão

		if (Input.GetButtonDown("Jump")) {

			playerRigidBody.AddForce (new Vector2 (0, forcaPulo));
		}

	}



	/**
			Funções para Triggers, seis ao total.
			//Funções de controle para colisores e para triggers.
	*/

	//Função responsável por detectar quando um objeto entrar em colisão com um colisor
	public void OnCollisionEnter2D(){

	//	print ("Entrei em colisão");

	}

	//Função responsável por detectar quando um objeto entrar em colisão com um trigger
	public void OnTriggerEnter2D(){
	//	print ("Entrei em colisão com um trigger");
	}

	//Função responsável por detectar quando um objeto sai de uma colisão com um colisor
	public void OnCollisionExit2D(){
	//	print ("Saí de uma colisão com um colisor");
	}

	//Função responsável por detectar quando um objeto sai de uma colisão com um trigger
	public void OnTriggerExit2D(){
	//	print ("Saí de uma colisão com um trigger");
	}

	//Função responsável por detectar se o objeto ainda está em colisão com um colisor.
	public void OnCollisionStay2D(){
	//	print ("Estou colidindo com um colisor");
	}

	//Função responsável por detectar se o objeto ainda está em colisão com um trigger.
	public void OnTriggerStay2D(){
	//	print ("Estou colidindo com um trigger");
	}
}
