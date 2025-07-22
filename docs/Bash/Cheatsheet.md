# Bash Cheatsheet

## Login with kinit
```bash
printf $PASSWORD | kinit $USERNAME@CONTOSO.COM 
klist
```

## Looping & IFS

IFS stands for "internal field separator". It is used by the shell to determine how to do word splitting, i. e. how to recognize word boundaries.

```bash
THINGS="foo \
bar \
baz"

for THING in THINGS; do
    echo $THING
done
```

```bash
IFS=';'
THINGS="foo;\
bar;\
baz"

for THING in THINGS; do
    echo $THING
done
```

## Unset

```bash
# remove function
unset -f SomeFunction
```

## Find

```bash
# find files in ./dir
find ./dir -type f

# find files in ./dir but not with name hidden
find ./dir -type f -not \( -name hidden \)
```

```plain
-maxdepth 1
-type d 
-name '*.md'
-printf "\"%p\" "
```

## Get top 10 IP address with count of established connections

```bash
ss -tnp | awk '
{
    gsub(/\[::ffff:/, "", $5); gsub(/\]/, "", $5);
    split($5, remote, ":");
    print remote[1];
}' | sort | uniq -c | sort -nr | head -n 10
```
