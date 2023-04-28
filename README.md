## FinalProject
CSCI 39537 Intro to APIs - MonsterHunter APIs
<br />
Due 4/26/2023 at 11:40PM
<br />
Rei Imai (24134776)
<br />
<br />

## Submission Checklist:
1. `README.md` on root repo ✅
2. `.sql` code for database ✅
3. three API endpoints and three HTTP methods ✅
4. at least one controller ✅
5. basic response model (statusCode, statusDescription, list of items) ✅

<br />
<br />

## Documentation:
what's this API about?
> Monster Hunter API allows a client to see information about player, player's weapon equipment, and its weapon type.

### Endpoints and HTTP methods:
> 3 endpoints are following: <br />
`api/Player` <br />
`api/Weapon` <br />
`api/WeaponType` <br/>

> HTTP methods for each endpoint: <br />
`GET, api/Player` will return a list of all players <br />
`GET, api/Player/ID` will return a player of ID specified <br />
`POST, api/Player` will add a new player to the players list <br />
`PUT, api/Player/ID` will update the player information <br />
`DELETE, api/Player/ID` will delete the player with ID from the list <br />
`GET, api/Weapon` will return a list of all weapons <br />
`GET, api/Weapon/ID` will return a weapon of ID specified <br />
`POST, api/Weapon` will add a new weapon to the weapons list <br />
`PUT, api/Weapon/ID` will update the weapon information <br />
`DELETE, api/Weapon/ID` will delete the weapon with ID from the list <br />
`GET, api/WeaponType` will return a list of all weapon types <br />
`GET, api/WeaponType/ID` will return a weapon type of ID specified <br />



### Sample Request Body:
1. `POST api/Player` <br />
> if you want to add Player with unspecified weapon:
```
{
  "playerName": "example_name", 
  "weapon": null
}
```
> if you want to add Player with specified weapon: <br />
where that weapon already exist in the database,
```
{
  "playerName": "example_name",
  "weapon": {
    "weaponID": integer, // pick weaponID that exist in GET api/Weapon
    "weaponType": null,
    "weaponName": "anyName", // as long as weaponID is correct, 
    // other properties will be automatically corrected to the ones match with weaponID
    // such as weaponType, weaponName, atk, critical
    "atk": any_integer,
    "critical": any_integer
  }
}
```
> if you want to add Player with specified weapon: <br />
where that weapon DOES NOT exist in the databse,
```
{
  "playerName": "example_name",
  "weapon": {
    "weaponID": integer, // make sure you pick number that's NOT in GET api/Weapon
    "weaponType": {
      "weaponTypeID": integer, // between 1~14, otherwise throws bad response
      "weaponTypeName": "anyName" // as long as weaponTypeID is correct, same logic as above
    },
    "weaponName": "newWeapon_name",
    "atk": any_integer,
    "critical": any_integer
  }
}
```
2. `PUT api/Player/ID` <br />
> if you want to update Player, into NO weapon: <br />
```
{
  "playerID": ID, // make sure it matches with ID written in your endpoint
  "playerName": "modified_name", // doesn't have to be diffent name
  "weapon": null
}
```
> if you want to update Player, into different weapon: <br />
where that weapon DOES or DOES NOT exist in the database, <br />
it's the same logic as `POST api/Player`, so I will skip here 👍 <br /> <br />

3. `POST api/Weapon` <br />
```
{
  "weaponType": { // CANNOT BE NULL!!! it will throw a bad response anyways
    "weaponTypeID": integer, // between 1~14
    "weaponTypeName": "anyName" // it will be automatically corrected with the one matches with weaponTypeID
  },
  "weaponName": "newWeapon_name",
  "atk": any_integer,
  "critical": any_integer
}
```
4. `PUT api/Weapon/ID` <br />
```
{
  "weaponID": integer, // make sure it matches with ID written in your endpoint
  "weaponType": { // CANNOT BE NULL
    "weaponTypeID": integer, // between 1~14
    "weaponeTypeName": "anyName" 
  },
  "weaponName": "modified_name", // doesn't have to be modified
  "atk": modified_integer,
  "critical": modified_integer
}
```



### Sample Response Body:
> this is a response body for `GET api/Weapon/4` <br />
```
{
  "statusCode": 200,
  "statusDescription": "GET successful",
  "weapons":[
    { 
      "weaponID": 4,
      "weaponType":{
        "weaponTypeID": 4,
        "weaponTypeName": "Dual Blades"
      },
      "weaponName": "Kamura Glintblades I",
      "atk": 50,
      "critical": 0
   }
  ]
}
```

<br />
<br />

## Problems/Concerns:
Wanted to add more to the database about Monsters too, but it's going to be useless for this project (since it's not connected to any of other tables) so I decided not to include here. Also I gave up implementing Armors tables since it's too many. I wanted this project to be less data. 😃 <br />

<br />
<br />

## MonsterHunter Rise/SunBreak:
it's a very good game you should play.
