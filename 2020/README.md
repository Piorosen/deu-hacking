## 개요 
SSL 인증서 검증 및 Pinning으로 데이터 종단간 암호화에 문제.

## 요약
1. SSL 인증서가 U-Check 회사에서 만든 인증서 인지, 아닌지 확인을 하지 않아서 생김.
2. 데이터를 종단간 암호화를 하는 SSL Pinning을 사용 하지 않음. 그러므로 데이터가 보여짐.
3. 스마트폰 <-> Proxy Server <-> 출석 인증 서버
  <br>스마트폰 -> 프록시 서버와 통신, 프록시 서버와 출석 인증 서버와 통신 하므로 출석 인증 서버는 데이터가 암호화가 되지 않고 보여짐.

## 발생 하는 문제
1. 프록시 서버로 중간에 데이터를 거치게 되는 경우 AES로 암호화 하거나, 디피 헬만 키 교환을 하더라도 데이터를 전부 확인 가능함.
2. 2019년에 있었던 원격으로 출석 체크가 가능함.
3. 데이터가 전부 보이므로 SQL Injection 또는 추가적인 위험이 발생할 가능성이 있음.

## 해결 방법
1. 서버단에서 SSL 인증서 검증
2. 클라이언트에서 SSL Pinning 적용.
<br>  클라이언트를 변조 할 경우 무의미 함.
3. 데이터 자체를 암호화 함.

## 구현 방법
1. Fiddler 웹 디버거를 이용하여 스마트폰과 컴퓨터 연결
2. Fiddler의 SSL 인증서를 등록하고 통신 함.
3. Fiddler에 데이터를 볼 수 있음.

### 현재 버그는 수정이 되었습니다. (2020년 12월 9일) 
```
[차주형] [오후 1:22] **대학교, 학생 출석에서 보안이슈가 있어 연락을 드립니다
[U-Check 고객지원] [오후 1:23] 네 맞습니다.
[U-Check 고객지원] [오후 1:23] 문의 내용 말씀 해 주시겠어요?
[차주형] [오후 1:24] 상담직원 인가요? 아니면 엔지니어분 인가요??
[U-Check 고객지원] [오후 1:24] 상담담당하고 있습니다. 내용 말씀 해 주시면 담당자에게 전달 드리도록 하겠습니다.
[차주형] [오후 1:26] 스마트폰과 서버 통신에서 중간자 공격을 통하면 패킷을 전부 확인이 가능하고, 해당 방식으로 원격 출석 가능합니다.
[차주형] [오후 1:27] 더 자세한건 엔지니어 분과 이야기를 하고 싶습니다.
[U-Check 고객지원] [오후 1:27] 연락처를 남겨주시면 전달 드리도록 하겠습니다. 학교 학생이신가요?
[차주형] [오후 1:28] ***-****-**** 이고, 학교 학생입니다
[U-Check 고객지원] [오후 1:30] 연락처 전달 드리도록 하겠습니다.
[차주형] [오후 1:30] 네
[차주형] [오후 1:31] 수고많으십니다
[차주형] [오후 2:34] 상담사님 혹시 이런 문제가 계속 발생하게 된다면 학교로 이야기 해서 진행할까요? 아니면 개인적으로 연락을 취할까요? 어떤게 더 편하시겠습니까?
[U-Check 고객지원] [오후 2:41] 저희쪽으로 취약점 알려주시는것도 감사하지만 학교 통해서 보안 패치 요청 해 주시는 편이 나을 것 같습니다.
[U-Check 고객지원] [오후 2:42] 오늘 알려주신 중간자 공격 관련한 SSL 검증처리 추가한건 오늘 배포하여 3$일 정도 지나면 사용 가능하실 거라고 담당자분이 전달 요청 해 주셨습니다.
[차주형] [오후 2:43] 중간자 공격에 보안패치 해주시는것에 감사합니다
[차주형] [오후 2:43] 그러면 혹시 학교와 연결되어있는 연락망 혹시 보내주실수 있으신가요?
[U-Check 고객지원] [오후 2:47] 학사지원팀 051-***-**** &&& 선생님 께서 전자출결 담당 하고 계십니다.
[차주형] [오후 2:59] 감사합니다!
[U-Check 고객지원] [오후 3:05] 네 좋은 하루 보내세요~
[차주형] [오후 3:06] 넵
```