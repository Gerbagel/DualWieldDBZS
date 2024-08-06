# DualWieldDBZS

## Prerequisites
- [Qt Creator](https://www.qt.io/download-open-source)
- [NSIS 3.0+](https://nsis.sourceforge.io/Main_Page) (for building the installer)

## Setup 
Qt Creator will need some kind of build environment installed. The primary one used is `Qt 6.7.2 MinGW 64-bit`.

If using the recommended build environment, the binaries folder will need to be added to environment variables. This should be under `C:/Qt/6.7.2/mingw_64/bin`

## Build
To build, open the project in Qt Creator and build the Release profile. 

Then run the `build.bat` file in the root directory. It will create a `release` folder where all necessary binaries are included.

Installer to be implemented at a later time.

This assumes the recommended build environment is being used. If anything else is used, the build script will need to be adapted.