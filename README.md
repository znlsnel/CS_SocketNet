![Typing SVG](https://readme-typing-svg.demolab.com?font=Fira+Code&size=30&pause=1000&width=435&lines=C%23+Server)

---

# Description
- **프로젝트 소개** <br>
  게임 서버를 직접 만들고 게임에 적용시켜보기 위해서 시작한 프로젝트입니다.<br>
  유니티 클라이언트와 연동하였으며, 3D 게임에서 데이터들을 실시간으로 연동시켜보는 것을 목표로 개발했습니다.

- **개발 기간** : 2024.02.27 - 2024.03.19
- **사용 기술** <br>
-언어 : C#<br>
-엔진 : Unity Engine <br>
-개발 환경 : Windows 11<br>
<br>

---
## 목차
- 기획 의도
- 발생 및 해결한 주요 문제점
- 핵심 기능
<br>

---
## 기획 의도
- 안정적으로 구동되는 게임 서버 개발
- 유니티 클라이언트의 데이터 실시간 동기화 환경 구축
<br>

---
## 발생 및 해결한 주요 문제점
### 비동기 통신간에 무분별하게 생성되는 스레드
- 전달 받은 데이터를 처리하기 위해 생성된 스레드가 너무 많아서 성능의 문제가 발생했습니다.
- 문제의 원인은 데이터를 처리하기 위해 lock을 거치는 단계가 반드시 필요했는데<br>
  이 부분에서 스레드들이 대기를 하다보니 당장 사용할 스레드를 추가로 생성하는것이 문제였습니다.
<div style="display: flex; justify-content: space-around;">
  <img src="https://github.com/user-attachments/assets/3fba4331-4b31-4b2a-841d-d7952a298813" alt="KakaoTalk_20241230_212921650_01" width="400">
</div>

- 이 문제를 해결하기 위해서는 스레드의 생명주기를 줄일 필요가 있었습니다.<br>
  데이터를 전송받은 스레드가 일감을 처리하고 종료하는 것이 아니라 일감을 Queue에 넣고 종료하는 방식으로 구현했습니다.<br>
  결과적으로 스레드의 생명주기가 줄어들었고 스레드가 무분별하게 생성되는 문제를 해결할 수 있었습니다.
<br><br>

### 클라이언트의 수가 많아지면 속도가 배로 느려지는 문제
- 서버를 테스트하는 과정에서 클라이언트의 수가 많아지면 데이터를 전송하는 속도가 너무 느려지는 문제가 발생했습니다.
- 1번이 전송한 데이터를 2번 ~ N번까지 모두 전송하는 방식으로 설게되었기 때문에<br>
  결과적으로 N^2번의 데이터 전송이 발생해서 생긴 문제였습니다.
  <div style="display: flex; justify-content: space-around;">
  <img src="https://github.com/user-attachments/assets/06275873-08f6-48d4-9c2e-6081f677fe7c" alt="KakaoTalk_20241230_212921650_01" width="400">
  <img src="https://github.com/user-attachments/assets/fea30652-97ef-4ac5-bde0-0528785aac11" alt="KakaoTalk_20241230_212921650_01" width="400">
</div>

- 데이터 전송 횟수를 줄이기 위해 데이터를 모아서 보내는 방식을 채택했습니다.
- 모든 클라이언트에게 보낼 패킷을 모아두었다가 특정 주기로 한번에 보내도록 하여 전송 횟수를 획기적으로 줄일 수 있었습니다.

<br>

---

## 핵심 기능
플레이 영상입니다. (아래 이미지를 클릭하면 영상 링크로 이동합니다.)
[![Video](https://img.youtube.com/vi/KqOvVzq0ZdU/0.jpg)](https://www.youtube.com/watch?time_continue=21&v=lpBEm4esF6Y&embeds_referring_euri=https%3A%2F%2Fwww.notion.so%2F&source_ve_path=MjM4NTE)
- 플레이어 정보 동기화<br>
  플레이어의 위치, 회전 값, 체력, 플레이어 상태를 동기화 하였습니다.

- 투사체 정보 동기화<br>
  투사체 생성 여부, 위치값, 충돌 여부를 동기화했습니다.

- 스코어 동기화<br>
  각 팀의 점수를 동기화 하였습니다.
  

  
