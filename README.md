# Camera Trigger Api Core

Camera trigger logging API used to feed into a react front end located at https://camera-trigger-consumer.azurewebsites.net/

# Design Decisions
1. Using .Net core 2.0
1. Creation of entries is handled as text/plain. This is due to a limitation on the software feeding the database which I cannot change (Blue Iris)
