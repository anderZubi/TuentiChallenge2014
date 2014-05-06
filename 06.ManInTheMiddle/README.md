###Challenge 6 - Man in the middle

You now work for the NSA. We have an important service being monitored, and we want to decipher and modify the communications going to and coming from this server. Fortunately, our agents obtained access to both the client and server code so you may study the communication protocol.

As you can see, the client uses a key phrase to obtain a secret message from the server. We have received a key phrase from an informer and we want to know the associated secret message.

We cannot connect directly to the server but we found a backdoor on the client’s router and we were able to install a man-in-the-middle service to spy and modify communications. The address to the service is:
```
54.83.207.90:6969
```
Your mission is to replace the client’s keyphrase with ours and decipher the associated secret message.

Once you connect to the man-in-the-middle service, you will receive strings like:
```
CLIENT->SERVER:hello?
```
This means that the client is sending the message “hello?” to the server. You can then modify the message (if you like) and send it back. The man-in-the-middle service will submit your message to the server.
