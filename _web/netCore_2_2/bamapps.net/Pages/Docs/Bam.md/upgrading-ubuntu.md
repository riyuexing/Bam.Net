# Upgrading Ubuntu

This document describes how to upgrade an ubuntu instance as of 02/09/2019.  Taken from [linuxconfig.org](https://linuxconfig.org/how-to-upgrade-to-ubuntu-18-04-lts-bionic-beaver).

Update your system:

```
sudo apt update
sudo apt upgrade
sudo apt dist-upgrade
```

Remove unused packages:

```
sudo apt autoremove
```

Install update manager:

```
sudo apt install update-manager-core
sudo do-release-upgrade
```