# Backend
The Backend repository

--------------------------------------

http://localhost:8000/api/positions
HttpPost requests
Json format 
```
{
    "position": {
        "timestamp":"123123",
        "x":"12",
        "y":"2"
    }
}
```
--------------------------------------

HttpGet request
Get all positions
http://localhost:8000/api/positions


--------------------------------------

HttpGet request
Get all obstacles
http://localhost:8000/api/obstacles

--------------------------------------

HttpPost request
Post an obstacle
http://localhost:8000/api/obstacles
Json format
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

