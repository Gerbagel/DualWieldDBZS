if not exist "release" mkdir release

windeployqt.exe --release --dir release build\Desktop_Qt_6_7_2_MinGW_64_bit-Release\DualWieldDBZS.exe

copy build\Desktop_Qt_6_7_2_MinGW_64_bit-Release\DualWieldDBZS.exe release\