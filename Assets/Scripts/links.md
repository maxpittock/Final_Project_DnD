# camera movemment
https://www.youtube.com/watch?v=cfjLQrMGEb4

# camera zoom 
https://www.youtube.com/watch?v=Txnyxo5M6D0

# making a UI looking good
https://www.youtube.com/watch?v=HwdweCX5aMI
* use this in the report 
    * talk about how the colour pallette was chosen based on advice from this video and that it isnt just randomly mismashed etc.


Explaining binary spcae partioning
https://www.youtube.com/watch?v=S0MNBfc0H_I&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=14

- start with a bounding box (one big room)
    - uses the split algorithms to split the box into different rooms and
        - keeps running until it can no longer split rooms

https://www.youtube.com/watch?v=8Q2VwObN8mo&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=18
- Explains wall theory abit - uses binary values leibniz and ching
    - when there in a floor tile put a 1
    - when there is no floor save it as a 0 
        - save this data to a neighbour empty string
            - neighrbour string is not a binary value that represents the wall tile type we need
                - check tilemap against the list which helps us find out which wall to place in which pos
                - Diagonal values pose more of a challenge as they would be touching multiple floor tiles



To create the different rooms uses the dijstra algorithm - this allows me to create treasure rooms, boss rooms and ememy rooms etc

- treasure rooms 
- normal rooms 
- player spawn room
- boss room

- objects 
    - pillars
    - torchs
    - barrels 
    - boxes
    - rocks
    - tables
    - campfires 
    - bookshelfs
    
https://firebase.blog/posts/2019/07/firebase-and-tasks-how-to-deal-with
