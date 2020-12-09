
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
		public System.DateTime updated = new System.DateTime(2020,12,8,17,24,47);
		public readonly string[] labels = new string[]{"EventName","iD","Name","Sentence"};
		private Feuille1[] _rows = new Feuille1[91];
		public void Init() {
			_rows = new Feuille1[]{
					new Feuille1("Fishman_FirstMeet",0,"Robert Fisher","<S:VOC_FishMan_Medium_01>Hmm...Hmm..."),
					new Feuille1("Fishman_FirstMeet",1,"Robert Fisher","<S:VOC_FishMan_Medium_01>Oh ! Forgive me. I was lost in thought. My name is Robert. I'm the fisherman of this little village! Nice to meet you!"),
					new Feuille1("Fishman_FirstMeet",2,"Robert Fisher","<S:VOC_FishMan_Medium_01>To be honest with you, things aren't going strong lately. You see my boat over there? The thing won't move anymore..."),
					new Feuille1("Fishman_FirstMeet",3,"Robert Fisher","<S:VOC_FishMan_Medium_01>No matter how hard I try or how long I wait. So I just sit here, with my telescope and my bottle."),
					new Feuille1("Fishman_FirstMeet",4,"Robert Fisher","<S:VOC_FishMan_Medium_01>Hmm...Hmm..."),
					new Feuille1("Fishman_FirstMeet",5,"Robert Fisher","<S:VOC_FishMan_Medium_01>Still not moving..."),
					new Feuille1("Fishman_FirstMeet",6,"Robert Fisher","<S:VOC_FishMan_Medium_01>How will I get fish for the village..."),
					new Feuille1("Fishman_FirstMeet",7,"Robert Fisher","<S:VOC_FishMan_Medium_01>Hmm..."),
					new Feuille1("Fishman_FirstBoat",8,"Robert Fisher","<S:VOC_FishMan_Medium_01>Hmm..."),
					new Feuille1("Fishman_FirstBoat",9,"Robert Fisher","<S:VOC_FishMan_Medium_01>Oh! Wait!... You repaired the boat!"),
					new Feuille1("Fishman_FirstBoat",10,"Robert Fisher","<S:VOC_FishMan_Medium_01>I don't know how you did it but thanks to you the village is going to receive fish again!"),
					new Feuille1("Fishman_FirstBoat",11,"Robert Fisher","<S:VOC_FishMan_Medium_01>As a token of my gratitude you can use the boat as you like!"),
					new Feuille1("Fishman_FirstBoat",12,"Robert Fisher","<S:VOC_FishMan_Medium_01>You can go visit the neighboring island if you want. I heard it is really beautiful this time of year."),
					new Feuille1("Fishman_FirstBoat",13,"Robert Fisher","<S:VOC_FishMan_Medium_01>Perhaps I can go pay a visit to that old hermit now..."),
					new Feuille1("Fishman_FirstBoat",14,"Robert Fisher","<S:VOC_FishMan_Medium_01>Finally tell her how I feel..."),
					new Feuille1("Fishman_AfterHermit",15,"Robert Fisher","<S:VOC_FishMan_Medium_01>Hmm...Hmm..."),
					new Feuille1("Fishman_AfterHermit",16,"Robert Fisher","<S:VOC_FishMan_Medium_01>Oh! You're back! Did you check on that Island? Did she talk about me?"),
					new Feuille1("Fishman_AfterHermit",17,"Robert Fisher","..."),
					new Feuille1("Fishman_AfterHermit",18,"Robert Fisher","<S:VOC_FishMan_Medium_01>She did?... Well..."),
					new Feuille1("Fishman_AfterHermit",19,"Robert Fisher","<S:VOC_FishMan_Medium_01>Thanks for coming back to tell me, I'm gonna pay her a visit as soon as I can!"),
					new Feuille1("Fishman_AfterHermit",20,"Robert Fisher","<S:VOC_FishMan_Medium_01>Thanks for coming back to tell me, I'm gonna pay her a visit as soon as I can!"),
					new Feuille1("Postman_FirstMeet",21,"Mac Mailing","<S:VOC_Facteur_Medium_01>You there ! You seem kind of lost. Come here."),
					new Feuille1("Postman_FirstMeet",22,"Mac Mailing","<S:VOC_Facteur_Medium_01>Hello ! You don’t look from here, far from it !"),
					new Feuille1("Postman_FirstMeet",23,"Mac Mailing","<S:VOC_Facteur_Medium_01>I am Charlie Laposte, the postman of this little township."),
					new Feuille1("Postman_FirstMeet",24,"Mac Mailing","<S:VOC_Facteur_Medium_01>Let me guess. Fell down from the postbird’s bag ?"),
					new Feuille1("Postman_FirstMeet",25,"Mac Mailing","<S:VOC_Facteur_Medium_01>That is unfortunate..."),
					new Feuille1("Postman_FirstMeet",26,"Mac Mailing","<S:VOC_Facteur_Medium_01>You see I’m just the postman of these two islands, well… only this one now, since the only boat of the village isn’t working anymore…"),
					new Feuille1("Postman_FirstMeet",27,"Mac Mailing","<S:VOC_Facteur_Medium_01>Anyway, I only deliver letters in this village. I don’t have any way of sending one farther away than that without a postbird."),
					new Feuille1("Postman_FirstMeet",28,"Mac Mailing","<S:VOC_Facteur_Medium_01>And let me tell you, they don’t come often around here. The next one only comes in a week !"),
					new Feuille1("Postman_FirstMeet",29,"Mac Mailing","..."),
					new Feuille1("Postman_FirstMeet",30,"Mac Mailing","<S:VOC_Facteur_Medium_01>Maybe the old hermit could help…"),
					new Feuille1("Postman_FirstMeet",31,"Mac Mailing","<S:VOC_Facteur_Medium_01>But even if you could get there, the bridge that leads to the next part of this island is broken…"),
					new Feuille1("Postman_FirstMeet",32,"Mac Mailing","<S:VOC_Facteur_Medium_01>Here take these sheets of paper, I tried to fix the bridge myself but that didn’t work out. Maybe you will have more luck than me."),
					new Feuille1("Postman_FirstMeet",33,"Mac Mailing","<S:VOC_Fishman_FirstMeet_FirstTalk_01>Sorry… I wish I could be more useful…"),
					new Feuille1("Postman_FirstMeet",34,"Mac Mailing","<S:VOC_Facteur_Medium_01>Sorry… I wish I could be more useful…"),
					new Feuille1("Postman_AfterTuto",35,"Mac Mailing","<S:VOC_Facteur_Medium_01>Amazing ! Who would have thought such a tiny letter would be able to repair that bridge !"),
					new Feuille1("Postman_AfterTuto",36,"Mac Mailing","<S:VOC_Facteur_Medium_01>If you could do that, fixing the old man’s boat won’t be a problem !"),
					new Feuille1("Postman_AfterTuto",37,"Mac Mailing","<S:VOC_Facteur_Medium_01>Maybe he will let you use his boat if you help him. And with that, go fetch some help from the hermit. I am sure she will have a solution to get you going."),
					new Feuille1("Postman_AfterTuto",38,"Mac Mailing","<S:VOC_Facteur_Medium_01>You can’t miss Robert. Follow the path after the bridge. You won’t miss him. He is probably grumbling next to the river of clouds as usual."),
					new Feuille1("Postman_AfterTuto",39,"Mac Mailing","<S:VOC_Facteur_Medium_01>You can’t miss Robert. Follow the path after the bridge. You won’t miss him. He is probably grumbling next to the river of clouds as usual."),
					new Feuille1("Jennifer Mayor",40,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>Hello you !"),
					new Feuille1("Jennifer Mayor",41,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>You must be the one I saw falling from that bird’s bag."),
					new Feuille1("Jennifer Mayor",42,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>I’m happy you are not injured from that fall."),
					new Feuille1("Jennifer Mayor",43,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>Well, you are a letter after all…"),
					new Feuille1("Jennifer Mayor",44,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>Go see our postman, I’m sure he will be able to help you !"),
					new Feuille1("Jennifer Mayor",45,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>Mac has been our postman for a long time now."),
					new Feuille1("Jennifer Mayor",46,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>I remember when he first came here."),
					new Feuille1("Jennifer Mayor",47,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>He was so shy and nervous."),
					new Feuille1("Jennifer Mayor",48,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>Now he’s all friendly and confident with everyone."),
					new Feuille1("Jennifer Mayor",49,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>I am glad he managed to fit so well in our little community !"),
					new Feuille1("Jennifer Mayor",50,"Jennifer Mayor","<S:VOC_Facteur_Medium_01>I am glad Mac managed to fit so well in our little community !"),
					new Feuille1("Mary Miller",51,"Mary Miller","<S:VOC_Facteur_Medium_01>Hello you !"),
					new Feuille1("Mary Miller",52,"Mary Miller","<S:VOC_Facteur_Medium_01>You must be the one I saw falling from that bird’s bag."),
					new Feuille1("Mary Miller",53,"Mary Miller","<S:VOC_Facteur_Medium_01>I’m happy you are not injured from that fall."),
					new Feuille1("Mary Miller",54,"Mary Miller","<S:VOC_Facteur_Medium_01>Well, you are a letter after all…"),
					new Feuille1("Mary Miller",55,"Mary Miller","<S:VOC_Facteur_Medium_01>Go see our postman, I’m sure he will be able to help you !"),
					new Feuille1("Mary Miller",56,"Mary Miller","<S:VOC_Facteur_Medium_01>Mac has been our postman for a long time now."),
					new Feuille1("Mary Miller",57,"Mary Miller","<S:VOC_Facteur_Medium_01>I remember when he first came here."),
					new Feuille1("Mary Miller",58,"Mary Miller","<S:VOC_Facteur_Medium_01>He was so shy and nervous."),
					new Feuille1("Mary Miller",59,"Mary Miller","<S:VOC_Facteur_Medium_01>Now he’s all friendly and confident with everyone."),
					new Feuille1("Mary Miller",60,"Mary Miller","<S:VOC_Facteur_Medium_01>I am glad he managed to fit so well in our little community !"),
					new Feuille1("Mary Miller",61,"Mary Miller","<S:VOC_Facteur_Medium_01>I am glad Mac managed to fit so well in our little community !"),
					new Feuille1("Mr and Mrs Mailling",62,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>Hello you !"),
					new Feuille1("Mr and Mrs Mailling",63,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>You must be the one I saw falling from that bird’s bag."),
					new Feuille1("Mr and Mrs Mailling",64,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>I’m happy you are not injured from that fall."),
					new Feuille1("Mr and Mrs Mailling",65,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>Well, you are a letter after all…"),
					new Feuille1("Mr and Mrs Mailling",66,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>Go see our postman, I’m sure he will be able to help you !"),
					new Feuille1("Mr and Mrs Mailling",67,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>Mac has been our postman for a long time now."),
					new Feuille1("Mr and Mrs Mailling",68,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>I remember when he first came here."),
					new Feuille1("Mr and Mrs Mailling",69,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>He was so shy and nervous."),
					new Feuille1("Mr and Mrs Mailling",70,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>Now he’s all friendly and confident with everyone."),
					new Feuille1("Mr and Mrs Mailling",71,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>I am glad he managed to fit so well in our little community !"),
					new Feuille1("Mr and Mrs Mailling",72,"Mr and Mrs Mailling","<S:VOC_Facteur_Medium_01>I am glad Mac managed to fit so well in our little community !"),
					new Feuille1("Raymond Mayor",73,"Raymond Mayor","<S:VOC_Facteur_Medium_01>Oh hi there little one !"),
					new Feuille1("Raymond Mayor",74,"Raymond Mayor","<S:VOC_Facteur_Medium_01>There sure is someone lucky to receive a letter."),
					new Feuille1("Raymond Mayor",75,"Raymond Mayor","<S:VOC_Facteur_Medium_01>It has been ages since I got one of you guys..."),
					new Feuille1("Raymond Mayor",76,"Raymond Mayor","<S:VOC_Facteur_Medium_01>It has been ages since I got one of you guys..."),
					new Feuille1("Nicolas Miller",77,"Nicolas Miller","<S:VOC_Facteur_Medium_01>Oh hi there little one !"),
					new Feuille1("Nicolas Miller",78,"Nicolas Miller","<S:VOC_Facteur_Medium_01>There sure is someone lucky to receive a letter."),
					new Feuille1("Nicolas Miller",79,"Nicolas Miller","<S:VOC_Facteur_Medium_01>It has been ages since I got one of you guys..."),
					new Feuille1("Nicolas Miller",80,"Nicolas Miller","<S:VOC_Facteur_Medium_01>It has been ages since I got one of you guys..."),
					new Feuille1("Tatiana Mayor",81,"Tatiana Mayor","<S:VOC_Facteur_Medium_01>You are so tiny !"),
					new Feuille1("Tatiana Mayor",82,"Tatiana Mayor","<S:VOC_Facteur_Medium_01>Are you lost ?"),
					new Feuille1("Tatiana Mayor",83,"Tatiana Mayor","<S:VOC_Facteur_Medium_01>You can come play at my house if you want !"),
					new Feuille1("Tatiana Mayor",84,"Tatiana Mayor","<S:VOC_Facteur_Medium_01>You can come play at my house if you want !"),
					new Feuille1("Zoe et Tim",85,"Zoe et Tim","<S:VOC_Facteur_Medium_01>You are so tiny !"),
					new Feuille1("Zoe et Tim",86,"Zoe et Tim","<S:VOC_Facteur_Medium_01>Are you lost ?"),
					new Feuille1("Zoe et Tim",87,"Zoe et Tim","<S:VOC_Facteur_Medium_01>You can come play at our house if you want !"),
					new Feuille1("Zoe et Tim",88,"Zoe et Tim","<S:VOC_Facteur_Medium_01>You can come play at our house if you want !"),
					new Feuille1("Mouton",89,"Mouton","<S:VOC_SFX_Dog_Yap_03>Wouf !"),
					new Feuille1("Chien",90,"Chien","<S:VOC_SFX_Sheep_OneShot>Meeeeeeeh !")
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
		public Feuille1 postman_FirstMeet{	get{ return _rows[21]; } }
		public Feuille1 postman_FirstMeet01{	get{ return _rows[22]; } }
		public Feuille1 postman_FirstMeet02{	get{ return _rows[23]; } }
		public Feuille1 postman_FirstMeet03{	get{ return _rows[24]; } }
		public Feuille1 postman_FirstMeet04{	get{ return _rows[25]; } }
		public Feuille1 postman_FirstMeet05{	get{ return _rows[26]; } }
		public Feuille1 postman_FirstMeet06{	get{ return _rows[27]; } }
		public Feuille1 postman_FirstMeet07{	get{ return _rows[28]; } }
		public Feuille1 postman_FirstMeet08{	get{ return _rows[29]; } }
		public Feuille1 postman_FirstMeet09{	get{ return _rows[30]; } }
		public Feuille1 postman_FirstMeet10{	get{ return _rows[31]; } }
		public Feuille1 postman_FirstMeet11{	get{ return _rows[32]; } }
		public Feuille1 postman_FirstMeet12{	get{ return _rows[33]; } }
		public Feuille1 postman_FirstMeet13{	get{ return _rows[34]; } }
		public Feuille1 postman_AfterTuto{	get{ return _rows[35]; } }
		public Feuille1 postman_AfterTuto01{	get{ return _rows[36]; } }
		public Feuille1 postman_AfterTuto02{	get{ return _rows[37]; } }
		public Feuille1 postman_AfterTuto03{	get{ return _rows[38]; } }
		public Feuille1 postman_AfterTuto04{	get{ return _rows[39]; } }
		public Feuille1 jenniferMayor{	get{ return _rows[40]; } }
		public Feuille1 jenniferMayor01{	get{ return _rows[41]; } }
		public Feuille1 jenniferMayor02{	get{ return _rows[42]; } }
		public Feuille1 jenniferMayor03{	get{ return _rows[43]; } }
		public Feuille1 jenniferMayor04{	get{ return _rows[44]; } }
		public Feuille1 jenniferMayor05{	get{ return _rows[45]; } }
		public Feuille1 jenniferMayor06{	get{ return _rows[46]; } }
		public Feuille1 jenniferMayor07{	get{ return _rows[47]; } }
		public Feuille1 jenniferMayor08{	get{ return _rows[48]; } }
		public Feuille1 jenniferMayor09{	get{ return _rows[49]; } }
		public Feuille1 jenniferMayor10{	get{ return _rows[50]; } }
		public Feuille1 maryMiller{	get{ return _rows[51]; } }
		public Feuille1 maryMiller01{	get{ return _rows[52]; } }
		public Feuille1 maryMiller02{	get{ return _rows[53]; } }
		public Feuille1 maryMiller03{	get{ return _rows[54]; } }
		public Feuille1 maryMiller04{	get{ return _rows[55]; } }
		public Feuille1 maryMiller05{	get{ return _rows[56]; } }
		public Feuille1 maryMiller06{	get{ return _rows[57]; } }
		public Feuille1 maryMiller07{	get{ return _rows[58]; } }
		public Feuille1 maryMiller08{	get{ return _rows[59]; } }
		public Feuille1 maryMiller09{	get{ return _rows[60]; } }
		public Feuille1 maryMiller10{	get{ return _rows[61]; } }
		public Feuille1 mrAndMrsMailling{	get{ return _rows[62]; } }
		public Feuille1 mrAndMrsMailling01{	get{ return _rows[63]; } }
		public Feuille1 mrAndMrsMailling02{	get{ return _rows[64]; } }
		public Feuille1 mrAndMrsMailling03{	get{ return _rows[65]; } }
		public Feuille1 mrAndMrsMailling04{	get{ return _rows[66]; } }
		public Feuille1 mrAndMrsMailling05{	get{ return _rows[67]; } }
		public Feuille1 mrAndMrsMailling06{	get{ return _rows[68]; } }
		public Feuille1 mrAndMrsMailling07{	get{ return _rows[69]; } }
		public Feuille1 mrAndMrsMailling08{	get{ return _rows[70]; } }
		public Feuille1 mrAndMrsMailling09{	get{ return _rows[71]; } }
		public Feuille1 mrAndMrsMailling10{	get{ return _rows[72]; } }
		public Feuille1 raymondMayor{	get{ return _rows[73]; } }
		public Feuille1 raymondMayor01{	get{ return _rows[74]; } }
		public Feuille1 raymondMayor02{	get{ return _rows[75]; } }
		public Feuille1 raymondMayor03{	get{ return _rows[76]; } }
		public Feuille1 nicolasMiller{	get{ return _rows[77]; } }
		public Feuille1 nicolasMiller01{	get{ return _rows[78]; } }
		public Feuille1 nicolasMiller02{	get{ return _rows[79]; } }
		public Feuille1 nicolasMiller03{	get{ return _rows[80]; } }
		public Feuille1 tatianaMayor{	get{ return _rows[81]; } }
		public Feuille1 tatianaMayor01{	get{ return _rows[82]; } }
		public Feuille1 tatianaMayor02{	get{ return _rows[83]; } }
		public Feuille1 tatianaMayor03{	get{ return _rows[84]; } }
		public Feuille1 zoeEtTim{	get{ return _rows[85]; } }
		public Feuille1 zoeEtTim01{	get{ return _rows[86]; } }
		public Feuille1 zoeEtTim02{	get{ return _rows[87]; } }
		public Feuille1 zoeEtTim03{	get{ return _rows[88]; } }
		public Feuille1 mouton{	get{ return _rows[89]; } }
		public Feuille1 chien{	get{ return _rows[90]; } }

	}
}