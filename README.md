# capstone_2
캡스톤 2차

# 팀 프로젝트: Unity 2D 기반 게임 개발

## 사용 언어 및 개발 환경
- **언어**: C#  
- **개발 환경**: Unity 2D  

## 개발 기간
- **2023.03 ~ 2023.05 (3개월)**

## 구성원 및 역할
- **구성원**: 3명  
- **역할**: 팀장, 클라이언트 개발  

---

## 기술적 고민

### 1. Firebase 연동 구현
로그인 시스템 구현 시 Firebase Authentication을 사용하여 사용자의 인증을 처리했습니다.  
- **도입한 인증 방식**: 이메일/비밀번호 기반 인증  
- **보안 문제 해결**:  
  - 각 사용자가 자신의 게임 기록을 저장하고 불러올 수 있도록 데이터베이스 구조 설계  
  - 인증된 사용자만 데이터에 대한 읽기 및 쓰기 권한 부여  
  - Firebase 보안 규칙을 활용하여 사용자 데이터가 다른 사용자에게 노출되거나 무단 수정되는 것을 방지  

이러한 과정을 통해 사용자 데이터 보호와 데이터 접근 관리에 대한 실질적인 경험을 쌓을 수 있었습니다.

---

## 회고

이번 프로젝트는 **팀장**으로서 **3명의 팀원**과 함께 진행한 팀 프로젝트였습니다.  

### 주요 활동
1. **기획 협력**  
   - 프로젝트 초기에는 기획 역할을 맡은 팀원과 협력하여 게임의 방향성과 요구사항을 명확히 정의했습니다.  

2. **주기적인 회의**  
   - 매주 진행 상황과 문제점을 공유하며, 문제 해결 방안을 논의했습니다.  

3. **UI와 그래픽 작업**  
   - UI 디자인과 그래픽 작업은 사용자 경험에 큰 영향을 미치는 요소였으며, 기획과 개발 간의 원활한 소통이 중요했습니다.  

### Firebase Realtime Database 적용
- 게임 점수를 기록하고 랭킹 시스템을 구현하며 사용자 데이터 보호 및 보안 규칙 설정을 중점적으로 다뤘습니다.  
- **구현 방식**:  
  - 사용자 ID를 기준으로 데이터를 계층화  
  - Firebase 보안 규칙 설정을 통해 각 사용자가 자신의 데이터에만 접근하도록 제한  

### 팀장으로서의 역할
- **역할 분담 명확화**: 각 팀원의 책임 의식을 높이고, 원활한 협업 환경 조성  
- **소통 강화**: 팀원들과의 지속적인 소통으로 문제를 빠르게 파악하고 해결  

### 프로젝트 완료 후 느낀 점
- 명확한 역할 분담은 프로젝트 진행 속도를 높이고, 팀원들의 책임감을 강화하는 데 크게 기여했습니다.  
- 성공적인 프로젝트 완수를 통해 팀 프로젝트에서 **소통**과 **책임감**의 중요성을 깊이 깨달을 수 있었습니다.
