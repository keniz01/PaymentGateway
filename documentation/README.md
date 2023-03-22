Testing

GET - api/v1/payment-gateway/vendor-account
---------------------------------------------
curl http://localhost:5123/api/v1/payment-gateway/vendor-account -H 'Content-Type: application/json' -H 'X-MEMBER-ID: 600010' -H 'X-CORRELATION-ID: ffae0601-15b1-452e-8f6b-76c33b030176' -H 'X-METAMEMBER-ID: 600010'

POST - api/v1/payment-gateway/vendor-account
---------------------------------------------
curl -X POST http://localhost:5123/api/v1/payment-gateway/vendor-account -H 'Content-Type: application/json' -H 'X-MEMBER-ID: 600010' -H 'X-CORRELATION-ID: ffae0601-15b1-452e-8f6b-76c33b030176' -H 'X-METAMEMBER-ID: 600010' -d '{"MemberId":600010,"MetaMemberId":600150,"ApiSecretKey":"sk_test_dvyisdgf9ebweusdf983DSFDS3udu","ApiPublicKey":"sk_test_dvyisdgf9ebweusdf983DSFDS3udu","IsActivated":true}'

docker-compose run postgres-db bash
psql --host=postgres-db --username=postgres --dbname=postgres