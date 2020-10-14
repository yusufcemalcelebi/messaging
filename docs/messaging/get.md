# Get User Messagings

Get the messagings of the current user with the specified receiver user. Pagination is implemented to provide performance optimization.

**URL** : `/api/v1/Messaging/`

**Method** : `GET`

**Auth required** : YES, JWT must be added to the Authorization header

## Query parameter examples

***ReceiverId***:1
***Size***:15
***Page***:1

## Success Response

**Code** : `200 OK`

**Content examples**

```json
{
  "response": {
    "receiverId": 1,
    "senderId": 2,
    "sentMessages": [
      {
        "text": "Nasılsın",
        "date": "2020-10-14T16:31:28.4728295"
      },
      {
        "text": "Hello ben fatih",
        "date": "2020-10-14T16:31:20.5412261"
      }
    ],
    "receivedMessages": [
      {
        "text": "Hello world",
        "date": "2020-10-14T16:23:53.0478534"
      }
    ],
    "size": 3,
    "page": 1
  }
}
```