using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointManager : MonoBehaviour {
	public GameObject FocusPointObj;
	public GameObject MakerObj;
	private RaycastHit Hit;
	public Text DisplayText;
	private int Click = 1;
	public float findingSquareDist = 0.5f;
	List<GameObject> list_toggle_ = new List<GameObject>();

    void Update () {
		Vector3 center = new Vector3(Screen.width/2, Screen.height/2, findingSquareDist);
		Ray ray = Camera.main.ScreenPointToRay (center);
		
		// Rayを飛ばす	　   （Rayの発生座標,   Rayの向き,Rayの距離, レイヤーマスク）
		if (Physics.Raycast (ray, out Hit/* , maxRayDistance, collisionLayerMask*/)) {
			//レイが当たった座標にFocusPointObjを移動させる
			FocusPointObj.transform.position = Hit.point;
			//レイが当たった平面を基準にFocusPointObjを回転させる
			FocusPointObj.transform.rotation = Hit.transform.rotation;
			return;
		}
	}
	
	public void ClickPlaceButton(){
		switch (Click){
			case 1:
			PlaceMarker();
			break;

			case 2:
			PlaceMarker();
			CalculateDistance();
			break;

			case 3:
			PlaceMarker();
			CalculateArea();
			break;

			case 4:
			Reset();
			break;
		}
	}

	//マーカーを設置する
	private void PlaceMarker(){
		if(Hit.point != new Vector3(0,0,0)){
			GameObject toggle_instance = Instantiate(MakerObj, Hit.point, Quaternion.identity) as GameObject;
			list_toggle_.Add(toggle_instance);
			Click++;
		}
	}

	//２点間の距離を求めて表示する
	public void CalculateDistance(){
		Vector3 pos1 = list_toggle_[0].transform.position;
		Vector3 pos2 = list_toggle_[1].transform.position;
		float DistanceMillimeter = Vector3.Distance(pos1, pos2);
		DisplayText.text = "２点間の距離"+(DistanceMillimeter * 100f).ToString("f2")+"cm";
	}

	public void CalculateArea(){
		Vector3 pos1 = list_toggle_[0].transform.position;
		Vector3 pos2 = list_toggle_[1].transform.position;
		Vector3 pos3 = list_toggle_[2].transform.position;
		
		Vector3 D = Vector3.Cross(pos2-pos1,pos3-pos1);
		float S = D.magnitude / 2;

		DisplayText.text = "３点の面積"+(S/2).ToString("f2")+"m^2";
	}

	//マーカーを削除する
	public void Reset(){
		for (int i = 0; i < list_toggle_.Count; i++)
		{
			Destroy(list_toggle_[i]);
        }
        //リスト自体をキレイにする
        list_toggle_.Clear();
		Click = 1;
	}
}