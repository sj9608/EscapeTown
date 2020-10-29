using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameClear : MonoBehaviour
{
    // 1. 대사 엔딩 크레딧처럼 올라가기 / 대사 끊어서 출력
    // 2. 대사 출력되면 버튼 다시하기, 게임 종료 버튼 활성화
    // 3. 대사 다 나오면 맨 마지막 유저 칭찬글(Good Play/nThanks to Play)

    public Text creditText; 
    private void Start() {
        
    }

    IEnumerator PrintEndingCredit()
    {
        string credit = "산지기의 집에서 산의 열쇠를 얻었다.\n해가뜨기 전 사람들 눈을 피해 산을 돌아 나왔고, 기차역을 찾을 수 있었다.\n위험한 곳이었다.\n사람을 죽인 이 손으로 다시 이전의 삶으로 돌아갈 수 있을까?\n그래도 살아남았어. 집으로 가자.";
        string writerText = "";

        // 대사가 한 글자씩 출력되는 연출
        for(int i=0; i<credit.Length; i++)
        {
            writerText += credit[i];
            creditText.text = writerText;
            yield return null;
        }

        
    }
}
