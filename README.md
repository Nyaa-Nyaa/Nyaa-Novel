Nyaa-Novel
==========

Content
-------

1. Introduction
2. Done/Working on/To-do
3. Files

Introduction
------------

Hello, My name is Mio-chan... Well, no, not really. I just watch too much anime and got too into japanese pop culture, but that is beyond the point. The point is, this is a Visual Novel engine created for nyaa-nyaa.com by the head admin. Visual Novels are called such because it's meant to be a story with some interaction, lot's of reading, music, and art. This is an engine written to fufill that niche. The engine itself is written in C# and and stories are put together with XML (Which have an extention of .nyaa to prevent accidental loading of random XML files). There is still a lot to be put in place but there is certainly enough to demo the application. 

Done/Working on/To-do
---------------------

Done:
- General XML loading and creating objects based on various parts
  Heirarchy: 
    - NyaaNovel
      - This class is the whole package, the story will be put into this one object upon loading. The Novel Class holds Chapter objects.
    - NyaaChapter
      - These are chapter objects. These are generated from various folders/files that you specify in the main NyaaNovel XML file. Each chapter has multiple scenes, which are contained within the chapter file.
    - NyaaScene 
      - Scenes are a collection of dialogs with a background and other parameters. 
    - NyaaDialog
      - Each of these contains exactly one dialog and (will soon) support setting flags through choices and having requirements for flags. These also contin paths for the images to display. And a (soon to be made) parameter that can mod the scene, simply named "SceneMod" and (will have) CSS like syntax. ( "bg-image: ./img/bg/2.jpg; music: ./msx/bgm/1.mp3; play-once: ./msx/sfx/surprise" ) 

- Displaying the scene
  Currently displays:
    - Character
    - Background
    - Optional Shadow
    - Text
    - Character Names
    - Background for both texts
    - (Buggy) Animations 

- Simple selection screen
  For testing purposes, a simple file load dialog will be shown at run, as well as allowing you to change scene and chapter to debug.

Working on:
  - Flags
      Crucial to making these novels interactive
  - Animation
      Need better animation

Todo:
  - Music/SFX
  - Character Position Mod
  - Background Zoom Mod
  - SceneMod
      CSS-like Dialog parameters than will change the scene.
  - Bugs... Bugs... More bugs... I'll need to buy more repellent

Files
-----

The ideal file stucture of a NyaaNovel Install is as follows:

	[Root of NyaaNovel Install]
	|--[story]
	| |
	| |--[chapters]
	| | |--[chapter1]
	| | | |--[img]
	| | | | |--[bg]
	| | | | | |- BackgroundImage1.png
	| | | | | |_
	| | | | |--[char]
	| | | | | |- Char1-Pose1.png
	| | | | | |_
	| | | | |--[other]
	| | | | | |- OtherRefrencedImage.png
	| | | | | |_
	| | | | |_
	| | | |--[msx]
	| | | | |-- [bgm]
	| | | | | |- BGM1.mp3
	| | | | | |_
	| | | | |-- [sfx]
	| | | | | |- SFX1.mp3
	| | | | | |_
	| | | | |-- [dialog]
	| | | | | |- Char1-Speech1.mp3
	| | | | | |_
	| | | |- chapter1.nyaa
	| | | |_
	| | |--[chapter2]
	| | | |- {More of the same}
	| | | |_
	| | |_
	| |- story.nyaa
	| |_
	|- NyaaNovel.exe
	|_