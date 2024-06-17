# VagabondK.Indicators [![NuGet](https://img.shields.io/nuget/v/VagabondK.Indicators.svg)](https://www.nuget.org/packages/VagabondK.Indicators/) [![License](https://img.shields.io/badge/license-LGPL--2.1-blue.svg)](https://licenses.nuget.org/LGPL-2.1-only)
데이터를 화면에 표시하기 위한 .NET 기반 인디케이터 관련 라이브러리입니다.  
그런데 아직은 Digital Indicator 밖에 없네요...  
Analog Indicator는 아직 못 만들었지만, 그래도 공유는 해 놓습니다.  
혹시나 필요하신 분은 얼마든지 가져다 쓰세요~  
개발 과정과 기타 설명은 개인 [블로그 글](https://blog.naver.com/vagabond-k/223481454735)에 기록했습니다.

## WPF 기반 샘플
https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/7800b474-5921-4b70-bd09-58f06d3889b5

## Digital Number
수치형 데이터를 디지털 문자로 화면에 표시하는 기능을 제공합니다.  
주로 사용하는 속성은 다음과 같습니다.
* IntegerDigits
    - 표시할 정수 자리 개수
* DecimalPlaces
    - 표시할 소수 자리 개수
* DecimalSeparatorSize
    - 디지털 문자 세로 크기 대비 소수점 크기 비율
* DecimalPlaceScale
    - 디지털 문자 세로 크기 대비 소수 자리 문자의 크기 비율
* PadZeroLeft
    - true일 때 왼쪽(정수 자리) 빈칸을 0으로 채움
* PadZeroRight
    - true일 때 오른쪽(소수 자리) 빈칸을 0으로 채움
* MinusAlignLeft
    - true일 때 마이너스 기호를 왼쪽 끝에 정렬

## Digital Text
값을 문자열 형식으로 변환하여 디지털 문자들로 표시하는 기능을 제공합니다.  
주로 사용하는 속성은 다음과 같습니다.
* Length
    - 표시할 문자열의 길이
* Format
    - 문자열 변환 포맷

## 7 Segment Font
일곱개의 세그먼트로 문자를 표시하는 기능을 제공합니다.  
주로 사용하는 속성은 다음과 같습니다.  
* Size  
![Size](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/cfa8ef76-d4c0-4442-8712-6d1291905015)
* WidthRatio  
![WidthRatio](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/5c02cba9-92ff-4368-8f07-ecc66884bdaa)  
* SlantAngle  
![SlantAngle](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/5efafac5-30a8-49b5-843e-42da2fe855d7)  
* Thickness  
![Thickness](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/cb143760-b060-4c9b-93af-447f3ce880f3)
* Gap  
![Gap](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/4e1b4d07-2b77-40ae-8d3d-c58023257d44)  
* CornerChamfer  
![CornerChamfer](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/535cbcbb-c4c2-41fc-bd35-b5d4cd371369)  
* MiddleChamfer  
![MiddleChamfer](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/92329b8b-9f79-4c2b-bacf-bc11989d9b3c)  
* CornerGapWarping  
![CornerGapWarping](https://github.com/Vagabond-K/VagabondK.Indicators/assets/75594977/fc64904a-56db-4562-a1d5-b1a01e659acb)  

## 5 × 7 Rounded Rectangle Cell Font
5열, 7행의 둥근 모퉁이 사각형 모양의 셀로 구성된 도트 매트릭스 기반 문자를 표시하는 기능을 제공합니다.  
주로 사용하는 속성은 다음과 같습니다.
* Size
    - 디지털 문자의 세로 크기를 설정합니다.
* WidthRatio
    - 디지털 문자의 기본 레이아웃 너비에 대한 비율을 설정하여 화면에 실제 표시할 너비를 결정합니다.
* SlantAngle
    - 디지털 문자의 길울임 각을 설정합니다.
* Gap
    - 셀 사이의 간격을 설정합니다.
* CornerRadius
    - 셀 모퉁이의 반경을 설정합니다.
