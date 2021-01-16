# Linux Setup

### Docker

```dockerfile
FROM docker.artifactory.uberit.net/centos:7.8.2003
RUN yum install -y krb5-workstation
```

```yaml
#docker-compose
MYService:
  build:
    context: ./MyService
    dockerfile: Dockerfile
  environment:
    KRB5CCNAME: /tmp/mykrb5.ccache 
    KRB5_CONFIG: /mnt/krb5/krb5.conf 
    KRB5_TRACE: /dev/stderr # for debugging
  volumes:
    - type: bind
      source: ./krb5
      target: /etc/krb5
      read_only: true
```


### Creating Keytabs
```bash
# first need to find the kvno
printf $MY_PASSWORD | kinit -c /tmp/temp_cache.ccache $MT_USERNAME@DOMAIN.COM 
KVNO_OUT=$(kvno -c /tmp/temp_cache.ccache krbtgt/DOMAIN.COM@DOMAIN.COM)
# krbtgt/DOMAIN.COM@DOMAIN.COM: kvno = 3

KVNO_VAL=$(echo $KVNO_OUT | sed 's/.*\([[:digit:]]\)$/\1/g')
rm /tmp/temp_cache.ccache
echo $KVNO_VAL

# use ktutil to create the keytab
printf "%s\n" "add_entry -password -p $AWACS_UBERIT_USERNAME@UBERIT.NET -e aes256-cts-hmac-sha1-96 -k $KVNO_VAL" "$AWACS_UBERIT_PASSWORD" "write_kt /tmp/kafkaadapter.kt" "quit" | ktutil

```
