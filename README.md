MUNI - Neuron
===================

###Introduction
This software builds a 3d model of neurons, from a set of axial pictures taken using a conventional light microscope. It also can measure distances in the resulting 3D model.

This project is focus on all that people that study the brain morphology, using the Golgi-Cox method to study the morphology of the neurons to prove their hypothesis. The use of this software will allow the scientific world to see details on the neurons that have never been seen and also it will improve the way to measure and study the nerve cells.

###How it works
The model is generated using the Marching Cubes Algorithm published and patented in SIGGRAPH 1987 but the patent expired in 2005. This algorithm was designed to reconstruct tomographic data in 3D but it was redesigned to work with 2D gray scale photographs.

The principal problem to get it working is to get the isolevel value. This value is a margin to determine what is background and what is part of the 3D structure. My team and I came with a semiautomatic strategy to get the isolevel of the 2D data. In this case the isolevel value is a specific gray tone, because we use conventional photos instead tomographic data.

This software is also capable of measure the 3d model, using the resolution (pixel/micron) of the image set and the micron distance between each plane.

###Motivation
I live in a country that doesn't want to spend money on science so we need to use our imagination and creativity to solve technical problems that have already been solved but cost money. So my university couldn't afford a commercial solution then I decided to focus my thesis to develop Muni a software that will do the same work of the commercial versions and someday it will beat them !.

###License
Although this software was developed using Visual Studio 2010 Express Edition and [TaoFramework 2.1.0](https://sourceforge.net/projects/taoframework/files/The%20Tao%20Framework/). It is distributed freely under the GNU/GPL License, so the binaries and the source code can be downloaded freely.

###Docs / Help
This software is my thesis work. So the documentation about it is very extensive, the only problem is that all the documents are in Spanish. But don't worry I'm working on a English version:

If you don't understand my code or you need help tunning up your microscope to work with Muni you can contact me.

###Downloads
Here you can get the installer, the raw microscopic photos and a 3D model example.

###Change Log
#####Muni v1.0
- This is the first version of Muni
#####Muni v1.1
- The generated models now saves on serialized binary files. So the models generated using previous Muni versions will not be compatible with Muni v1.1.
- The middle button mouse can be use to move the camera around the scene
- All the source code has been commented and the respective CHM file is available.
- The source code had been ported to Visual Studio 2010 C# Express.
- A NSIS installer has been released it includes the binaries, the Tao Framework Libraries and several 3D models.

###TODO List
- Port Muni to Linux using Mono and GTK.
- Calibration on Z axis using just a ruler, instead a dendrite diameter (in microns).
- An automatic dendrite measurement strategy based on the 3D model data.
- Automatize the calibration step.
- Dendrite tree measurement.
