using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameHandler : MonoBehaviour {

	
	private FadeEffect _gameOverEffect;
	private FadeEffect _blackPanelEffect;
	private bool _gameover = false;


	private static float _score = 0;

	public int ScorePerSecond = 50;
	public Text ScoreText;
	public Text SecondScoreText;
	public GameObject TextDecor;

	public float Score{get{return _score;}}

	// Use this for initialization
	void Awake () 
	{
	
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Sailorman"), LayerMask.NameToLayer("WaterInteract"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("Sailorman"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("InteractiveObj"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("WaterInteract"), true);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("Monsters"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("InteractiveObj"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("WaterInteract"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Markers"), LayerMask.NameToLayer("InteractiveObj"), true); 

		_gameOverEffect = GameObject.Find("/InGameUI/GameOverPanel").GetComponent<FadeEffect>();
		_blackPanelEffect = GameObject.Find("/InGameUI/BlackEndPanel").GetComponent<FadeEffect>();
		_score = 0;

	}

	public static void AddScore(int amount){
		_score += amount;
	}

	void Update()
	{


		if (UserInput.Exit()){
			Application.Quit();
		}

		if(Ship.instance.CurrentHp == 0 && !_gameover){
			setGameOver();
			return;
		}
		else if(_gameover){
			if(Input.GetKeyDown(KeyCode.Return))
				Application.LoadLevel("StartScreen");
			return;
		}


	}

	private void setGameOver(){
		_gameover = true;
		Monster[] monsters = Transform.FindObjectsOfType<Monster>();
		for(int i=0; i<monsters.Length; i++)
			monsters[i].StopAttacking();

		FlyingLion	._headCount = 0; //HACK!!!!!
		LizardMonster._headCount = 0;

//		float loadAfter = _gameOverEffect.FadeTime + _gameOverEffect.Delay + _blackPanelEffect.FadeTime + _blackPanelEffect.Delay;
		Invoke("ToTitleScreen",13);
		SecondScoreText.text = ScoreText.text;
		//TextDecor.SetActive(false);

		_gameOverEffect.Fade();
		_blackPanelEffect.Fade();

		AudioSource audio = GameObject.FindWithTag("SoundTrack").GetComponent<AudioSource>();
		audio.Stop();
	}

	private void ToTitleScreen(){
		Application.LoadLevel("StartScreen");
	}
}
