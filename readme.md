
# RabbitMQ Practice Publisher and Consumer

Just for practice.

**DO:**
1. Have Docker
2. Run:`docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management`
3. Enjoy! It is suggested to leave the consumer running while simultaneously running the publisher (with args) to see the messages being delivered realtime.


PS: Messsage deserialization doesnt work *yet*.
