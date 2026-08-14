{
  description = "A very basic flake";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
  };

  outputs =
    { self, nixpkgs }:
    let
      pkgs = nixpkgs.legacyPackages.x86_64-linux;
    in
    {

      devShells.x86_64-linux.default = pkgs.mkShell rec {

        dotnetPkg =
          with pkgs.dotnetCorePackages;
          combinePackages [
            # sdk_11_0
            sdk_10_0
            sdk_8_0
            sdk_7_0
          ];

        deps = [
          pkgs.zlib
          pkgs.zlib.dev
          pkgs.openssl
          pkgs.fontconfig
          pkgs.libX11
          pkgs.libice
          pkgs.libsm
          pkgs.icu
          dotnetPkg
        ];

        NIX_LD_LIBRARY_PATH = pkgs.lib.makeLibraryPath (
          [
            pkgs.stdenv.cc.cc
          ]
          ++ deps
        );
        LD_LIBRARY_PATH = NIX_LD_LIBRARY_PATH;

        NIX_LD = "${pkgs.stdenv.cc.libc_bin}/bin/ld.so";
        nativeBuildInputs = [
          pkgs.vscode
          # pkgs.vscodium
          pkgs.csharpier
        ]
        ++ deps;

        DOTNET_ROOT = "${dotnetPkg}";
      };

    };
}
