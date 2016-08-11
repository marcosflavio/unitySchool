using UnityEngine;
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

	//Variável para saber se o personagem está pisando no chao ou não.
	public bool pisandoNoChao; 

	//Objeto Transform para manipularmos o filho groundCheck do nosso Personagem
	public Transform 	groundCheck;

	//Objeto para saber se nosso personagem pode pular em uma determinada área ou não.
	//permite selecionar layers
	public LayerMask whatIsGround;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		changeAnimation ();
		controlarCorpoRigido ();
		pular ();
		pisar2 ();

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

		//saber a velocidade de queda
		print(playerRigidBody.velocity.y);

		//Enviar o valor da velocidade de queda e de salto do personagem para o parametro no animator.
		anim.SetFloat("velocidadeY",playerRigidBody.velocity.y);


		// para que o walk do script jogue o valor no walk do parametro no anmator
		anim.SetBool("walk", walk);

		// para que o pisandoNoChao do script jogue o valor no pisandoChao do parametro no animator
		anim.SetBool ("pisandoChao", pisandoNoChao);
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

		if (pisandoNoChao && Input.GetButtonDown("Jump")) {

			playerRigidBody.AddForce (new Vector2 (0, forcaPulo));
		}

	}



	/**
			Funções para Triggers, seis ao total.
			//Funções de controle para colisores e para triggers.
	*/

	//Função responsável por detectar quando um objeto entrar em colisão com um colisor
	//Colisão usa Collision2D
	public void OnCollisionEnter2D(Collision2D collision){

	//	if(collision.gameObject.tag.Equals("quadrado")){
	//		print ("Me choquei com um quadrado");
	//	}

	//	print ("Entrei em colisão");

	}

	//Função responsável por detectar quando um objeto entrar em colisão com um trigger
	//Colisão usa Collider2D
	public void OnTriggerEnter2D(Collider2D collider){

	//	if(collider.gameObject.tag.Equals("gatilho")){
	//		print ("Me choquei com um gatilho");
	//	}

	//	print ("Entrei em colisão com um trigger");
	}

	//Função responsável por detectar quando um objeto sai de uma colisão com um colisor
	public void OnCollisionExit2D(Collision2D collision){
	//	print ("Saí de uma colisão com um colisor");
	//	if(collision.gameObject.tag.Equals("quadrado")){
	//		print ("Saí de uma colisão com um quadrado");
	//	}else if(collision.gameObject.tag.Equals("chao")){
	//		pisandoNoChao = false;
	//		}

	}

	//Função responsável por detectar quando um objeto sai de uma colisão com um trigger
	public void OnTriggerExit2D(Collider2D collider){
	//	print ("Saí de uma colisão com um trigger");
	//	if(collider.gameObject.tag.Equals("gatilho")){
	//		print ("Saí da colisão com um gatilho");
	//	}
	}

	//Função responsável por detectar se o objeto ainda está em colisão com um colisor.
	public void OnCollisionStay2D(Collision2D collision){
	//	print ("Estou colidindo com um colisor");

	//	if(collision.gameObject.tag.Equals("quadrado")){
	//		print ("Estou me chocando com um quadrado");
	//	}
	//	else if(collision.gameObject.tag.Equals("chao")){
	//		pisandoNoChao = true;
	//	}
	}

	//Função responsável por detectar se o objeto ainda está em colisão com um trigger.
	public void OnTriggerStay2D(Collider2D collider){
	//	print ("Estou colidindo com um trigger");
	//	if(collider.gameObject.tag.Equals("gatilho")){
	//		print ("Estou me chocando com um gatilho");
	//	}
	}

	//Função responsável por criar um OverlapCircle para que possamos saber
	//se está ocorrendo um contato ao chão ou não, utilizando o transform do objeto filho(groundCheck)
	//para isso, pois ele servirá somente para isso

	//Overlap é como se fosse um colisor, mas não colide, só detecta se há algo ou não;
	//É passado um vector2 para pegar a posição da criação do overlap e um radius, que é
	//o tamanho desse overlapcircle

	public void pisar(){

		//O método Physics2D.OverlapCircle recebem um vector2
		//pra isso, pegamos o transform do filho(groundChek.position) que retorna
		//um vector3 com a posição do objeto e um radius, que será o tamanho do circulo
		//esse radius deve ser pequeno!!
		pisandoNoChao = Physics2D.OverlapCircle (groundCheck.position, 0.02f);
	}


	public void pisar2(){

		//O método Physics2D.OverlapCircle recebem um vector2
		//pra isso, pegamos o transform do filho(groundChek.position) que retorna
		//um vector3 com a posição do objeto e um radius, que será o tamanho do circulo
		//esse radius deve ser pequeno!!
		//Passo um LayerMask e com isso, só vou detectar as colisões dos layers marcados
		//Pisando na lava seria:
		//pisandoNaLava = Physics2D.OverlapCircle (groundCheck.position, 0.02f,whatIsMagma);
		pisandoNoChao = Physics2D.OverlapCircle (groundCheck.position, 0.02f,whatIsGround);

	}
}
