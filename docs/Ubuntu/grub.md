# Grub Bootloader

```bash
# config files
ls /etc/default/grub
ls /etc/grub.d

# update changes to /boot/grub/grub.cfg
sudo update-grub

# see the result and look for menu entires
sudo less /boot/grub/grub.cfg
```

```plain
menuentry 'Ubuntu' --class ubuntu --class gnu-linux --class gnu --class os $menuentry_id_option 'gn
ulinux-simple-34e260a8-9589-463b-9f65-8eb479eeef08' {
        recordfail
        load_video
        gfxmode $linux_gfx_mode
        insmod gzio
        if [ x$grub_platform = xxen ]; then insmod xzio; insmod lzopio; fi
        insmod part_gpt
        insmod ext2
        search --no-floppy --fs-uuid --set=root 34e260a8-9589-463b-9f65-8eb479eeef08
        linux   /boot/vmlinuz-6.11.0-19-generic root=UUID=34e260a8-9589-463b-9f65-8eb479eeef08 ro
quiet splash resume=/dev/nvme0n1p6 $vt_handoff
        initrd  /boot/initrd.img-6.11.0-19-generic
}
```

Windows entry is found by the os-probe script in grub.d

```plain
menuentry 'Windows Boot Manager (on /dev/nvme0n1p2)' --class windows --class os $menuentry_id_optio
n 'osprober-efi-1AB6-7746' {
        insmod part_gpt
        insmod fat
        search --no-floppy --fs-uuid --set=root 1AB6-7746
        chainloader /EFI/Microsoft/Boot/bootmgfw.efi
}
```

## To Add a Safemode option

You cn add `/SAFEBOOT:Minimal` or `SAFEBOOT:Network` to the menuitem found by os-probe

```bash
sudo nano /etc/grub.d/40_custom
```

```plain
menuentry 'Windows Boot Manager (Safe Mode on /dev/nvme0n1p2)' --class windows --class os $menuentry_id_option 'osprober-efi-1AB6-7746' {
    insmod part_gpt
    insmod fat
    search --no-floppy --fs-uuid --set=root 1AB6-7746
    chainloader /EFI/Microsoft/Boot/bootmgfw.efi /SAFEBOOT:Minimal
}
```

## Grub command line

On the grub menu you can press `c` to get a grub command line

```plain
# list disk and partitions
ls

# look for files
ls (hd0,gpt1)/efi/Microsoft/Boot
```
