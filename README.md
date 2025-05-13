# 🛰️ 위성 영상 자동 처리 시스템 (Satellite Image Processor)

**위성 영상 자동 수신 → 전처리 → AI 객체 추론 → 대시보드 시각화까지 전 과정을 자동화한 엔드 투 엔드 시스템**

이 프로젝트는 아리랑 위성 영상 데이터를 기반으로, 영상의 자동 수신, 클라우드 제거 등 전처리, AI 기반 관심 객체 추출, 대용량 파일 저장, 운영자 UI 시각화를 포함한 위성 영상 분석 자동화 시스템입니다.  
재난, 국토, 환경, 자원, 안보 등 다양한 분야에서 실시간 위성 데이터를 효율적으로 활용하기 위한 목적을 가지고 있습니다.

---

## 📌 주요 기능

| 기능 | 설명 |
|------|------|
| 📥 **영상 수신 자동 감지** | 지정 폴더 또는 FTP 서버에서 위성 영상 수신 감지 |
| 🧹 **영상 전처리 모듈** | 클라우드 제거, 밝기 정규화, 메타데이터 추출 등 |
| 🧠 **AI 객체 추론** | ONNX 기반 YOLOv8 모델로 도로, 건물 등 검출 |
| 💾 **결과 및 원본 저장** | 원본/결과 이미지, 추론 결과, 메타데이터 DB 저장 |
| 🖥️ **운영자 대시보드** | 썸네일 뷰, 검색, 필터링 가능한 데스크탑 UI |
| 🔔 **이벤트 알림 시스템** | 객체 이상 탐지 시 알림 메시지 또는 REST 전송 |

---

## 🔧 사용 기술 스택

- **개발 언어**: C# (.NET 6 이상)
- **UI**: WPF (MVVM 패턴)
- **영상처리**: OpenCVSharp, FFmpeg
- **AI 추론**: YOLOv8 ONNX (또는 Unity Sentis)
- **데이터베이스**: SQLite + Entity Framework Core
- **파일/통신 처리**: FileSystemWatcher, SSH.NET (SFTP), REST API
- **로그/알림**: Serilog, 팝업 알림, HTTP 알림 전송

---

## 📂 프로젝트 폴더 구조

    /ArirangGroundProcessor
    ├── /Core # 영상 처리 및 파일 감지 로직
    │ ├── FileWatcher.cs
    │ ├── Preprocessor.cs
    │ ├── InferenceEngine.cs
    │ ├── MetadataExtractor.cs
    │ └── FileStorageManager.cs
    ├── /Database # DB 모델 및 컨텍스트
    │ ├── DetectionResult.cs
    │ ├── ImageInfo.cs
    │ └── DatabaseContext.cs
    ├── /UI # 운영자 UI (WPF)
    │ ├── MainWindow.xaml
    │ ├── ViewModels/
    │ └── Controls/
    ├── /Resources # 테스트용 이미지, 모델 등
    │ ├── SampleImages/
    │ └── Labels/
    ├── /Models # 공통 데이터 모델
    ├── /Utils # 유틸리티 클래스
    └── README.md
