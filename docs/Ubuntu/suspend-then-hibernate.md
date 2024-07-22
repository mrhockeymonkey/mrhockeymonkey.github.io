# Suspend Then Hibernate

Use a swap partition, I had no success with swapfile

```bash
# find devide id
swapon --show
NAME      TYPE      SIZE USED PRIO
/dev/sda2 partition 9.3G   0B   -2

sudo nano /etc/default/grub

# update line
# GRUB_CMDLINE_LINUX_DEFAULT="quiet splash resume=/dev/sda2"

sudo nano /etc/systemd/sleep.conf

# HibernateDelaySec=300

# test it works
sudo systemctl sleep-then-hibernate
```

If that all works correctly you can configure the system

```bash
# to hibernate when idle...
# (this setting doesnt support suspend-then-hibernate)
# https://ubuntuhandbook.org/index.php/2021/06/automatic-shutdown-hibernate-on-idle-ubuntu/
gsettings set org.gnome.settings-daemon.plugins.power sleep-inactive-battery-type 'hibernate'
gsettings set org.gnome.settings-daemon.plugins.power sleep-inactive-battery-timeout 1800

# to hibernate by event

# sudo nano /etc/systemd/logind.conf

HandleLidSwitch=suspend-then-hibernate
HandleLidSwitchExternalPower=suspend-then-hibernate
...
IdleAction=lock
IdleActionSec=5min

# idle action lock becuase suspend seems to block the hibernate after timeout above...

```

To fix wifi adapter missing after hibernate for my Surface 3 Pro

```bash
# find the driver in use for that card
lspci -nnk | grep -A3 Ethernet

01:00.0 Ethernet controller [0200]: Marvell Technology Group Ltd. 88W8897 [AVASTAR] 802.11ac Wireless [11ab:2b38]
	Subsystem: SafeNet (wrong ID) 88W8897 [AVASTAR] 802.11ac Wireless [0001:045e]
	Kernel driver in use: mwifiex_pcie # < this thing
	Kernel modules: mwifiex_pcie

# script unload and reload when sleeping with systemd and make it executable
ll /lib/systemd/system-sleep/
total 24
drwxr-xr-x  2 root root 4096 Jul 22 21:56 ./
drwxr-xr-x 19 root root 4096 Apr 24 11:48 ../
-rwxr-xr-x  1 root root   92 Oct  6  2022 hdparm*
-rwxr-xr-x  1 root root  227 Jan  9  2024 sysstat.sleep*
-rwxr-xr-x  1 root root  219 Feb 12 17:50 unattended-upgrades*
-rwxr-xr-x  1 root root  187 Jul 22 21:56 wifi_sleep*  # < make this file executable

cat /lib/systemd/system-sleep/wifi_sleep 
#!/bin/sh

case "$1" in
	pre)
		# Unload
		modprobe -r mwifiex_pcie
		#touch /home/scott/suspending
		;;
	post)
		# Reload
		modprobe mwifiex_pcie
		#touch /home/scott/resuming
		;;
esac


```
