# cloudinteractive-passkit-generator
<img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=Docker&logoColor=white"> <img src="https://img.shields.io/badge/ASP.NET-512BD4?style=for-the-badge&logo=blazor&logoColor=white">

**귀사의 ID 카드, 출입증을 손쉽게 Apple PassKit 프레임워크를 사용하여 Apple Wallet에 추가하십시오.**

이 프로젝트는 Microsoft ASP.NET MVC 프레임워크를 통해 웹에서 사원증, 출입 카드등을 쉽게 Apple PassKit으로 생성하는 도구입니다.

패스의 탬플릿을 생성하고 이를 통해 다양한 패스를 생성할 수 있습니다.

<img src="img/title.png" style="height: 40%;width=auto">

>[!WARNING]
>**ID 카드, 출입증과 같은 공문서를 발급 기관의 명시적인 허가 없이 무단으로 생성하지 마십시오.**
>
> 이 소프트웨어를 사용하여 발급한 패스를 사용하여 발생한 법적 처벌, 불이익에 대하여 어떠한 책임도 지지 않습니다.

### Table of Content
- [Requirements](#requirements)
- [How to Use](#how-to-use)
- [API Documentation](#api-documentation)
- [Dependencies](#dependencies)
- [Showcase](#showcase)

## Requirements

- Linux Docker Engine
- Apple WWDR Intermediate Certificate G4
- Apple Pass Type ID Certificate

>[!NOTE]
>유효한 Apple Pass Type ID Certificate를 발급받기 위해서 Apple Developer Program이 필요합니다.

## How to Use

## Dependencies
- **dotnet-passbook** - https://www.nuget.org/packages/dotnet-passbook/3.2.4?_src=template
- **Newtonsoft.Json** - https://www.nuget.org/packages/Newtonsoft.Json/13.0.3?_src=template
- **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** - https://www.nuget.org/packages/Microsoft.VisualStudio.Azure.Containers.Tools.Targets/1.17.2?_src=template
