.PHONY: all release-linux release-windows

all: release-linux release-windows

release-linux:
	cargo build --release --target x86_64-unknown-linux-gnu

release-windows:
	cargo build --release --target x86_64-pc-windows-gnu

package: target/x86_64-unknown-linux-gnu/release/DualWieldDBZS target/x86_64-pc-windows-gnu/release/DualWieldDBZS.exe
	tar -czvf DualWieldDBZS-release-linux.tar.gz target/x86_64-unknown-linux-gnu/release/DualWieldDBZS
	zip -j DualWieldDBZS-release-windows.zip target/x86_64-pc-windows-gnu/release/DualWieldDBZS.exe
