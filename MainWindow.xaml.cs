using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace Tower_of_Hanoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        /// <summary>
        /// Variables
        /// </summary>

        private int Turn = 0;
        private int Disk1Turn = -1;
        private int x1 = 0;
        private int x2 = 0;
        private int x3 = 0;
        private int x4 = 0;
        private int x5 = 0;
        private int x6 = 0;
        private int x7 = 0;
        private int x8 = 0;

        private Double y1 = 0;
        private Double y2 = 1;
        private Double y3 = 2;
        private Double y4 = 3;
        private Double y5 = 4;
        private Double y6 = 5;
        private Double y7 = 6;
        private Double y8 = 7;

        private int z1 = 1;
        private int z2 = 2;
        private int z3 = 3;
        private int z4 = 4;
        private int z5 = 5;
        private int z6 = 6;
        private int z7 = 7;
        private int z8 = 8;

        private double zx0;
        private double zx1;
        private double zx2;

        Point3DCollection  list_X0 = new Point3DCollection();
        Point3DCollection  list_X1 = new Point3DCollection();
        Point3DCollection  list_X2 = new Point3DCollection();

        private Double KleinsteZ;

        #endregion
               
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            #region End 
            if (Turn == 255)
            {
                MessageBox.Show("Finished!");
                return;
            }
            #endregion

            #region Turn variables
            ///<summary>
            ///Turn variable, Disk 1 turn variable and 3DPoint variables
            ///</summary>
            ++Turn;
            Disk1Turn = Disk1Turn * -1;
            
            Point3D point1 = new Point3D(x1, y1, z1);
            Point3D point2 = new Point3D(x2, y2, z2);
            Point3D point3 = new Point3D(x3, y3, z3);
            Point3D point4 = new Point3D(x4, y4, z4);
            Point3D point5 = new Point3D(x5, y5, z5);
            Point3D point6 = new Point3D(x6, y6, z6);
            Point3D point7 = new Point3D(x7, y7, z7);
            Point3D point8 = new Point3D(x8, y8, z8);
            #endregion

            #region First Turn
            if (Turn == 1)
            {
                list_X0.Add(point3);
                list_X0.Add(point4);
                list_X0.Add(point5);
                list_X0.Add(point6);
                list_X0.Add(point7);
                list_X0.Add(point8);
            }
            #endregion
                        
            #region Disk 1
            if (Disk1Turn > 0)
            {
                x1++;
                if (x1 == 3) x1 = 0;
                list_X0.Remove(point1);
                list_X1.Remove(point1);
                list_X2.Remove(point1);
                point1.X = x1;                                       
                
                if (x1 == 0)
                {
                    if (list_X0.Count > 0)
                        y1 =list_X0.Min(y => y.Y) - 1 ;
                    else
                        y1 = 7;
                    point1.Y = y1;
                    list_X0.Add(point1);
                }
                else if(x1 == 1)
                {
                    if (list_X1.Count > 0)
                        y1 = list_X1.Min(y => y.Y) - 1 ;
                    else
                        y1 = 7;
                    point1.Y = y1;
                    list_X1.Add(point1);
                }
                else
                {
                    if (list_X2.Count > 0)
                        y1 = list_X2.Min(y => y.Y) - 1 ;
                    else
                        y1 = 7;
                    point1.Y = y1;
                    list_X2.Add(point1);
                }
                Grid.SetColumn(Disk1, x1);
                int yy1 = (int)y1;
                Grid.SetRow(Disk1, yy1);
            }

            #endregion

            #region Disk 2
            if ((Turn - 2) % 4 == 0 || Turn == 2)
            {
                list_X0.Remove(point2);
                list_X1.Remove(point2);
                list_X2.Remove(point2);
                x2 = x1 + 1;
                if (x2 == 3) x2 = 0;
                point2.X = x2;
                if (x2 == 0)
                {
                    if (list_X0.Count > 0)
                        y2 = list_X0.Min(y => y.Y) - 1;
                    else
                        y2 = 7;
                    point2.Y = y2;
                    list_X0.Add(point2);

                }
                else if (x2 == 1)
                {
                    if (list_X1.Count > 0)
                        y2 = list_X1.Min(y => y.Y) - 1;
                    else
                        y2 = 7;
                    point2.Y = y2;
                    list_X1.Add(point2);
                }
                else
                {
                    if (list_X2.Count > 0)
                        y2 = list_X2.Min(y => y.Y) - 1;
                    else
                        y2 = 7;
                    point2.Y = y2;
                    list_X2.Add(point2);
                }
                Grid.SetColumn(Disk2, x2);
                int yy2= (int)y2;
                Grid.SetRow(Disk2, yy2);
            }
            #endregion

            #region Other Disks
            if (Turn % 4 == 0)
            {
                #region Variables
                ///<sumarry>
                ///Variables to see wich disk should turn and from to wich pole it should turn
                ///Also calculates the vertical position as in if there are disks beneath the postion the disk goes to
                ///</sumarry>
                
                if (!list_X0.Contains(point1))
                {
                    if (list_X0.Any())
                    { zx0 = list_X0.Min(z => z.Z); }
                    else
                    { zx0 = 9; }
                }
                else
                {
                    zx0 = 10;
                }
                
                if (!list_X1.Contains(point1))
                {
                    if (list_X1.Any())
                    { zx1 = list_X1.Min(z => z.Z); }
                    else
                    { zx1 = 9; }
                }
                else
                {
                    zx1 = 10;
                }

                if (!list_X2.Contains(point1))
                {
                    if (list_X2.Any())
                    { zx2 = list_X2.Min(z => z.Z); }
                    else
                    { zx2 = 9; }
                }
                else
                {
                    zx2 = 10;
                }

            KleinsteZ = Math.Min(zx0, Math.Min(zx1, zx2));
                #endregion

                switch (KleinsteZ)
                {
                    #region Disk 3
                    case 3:

                        if (zx0 == KleinsteZ)
                        { list_X0.Remove(point3);
                            if (Math.Min(zx1, zx2) == zx1)
                                {
                                    x3 = 1;
                                    if (zx1 != 9)
                                        y3 = list_X1.Min(y => y.Y) - 1;
                                    else
                                        y3 = 7;

                                    point3.X = x3;
                                    point3.Y = y3;
                                    list_X1.Add(point3);
                                }
                            else
                                {
                                    x3 = 2;
                                    if (zx2 != 9)
                                        y3 = list_X2.Min(y => y.Y) - 1;
                                    else
                                        y3 = 7;

                                    point3.X = x3;
                                    point3.Y = y3;
                                    list_X2.Add(point3);
                                }
                        }
                
                        else if (zx1 == KleinsteZ)
                        { list_X1.Remove(point3);
                            if (Math.Min(zx0, zx2) == zx0)
                                {
                                    x3 = 0;
                                    if (zx0 != 9)
                                        y3 = list_X0.Min(y => y.Y) - 1;
                                    else
                                        y3 = 7;

                                    point3.X = x3;
                                    point3.Y = y3;
                                    list_X0.Add(point3);
                                }
                            else
                                {
                                    x3 = 2;
                                    if (zx2 != 9)
                                        y3 = list_X2.Min(y => y.Y) - 1;
                                    else
                                        y3 = 7;

                                    point3.X = x3;
                                    point3.Y = y3;
                                    list_X2.Add(point3);
                                }
                            
                        }
                        else
                        { list_X2.Remove(point3);
                            if (Math.Min(zx0, zx1) == zx0)
                                {
                                    x3 = 0;
                                    if (zx0 != 9)
                                        y3 = list_X0.Min(y => y.Y) - 1;
                                    else
                                        y3 = 7;

                                    point3.X = x3;
                                    point3.Y = y3;
                                    list_X0.Add(point3);
                                }
                            else
                                {
                                    x3 = 1;
                                    if (zx1 != 9)
                                        y3 = list_X1.Min(y => y.Y) - 1;
                                    else
                                        y3 = 7;

                                    point3.X = x3;
                                    point3.Y = y3;
                                    list_X1.Add(point3);
                                }
                        }
                        Grid.SetColumn(Disk3, x3);
                        int yy3 = (int)y3;
                        Grid.SetRow(Disk3, yy3);
                        break;
                    #endregion
                    #region Disk 4
                    case 4:
                        if (zx0 == KleinsteZ)
                        { list_X0.Remove(point4);
                            if (Math.Min(zx1, zx2) == zx1)
                                {
                                    x4 = 1;
                                    if (zx1 != 9)
                                        y4 = list_X1.Min(y => y.Y) - 1;
                                    else
                                        y4 = 7;

                                    point4.X = x4;
                                    point4.Y = y4;
                                    list_X1.Add(point4);
                                }
                            else
                                {
                                    x4 = 2;
                                    if (zx2 != 9)
                                        y4 = list_X2.Min(y => y.Y) - 1;
                                    else
                                        y4 = 7;

                                    point4.X = x4;
                                    point4.Y = y4;
                                    list_X2.Add(point4);
                                }
                        }
                        else if (zx1 == KleinsteZ)
                        { list_X1.Remove(point4);
                            if (Math.Min(zx0, zx2) == zx0)
                                {
                                    x4 = 0;
                                    if (zx0 != 9)
                                        y4 = list_X0.Min(y => y.Y) - 1;
                                    else
                                        y4 = 7;

                                    point4.X = x4;
                                    point4.Y = y4;
                                    list_X0.Add(point4);
                                }
                            else
                                {
                                    x4 = 2;
                                    if (zx2 != 9)
                                        y4 = list_X2.Min(y => y.Y) - 1;
                                    else
                                        y4 = 7;

                                    point4.X = x4;
                                    point4.Y = y4;
                                    list_X2.Add(point4);
                                }

                         }
                        else
                        { list_X2.Remove(point4);
                            if (Math.Min(zx0, zx1) == zx0)
                                {
                                    x4 = 0;
                                    if (zx0 != 9)
                                        y4 = list_X0.Min(y => y.Y) - 1;
                                    else
                                        y4 = 7;

                                    point4.X = x4;
                                    point4.Y = y4;
                                    list_X0.Add(point4);
                                }
                            else
                                {
                                    x4 = 1;
                                    if (zx1 != 9)
                                        y4 = list_X1.Min(y => y.Y) - 1;
                                    else
                                        y4 = 7;

                                    point4.X = x4;
                                    point4.Y = y4;
                                    list_X1.Add(point4);
                                }
                        }

                        Grid.SetColumn(Disk4, x4);
                        int yy4 = (int)y4;
                        Grid.SetRow(Disk4, yy4);
                        break;
                    #endregion
                    #region Disk 5
                    case 5:
                        if (zx0 == KleinsteZ)
                        {
                            list_X0.Remove(point5);
                            if (Math.Min(zx1, zx2) == zx1)
                            {
                                x5 = 1;
                                if (zx1 != 9)
                                    y5 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y5 = 7;

                                point5.X = x5;
                                point5.Y = y5;
                                list_X1.Add(point5);
                            }
                            else
                            {
                                x5 = 2;
                                if (zx2 != 9)
                                    y5 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y5 = 7;

                                point5.X = x5;
                                point5.Y = y5;
                                list_X2.Add(point5);
                            }
                        }
                        else if (zx1 == KleinsteZ)
                        {
                            list_X1.Remove(point5);
                            if (Math.Min(zx0, zx2) == zx0)
                            {
                                x5 = 0;
                                if (zx0 != 9)
                                    y5 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y5 = 7;

                                point5.X = x5;
                                point5.Y = y5;
                                list_X0.Add(point5);
                            }
                            else
                            {
                                x5 = 2;
                                if (zx2 != 9)
                                    y5 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y5 = 7;

                                point5.X = x5;
                                point5.Y = y5;
                                list_X2.Add(point5);
                            }

                        }
                        else
                        {
                            list_X2.Remove(point5);
                            if (Math.Min(zx0, zx1) == zx0)
                            {
                                x5 = 0;
                                if (zx0 != 9)
                                    y5 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y5 = 7;

                                point5.X = x5;
                                point5.Y = y5;
                                list_X0.Add(point5);
                            }
                            else
                            {
                                x5 = 1;
                                if (zx1 != 9)
                                    y5 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y5 = 7;

                                point5.X = x5;
                                point5.Y = y5;
                                list_X1.Add(point5);
                            }
                        }

                        Grid.SetColumn(Disk5, x5);
                        int yy5 = (int)y5;
                        Grid.SetRow(Disk5, yy5);
                        break;
                    #endregion
                    #region Disk 6
                    case 6:
                        if (zx0 == KleinsteZ)
                        {
                            list_X0.Remove(point6);
                            if (Math.Min(zx1, zx2) == zx1)
                            {
                                x6 = 1;
                                if (zx1 != 9)
                                    y6 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y6 = 7;

                                point6.X = x6;
                                point6.Y = y6;
                                list_X1.Add(point6);
                            }
                            else
                            {
                                x6 = 2;
                                if (zx2 != 9)
                                    y6 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y6 = 7;

                                point6.X = x6;
                                point6.Y = y6;
                                list_X2.Add(point6);
                            }
                        }
                        else if (zx1 == KleinsteZ)
                        {
                            list_X1.Remove(point6);
                            if (Math.Min(zx0, zx2) == zx0)
                            {
                                x6 = 0;
                                if (zx0 != 9)
                                    y6 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y6 = 7;

                                point6.X = x6;
                                point6.Y = y6;
                                list_X0.Add(point6);
                            }
                            else
                            {
                                x6 = 2;
                                if (zx2 != 9)
                                    y6 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y6 = 7;

                                point6.X = x6;
                                point6.Y = y6;
                                list_X2.Add(point6);
                            }

                        }
                        else
                        {
                            list_X2.Remove(point6);
                            if (Math.Min(zx0, zx1) == zx0)
                            {
                                x6 = 0;
                                if (zx0 != 9)
                                    y6 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y6 = 7;

                                point6.X = x6;
                                point6.Y = y6;
                                list_X0.Add(point6);
                            }
                            else
                            {
                                x6 = 1;
                                if (zx1 != 9)
                                    y6 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y6 = 7;

                                point6.X = x6;
                                point6.Y = y6;
                                list_X1.Add(point6);
                            }
                        }

                        Grid.SetColumn(Disk6, x6);
                        int yy6 = (int)y6;
                        Grid.SetRow(Disk6, yy6);
                        break;
                    #endregion
                    #region Disk 7
                    case 7:
                        if (zx0 == KleinsteZ)
                        {
                            list_X0.Remove(point7);
                            if (Math.Min(zx1, zx2) == zx1)
                            {
                                x7 = 1;
                                if (zx1 != 9)
                                    y7 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y7 = 7;

                                point7.X = x7;
                                point7.Y = y7;
                                list_X1.Add(point7);
                            }
                            else
                            {
                                x7 = 2;
                                if (zx2 != 9)
                                    y7 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y7 = 7;

                                point7.X = x7;
                                point7.Y = y7;
                                list_X2.Add(point7);
                            }
                        }
                        else if (zx1 == KleinsteZ)
                        {
                            list_X1.Remove(point7);
                            if (Math.Min(zx0, zx2) == zx0)
                            {
                                x7 = 0;
                                if (zx0 != 9)
                                    y7 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y7 = 7;

                                point7.X = x7;
                                point7.Y = y7;
                                list_X0.Add(point7);
                            }
                            else
                            {
                                x7 = 2;
                                if (zx2 != 9)
                                    y7 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y7 = 7;

                                point7.X = x7;
                                point7.Y = y7;
                                list_X2.Add(point7);
                            }

                        }
                        else
                        {
                            list_X2.Remove(point7);
                            if (Math.Min(zx0, zx1) == zx0)
                            {
                                x7 = 0;
                                if (zx0 != 9)
                                    y7 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y7 = 7;

                                point7.X = x7;
                                point7.Y = y7;
                                list_X0.Add(point7);
                            }
                            else
                            {
                                x7 = 1;
                                if (zx1 != 9)
                                    y7 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y7 = 7;

                                point7.X = x7;
                                point7.Y = y7;
                                list_X1.Add(point7);
                            }
                        }

                        Grid.SetColumn(Disk7, x7);
                        int yy7 = (int)y7;
                        Grid.SetRow(Disk7, yy7);
                        break;
                    #endregion
                    #region Disk 8
                    case 8:
                        if (zx0 == KleinsteZ)
                        {
                            list_X0.Remove(point8);
                            if (Math.Min(zx1, zx2) == zx1)
                            {
                                x8 = 1;
                                if (zx1 != 9)
                                    y8 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y8 = 7;

                                point8.X = x8;
                                point8.Y = y8;
                                list_X1.Add(point8);
                            }
                            else
                            {
                                x8 = 2;
                                if (zx2 != 9)
                                    y8 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y8 = 7;

                                point8.X = x8;
                                point8.Y = y8;
                                list_X2.Add(point8);
                            }
                        }
                        else if (zx1 == KleinsteZ)
                        {
                            list_X1.Remove(point8);
                            if (Math.Min(zx0, zx2) == zx0)
                            {
                                x8 = 0;
                                if (zx0 != 9)
                                    y8 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y8 = 7;

                                point8.X = x8;
                                point8.Y = y8;
                                list_X0.Add(point8);
                            }
                            else
                            {
                                x8 = 2;
                                if (zx2 != 9)
                                    y8 = list_X2.Min(y => y.Y) - 1;
                                else
                                    y8 = 7;

                                point8.X = x8;
                                point8.Y = y8;
                                list_X2.Add(point8);
                            }

                        }
                        else
                        {
                            list_X2.Remove(point8);
                            if (Math.Min(zx0, zx1) == zx0)
                            {
                                x8 = 0;
                                if (zx0 != 9)
                                    y8 = list_X0.Min(y => y.Y) - 1;
                                else
                                    y8 = 7;

                                point8.X = x8;
                                point8.Y = y8;
                                list_X0.Add(point8);
                            }
                            else
                            {
                                x8 = 1;
                                if (zx1 != 9)
                                    y8 = list_X1.Min(y => y.Y) - 1;
                                else
                                    y8 = 7;

                                point8.X = x8;
                                point8.Y = y8;
                                list_X1.Add(point8);
                            }
                        }

                        Grid.SetColumn(Disk8, x8);
                        int yy8 = (int)y8;
                        Grid.SetRow(Disk8, yy8);
                        break;
                        #endregion
                }
            }
            #endregion

            #region Show turn at label
            BindingExpression be = Turn_count.GetBindingExpression(TextBox.TextProperty);
            Turn_count.Text = Turn.ToString();
            be.UpdateSource();
            #endregion

        }

    }

    }
