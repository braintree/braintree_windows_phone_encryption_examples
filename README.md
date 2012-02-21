# Braintree Windows Phone Encryption Integration Examples

This project contains examples of integrating with the [Braintree](http://www.braintreepaymentsolutions.com/)
payment gateway using the [Braintree Windows Phone encryption library](https://github.com/braintree/braintree_windows_phone_encryption).

## Windows Phone

An example Windows Phone application that uses the Windows Phone encryption library to encrypt sensitive data and send it to a test server.
See windows_phone/README.md for more information.

## Server

And example Ruby (Sinatra) server that receives requests with encrypted fields from the Windows Phone example application and forwards them
to Braintree's payment gateway using the server-to-server API.  See server/README.md for more information.
