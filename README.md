## Run in Docker

### DNS Setup
* Add the following entry to resolve the DNS:
```custom
127.0.0.1 playground.local sts.playground.local is-admin.playground.local is-admin-api.playground.local playground-api.playground.local
```
### Certificates

We also need certificates in order to serve on HTTPS. We'll make our own self-signed certificates with [mkcert](https://github.com/FiloSottile/mkcert). 

> If the domain is publicly available through DNS, you can use [Let's Encypt](https://letsencrypt.org/). Nginx-proxy has support for that, which is left out in this setup.

#### MkCert

##### Create the root certificate

Use [mkcert](https://github.com/FiloSottile/mkcert) to generate local self-signed certificates.

On windows `mkcert -install` must be executed under elevated Administrator privileges. Then copy over the CA Root certificate over to the project as we want to mount this in later into the containers without using an environment variable.

```bash
cd shared/nginx/certs
mkcert --install
copy $env:LOCALAPPDATA\mkcert\rootCA.pem ./cacerts.pem
copy $env:LOCALAPPDATA\mkcert\rootCA.pem ./cacerts.crt
```
##### Create the `skoruba.local` certificates

Generate a certificate for `skoruba.local` with wildcards for the subdomains. The name of the certificate files need to match with actual domain-names in order for the nginx-proxy to pick them up correctly. We want both the crt-key and the pfx version.

```bash
cd shared/nginx/certs
mkcert -cert-file playground.local.crt -key-file playground.local.key playground.local *.playground.local
mkcert -pkcs12 playground.local.pfx playground.local *.playground.local
```