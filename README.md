# Contextual-Cueing-Unity
The classic contextual cueing paradigm for visual search `(Chun and Jiang, 1998)` in the `Unity Gaming Engine (v. 2019.1.0f2)`. This implementation aims for flexibility in the design of the search displays and will be continuously extended.

**Classic 2D Contextual Cueing (left) and an example Contextual Cueing VR configuration:**
![Classic 2D Contextual Cueing (left) and an example Contextual Cueing VR configuration](https://i.ibb.co/K2GgB8Y/Posterzeugs.png)

**Calibration using Pupil-Labs-Eyetracking inside of Unity:**
![Calibration using Pupil-Labs-Eyetracking inside of Unity](https://github.com/nimarek/Contextual-Cueing-Unity/blob/master/images/ccvr_0.png) 

**An example search display:**
![An example search display](https://github.com/nimarek/Contextual-Cueing-Unity/blob/master/images/ccvr_1.png) 

**Features:**

- The experiment can be performed either directly on a computer with mouse and keyboard or by using a virtual reality headset.
- General workflow: Number of trials within a block and the overall number of blocks can be flexibly defined via the Unity Editor Interface.
- Search displays: Distance, size, number and positioning of individual objects can be flexibly defined via the Unity Editor Interface.
- Inter trial gaze pointer with an adjustable sphere to detect fixations.
- If the same old search display is repeated within one block, a Fisher-Yates shuffle is used to check for repetitions.
- Measurement of reaction times, head movements and display configurations in `/data/sub-**/ folder`.

![Detailed infos](https://github.com/nimarek/Contextual-Cueing-Unity/blob/master/images/cc_vr_2.png) 

Currently objects are placed on a variable number of circles (default: six) surrounding the participant, but future version will include other methods to spawn distractors and targets. The number of repeated and randomly generated search displays can be set separately, as well as the total number of distractors and their placement in virtual space.
