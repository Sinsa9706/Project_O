# Project_O
Unity 게임 개발 팀 프로젝트

## 프로젝트 소개
컨셉 : 구슬 타워 디펜스  
레퍼런스 게임 : 
게임 방식, 목표: 몬스터들이 출발점에서 시작해서 정해진 경로를 따라 도착지까지 3마리 이상 도착하지 않게 구슬을 설치하여 막는 게임

## 개발 기간
24.2.9금 ~ 24.2.16금

## 멤버 구성
팀장 진보경: Game UI, Inventory, MonsterSpawn   
팀원 조수정: GameManager  
팀원 장지희: GameManager   
팀원 염고운: Marble Supervise   
팀원 이경섭: Game UI, SceneManager

## 개발 환경
Unity Editor Version 2022.3.2f1   
IDE: Visual Studio 2022

## 게임플레이
(사진)
난쟁이의 모험 \~스파르타 코딩클럽에서 살아남기\~  
==============================================

## 역할분담

이호열 : 전체적인 UI 구현  
유호진 : 아이템 구현  
이승배 : 문영오 매니저님 만들기  
신은지 : Player 구현 및 level 배치  
조수정 : 각종 장애물 구현  

## 게임 소개

![image](https://github.com/NBC-Unity3/Platformer/assets/49552752/8c11dd94-d9f7-4ad8-aa95-3e6a322b7a37)

스파르타 코딩클럽에서 공부하고 있는 당신!  
여러가지 유혹과 매니저님의 잔소리를 이겨내며 오늘 하루도 평화롭게 출석을 완료하는 것이 목표입니다.  

- 타이틀 화면  
  ![image](https://github.com/NBC-Unity3/Platformer/assets/49552752/2d7fb4dc-71b2-4618-9d4e-288312071891)
  - 입실 : 게임을 시작합니다.
  - 설정 : 볼륨을 설정할 수 있습니다.
  - 퇴실 : 게임을 종료합니다.

- 게임 진행  
  ![image](https://github.com/NBC-Unity3/Platformer/assets/49552752/edf9c344-5ad3-4069-9d1e-90718fc9bbe2)
  
  - 퇴실버튼 : 하루 출석을 마무리하기 위해 도달해야하는 목표물입니다.  
  - 아이템 : 각종 아이템입니다. LeftShift로 사용할 수 있습니다.
    
    - 사과 : 사용하면 1번만 트리플점프가 가능합니다.
    - 파인애플 : 장애물과 충돌을 1회 막아주는 방어막을 생성합니다.
    - 체리 : 플레이어의 이동속도가 빨라집니다.

  - 장애물 : 침대, 유튜브 등 출석을 방해하는 유혹입니다. 충돌시 개별 엔딩이 출력됩니다.
  - 매니저 : 플레이어를 따라다니며 잔소리를 날립니다.
    - 잔소리  
    ![image](https://github.com/NBC-Unity3/Platformer/assets/49552752/cb1b1042-8c0c-4bf6-8b09-aabc46d92c55)  
    플레이어의 정상적인 진행을 방해하는 장애물입니다.  
    **매니저님의 잔소리에 파묻히기 전에 재빨리 도망가세요!**