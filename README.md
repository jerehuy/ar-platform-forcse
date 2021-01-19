# ar-platform-forcse

## Table of Contents

1. [About the project](https://github.com/jerehuy/ar-platform-forcse/blob/master/README.md#about-the-project)
2. [Getting started](https://github.com/jerehuy/ar-platform-forcse/blob/master/README.md#getting-started)
   * [Prerequisites](https://github.com/jerehuy/ar-platform-forcse/blob/master/README.md#prerequisites)
   * [Installation](https://github.com/jerehuy/ar-platform-forcse/blob/master/README.md#installation)
3. [Usage](https://github.com/jerehuy/ar-platform-forcse/blob/master/README.md#usage)

## About the project

This is a augmented reality application, done for a project work course in Tampere University. This application was commissioned for Amuri Museum of Workers' Housing.
The purpose of this application was to offer a way for visitors to use their smartphones to scan objects, and use their location to access augmented reality material.
This application was done with Unity version 2020.1.4f1. The target platform for this application is Android, with the application tested on Android version 11.

## Getting started

In order to use this application, follow these steps.

### Prerequisites

1. Make sure you are using Unity version 2020.1.4f1.
2. Make sure you have the following packages:
   * AR Foundation 3.1.3.
   * ARCore XR Plugin 3.1.5.
   * ARKit XR Plugin 3.1.3.
   * TextMeshPro 3.0.1.
   * Timeline 1.3.5.
   * Unity UI 1.0.0.
3. Make sure you have smartphone running on Android.

### Installation

In order to install the application, follow these steps.

1. First you need to copy this repository. To see how this is done, follow guide on how to copy projects using GIT. 
You can also go to [code](https://github.com/jerehuy/ar-platform-forcse) and press the green "code" button on the side, and then download this project as a ZIP.
2. After that go to your Unity and open this project.
3. Inside the Unity application, go to "File" > "Build settings", make sure you have chosen all scenes to build and your platform is Android. After that, press "Build" to build
the application.
4. After you have successfully build the application, copy the APK file you just created and send it to your smartphone.
5. On your smartphone, find the APK file and press it. This should let you install the application.

## Usage

In order to use the application, make sure you have given permission for the application to use your GPS and camera. In the app you can scan new objects, which give you access
to new text information and audio files. You can reset the scanned object by scanning a new object or pressing the button on top of the view, next to the name of the scanned 
object.
