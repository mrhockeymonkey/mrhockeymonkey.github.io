# Certificates

```bash
# get info on cert from server
openssl s_client -connect "hostname.com:443" -showcerts
```

```bash
# check k8s cert

kubectl --context <cluster> -n <ns> get secret my-ssl-cert -o json | jq -r '.data."tls.crt"' | base64 -d | openssl x509 -text -noout | grep "Not After"

# ssl from inside a pod with curl or wget
openssl s_client -connect example.com:443 -servername example.com -showcerts </dev/null
openssl s_client -connect example.com:443 -servername example.com -showcerts -CAfile /usr/local/share/ca-certificates/configmap/customCA.crt </dev/null
```
