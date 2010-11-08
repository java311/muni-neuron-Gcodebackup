using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace Muni
{
    /// <summary>
    /// This class implements the Marching Cubes Algorithm
    /// </summary>
    class MarchingCubes
    {
        private System.Globalization.CultureInfo es = new System.Globalization.CultureInfo("es-MX");
        private int dimx, dimy, dimz;   //The dimentions image package
        private int isolevel;           //The calculated isolavel value used to discern a neuron from the background
        private int cube_x;             //The X length of the marching cube
        private int cube_y;             //The Y length of the marching cube
        private string[] filenames;     //
        private ProgressBar progressbar;   //This is a progress bar object to show the progress of the task
        private Label label_progress;      //This label show the current progress of the task
        private Button cancel;             //This is the cancel button to cancel the reconstruction
        private Button finish;             //This is the finish button from the Image_Form
        
        
        #region EdgeTable
        /// <summary>
        /// This table is very important for the reconstruction task, this table saves all the posible combinations
        /// of edges intersected by the isosurface. These intersected combinations can only be consulted using the 
        /// edgeindex value.
        /// </summary>
        private int[] edgeTable = new int[]
        {
            0x0  , 0x109, 0x203, 0x30a, 0x406, 0x50f, 0x605, 0x70c,
            0x80c, 0x905, 0xa0f, 0xb06, 0xc0a, 0xd03, 0xe09, 0xf00,
            0x190, 0x99 , 0x393, 0x29a, 0x596, 0x49f, 0x795, 0x69c,
            0x99c, 0x895, 0xb9f, 0xa96, 0xd9a, 0xc93, 0xf99, 0xe90,
            0x230, 0x339, 0x33 , 0x13a, 0x636, 0x73f, 0x435, 0x53c,
            0xa3c, 0xb35, 0x83f, 0x936, 0xe3a, 0xf33, 0xc39, 0xd30,
            0x3a0, 0x2a9, 0x1a3, 0xaa , 0x7a6, 0x6af, 0x5a5, 0x4ac,
            0xbac, 0xaa5, 0x9af, 0x8a6, 0xfaa, 0xea3, 0xda9, 0xca0,
            0x460, 0x569, 0x663, 0x76a, 0x66 , 0x16f, 0x265, 0x36c,
            0xc6c, 0xd65, 0xe6f, 0xf66, 0x86a, 0x963, 0xa69, 0xb60,
            0x5f0, 0x4f9, 0x7f3, 0x6fa, 0x1f6, 0xff , 0x3f5, 0x2fc,
            0xdfc, 0xcf5, 0xfff, 0xef6, 0x9fa, 0x8f3, 0xbf9, 0xaf0,
            0x650, 0x759, 0x453, 0x55a, 0x256, 0x35f, 0x55 , 0x15c,
            0xe5c, 0xf55, 0xc5f, 0xd56, 0xa5a, 0xb53, 0x859, 0x950,
            0x7c0, 0x6c9, 0x5c3, 0x4ca, 0x3c6, 0x2cf, 0x1c5, 0xcc ,
            0xfcc, 0xec5, 0xdcf, 0xcc6, 0xbca, 0xac3, 0x9c9, 0x8c0,
            0x8c0, 0x9c9, 0xac3, 0xbca, 0xcc6, 0xdcf, 0xec5, 0xfcc,
            0xcc , 0x1c5, 0x2cf, 0x3c6, 0x4ca, 0x5c3, 0x6c9, 0x7c0,
            0x950, 0x859, 0xb53, 0xa5a, 0xd56, 0xc5f, 0xf55, 0xe5c,
            0x15c, 0x55 , 0x35f, 0x256, 0x55a, 0x453, 0x759, 0x650,
            0xaf0, 0xbf9, 0x8f3, 0x9fa, 0xef6, 0xfff, 0xcf5, 0xdfc,
            0x2fc, 0x3f5, 0xff , 0x1f6, 0x6fa, 0x7f3, 0x4f9, 0x5f0,
            0xb60, 0xa69, 0x963, 0x86a, 0xf66, 0xe6f, 0xd65, 0xc6c,
            0x36c, 0x265, 0x16f, 0x66 , 0x76a, 0x663, 0x569, 0x460,
            0xca0, 0xda9, 0xea3, 0xfaa, 0x8a6, 0x9af, 0xaa5, 0xbac,
            0x4ac, 0x5a5, 0x6af, 0x7a6, 0xaa , 0x1a3, 0x2a9, 0x3a0,
            0xd30, 0xc39, 0xf33, 0xe3a, 0x936, 0x83f, 0xb35, 0xa3c,
            0x53c, 0x435, 0x73f, 0x636, 0x13a, 0x33 , 0x339, 0x230,
            0xe90, 0xf99, 0xc93, 0xd9a, 0xa96, 0xb9f, 0x895, 0x99c,
            0x69c, 0x795, 0x49f, 0x596, 0x29a, 0x393, 0x99 , 0x190,
            0xf00, 0xe09, 0xd03, 0xc0a, 0xb06, 0xa0f, 0x905, 0x80c,
            0x70c, 0x605, 0x50f, 0x406, 0x30a, 0x203, 0x109, 0x0  
        };
        #endregion EdgeTable

        #region TriTable
        /// <summary>
        /// This table is also very important. This one saves all the triangles configurations. These 
        /// configurations are sorted using the edgeindex as the main index. So you can get the triangle
        /// configuration on every vertex combination inside the isosurface.
        /// </summary>
        private int[,] triTable = new int[256, 16]
        {{-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 1, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 8, 3, 9, 8, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 3, 1, 2, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {9, 2, 10, 0, 2, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {2, 8, 3, 2, 10, 8, 10, 9, 8, -1, -1, -1, -1, -1, -1, -1},
        {3, 11, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 11, 2, 8, 11, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 9, 0, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 11, 2, 1, 9, 11, 9, 8, 11, -1, -1, -1, -1, -1, -1, -1},
        {3, 10, 1, 11, 10, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 10, 1, 0, 8, 10, 8, 11, 10, -1, -1, -1, -1, -1, -1, -1},
        {3, 9, 0, 3, 11, 9, 11, 10, 9, -1, -1, -1, -1, -1, -1, -1},
        {9, 8, 10, 10, 8, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 7, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 3, 0, 7, 3, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 1, 9, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 1, 9, 4, 7, 1, 7, 3, 1, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 10, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {3, 4, 7, 3, 0, 4, 1, 2, 10, -1, -1, -1, -1, -1, -1, -1},
        {9, 2, 10, 9, 0, 2, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1},
        {2, 10, 9, 2, 9, 7, 2, 7, 3, 7, 9, 4, -1, -1, -1, -1},
        {8, 4, 7, 3, 11, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {11, 4, 7, 11, 2, 4, 2, 0, 4, -1, -1, -1, -1, -1, -1, -1},
        {9, 0, 1, 8, 4, 7, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1},
        {4, 7, 11, 9, 4, 11, 9, 11, 2, 9, 2, 1, -1, -1, -1, -1},
        {3, 10, 1, 3, 11, 10, 7, 8, 4, -1, -1, -1, -1, -1, -1, -1},
        {1, 11, 10, 1, 4, 11, 1, 0, 4, 7, 11, 4, -1, -1, -1, -1},
        {4, 7, 8, 9, 0, 11, 9, 11, 10, 11, 0, 3, -1, -1, -1, -1},
        {4, 7, 11, 4, 11, 9, 9, 11, 10, -1, -1, -1, -1, -1, -1, -1},
        {9, 5, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {9, 5, 4, 0, 8, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 5, 4, 1, 5, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {8, 5, 4, 8, 3, 5, 3, 1, 5, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 10, 9, 5, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {3, 0, 8, 1, 2, 10, 4, 9, 5, -1, -1, -1, -1, -1, -1, -1},
        {5, 2, 10, 5, 4, 2, 4, 0, 2, -1, -1, -1, -1, -1, -1, -1},
        {2, 10, 5, 3, 2, 5, 3, 5, 4, 3, 4, 8, -1, -1, -1, -1},
        {9, 5, 4, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 11, 2, 0, 8, 11, 4, 9, 5, -1, -1, -1, -1, -1, -1, -1},
        {0, 5, 4, 0, 1, 5, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1},
        {2, 1, 5, 2, 5, 8, 2, 8, 11, 4, 8, 5, -1, -1, -1, -1},
        {10, 3, 11, 10, 1, 3, 9, 5, 4, -1, -1, -1, -1, -1, -1, -1},
        {4, 9, 5, 0, 8, 1, 8, 10, 1, 8, 11, 10, -1, -1, -1, -1},
        {5, 4, 0, 5, 0, 11, 5, 11, 10, 11, 0, 3, -1, -1, -1, -1},
        {5, 4, 8, 5, 8, 10, 10, 8, 11, -1, -1, -1, -1, -1, -1, -1},
        {9, 7, 8, 5, 7, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {9, 3, 0, 9, 5, 3, 5, 7, 3, -1, -1, -1, -1, -1, -1, -1},
        {0, 7, 8, 0, 1, 7, 1, 5, 7, -1, -1, -1, -1, -1, -1, -1},
        {1, 5, 3, 3, 5, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {9, 7, 8, 9, 5, 7, 10, 1, 2, -1, -1, -1, -1, -1, -1, -1},
        {10, 1, 2, 9, 5, 0, 5, 3, 0, 5, 7, 3, -1, -1, -1, -1},
        {8, 0, 2, 8, 2, 5, 8, 5, 7, 10, 5, 2, -1, -1, -1, -1},
        {2, 10, 5, 2, 5, 3, 3, 5, 7, -1, -1, -1, -1, -1, -1, -1},
        {7, 9, 5, 7, 8, 9, 3, 11, 2, -1, -1, -1, -1, -1, -1, -1},
        {9, 5, 7, 9, 7, 2, 9, 2, 0, 2, 7, 11, -1, -1, -1, -1},
        {2, 3, 11, 0, 1, 8, 1, 7, 8, 1, 5, 7, -1, -1, -1, -1},
        {11, 2, 1, 11, 1, 7, 7, 1, 5, -1, -1, -1, -1, -1, -1, -1},
        {9, 5, 8, 8, 5, 7, 10, 1, 3, 10, 3, 11, -1, -1, -1, -1},
        {5, 7, 0, 5, 0, 9, 7, 11, 0, 1, 0, 10, 11, 10, 0, -1},
        {11, 10, 0, 11, 0, 3, 10, 5, 0, 8, 0, 7, 5, 7, 0, -1},
        {11, 10, 5, 7, 11, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {10, 6, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 3, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {9, 0, 1, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 8, 3, 1, 9, 8, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1},
        {1, 6, 5, 2, 6, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 6, 5, 1, 2, 6, 3, 0, 8, -1, -1, -1, -1, -1, -1, -1},
        {9, 6, 5, 9, 0, 6, 0, 2, 6, -1, -1, -1, -1, -1, -1, -1},
        {5, 9, 8, 5, 8, 2, 5, 2, 6, 3, 2, 8, -1, -1, -1, -1},
        {2, 3, 11, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {11, 0, 8, 11, 2, 0, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1},
        {0, 1, 9, 2, 3, 11, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1},
        {5, 10, 6, 1, 9, 2, 9, 11, 2, 9, 8, 11, -1, -1, -1, -1},
        {6, 3, 11, 6, 5, 3, 5, 1, 3, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 11, 0, 11, 5, 0, 5, 1, 5, 11, 6, -1, -1, -1, -1},
        {3, 11, 6, 0, 3, 6, 0, 6, 5, 0, 5, 9, -1, -1, -1, -1},
        {6, 5, 9, 6, 9, 11, 11, 9, 8, -1, -1, -1, -1, -1, -1, -1},
        {5, 10, 6, 4, 7, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 3, 0, 4, 7, 3, 6, 5, 10, -1, -1, -1, -1, -1, -1, -1},
        {1, 9, 0, 5, 10, 6, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1},
        {10, 6, 5, 1, 9, 7, 1, 7, 3, 7, 9, 4, -1, -1, -1, -1},
        {6, 1, 2, 6, 5, 1, 4, 7, 8, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 5, 5, 2, 6, 3, 0, 4, 3, 4, 7, -1, -1, -1, -1},
        {8, 4, 7, 9, 0, 5, 0, 6, 5, 0, 2, 6, -1, -1, -1, -1},
        {7, 3, 9, 7, 9, 4, 3, 2, 9, 5, 9, 6, 2, 6, 9, -1},
        {3, 11, 2, 7, 8, 4, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1},
        {5, 10, 6, 4, 7, 2, 4, 2, 0, 2, 7, 11, -1, -1, -1, -1},
        {0, 1, 9, 4, 7, 8, 2, 3, 11, 5, 10, 6, -1, -1, -1, -1},
        {9, 2, 1, 9, 11, 2, 9, 4, 11, 7, 11, 4, 5, 10, 6, -1},
        {8, 4, 7, 3, 11, 5, 3, 5, 1, 5, 11, 6, -1, -1, -1, -1},
        {5, 1, 11, 5, 11, 6, 1, 0, 11, 7, 11, 4, 0, 4, 11, -1},
        {0, 5, 9, 0, 6, 5, 0, 3, 6, 11, 6, 3, 8, 4, 7, -1},
        {6, 5, 9, 6, 9, 11, 4, 7, 9, 7, 11, 9, -1, -1, -1, -1},
        {10, 4, 9, 6, 4, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 10, 6, 4, 9, 10, 0, 8, 3, -1, -1, -1, -1, -1, -1, -1},
        {10, 0, 1, 10, 6, 0, 6, 4, 0, -1, -1, -1, -1, -1, -1, -1},
        {8, 3, 1, 8, 1, 6, 8, 6, 4, 6, 1, 10, -1, -1, -1, -1},
        {1, 4, 9, 1, 2, 4, 2, 6, 4, -1, -1, -1, -1, -1, -1, -1},
        {3, 0, 8, 1, 2, 9, 2, 4, 9, 2, 6, 4, -1, -1, -1, -1},
        {0, 2, 4, 4, 2, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {8, 3, 2, 8, 2, 4, 4, 2, 6, -1, -1, -1, -1, -1, -1, -1},
        {10, 4, 9, 10, 6, 4, 11, 2, 3, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 2, 2, 8, 11, 4, 9, 10, 4, 10, 6, -1, -1, -1, -1},
        {3, 11, 2, 0, 1, 6, 0, 6, 4, 6, 1, 10, -1, -1, -1, -1},
        {6, 4, 1, 6, 1, 10, 4, 8, 1, 2, 1, 11, 8, 11, 1, -1},
        {9, 6, 4, 9, 3, 6, 9, 1, 3, 11, 6, 3, -1, -1, -1, -1},
        {8, 11, 1, 8, 1, 0, 11, 6, 1, 9, 1, 4, 6, 4, 1, -1},
        {3, 11, 6, 3, 6, 0, 0, 6, 4, -1, -1, -1, -1, -1, -1, -1},
        {6, 4, 8, 11, 6, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {7, 10, 6, 7, 8, 10, 8, 9, 10, -1, -1, -1, -1, -1, -1, -1},
        {0, 7, 3, 0, 10, 7, 0, 9, 10, 6, 7, 10, -1, -1, -1, -1},
        {10, 6, 7, 1, 10, 7, 1, 7, 8, 1, 8, 0, -1, -1, -1, -1},
        {10, 6, 7, 10, 7, 1, 1, 7, 3, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 6, 1, 6, 8, 1, 8, 9, 8, 6, 7, -1, -1, -1, -1},
        {2, 6, 9, 2, 9, 1, 6, 7, 9, 0, 9, 3, 7, 3, 9, -1},
        {7, 8, 0, 7, 0, 6, 6, 0, 2, -1, -1, -1, -1, -1, -1, -1},
        {7, 3, 2, 6, 7, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {2, 3, 11, 10, 6, 8, 10, 8, 9, 8, 6, 7, -1, -1, -1, -1},
        {2, 0, 7, 2, 7, 11, 0, 9, 7, 6, 7, 10, 9, 10, 7, -1},
        {1, 8, 0, 1, 7, 8, 1, 10, 7, 6, 7, 10, 2, 3, 11, -1},
        {11, 2, 1, 11, 1, 7, 10, 6, 1, 6, 7, 1, -1, -1, -1, -1},
        {8, 9, 6, 8, 6, 7, 9, 1, 6, 11, 6, 3, 1, 3, 6, -1},
        {0, 9, 1, 11, 6, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {7, 8, 0, 7, 0, 6, 3, 11, 0, 11, 6, 0, -1, -1, -1, -1},
        {7, 11, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {7, 6, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {3, 0, 8, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 1, 9, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {8, 1, 9, 8, 3, 1, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1},
        {10, 1, 2, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 10, 3, 0, 8, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1},
        {2, 9, 0, 2, 10, 9, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1},
        {6, 11, 7, 2, 10, 3, 10, 8, 3, 10, 9, 8, -1, -1, -1, -1},
        {7, 2, 3, 6, 2, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {7, 0, 8, 7, 6, 0, 6, 2, 0, -1, -1, -1, -1, -1, -1, -1},
        {2, 7, 6, 2, 3, 7, 0, 1, 9, -1, -1, -1, -1, -1, -1, -1},
        {1, 6, 2, 1, 8, 6, 1, 9, 8, 8, 7, 6, -1, -1, -1, -1},
        {10, 7, 6, 10, 1, 7, 1, 3, 7, -1, -1, -1, -1, -1, -1, -1},
        {10, 7, 6, 1, 7, 10, 1, 8, 7, 1, 0, 8, -1, -1, -1, -1},
        {0, 3, 7, 0, 7, 10, 0, 10, 9, 6, 10, 7, -1, -1, -1, -1},
        {7, 6, 10, 7, 10, 8, 8, 10, 9, -1, -1, -1, -1, -1, -1, -1},
        {6, 8, 4, 11, 8, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {3, 6, 11, 3, 0, 6, 0, 4, 6, -1, -1, -1, -1, -1, -1, -1},
        {8, 6, 11, 8, 4, 6, 9, 0, 1, -1, -1, -1, -1, -1, -1, -1},
        {9, 4, 6, 9, 6, 3, 9, 3, 1, 11, 3, 6, -1, -1, -1, -1},
        {6, 8, 4, 6, 11, 8, 2, 10, 1, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 10, 3, 0, 11, 0, 6, 11, 0, 4, 6, -1, -1, -1, -1},
        {4, 11, 8, 4, 6, 11, 0, 2, 9, 2, 10, 9, -1, -1, -1, -1},
        {10, 9, 3, 10, 3, 2, 9, 4, 3, 11, 3, 6, 4, 6, 3, -1},
        {8, 2, 3, 8, 4, 2, 4, 6, 2, -1, -1, -1, -1, -1, -1, -1},
        {0, 4, 2, 4, 6, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 9, 0, 2, 3, 4, 2, 4, 6, 4, 3, 8, -1, -1, -1, -1},
        {1, 9, 4, 1, 4, 2, 2, 4, 6, -1, -1, -1, -1, -1, -1, -1},
        {8, 1, 3, 8, 6, 1, 8, 4, 6, 6, 10, 1, -1, -1, -1, -1},
        {10, 1, 0, 10, 0, 6, 6, 0, 4, -1, -1, -1, -1, -1, -1, -1},
        {4, 6, 3, 4, 3, 8, 6, 10, 3, 0, 3, 9, 10, 9, 3, -1},
        {10, 9, 4, 6, 10, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 9, 5, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 3, 4, 9, 5, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1},
        {5, 0, 1, 5, 4, 0, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1},
        {11, 7, 6, 8, 3, 4, 3, 5, 4, 3, 1, 5, -1, -1, -1, -1},
        {9, 5, 4, 10, 1, 2, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1},
        {6, 11, 7, 1, 2, 10, 0, 8, 3, 4, 9, 5, -1, -1, -1, -1},
        {7, 6, 11, 5, 4, 10, 4, 2, 10, 4, 0, 2, -1, -1, -1, -1},
        {3, 4, 8, 3, 5, 4, 3, 2, 5, 10, 5, 2, 11, 7, 6, -1},
        {7, 2, 3, 7, 6, 2, 5, 4, 9, -1, -1, -1, -1, -1, -1, -1},
        {9, 5, 4, 0, 8, 6, 0, 6, 2, 6, 8, 7, -1, -1, -1, -1},
        {3, 6, 2, 3, 7, 6, 1, 5, 0, 5, 4, 0, -1, -1, -1, -1},
        {6, 2, 8, 6, 8, 7, 2, 1, 8, 4, 8, 5, 1, 5, 8, -1},
        {9, 5, 4, 10, 1, 6, 1, 7, 6, 1, 3, 7, -1, -1, -1, -1},
        {1, 6, 10, 1, 7, 6, 1, 0, 7, 8, 7, 0, 9, 5, 4, -1},
        {4, 0, 10, 4, 10, 5, 0, 3, 10, 6, 10, 7, 3, 7, 10, -1},
        {7, 6, 10, 7, 10, 8, 5, 4, 10, 4, 8, 10, -1, -1, -1, -1},
        {6, 9, 5, 6, 11, 9, 11, 8, 9, -1, -1, -1, -1, -1, -1, -1},
        {3, 6, 11, 0, 6, 3, 0, 5, 6, 0, 9, 5, -1, -1, -1, -1},
        {0, 11, 8, 0, 5, 11, 0, 1, 5, 5, 6, 11, -1, -1, -1, -1},
        {6, 11, 3, 6, 3, 5, 5, 3, 1, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 10, 9, 5, 11, 9, 11, 8, 11, 5, 6, -1, -1, -1, -1},
        {0, 11, 3, 0, 6, 11, 0, 9, 6, 5, 6, 9, 1, 2, 10, -1},
        {11, 8, 5, 11, 5, 6, 8, 0, 5, 10, 5, 2, 0, 2, 5, -1},
        {6, 11, 3, 6, 3, 5, 2, 10, 3, 10, 5, 3, -1, -1, -1, -1},
        {5, 8, 9, 5, 2, 8, 5, 6, 2, 3, 8, 2, -1, -1, -1, -1},
        {9, 5, 6, 9, 6, 0, 0, 6, 2, -1, -1, -1, -1, -1, -1, -1},
        {1, 5, 8, 1, 8, 0, 5, 6, 8, 3, 8, 2, 6, 2, 8, -1},
        {1, 5, 6, 2, 1, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 3, 6, 1, 6, 10, 3, 8, 6, 5, 6, 9, 8, 9, 6, -1},
        {10, 1, 0, 10, 0, 6, 9, 5, 0, 5, 6, 0, -1, -1, -1, -1},
        {0, 3, 8, 5, 6, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {10, 5, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {11, 5, 10, 7, 5, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {11, 5, 10, 11, 7, 5, 8, 3, 0, -1, -1, -1, -1, -1, -1, -1},
        {5, 11, 7, 5, 10, 11, 1, 9, 0, -1, -1, -1, -1, -1, -1, -1},
        {10, 7, 5, 10, 11, 7, 9, 8, 1, 8, 3, 1, -1, -1, -1, -1},
        {11, 1, 2, 11, 7, 1, 7, 5, 1, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 3, 1, 2, 7, 1, 7, 5, 7, 2, 11, -1, -1, -1, -1},
        {9, 7, 5, 9, 2, 7, 9, 0, 2, 2, 11, 7, -1, -1, -1, -1},
        {7, 5, 2, 7, 2, 11, 5, 9, 2, 3, 2, 8, 9, 8, 2, -1},
        {2, 5, 10, 2, 3, 5, 3, 7, 5, -1, -1, -1, -1, -1, -1, -1},
        {8, 2, 0, 8, 5, 2, 8, 7, 5, 10, 2, 5, -1, -1, -1, -1},
        {9, 0, 1, 5, 10, 3, 5, 3, 7, 3, 10, 2, -1, -1, -1, -1},
        {9, 8, 2, 9, 2, 1, 8, 7, 2, 10, 2, 5, 7, 5, 2, -1},
        {1, 3, 5, 3, 7, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 7, 0, 7, 1, 1, 7, 5, -1, -1, -1, -1, -1, -1, -1},
        {9, 0, 3, 9, 3, 5, 5, 3, 7, -1, -1, -1, -1, -1, -1, -1},
        {9, 8, 7, 5, 9, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {5, 8, 4, 5, 10, 8, 10, 11, 8, -1, -1, -1, -1, -1, -1, -1},
        {5, 0, 4, 5, 11, 0, 5, 10, 11, 11, 3, 0, -1, -1, -1, -1},
        {0, 1, 9, 8, 4, 10, 8, 10, 11, 10, 4, 5, -1, -1, -1, -1},
        {10, 11, 4, 10, 4, 5, 11, 3, 4, 9, 4, 1, 3, 1, 4, -1},
        {2, 5, 1, 2, 8, 5, 2, 11, 8, 4, 5, 8, -1, -1, -1, -1},
        {0, 4, 11, 0, 11, 3, 4, 5, 11, 2, 11, 1, 5, 1, 11, -1},
        {0, 2, 5, 0, 5, 9, 2, 11, 5, 4, 5, 8, 11, 8, 5, -1},
        {9, 4, 5, 2, 11, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {2, 5, 10, 3, 5, 2, 3, 4, 5, 3, 8, 4, -1, -1, -1, -1},
        {5, 10, 2, 5, 2, 4, 4, 2, 0, -1, -1, -1, -1, -1, -1, -1},
        {3, 10, 2, 3, 5, 10, 3, 8, 5, 4, 5, 8, 0, 1, 9, -1},
        {5, 10, 2, 5, 2, 4, 1, 9, 2, 9, 4, 2, -1, -1, -1, -1},
        {8, 4, 5, 8, 5, 3, 3, 5, 1, -1, -1, -1, -1, -1, -1, -1},
        {0, 4, 5, 1, 0, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {8, 4, 5, 8, 5, 3, 9, 0, 5, 0, 3, 5, -1, -1, -1, -1},
        {9, 4, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 11, 7, 4, 9, 11, 9, 10, 11, -1, -1, -1, -1, -1, -1, -1},
        {0, 8, 3, 4, 9, 7, 9, 11, 7, 9, 10, 11, -1, -1, -1, -1},
        {1, 10, 11, 1, 11, 4, 1, 4, 0, 7, 4, 11, -1, -1, -1, -1},
        {3, 1, 4, 3, 4, 8, 1, 10, 4, 7, 4, 11, 10, 11, 4, -1},
        {4, 11, 7, 9, 11, 4, 9, 2, 11, 9, 1, 2, -1, -1, -1, -1},
        {9, 7, 4, 9, 11, 7, 9, 1, 11, 2, 11, 1, 0, 8, 3, -1},
        {11, 7, 4, 11, 4, 2, 2, 4, 0, -1, -1, -1, -1, -1, -1, -1},
        {11, 7, 4, 11, 4, 2, 8, 3, 4, 3, 2, 4, -1, -1, -1, -1},
        {2, 9, 10, 2, 7, 9, 2, 3, 7, 7, 4, 9, -1, -1, -1, -1},
        {9, 10, 7, 9, 7, 4, 10, 2, 7, 8, 7, 0, 2, 0, 7, -1},
        {3, 7, 10, 3, 10, 2, 7, 4, 10, 1, 10, 0, 4, 0, 10, -1},
        {1, 10, 2, 8, 7, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 9, 1, 4, 1, 7, 7, 1, 3, -1, -1, -1, -1, -1, -1, -1},
        {4, 9, 1, 4, 1, 7, 0, 8, 1, 8, 7, 1, -1, -1, -1, -1},
        {4, 0, 3, 7, 4, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {4, 8, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {9, 10, 8, 10, 11, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {3, 0, 9, 3, 9, 11, 11, 9, 10, -1, -1, -1, -1, -1, -1, -1},
        {0, 1, 10, 0, 10, 8, 8, 10, 11, -1, -1, -1, -1, -1, -1, -1},
        {3, 1, 10, 11, 3, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 2, 11, 1, 11, 9, 9, 11, 8, -1, -1, -1, -1, -1, -1, -1},
        {3, 0, 9, 3, 9, 11, 1, 2, 9, 2, 11, 9, -1, -1, -1, -1},
        {0, 2, 11, 8, 0, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {3, 2, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {2, 3, 8, 2, 8, 10, 10, 8, 9, -1, -1, -1, -1, -1, -1, -1},
        {9, 10, 2, 0, 9, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {2, 3, 8, 2, 8, 10, 0, 1, 8, 1, 10, 8, -1, -1, -1, -1},
        {1, 10, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {1, 3, 8, 9, 1, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 9, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {0, 3, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}};
        #endregion TriTable

        public BackgroundWorker thread;
        public Container cajita;
        public Mesh my_mesh;
        public bool Cancelled;

        private byte[, ,] matrix3d;
        private int trian_count;

        /// <summary>
        /// This is the main constructor of the Marching Cubes algorithm. This basically initilizes several 
        /// parameters for the Algorithm
        /// </summary>
        /// <param name="dimx">The width of the picture package</param>
        /// <param name="dimy">The heigth of the picture package</param>
        /// <param name="dimz">The number of images on the picture package</param>
        /// <param name="isolevel">The calculated isolevel value</param>
        /// <param name="cube_x">The cube x length</param>
        /// <param name="cube_y">The cube y length</param>
        /// <param name="filenames">A string array with the filenames of every image on the package</param>
        /// <param name="progressbar">A progressbar object to indicate the task progress</param>
        /// <param name="label_progress">A label to indicate the task objectives</param>
        /// <param name="cancel">A cancel button to stop all the task</param>
        /// <param name="finish">The finish button on the Image_Form</param>
        public MarchingCubes(int dimx, int dimy, int dimz,  int isolevel, int cube_x, int cube_y, ref string[] filenames, ref ProgressBar progressbar, ref Label label_progress, ref Button cancel, ref Button finish)
        {
            this.dimx = dimx;
            this.dimy = dimy;
            this.dimz = dimz;
            this.isolevel = isolevel;
            this.cube_x = cube_x;
            this.cube_y = cube_y;
            this.filenames = filenames;
            this.progressbar = progressbar;
            this.label_progress = label_progress;
            this.cancel = cancel;
            this.finish = finish;
            Initalize_BgWorker();

        }

        /// <summary>
        /// This function initilizes the backgroundworker parameters, like the progressbar value and steps.
        /// </summary>
        private void Initalize_BgWorker()
        {
            thread = new BackgroundWorker();

            this.thread.WorkerReportsProgress = true;
            this.thread.WorkerSupportsCancellation = true;

            this.thread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.thread_DoWork);             
            this.thread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.thread_ProgressChanged);
            this.thread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.thread_RunWorkerCompleted);
        }

        /// <summary>
        /// This funtion implements the Marching Cubes Algortihm on another thread. This was implemented trying to save memory
        /// so the images are loaded in to the memory 4 by 4, and then the algorithm runs on them and then we save the mesh 
        /// calulated data on a temporal file. When all the process finishes then all the data on the temporal file is loaded
        /// on a new Mesh object and then the 3D model shows on the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Punto> vertexes;
            List<Punto> normals;
            //bool[, ,] voxels = new bool[(dimx/cube_x)+1, (dimy/cube_y)+1, dimz];
            int x, y, z;
            int dimex, dimey;
            int edgeindex = 0;
            int img_chk = 0;

            cajita = new Container(dimx, dimy, dimz, dimx / 2, dimy / 2, dimz / 2);
            FileStream fs = new FileStream(Application.ExecutablePath + "mod_temp.txt", FileMode.Create, FileAccess.ReadWrite);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                for (z = 1; z < dimz - 2; z = z + 1)
                {
                    thread.ReportProgress(z);

                    if (thread.CancellationPending)//checks for cancel request
                    {
                        e.Cancel = true;
                        return;
                    }

                    img_chk = Fill_Matrix(z);
                    if (img_chk != 0)
                    {
                        MessageBox.Show("The image " + img_chk.ToString()  + " - " + filenames[img_chk] + " doesn't have the same size as the others.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }

                    for (y = 1; y < dimy - cube_y-1; y = y + cube_y)
                    {
                        for (x = 1; x < dimx - cube_x-1; x = x + cube_x)
                        {
                            dimex = x + cube_x;
                            dimey = y + cube_y;
                            
                            edgeindex = 0;
                            Corner_Check(ref edgeindex, isolevel, x, y, 1, dimex, dimey, 2);
                            if (edgeindex != 0)
                            {
                                //voxels[(x/cube_x), (y/cube_y), z] = true;
                                vertexes = Get_Vertexes(ref edgeindex, isolevel, x, y, dimex, dimey, z);
                                normals = Get_Normals(ref edgeindex, isolevel, x, y, dimex, dimey, z);
                                Add_Marching(sw, ref edgeindex, vertexes, normals);

                                if (vertexes != null)
                                    vertexes.Clear();
                                if (normals != null)
                                    normals.Clear();
                            }
                        }
                    }
                }
            }
            fs.Dispose();
            fs = new FileStream(Application.ExecutablePath + "mod_temp.txt", FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fs))
            {
                my_mesh = new Mesh(sr, trian_count, dimx, dimy, dimz);
                //my_mesh.voxels = voxels;
            }
            fs.Dispose();
        }

        /// <summary>
        /// Every time the progressbar value changes, the label status text shows the current operating plane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label_progress.Text = progressbar.Value.ToString() +" of " + progressbar.Maximum.ToString();
            progressbar.PerformStep();
        }

        /// <summary>
        /// This is called when the thread task is canceled or completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                matrix3d = null;
                my_mesh = null;
                
                label_progress.Text = string.Empty;
                progressbar.Value = 0;
                cancel.Enabled = false;
                finish.Enabled = true;
                Cancelled = true;
                MessageBox.Show("The operation have been cancelled.", "INFO - Muni", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                matrix3d = null;
                cancel.Enabled = false;
                finish.Enabled = true;
                Cancelled = false;
                MessageBox.Show("The model have been successfully created.", "INFO - Muni", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// This fucntion fills a byte matrix with the pixel data inside a image file 4 by 4. This means
        /// if n is 1  then 1,2,3 and 4 planes are loaded and if n is 2 then 2,3,4. This operation is 
        /// necesary if we want to get some good time performance on the Marching Cube Algorithm.
        /// </summary>
        /// <param name="z">Recibes the number of the plane to be loaded</param>
        /// <returns>0 on no error and a differen value on error</returns>
        private int Fill_Matrix(int z)
        {
            if (z == 1)
            {
                FileStream fs, fs1, fs2, fs3;
                Bitmap b, b1, b2, b3;
                BitmapData pixi, pixi1, pixi2, pixi3;
                System.IntPtr scano, scano1, scano2, scano3;
                int stride, stride1, stride2, stride3;

                Rectangle rc = new Rectangle(0, 0, dimx, dimy);
                matrix3d = null;
                matrix3d = new byte[rc.Width, rc.Height, 4];

                fs = new FileStream(filenames[0], FileMode.Open, FileAccess.Read);
                fs1 = new FileStream(filenames[1], FileMode.Open, FileAccess.Read);
                fs2 = new FileStream(filenames[2], FileMode.Open, FileAccess.Read);
                fs3 = new FileStream(filenames[3], FileMode.Open, FileAccess.Read);
                b = (Bitmap)Bitmap.FromStream(fs, true, false);
                b1 = (Bitmap)Bitmap.FromStream(fs1, true, false);
                b2 = (Bitmap)Bitmap.FromStream(fs2, true, false);
                b3 = (Bitmap)Bitmap.FromStream(fs3, true, false);

                if ((b.Width != dimx) || (b.Height != dimy)) { return 1; }
                if ((b1.Width != dimx) || (b1.Height != dimy)) { return 2; }
                if ((b2.Width != dimx) || (b2.Height != dimy)) { return 3; }
                if ((b3.Width != dimx) || (b3.Height != dimy)) { return 4; }

                pixi = b.LockBits(rc, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                pixi1 = b1.LockBits(rc, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                pixi2 = b2.LockBits(rc, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                pixi3 = b3.LockBits(rc, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                scano = pixi.Scan0;
                scano1 = pixi1.Scan0;
                scano2 = pixi2.Scan0;
                scano3 = pixi3.Scan0;
                stride = pixi.Stride;
                stride1 = pixi1.Stride;
                stride2 = pixi2.Stride;
                stride3 = pixi3.Stride;

                unsafe
                {
                    byte prom, prom1, prom2, prom3;
                    byte* p = (byte*)(void*)scano;
                    byte* p1 = (byte*)(void*)scano1;
                    byte* p2 = (byte*)(void*)scano2;
                    byte* p3 = (byte*)(void*)scano3;
                    int nOffset = stride - rc.Width * 3;
                    int nOffset1 = stride1 - rc.Width * 3;
                    int nOffset2 = stride2 - rc.Width * 3;
                    int nOffset3 = stride3 - rc.Width * 3;
                    int nWidth = rc.Width;

                    for (int y = 0; y < rc.Height; ++y)
                    {
                        for (int x = 0; x < nWidth; ++x)
                        {
                            prom = (byte)(((p[0] + p[1] + p[2])) / 3);
                            prom1 = (byte)(((p1[0] + p1[1] + p1[2])) / 3);
                            prom2 = (byte)(((p2[0] + p2[1] + p2[2])) / 3);
                            prom3 = (byte)(((p3[0] + p3[1] + p3[2])) / 3);
                            matrix3d[x, y, 0] = prom;
                            matrix3d[x, y, 1] = prom1;
                            matrix3d[x, y, 2] = prom2;
                            matrix3d[x, y, 3] = prom3;
                            p = p + 3;
                            p1 = p1 + 3;
                            p2 = p2 + 3;
                            p3 = p3 + 3;
                        }
                        p = p + nOffset;
                        p1 = p1 + nOffset1;
                        p2 = p2 + nOffset2;
                        p3 = p3 + nOffset3;
                    }
                }
                b.UnlockBits(pixi);
                b1.UnlockBits(pixi1);
                b2.UnlockBits(pixi2);
                b3.UnlockBits(pixi3);

                fs.Close(); fs1.Close(); fs2.Close(); fs3.Close();
                fs.Dispose(); fs1.Dispose(); fs2.Dispose(); fs3.Dispose();
                b.Dispose(); b1.Dispose(); b2.Dispose(); b3.Dispose();

                return 0;
            }
            else
            {
                FileStream fs;
                Bitmap b;
                BitmapData pixi;
                System.IntPtr scano;
                int stride;

                Rectangle rc = new Rectangle(0, 0, dimx, dimy);

                fs = new FileStream(filenames[z+2], FileMode.Open, FileAccess.Read);
                b = (Bitmap)Bitmap.FromStream(fs, true, false);
                if ((b.Width != dimx) || (b.Height != dimy)) { return z+3; }
                pixi = b.LockBits(rc, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                scano = pixi.Scan0;
                stride = pixi.Stride;

                unsafe
                {
                    byte prom;
                    byte* p = (byte*)(void*)scano;
                    int nOffset = stride - rc.Width * 3;
                    int nWidth = rc.Width;

                    for (int y = 0; y < rc.Height; ++y)
                    {
                        for (int x = 0; x < nWidth; ++x)
                        {
                            prom = (byte)(((p[0] + p[1] + p[2])) / 3);
                            matrix3d[x, y, 0] = matrix3d[x, y, 1];
                            matrix3d[x, y, 1] = matrix3d[x, y, 2];
                            matrix3d[x, y, 2] = matrix3d[x, y, 3];
                            matrix3d[x, y, 3] = prom;
                            p = p + 3;
                        }
                        p = p + nOffset;
                    }
                }
                b.UnlockBits(pixi);
                fs.Close(); fs.Dispose(); b.Dispose();

                return 0;
            }
        }

        /// <summary>
        /// This functions add a new marching cube to a temporary file on the root directory of the program.
        /// Later this file will be readed again to build the final Mesh object. This is for save RAM memory.
        /// </summary>
        /// <param name="sw">This is the stream writer of the temporary file</param>
        /// <param name="cubeindex">this is the cubeindex number and also called endeindex</param>
        /// <param name="vertexes">this list has all the vertex dots for that machingcube</param>
        /// <param name="normals">this list has all the normals for each dot of the mesh</param>
        private void Add_Marching(StreamWriter sw, ref int cubeindex, List<Punto> vertexes, List<Punto> normals)
        {
            if (vertexes != null)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbn = new StringBuilder();
                
                for (int i = 0; triTable[cubeindex, i] != -1; i += 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (vertexes[triTable[cubeindex, i + j]] == null)
                            break;
                        sb.Append(vertexes[triTable[cubeindex, i + j]].x.ToString(es));
                        sb.Append(',');
                        sb.Append(vertexes[triTable[cubeindex, i + j]].y.ToString(es));
                        sb.Append(',');
                        sb.Append(vertexes[triTable[cubeindex, i + j]].z.ToString(es));
                        sb.Append(',');
                        sb.Append(normals[triTable[cubeindex, i + j]].x.ToString(es));
                        sb.Append(',');
                        sb.Append(normals[triTable[cubeindex, i + j]].y.ToString(es));
                        sb.Append(',');
                        sb.Append(normals[triTable[cubeindex, i + j]].z.ToString(es));

                        sw.WriteLine(sb.ToString());
                        sb.Length = 0;
                    }
                    trian_count = trian_count + 1;
                }
            }
        }

        /// <summary>
        /// This function interpolates a dot position on intersected edge line
        /// </summary>
        /// <param name="isolevel">The isolevel value it's used to interpolate the vertex position between the edge</param>
        /// <param name="p1x">x coordenate of the first point</param>
        /// <param name="p1y">y coordenate of the first point</param>
        /// <param name="p1z">z coordenate of the first point</param>
        /// <param name="p2x">x coordenate of the last point</param>
        /// <param name="p2y">y coordenate of the last point</param>
        /// <param name="p2z">z coordenate of the last point</param>
        /// <param name="iz1"></param>
        /// <param name="iz2"></param>
        /// <returns></returns>
        private Punto Vertex_Interpolate(int isolevel, int p1x, int p1y, int p1z, int p2x, int p2y, int p2z, int iz1, int iz2)
        {
            float mu = (float)(isolevel - matrix3d[p1x, p1y, p1z]) / (matrix3d[p2x, p2y, p2z] - matrix3d[p1x, p1y, p1z]);
            Punto res = new Punto();

            res.x = (float)(p1x + mu * (p2x - p1x));
            res.y = (float)(p1y + mu * (p2y - p1y));
            res.z = (float)(iz1 + mu * (iz2 - iz1));

            return res;
        }

        /// <summary>
        /// This function interpolates the normal value for the interpolated vertex in the intersected edge
        /// </summary>
        /// <param name="isolevel">The isolevel value helps to calculate the normal of the vertex</param>
        /// <param name="p1x">x coordenate of the first point</param>
        /// <param name="p1y">y coordenate of the first point</param>
        /// <param name="p1z">z coordenate of the first point</param>
        /// <param name="p2x">x coordenate of the last point</param>
        /// <param name="p2y">y coordenate of the last point</param>
        /// <param name="p2z">z coordenate of the last point</param>
        /// <returns>This function return a Punto object with the normal coordenates</returns>
        private Punto Normal_Interpolate(int isolevel, int p1x, int p1y, int p1z, int p2x, int p2y, int p2z)
        {
            float gx1 = (float)((matrix3d[p1x - 1, p1y, p1z] - matrix3d[p1x + 1, p1y, p1z]) / ((float)(cube_x)));
            float gy1 = (float)((matrix3d[p1x, p1y - 1, p1z] - matrix3d[p1x, p1y + 1, p1z]) / ((float)(cube_y)));
            float gz1 = (float)(matrix3d[p1x, p1y, p1z - 1] - matrix3d[p1x, p1y, p1z + 1]);

            float gx2 = (float)((matrix3d[p2x - 1, p2y, p2z] - matrix3d[p2x + 1, p2y, p2z]) / ((float)(cube_x)));
            float gy2 = (float)((matrix3d[p2x, p2y - 1, p2z] - matrix3d[p2x, p2y + 1, p2z]) / ((float)(cube_y)));
            float gz2 = (float)(matrix3d[p2x, p2y, p2z - 1] - matrix3d[p2x, p2y, p2z + 1]);

            float mu = (float)(isolevel - matrix3d[p1x, p1y, p1z]) / (matrix3d[p2x, p2y, p2z] - matrix3d[p1x, p1y, p1z]);

            Punto res = new Punto();

            res.x = (float)(gx1 + mu * (gx2 - gx1));
            res.y = (float)(gy1 + mu * (gy2 - gy1));
            res.z = (float)(gz1 + mu * (gz2 - gz1));

            return res;
        }

        /// <summary>
        /// This function interpolates all the vertexes founded on a marching cube, and return them on a 
        /// Point list. This list of vertex are obtained using the edgeindex, and the isolevel valuee. 
        /// Because every single vertex is interpolated on a edge using the isolevel value.
        /// </summary>
        /// <param name="edgeindex">The edgeindex value with the vertex cube configuration</param>
        /// <param name="isolevel">The isolevel value to interpolate the vertexes</param>
        /// <param name="ix">the x coordinate value for the image postion cursor</param>
        /// <param name="iy">the y coordinate value for the image postion cursor</param>
        /// <param name="dim_x">The width of the marching cube</param>
        /// <param name="dim_y">The heigth of the marching cube</param>
        /// <param name="iz">This has the index of the current image plane</param>
        /// <returns>Returns a list of Punto objects, with the coordenates interpolated vertexes</returns>
        private List<Punto> Get_Vertexes(ref int edgeindex, int isolevel, int ix, int iy, int dim_x, int dim_y, int iz)
        {
            if (edgeTable[edgeindex] == 0)
                return null;
            List<Punto> vertexes = new List<Punto>();
            for (int i = 0; i <= 11; i++)
            {
                vertexes.Add(null);
            }

            //ABAJO
            if ((edgeTable[edgeindex] & 1) == 1)
            {
                vertexes[0] = Vertex_Interpolate(isolevel, ix, iy, 2, dim_x, iy, 2, iz+1, iz+1);
            }
            if ((edgeTable[edgeindex] & 2) == 2)
            {
                vertexes[1] = Vertex_Interpolate(isolevel, dim_x, iy, 2, dim_x, dim_y, 2, iz+1, iz+1);
            }
            if ((edgeTable[edgeindex] & 4) == 4)
            {
                vertexes[2] = Vertex_Interpolate(isolevel, dim_x, dim_y, 2, ix, dim_y, 2, iz+1, iz+1);
            }
            if ((edgeTable[edgeindex] & 8) == 8)
            {
                vertexes[3] = Vertex_Interpolate(isolevel, ix, dim_y, 2, ix, iy, 2, iz+1, iz+1);
            }
            //ARRIBA
            if ((edgeTable[edgeindex] & 16) == 16)
            {
                vertexes[4] = Vertex_Interpolate(isolevel, ix, iy, 1, dim_x, iy, 1, iz, iz);
            }
            if ((edgeTable[edgeindex] & 32) == 32)
            {
                vertexes[5] = Vertex_Interpolate(isolevel, dim_x, iy, 1, dim_x, dim_y, 1, iz, iz);
            }
            if ((edgeTable[edgeindex] & 64) == 64)
            {
                vertexes[6] = Vertex_Interpolate(isolevel, dim_x, dim_y, 1, ix, dim_y, 1, iz, iz);
            }
            if ((edgeTable[edgeindex] & 128) == 128)
            {
                vertexes[7] = Vertex_Interpolate(isolevel, ix, dim_y, 1, ix, iy, 1, iz, iz);
            }
            //LADOS
            if ((edgeTable[edgeindex] & 256) == 256)
            {
                vertexes[8] = Vertex_Interpolate(isolevel, ix, iy, 2, ix, iy, 1, iz, iz+1);
            }
            if ((edgeTable[edgeindex] & 512) == 512)
            {
                vertexes[9] = Vertex_Interpolate(isolevel, dim_x, iy, 2, dim_x, iy, 1, iz, iz+1);
            }
            if ((edgeTable[edgeindex] & 1024) == 1024)
            {
                vertexes[10] = Vertex_Interpolate(isolevel, dim_x, dim_y, 2, dim_x, dim_y, 1, iz, iz+1);
            }
            if ((edgeTable[edgeindex] & 2048) == 2048)
            {
                vertexes[11] = Vertex_Interpolate(isolevel, ix, dim_y, 2, ix, dim_y, 1, iz, iz+1);
            }
            return vertexes;
        }

        /// <summary>
        /// This function interpolates a normal for a interpolated vertex.
        /// </summary>
        /// <param name="edgeindex">The edgeindex value taken from the vertex cube configuration</param>
        /// <param name="isolevel">The calculated isolevel value</param>
        /// <param name="ix">the x coordinate value for the image postion cursor</param>
        /// <param name="iy">the y coordinate value for the image postion cursor</param>
        /// <param name="dim_x">The width of the marching cube</param>
        /// <param name="dim_y">The heigth of the marching cube</param>
        /// <param name="iz">This has the index of the current image plane</param>
        /// <returns>Returns a list of Punto objects, with the coordenates interpolated normals</returns>
        private List<Punto> Get_Normals(ref int edgeindex, int isolevel, int ix, int iy, int dim_x, int dim_y, int iz)
        {
            if (edgeTable[edgeindex] == 0)
                return null;
            List<Punto> normals = new List<Punto>();
            for (int i = 0; i <= 11; i++)
            {
                normals.Add(null);
            }

            //ABAJO
            if ((edgeTable[edgeindex] & 1) == 1)
            {
                normals[0] = Normal_Interpolate(isolevel, ix, iy, 2, dim_x, iy, 2);
            }
            if ((edgeTable[edgeindex] & 2) == 2)
            {
                normals[1] = Normal_Interpolate(isolevel, dim_x, iy, 2, dim_x, dim_y, 2);
            }
            if ((edgeTable[edgeindex] & 4) == 4)
            {
                normals[2] = Normal_Interpolate(isolevel, dim_x, dim_y, 2, ix, dim_y, 2);
            }
            if ((edgeTable[edgeindex] & 8) == 8)
            {
                normals[3] = Normal_Interpolate(isolevel, ix, dim_y, 2, ix, iy, 2);
            }
            //ARRIBA
            if ((edgeTable[edgeindex] & 16) == 16)
            {
                normals[4] = Normal_Interpolate(isolevel, ix, iy, 1, dim_x, iy, 1);
            }
            if ((edgeTable[edgeindex] & 32) == 32)
            {
                normals[5] = Normal_Interpolate(isolevel, dim_x, iy, 1, dim_x, dim_y, 1);
            }
            if ((edgeTable[edgeindex] & 64) == 64)
            {
                normals[6] = Normal_Interpolate(isolevel, dim_x, dim_y, 1, ix, dim_y, 1);
            }
            if ((edgeTable[edgeindex] & 128) == 128)
            {
                normals[7] = Normal_Interpolate(isolevel, ix, dim_y, 1, ix, iy, 1);
            }
            //LADOS
            if ((edgeTable[edgeindex] & 256) == 256)
            {
                normals[8] = Normal_Interpolate(isolevel, ix, iy, 2, ix, iy, 1);
            }
            if ((edgeTable[edgeindex] & 512) == 512)
            {
                normals[9] = Normal_Interpolate(isolevel, dim_x, iy, 2, dim_x, iy, 1);
            }
            if ((edgeTable[edgeindex] & 1024) == 1024)
            {
                normals[10] = Normal_Interpolate(isolevel, dim_x, dim_y, 2, dim_x, dim_y, 1);
            }
            if ((edgeTable[edgeindex] & 2048) == 2048)
            {
                normals[11] = Normal_Interpolate(isolevel, ix, dim_y, 2, ix, dim_y, 1);
            }
            return normals;
        }

        /// <summary>
        /// This function checks what corners of the cube are inside the isosurface using the isolevel value. 
        /// This function returns the combination of vertexes inside the isosurface coded on a 8 bit integer, the 
        /// edgeindex code is used as index to get the intersected edges and also the triangles configuration
        /// </summary>
        /// <param name="edgeindex">Reference value to return the edgeindex value for the vertex configuration of the cube</param>
        /// <param name="edge">This parameter recives the isovalue to check wich vertexes are inside the surface</param>
        /// <param name="ix">This is the x coordinate of the marching cube cursor</param>
        /// <param name="iy">This is the y coordinate of the marching cube cursor</param>
        /// <param name="iz">This is the z coordinate of the marching cube cursor</param>
        /// <param name="dim_x">This is the width of the marching cube</param>
        /// <param name="dim_y">This is the length of the marching cube</param>
        /// <param name="dim_z">This is the depth of the marching cube</param>
        private void Corner_Check(ref int edgeindex, int edge, int ix, int iy, int iz, int dim_x, int dim_y, int dim_z)
        {
            //0
            if (matrix3d[ix, iy, dim_z] < edge)
            {
                edgeindex |= 1;
            }
            //1
            if (matrix3d[dim_x, iy, dim_z] < edge)
            {
                edgeindex |= 2;
            }
            //2
            if (matrix3d[dim_x, dim_y, dim_z] < edge)
            {
                edgeindex |= 4;
            }
            //3
            if (matrix3d[ix, dim_y, dim_z] < edge)
            {
                edgeindex |= 8;
            }
            //4
            if (matrix3d[ix, iy, iz] < edge)
            {
                edgeindex |= 16;
            }
            //5
            if (matrix3d[dim_x, iy, iz] < edge)
            {
                edgeindex |= 32;
            }
            //6
            if (matrix3d[dim_x, dim_y, iz] < edge)
            {
                edgeindex |= 64;
            }
            //7
            if (matrix3d[ix, dim_y, iz] < edge)
            {
                edgeindex |= 128;
            }
        }

        /// <summary>
        /// This function wraps the entire Marhing Cube Algorithm process.
        /// </summary>
        /// <returns>Returns false if an error ocurrs and true otherwise</returns>
        public bool Generate_Model()
        {
            label_progress.Text = string.Empty;
            progressbar.Maximum = 1;
            progressbar.Value = 1;
            progressbar.Maximum = dimz-2;
            cancel.Enabled = true;
            finish.Enabled = false;
            thread.RunWorkerAsync();
            return true;
        }


    }
}
