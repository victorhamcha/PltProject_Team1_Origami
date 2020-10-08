
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataSheetDialogue{
	//Document URL: https://spreadsheets.google.com/feeds/worksheets/1uVac3YcyRJlkmTot-pWZkQiF85cwPysqaLaUMa8Q7e0/public/basic?alt=json-in-script

	//Sheet SheetFeuille1
	public static DataSheetDialogueTypes.SheetFeuille1 feuille1 = new DataSheetDialogueTypes.SheetFeuille1();
	static DataSheetDialogue(){
		feuille1.Init(); 
	}
}


namespace DataSheetDialogueTypes{
	public class Feuille1{
		public string name;
		public string sentence;
		public string color;

		public Feuille1(){}

		public Feuille1(string name, string sentence, string color){
			this.name = name;
			this.sentence = sentence;
			this.color = color;
		}
	}
	public class SheetFeuille1: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,10,7,23,0,30);
		public readonly string[] labels = new string[]{"Name","Sentence","Color"};
		private Feuille1[] _rows = new Feuille1[3];
		public void Init() {
			_rows = new Feuille1[]{
					new Feuille1("Richard","Va te faire voire","Bleu"),
					new Feuille1("Marie","J'aime les fraises","Rouge"),
					new Feuille1("Paul-Antoine","Vous êtes ou ouesh ?","")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetFeuille1 t;
			public SheetEnumerator(SheetFeuille1 t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public Feuille1 this[int index]{
			get{
				return _rows[index];
			}
		}
		public Feuille1 this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].name == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].name == key){ return true; }
			}
			return false;
		}
		public Feuille1[] ToArray(){
			return _rows;
		}
		public Feuille1 Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Feuille1 richard{	get{ return _rows[0]; } }
		public Feuille1 marie{	get{ return _rows[1]; } }
		public Feuille1 paulAntoine{	get{ return _rows[2]; } }

	}
}