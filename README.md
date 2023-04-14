# Backend
The Backend repository

## API Endpoints
#### Live server: https://ims-group4-backend.azurewebsites.net
-------------
### **Positions**

**POST** /api/positions
##### Body:
```
{
    "position": {
        "timestamp":"123123",
        "x":"12",
        "y":"2"
    }
}
```
-------------

**GET** /api/positions
##### Result:
```
[
    {
        "timestamp": "12243123",
        "x": 12,
        "y": 2
    }
]
```
-------------

### **Obstacles**

**GET** /api/obstacles
##### Result:
```
[
    {
        "timestamp": "1234567",
        "x": 1,
        "y": 1,
        "base64_image": "your_image_string_here"
        "infos_image": {
            "Confidence": 0,
            "Description": "Plant",
            "Locale": "",
            "Mid": "/m/05s2s",
            "Score": 0.9638823,
            "Topicality": 0.9638823
        }
    }
]
```
-------------

**POST** /api/obstacles
##### Body:
```
{
    "obstacle": {
        "base64_image": "yourimageinbase64",
        "timestamp":"123123",
        "x":"12",
        "y":"2"
    }
}
```
-------------
