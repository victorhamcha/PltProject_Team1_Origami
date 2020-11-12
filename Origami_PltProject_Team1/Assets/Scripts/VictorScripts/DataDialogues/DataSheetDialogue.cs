
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
		public string eventName;
		public int iD;
		public string name;
		public string sentence;

		public Feuille1(){}

		public Feuille1(string eventName, int iD, string name, string sentence){
			this.eventName = eventName;
			this.iD = iD;
			this.name = name;
			this.sentence = sentence;
		}
	}
	public class SheetFeuille1: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,11,10,17,17,20);
		public readonly string[] labels = new string[]{"EventName","iD","Name","Sentence"};
		private Feuille1[] _rows = new Feuille1[21];
		public void Init() {
			_rows = new Feuille1[]{
					new Feuille1("Fishman_FirstMeet",0,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_FirstTalk_01>Hmm...Hmm..."),
					new Feuille1("Fishman_FirstMeet",1,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_FirstTalk_02>Oh ! Forgive me. I was lost in thought. My name is Robert. I'm the fisherman of this little village! Nice to meet you!"),
					new Feuille1("Fishman_FirstMeet",2,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_FirstTalk_03>To be honest with you, things aren't going strong lately. You see my boat over there? The thing won't move anymore..."),
					new Feuille1("Fishman_FirstMeet",3,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_FirstTalk_04>No matter how hard I try or how long I wait. So I just sit here, with my telescope and my bottle."),
					new Feuille1("Fishman_FirstMeet",4,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_SecTalk_01>Hmm...Hmm..."),
					new Feuille1("Fishman_FirstMeet",5,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_SecTalk_02>Still not moving..."),
					new Feuille1("Fishman_FirstMeet",6,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_SecTalk_03>How will I get fish for the village..."),
					new Feuille1("Fishman_FirstMeet",7,"Robert Fisher","<SoundManager:VOC_Fishman_FirstMeet_ThirdTalk_01>Hmm..."),
					new Feuille1("Fishman_FirstBoat",8,"Robert Fisher","<SoundManager:VOC_Fishman_FirstBoat_First_01>Hmm..."),
					new Feuille1("Fishman_FirstBoat",9,"Robert Fisher","<SoundManager:VOC_Fishman_FirstBoat_First_02>Oh! Wait!... You repaired the boat!"),
					new Feuille1("Fishman_FirstBoat",10,"Robert Fisher","<SoundManager:VOC_Fishman_FirstBoat_First_03>I don't know how you did it but thanks to you the village is going to receive fish again!"),
					new Feuille1("Fishman_FirstBoat",11,"Robert Fisher","<SoundManager:VOC_Fishman_FirstBoat_First_04>As a token of my gratitude you can use the boat as you like!"),
					new Feuille1("Fishman_FirstBoat",12,"Robert Fisher","<SoundManager:VOC_Fishman_FirstBoat_First_05>You can go visit the neighboring island if you want. I heard it is really beautiful this time of year."),
					new Feuille1("Fishman_FirstBoat",13,"Robert Fisher","<SoundManager:VOC_Fishman_FirstBoat_Second_01>Perhaps I can go pay a visit to that old hermit now..."),
					new Feuille1("Fishman_FirstBoat",14,"Robert Fisher","<SoundManager:VOC_Fishman_FirstBoat_Second_02>Finally tell her how I feel..."),
					new Feuille1("Fishman_AfterHermit",15,"Robert Fisher","<SoundManager:VOC_Fishman_AfterHermit_01>Hmm...Hmm..."),
					new Feuille1("Fishman_AfterHermit",16,"Robert Fisher","<SoundManager:VOC_Fishman_AfterHermit_02>Oh! You're back! Did you check on that Island? Did she talk about me?"),
					new Feuille1("Fishman_AfterHermit",17,"Robert Fisher","..."),
					new Feuille1("Fishman_AfterHermit",18,"Robert Fisher","<SoundManager:VOC_Fishman_AfterHermit_03>She did?... Well..."),
					new Feuille1("Fishman_AfterHermit",19,"Robert Fisher","<SoundManager:VOC_Fishman_AfterHermit_04>Thanks for coming back to tell me, I'm gonna pay her a visit as soon as I can!"),
					new Feuille1("Fishman_AfterHermit",20,"Robert Fisher","<SoundManager:VOC_Fishman_AfterHermit_04>Thanks for coming back to tell me, I'm gonna pay her a visit as soon as I can!")
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
					if( _rows[i].eventName == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].eventName == key){ return true; }
			}
			return false;
		}
		public Feuille1[] ToArray(){
			return _rows;
		}
		public Feuille1 Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Feuille1 fishman_FirstMeet{	get{ return _rows[0]; } }
		public Feuille1 fishman_FirstMeet01{	get{ return _rows[1]; } }
		public Feuille1 fishman_FirstMeet02{	get{ return _rows[2]; } }
		public Feuille1 fishman_FirstMeet03{	get{ return _rows[3]; } }
		public Feuille1 fishman_FirstMeet04{	get{ return _rows[4]; } }
		public Feuille1 fishman_FirstMeet05{	get{ return _rows[5]; } }
		public Feuille1 fishman_FirstMeet06{	get{ return _rows[6]; } }
		public Feuille1 fishman_FirstMeet07{	get{ return _rows[7]; } }
		public Feuille1 fishman_FirstBoat{	get{ return _rows[8]; } }
		public Feuille1 fishman_FirstBoat01{	get{ return _rows[9]; } }
		public Feuille1 fishman_FirstBoat02{	get{ return _rows[10]; } }
		public Feuille1 fishman_FirstBoat03{	get{ return _rows[11]; } }
		public Feuille1 fishman_FirstBoat04{	get{ return _rows[12]; } }
		public Feuille1 fishman_FirstBoat05{	get{ return _rows[13]; } }
		public Feuille1 fishman_FirstBoat06{	get{ return _rows[14]; } }
		public Feuille1 fishman_AfterHermit{	get{ return _rows[15]; } }
		public Feuille1 fishman_AfterHermit01{	get{ return _rows[16]; } }
		public Feuille1 fishman_AfterHermit02{	get{ return _rows[17]; } }
		public Feuille1 fishman_AfterHermit03{	get{ return _rows[18]; } }
		public Feuille1 fishman_AfterHermit04{	get{ return _rows[19]; } }
		public Feuille1 fishman_AfterHermit05{	get{ return _rows[20]; } }

	}
}