# Certificates



```bash
#Add .crt to /usr/local/share/ca-certificates
sudo update-ca-certificates
```

```bash
# get info on cert from server
openssl s_client -connect "hostname.com:443" -showcerts
```

```bash
# test ssl verify on a chain of certs
openssl s_client -connect bedrock-runtime.us-east-1.amazonaws.com:443     -CApath /etc/ssl/certs -showcerts </dev/null 2>&1 | grep -E 'depth|verify|issuer|subject'

depth=2 C = US, O = Amazon, CN = Amazon Root CA 1
verify error:num=2:unable to get issuer certificate # this is the issues
issuer= C = US, ST = Arizona, L = Scottsdale, O = "Starfield Technologies, Inc.", CN = Starfield Services Root Certificate Authority - G2
verify return:1
depth=1 C = US, O = Amazon, CN = Amazon RSA 2048 M02
issuer= C = US, O = Amazon, CN = Amazon Root CA 1
verify return:1
depth=0 CN = bedrock-runtime.us-east-1.amazonaws.com
issuer= C = US, O = Amazon, CN = Amazon RSA 2048 M02
verify return:1
subject=CN = bedrock-runtime.us-east-1.amazonaws.com
issuer=C = US, O = Amazon, CN = Amazon RSA 2048 M02
Verification error: unable to get issuer certificate
Verify return code: 2 (unable to get issuer certificate)
```

```bash
# check k8s cert

kubectl --context <cluster> -n <ns> get secret my-ssl-cert -o json | jq -r '.data."tls.crt"' | base64 -d | openssl x509 -text -noout | grep "Not After"

# ssl from inside a pod with curl or wget
openssl s_client -connect example.com:443 -servername example.com -showcerts </dev/null
openssl s_client -connect example.com:443 -servername example.com -showcerts -CAfile /usr/local/share/ca-certificates/configmap/customCA.crt </dev/null
```
