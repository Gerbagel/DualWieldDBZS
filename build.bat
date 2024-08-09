set QtDir=C:\Qt\6.7.2\mingw_64\bin

if not exist "release" mkdir release

copy build\Desktop_Qt_6_7_2_MinGW_64_bit-Release\DualWieldDBZS.exe release\

windeployqt.exe --release release\DualWieldDBZS.exe

copy "%QtDir%\libgcc_s_seh-1.dll" release\
copy "%QtDir%\libstdc++-6.dll" release\
copy "%QtDir%\libwinpthread-1.dll" release\
pause