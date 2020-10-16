using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    // 게임 플레이 화면 UI
    // 퀵슬롯, HP바, 퀘스트 목록, 시스템 로그, 총알 수, 대화 텍스트
    
    public Text numOfPotion;           // 퀵슬롯 변수
    public Text numOfBullet;

    public float maxHP = 100;           // HP 변수
    public float curHP;
    
    // 퀘스트 목록 관련 변수
    
    // 시스템 로그 관련 변수

    public Text remainBullet;            // 총 탄 수/ 잔탄 수
    public Text curBullet;

    public ChatController chatController;      // 대화 텍스트

    private void Start() {
        // numOfPotion 값 불러오기
        // numOfBullet 값 불러오기

        // curHP 값 불러오기
        
        // 총 탄 수 / 잔 탄 수 불러오기
    }

    private void Update() {
        // numOfPotion 값 불러오기
        // UI 반영

        // numOfBullet 값 불러오기
        // UI 반영

        // curHP 값 불러오기
        // UI 반영

        // 총 탄 수 / 잔 탄 수 불러오기
        // UI 반영
    }
}
