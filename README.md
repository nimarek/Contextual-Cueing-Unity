# Contextual-Cueing-Unity
The classic contextual cueing paradigm for visual search (Chun and Jiang, 1998) in the Unity Gaming Engine (v. 2019.1.0f2). This implementation aims for flexibility in the design of the search displays and will be continuously extended. Currently objects are placed on a variable number of circles (default: six) surrounding the participant, but future version will include other methods to spawn distractors and targets.

**An example search display**
![An example search display](https://github.com/nimarek/Contextual-Cueing-Unity/blob/master/images/ccvr_1.png)

The experiment can be performed either directly on a computer with mouse and keyboard or by using a virtual reality headset. Display configurations, reaction times and (if VR is active) head movement data are stored individually in a /data/ folder. 

The number of repeated and randomly generated search displays can be set separately, as well as the total number of distractors and their placement in virtual space.
