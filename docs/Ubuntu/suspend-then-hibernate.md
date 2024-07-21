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
PowerKeyIgnoreInhibited=yes
...
IdleAction=lock
IdleActionSec=5min

# idle action lock becuase suspend seems to block the hibernate after timeout above...

```
