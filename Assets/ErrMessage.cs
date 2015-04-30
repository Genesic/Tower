using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using DaikonForge.Tween;
using DaikonForge.Tween.Interpolation;


public class ErrMessage : MonoBehaviour {

	public Text useText;
	private Tween<float> textAlpha;

	void Awake (){
		textAlpha = new Tween<float> ();
		textAlpha.SetDuration (2.0F);
		textAlpha.SetStartValue (1.0F);
		textAlpha.SetEndValue( 0.0F );
		textAlpha.OnExecute ((float alpha) => { useText.color = new Color(1,1,1,alpha); });
		textAlpha.OnCompleted ( (TweenBase sender) => {useText.gameObject.SetActive(false); } );
	}

	// Use this for initialization
	public void show_message ( string content) {
		useText.text = content;
		useText.gameObject.SetActive (true);
		textAlpha.Stop ();
		textAlpha.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
