# Backend
The Backend repository

## API Endpoints
#### Live server: https://ims-group-4-backend-david.azurewebsites.net

-------------
### **Session**

**GET** /api/new_session
##### Result:
```
{
    true
}
```
-------------

### **Positions**

**POST** /api/positions
##### Body:
```
[
    {
        "position": {
            "x": 12,
            "y": 2
        }
    },
    {
        "position": {
            "x": 13,
            "y": 3
        }
    }
]
```
-------------

**GET** /api/positions
##### Result:
```
[
    {
        "x": 12,
        "y": 2
    }
]
```
-------------

### **Obstacles**

**GET** /api/obstacles/coordinates
##### Result:
```
[
    {
        "x": 12,
        "y": 2
    }
]
```
-------------

**GET** /api/obstacles
##### Result:
```
[
    {
        "x": 1,
        "y": 1,
        "base64_image": "your_image_string_here"
        "infos_image": [
            {
                "confidence": 0,
                "description": "Plant",
                "locale": "",
                "mid": "/m/05s2s",
                "score": 0.9638823,
                "topicality": 0.9638823
            }
        ]
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
        "x":"12",
        "y":"2"
    }
}
```
-------------

### **Surroundings**

**GET** /api/surrounding
##### Result:
```
[
    {
        "x": 1,
        "y": 0
    }
]
```
-------------

**POST** /api/surrounding
##### Body:
```
[
    {
        "x": 1,
        "y": 0
    },
    {
        "x": 1,
        "y": 1
    }
]
```
-------------

