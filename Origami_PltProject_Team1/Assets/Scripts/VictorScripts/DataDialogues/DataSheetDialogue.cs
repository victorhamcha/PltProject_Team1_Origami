
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
		public string nameImg;

		public Feuille1(){}

		public Feuille1(string eventName, int iD, string name, string sentence, string nameImg){
			this.eventName = eventName;
			this.iD = iD;
			this.name = name;
			this.sentence = sentence;
			this.nameImg = nameImg;
		}
	}
	public class SheetFeuille1: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,12,14,15,18,50);
		public readonly string[] labels = new string[]{"EventName","iD","Name","Sentence","NameImg"};
		private Feuille1[] _rows = new Feuille1[92];
		public void Init() {
			_rows = new Feuille1[]{
					new Feuille1("Fishman_FirstMeet",0,"Robert Fisher","<S:VOC_FishMan_Short_01>Hmm...Hmm...","RobertFisher"),
					new Feuille1("Fishman_FirstMeet",1,"Robert Fisher","<S:VOC_FishMan_Long_01>Oh ! Forgive me. I was lost in thought. My name is Robert. I'm the fisherman of this little village! Nice to meet you!","RobertFisher"),
					new Feuille1("Fishman_FirstMeet",2,"Robert Fisher","<S:VOC_FishMan_Long_02>To be honest with you, things aren't going strong lately. You see my boat over there? The thing won't move anymore...","RobertFisher"),
					new Feuille1("Fishman_FirstMeet",3,"Robert Fisher","<S:VOC_FishMan_Long_03>No matter how hard I try or how long I wait. So I just sit here, with my telescope and my bottle.","RobertFisher"),
					new Feuille1("Fishman_FirstMeet",4,"Robert Fisher","<S:VOC_FishMan_Short_01>Hmm...Hmm...","RobertFisher"),
					new Feuille1("Fishman_FirstMeet",5,"Robert Fisher","<S:VOC_FishMan_Short_02>Still not moving...","RobertFisher"),
					new Feuille1("Fishman_FirstMeet",6,"Robert Fisher","<S:VOC_FishMan_Medium_01>How will I get fish for the village...","RobertFisher"),
					new Feuille1("Fishman_FirstMeet",7,"Robert Fisher","<S:VOC_FishMan_Mini_01>Hmm...","RobertFisher"),
					new Feuille1("Fishman_FirstBoat",8,"Robert Fisher","<S:VOC_FishMan_Mini_01>Hmm...","RobertFisher"),
					new Feuille1("Fishman_FirstBoat",9,"Robert Fisher","<S:VOC_FishMan_Medium_02>Oh! Wait!... You repaired the boat!","RobertFisher"),
					new Feuille1("Fishman_FirstBoat",10,"Robert Fisher","<S:VOC_FishMan_Long_04>I don't know how you did it but thanks to you the village is going to receive fish again!","RobertFisher"),
					new Feuille1("Fishman_FirstBoat",11,"Robert Fisher","<S:VOC_FishMan_Medium_03>As a token of my gratitude you can use the boat as you like!","RobertFisher"),
					new Feuille1("Fishman_FirstBoat",12,"Robert Fisher","<S:VOC_FishMan_Long_01>You can go visit the neighboring island if you want. I heard it is really beautiful this time of year.","RobertFisher"),
					new Feuille1("Fishman_FirstBoat",13,"Robert Fisher","<S:VOC_FishMan_Medium_02>Perhaps I can go pay a visit to that old hermit now...","RobertFisher"),
					new Feuille1("Fishman_FirstBoat",14,"Robert Fisher","<S:VOC_FishMan_Short_03>Finally tell her how I feel...","RobertFisher"),
					new Feuille1("Fishman_AfterHermit",15,"Robert Fisher","<S:VOC_FishMan_Medium_01>Hmm...Hmm...","RobertFisher"),
					new Feuille1("Fishman_AfterHermit",16,"Robert Fisher","<S:VOC_FishMan_Medium_01>Oh! You're back! Did you check on that Island? Did she talk about me?","RobertFisher"),
					new Feuille1("Fishman_AfterHermit",17,"Robert Fisher","...","RobertFisher"),
					new Feuille1("Fishman_AfterHermit",18,"Robert Fisher","<S:VOC_FishMan_Medium_01>She did?... Well...","RobertFisher"),
					new Feuille1("Fishman_AfterHermit",19,"Robert Fisher","<S:VOC_FishMan_Medium_01>Thanks for coming back to tell me, I'm gonna pay her a visit as soon as I can!","RobertFisher"),
					new Feuille1("Fishman_AfterHermit",20,"Robert Fisher","<S:VOC_FishMan_Medium_01>Thanks for coming back to tell me, I'm gonna pay her a visit as soon as I can!","RobertFisher"),
					new Feuille1("Postman_FirstMeet",21,"Mac Mailing","<S:VOC_Facteur_Medium_01>You there ! You seem kind of lost. Come here.","MacMailing"),
					new Feuille1("Postman_FirstMeet",22,"Mac Mailing","<S:VOC_Facteur_Medium_02>Hello ! You don’t look from here, far from it !","MacMailing"),
					new Feuille1("Postman_FirstMeet",23,"Mac Mailing","<S:VOC_Facteur_Medium_03>I am Mac Mailing, the postman of this little township.","MacMailing"),
					new Feuille1("Postman_FirstMeet",24,"Mac Mailing","<S:VOC_Facteur_Medium_04>Let me guess. Fell down from the postbird’s bag ?","MacMailing"),
					new Feuille1("Postman_FirstMeet",25,"Mac Mailing","<S:VOC_Facteur_Short_01>That is unfortunate...","MacMailing"),
					new Feuille1("Postman_FirstMeet",26,"Mac Mailing","<S:VOC_Facteur_Long_01>You see I’m just the postman of these two islands, well… only this one now, since the only boat of the village isn’t working anymore…","MacMailing"),
					new Feuille1("Postman_FirstMeet",27,"Mac Mailing","<S:VOC_Facteur_Long_02>Anyway, I only deliver letters in this village. I don’t have any way of sending one farther away than that without a postbird.","MacMailing"),
					new Feuille1("Postman_FirstMeet",28,"Mac Mailing","<S:VOC_Facteur_Long_03>And let me tell you, they don’t come often around here. The next one only comes in a week !","MacMailing"),
					new Feuille1("Postman_FirstMeet",29,"Mac Mailing","...","MacMailing"),
					new Feuille1("Postman_FirstMeet",30,"Mac Mailing","<S:VOC_Facteur_Medium_02>Maybe the old hermit could help…","MacMailing"),
					new Feuille1("Postman_FirstMeet",31,"Mac Mailing","<S:VOC_Facteur_Long_04>But even if you could get there, the bridge that leads to the next part of this island is broken…","MacMailing"),
					new Feuille1("Postman_FirstMeet",32,"Mac Mailing","<S:VOC_Facteur_Long_01>Here take these sheets of paper, I tried to fix the bridge myself but that didn’t work out. Maybe you will have more luck than me.","MacMailing"),
					new Feuille1("Postman_FirstMeet",33,"Mac Mailing","<S:VOC_Facteur_Medium_03>Sorry… I wish I could be more useful…","MacMailing"),
					new Feuille1("Postman_FirstMeet",34,"Mac Mailing","<S:VOC_Facteur_Medium_03>Sorry… I wish I could be more useful…","MacMailing"),
					new Feuille1("Postman_AfterTuto",35,"Mac Mailing","<S:VOC_Facteur_Long_02>Amazing ! Who would have thought such a tiny letter would be able to repair that bridge !","MacMailing"),
					new Feuille1("Postman_AfterTuto",36,"Mac Mailing","<S:VOC_Facteur_Medium_04>If you could do that, fixing the old man’s boat won’t be a problem !","MacMailing"),
					new Feuille1("Postman_AfterTuto",37,"Mac Mailing","<S:VOC_Facteur_Long_03>Maybe he will let you use his boat if you help him. And with that, go fetch some help from the hermit. I am sure she will have a solution to get you going.","MacMailing"),
					new Feuille1("Postman_AfterTuto",38,"Mac Mailing","<S:VOC_Facteur_Long_04>You can’t miss Robert. Follow the path after the bridge. You won’t miss him. He is probably grumbling next to the river of clouds as usual.","MacMailing"),
					new Feuille1("Postman_AfterTuto",39,"Mac Mailing","<S:VOC_Facteur_Long_04>You can’t miss Robert. Follow the path after the bridge. You won’t miss him. He is probably grumbling next to the river of clouds as usual.","MacMailing"),
					new Feuille1("Jennifer Mayor",40,"Jennifer Mayor","<S:VOC_Villager_ShortWoman_01>Hello darling.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",41,"Jennifer Mayor","<S:VOC_Villager_MedWoman_01>I’m the mayor of this little village.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",42,"Jennifer Mayor","<S:VOC_Villager_MedWoman_02>Enjoy your visit in our community.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",43,"Jennifer Mayor","<S:VOC_Villager_MedWoman_03>You can go visit our harbor up north.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",44,"Jennifer Mayor","<S:VOC_Villager_MedWoman_04>And our famous windmill north-east from here.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",45,"Jennifer Mayor","<S:VOC_Villager_MedWoman_01>You look like you enjoy a good panorama.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",46,"Jennifer Mayor","<S:VOC_Villager_MedWoman_02>There is a little bench on the other part of the island near the bridge.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",47,"Jennifer Mayor","<S:VOC_Villager_MedWoman_03>You can go sit there and enjoy the view !","JenniferMayor"),
					new Feuille1("Jennifer Mayor",48,"Jennifer Mayor","<S:VOC_Villager_MedWoman_04>There should also be my husband around.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",49,"Jennifer Mayor","<S:VOC_Villager_LongWoman_01>If you are into sports and mediations, I’m sure he will gladly teach you some tricks.","JenniferMayor"),
					new Feuille1("Jennifer Mayor",50,"Jennifer Mayor","<S:VOC_Villager_MedWoman_01>Raymond is always doing his daily yoga not far from here.","JenniferMayor"),
					new Feuille1("Mary Miller",51,"Mary Miller","<S:VOC_Villager_ShortWoman_01>Hey there !","MaryMiller"),
					new Feuille1("Mary Miller",52,"Mary Miller","<S:VOC_Villager_MedWoman_01>I thought I heard my husband arriving but it was you !","MaryMiller"),
					new Feuille1("Mary Miller",53,"Mary Miller","<S:VOC_Villager_MedWoman_02>He must still be busy with the sheeps...","MaryMiller"),
					new Feuille1("Mary Miller",54,"Mary Miller","<S:VOC_Villager_MedWoman_03>I wish he could help me with the windmill.","MaryMiller"),
					new Feuille1("Mary Miller",55,"Mary Miller","<S:VOC_Villager_MedWoman_04>Guess I will have to figure out a solution myself...","MaryMiller"),
					new Feuille1("Mary Miller",56,"Mary Miller","<S:VOC_Villager_ShortWoman_02>The windmill is broken...","MaryMiller"),
					new Feuille1("Mary Miller",57,"Mary Miller","<S:VOC_Villager_ShortWoman_03>I can't work without it.","MaryMiller"),
					new Feuille1("Mary Miller",58,"Mary Miller","<S:VOC_Villager_MedWoman_01>I’ve already repaired it in the past.","MaryMiller"),
					new Feuille1("Mary Miller",59,"Mary Miller","<S:VOC_Villager_MedWoman_02>But it’s a two person’s job, and Nicolas is not available right now.","MaryMiller"),
					new Feuille1("Mary Miller",60,"Mary Miller","<S:VOC_Villager_MedWoman_03>Can’t blame him, it’s a lot of work to take care of these animals","MaryMiller"),
					new Feuille1("Mary Miller",61,"Mary Miller","<S:VOC_Villager_MedWoman_04>Wow ! You are full of resources ! Thanks a lot, little one !","MaryMiller"),
					new Feuille1("Mr and Mrs Mailling",62,"Mr and Mrs Mailling","<S:VOC_Villager_ShortWoman_01>Hello you !","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",63,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_01>You must be the one we saw falling from that bird’s bag.","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",64,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_02>Thankfully you are not injured from that fall.","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",65,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_03>Well, you are a letter after all…","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",66,"Mr and Mrs Mailling","<S:VOC_Villager_LongWoman_01>Go see our grand-son ! He is the postman, I’m sure he will be able to help you !","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",67,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_04>Mac has been our postman for a long time now.","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",68,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_01>You should have seen him when he was just a child.","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",69,"Mr and Mrs Mailling","<S:VOC_Villager_ShortWoman_02>He was so shy and nervous.","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",70,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_02>Now he’s all friendly and confident with everyone.","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",71,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_03>He fits so well in our little community !","MrandMrsMailling"),
					new Feuille1("Mr and Mrs Mailling",72,"Mr and Mrs Mailling","<S:VOC_Villager_MedWoman_04>Mac fits so well in our little community !","MrandMrsMailling"),
					new Feuille1("Raymond Mayor",73,"Raymond Mayor","<S:VOC_Villager_MedMan_01>There are two important things in life.","RaymondMayor"),
					new Feuille1("Raymond Mayor",74,"Raymond Mayor","<S:VOC_Villager_MedMan_02>Your body and your mind !","RaymondMayor"),
					new Feuille1("Raymond Mayor",75,"Raymond Mayor","<S:VOC_Villager_MedMan_03>Never neglect these two.","RaymondMayor"),
					new Feuille1("Raymond Mayor",76,"Raymond Mayor","<S:VOC_Villager_LongMan_01>I love to come here to do yoga. You should join me !","RaymondMayor"),
					new Feuille1("Nicolas Miller",77,"Nicolas Miller","<S:VOC_Villager_MiniMan_01>Oh hi !","NicolasMiller"),
					new Feuille1("Nicolas Miller",78,"Nicolas Miller","<S:VOC_Villager_LongMan_01>Sorry I don’t have much time to talk, it’s a full time job to take care of these little devils !","NicolasMiller"),
					new Feuille1("Nicolas Miller",79,"Nicolas Miller","<S:VOC_Villager_MedMan_01>I have to finish fast so I can go help my wife.","NicolasMiller"),
					new Feuille1("Nicolas Miller",80,"Nicolas Miller","<S:VOC_Villager_MedMan_02>I have to finish fast so I can go help my wife.","NicolasMiller"),
					new Feuille1("Tatiana Mayor",81,"Tatiana Mayor","<S:VOC_Villager_MedChild_01>My mother is the mayor of this village !","TatianaMayor"),
					new Feuille1("Tatiana Mayor",82,"Tatiana Mayor","<S:VOC_Villager_MedChild_02>And my father is the strongest !","TatianaMayor"),
					new Feuille1("Tatiana Mayor",83,"Tatiana Mayor","<S:VOC_Villager_ShortChild_01>I have the best parents !","TatianaMayor"),
					new Feuille1("Tatiana Mayor",84,"Tatiana Mayor","<S:VOC_Villager_ShortChild_01>I have the best parents !","TatianaMayor"),
					new Feuille1("Zoe et Tim",85,"Zoe & Tim","<S:VOC_Villager_ShortChild_01>You are so tiny !","Zoe"),
					new Feuille1("Zoe et Tim",86,"Zoe & Tim","<S:VOC_Villager_ShortChild_02>Are you lost ?","Tim"),
					new Feuille1("Zoe et Tim",87,"Zoe & Tim","<S:VOC_Villager_MedChild_01>You can come play at our house if you want !","Zoe"),
					new Feuille1("Zoe et Tim",88,"Zoe & Tim","<S:VOC_Villager_MedChild_01>You can come play at our house if you want !","Tim"),
					new Feuille1("Mouton",89,"Mouton_1","<S:VOC_SFX_Sheep_OneShot>Meeeeeeeh !","Mouton_1"),
					new Feuille1("Chien",90,"Chien","<S:VOC_SFX_Dog_Yap_03>Wouf !","Chien"),
					new Feuille1("Mouton",91,"Mouton_2","<S:VOC_SFX_Sheep_OneShot>Meeeeeeeh !","Mouton_2")
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
		public Feuille1 mouton01{	get{ return _rows[91]; } }

	}
}