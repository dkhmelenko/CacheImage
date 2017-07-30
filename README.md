# Project description
[![Build status](https://ci.appveyor.com/api/projects/status/0n3fe76exx2hx6f6?svg=true)](https://ci.appveyor.com/project/brave-warrior/cacheimage)
[![Latest stable version](https://img.shields.io/nuget/v/CacheImage-UWP.svg)](https://www.nuget.org/packages/CacheImage-UWP/)

CacheImage (Silverlight) for WP8/WP8.1

[![Latest stable version (deprecated) ](https://img.shields.io/nuget/v/CacheImage.svg)](https://www.nuget.org/packages/CacheImage)  (*deprecated*)

CacheImage is a lightweight library, which introduces new control - CacheImage for Windows Phone 8/8.1 (Silverlight) and Universal Windows Platform (UWP Windows 10). This control can be used for caching images on disk. As soon as image loaded from the web, local copy will be used. The file name of the local copy contains a hash of the web link. Also, this library provides an ability to set up placeholder, while the image is downloading. The idea of the control came from [here](http://chandermani.blogspot.de/2012/05/caching-images-downloaded-from-web-on.html).

# Usage
```xaml
xmlns:lib="using:CacheImage"
    
...
    
<lib:CacheImage Url="http://www.google.com/doodle4google/images/d4g_logo_global.jpg" 
                Placeholder="/Assets/placeholder.png"
                />
```
Control contains the following own properties:
- `Url` - link to the image in the network starting with http or https
- `Placeholder` - path to the local image for using as a placeholder
- `DecodePixelHeight` - appropriate property of BitmapImage of the main image
- `DecodePixelWidth` - appropriate property of BitmapImage of the main image
- `DecodePixelHeightPlaceholder` - property DecodePixelHeight of BitmapImage of the placeholder image
- `DecodePixelWidthPlaceholder` - property DecodePixelWidth of BitmapImage of the placeholder image
- `Stretch` - appropriate property of BitmapImage of the main image
- `StretchPlaceholder` - property Stretch of BitmapImage of the placeholder image

# Contribution
If you have any suggestions or ideas, feel free to create pull request.

# Warranties
Currently the version of the library for UWP (Windows 10) is the main implementation. 

Library for Windows Phone 8/8.1 (Silverlight) is now *deprecated* and is not recommended for using because of the performance issues.

In case you found any problem, please submit an issue or feel free to create a pull request.

# License

[Apache Licence 2.0](http://www.apache.org/licenses/LICENSE-2.0)

Copyright 2015 Dmytro Khmelenko

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
