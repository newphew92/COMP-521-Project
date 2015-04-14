The unity package was made with Unity 5. It will not work on a lower version. In case you do not have Unity 5 we have included a rar file with our workspace which contains all scripts.

--------------

Builds have been made for AI vs AI in all 3 scenes as these are the most interesting scenarios.

Unfortunately, the build for scene 2 does not work correctly although it does in the editor.

Since there are many different parameter settings to choose from, we have decided to include an exported package to ease evaluation and testing.


There are two prefabs, red and blue bots.

In order to change which script drives their AI, either the "Unit Controller" script or "Naive"  can be active (not both!).

After choosing which script to play from, press "apply" in the top right button in the prefab option bar. This will apply the changes to all the prefab instances.

In the inspector tab, there is a checkbox for "Cheating" at the "Naive" script component. 

This checkbox can be used to toggle wether or not the Naive script is cheating or not.

-----

To adjust terrain analysis, click on the game object called "Level" and choose the type via the dropdown menu. You can also change the influence radius of each unit, how intense the influence is on the tile is standing on, the maximum displayed tile intensity, the maximum intensity that can be generated from terrain analysis and a number of other settings however we recommend leaving those alone as they are currently set to what we used to test with.