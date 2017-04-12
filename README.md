
<img src='icons/preview.gif' />

<img src='icons/icon.png' width='150' height='150' align='right' />

# Voice Logger &nbsp; &nbsp; &nbsp; &nbsp; [![Build status](https://ci.appveyor.com/api/projects/status/9y0w7cl80g4874ia?svg=true)](https://ci.appveyor.com/project/william-taylor/voice-logger) [![Open Source Love](https://badges.frapsoft.com/os/v1/open-source.svg?v=102)](https://github.com/ellerbrock/open-source-badge/)  [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

Voice Logger is an application the uses Microsoft's Kinect (V1) for recording voice logs that can then be exported to a ZIP file for storage or sharing. The idea was instead of putting comments in the code for myself to refer back to after a weekend of coding I could record voice logs instead. I used the Kinect as it was a prize I won in the recent university competition and I was excited to use it.

## Overview

The application was written in C# and uses the Kinect SDK for voice recognition and NAudio for audio file creation. The UI is WPF based with help from the Metro UI framework which helps give the app modern styling to look more professional. The application uses the built in voice recognition technology found in the Kinect so interaction with the GUI isn't necessary and instead commands can be issued via voice saving time and means the app doesn't get in the way of coding. 
 
## Development

While I am not planning to add to this project in the near future there are many features I could implement, these potential options are listed below.

* Screenshot feature
* Microphone backup
* Kinect V2 port
* MVVM refactor

## License

Apache 2.0
