# Getting Started with ESP32

# Installation

- install git for windows
- install rustup https://rustup.rs/ and switch to nightly toolchain

```powershell
rustup default nightly
```

- install VS 2022 Community w/ Desktop C++ workload
- download [xtensa toolchain][1] and put in PATH 
- install [esp-idf][3]
- clone [esp-rs/rust-build][2] install esp toolchain

```powershell
. ./Install-RustToolchain.ps1
rustup toolchain list
# esp should be listed
```

- install build tools

```cmd
"C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat"
cargo install ldproxy
cargo install espflash --version 1.5.1
cargo install espmonitor
```

## Environment

To compile using the esp fork you will need to setup your shell with various variables

```powershell
# open cmd.exe
"C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat"

powershell
. C:\Users\scott\esp\esp-idf\export.ps1
$env:PATH="C:\Users\scott\.espressif\python_env\idf4.4_py3.8_env\Scripts;$env:PATH"

# these vars were generate from running Install-RustToolchain.ps1 -InstallationMode export
$env:PATH="C:\Users\scott/.espressif/tools/xtensa-esp32-elf-clang/esp-14.0.0-20220415-x86_64-pc-windows-msvc/bin/;$env:PATH"
$env:LIBCLANG_PATH="C:\Users\scott/.espressif/tools/xtensa-esp32-elf-clang/esp-14.0.0-20220415-x86_64-pc-windows-msvc/bin/libclang.dll"
$env:PIP_USER="no"
```

## Repos

- [espressif/esp-idf](https://github.com/espressif/esp-idf.git) is the esp development framework
- [esp-rs/rust](https://github.com/esp-rs/rust) a fork of the rust compiler with xtensa support
- [esp-rs/rust-build][2] Releases and install scripts for the esp fork

## Creating a new project

- use cargo generate to template a new project
```powershell
cargo generate --git https://github.com/esp-rs/esp-idf-template cargo
```

- add the following to `.cargo/config.toml` to avoid 
```toml
[env]
...
ESP_IDF_TOOLS_INSTALL_DIR = { value = "global" } # Use the esp-idf dir at ~/.espressif
```



[1]: https://github.com/espressif/crosstool-NG/releases
[2]: https://github.com/esp-rs/rust-build.git
[3]: https://docs.espressif.com/projects/esp-idf/en/latest/esp32/get-started/windows-setup.html