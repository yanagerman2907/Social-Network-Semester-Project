   M S B A M L   `   `   `  ����K  GWpfSocialNetwork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfSocialNetworkWpfSocialNetwork  5   	   '    !WpfSocialNetwork.Windows.HomePage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WpfSocialNetwork     5   	   $	��600��5   	   $
��1000��6     � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   $��Georgia)�5   	   .��5      � -   5        
������� � ColumnDefinitions 5   
   �� 5      $	��800 �6   �� 5      $	��200 �6   .+�5   
   � $��194,0,6,0.4q�6   $��2��6$   .+�5      � -   5         �Name$ MainGrid��5      $��Center=�6%   $��Center=�6B   $��0��6]   5      � 5   
   ���#add7db$��-2,-5,602,4.4q�6$   .+�5      �� -   5         ��Name$ ListViewMenu��5      $��0 40 0 0q�6-   ���#add7db$��0q�6T   ���#7451eb$��18(�6}    �� SelectedIndex$ 0��6�   .@�5      �� $��70��6   .��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��Timeline��6$   $��17(�64   $��Center=�6B   $
��20 0q�6]   5      5      �� 5      $��70��6   .��5       �� $ 
Horizontal=�6!   .+�5!      �� $��My Page��6$   $��17(�63   $��Center=�6A   $
��20 0q�6\   5"      5#      �� 5$      $��70��6   .��5%      �� $ 
Horizontal=�6!   .+�5&      �� $��Settings��6$   $��17(�64   $��Center=�6B   $
��20 0q�6]   5'      5(      5)      �� 5+      $�� Search Friends��6   $��Center=�6/   $��20,120,20,130.667q�6J   ���#7451eb$��17(�6z   �� 5,      -   5         ��Name$ txtLogin��5,      $��20,310,30,230q�6(   $��Login��6?   $��15(�6L   ���#00adbd
�������6o   �� 5-      -   5         ��Name$ 	btnSearch��5-      $��Search��6(   ���#00838f$��20,350,30,190q�6f   
�������
���\\��$��13(�6�   �� 5.      -   5        $ 	btnLogout��5.      $��Log Out��6(   ���#00adbd$��71,512,73,28.4q�6g   
�������
���\\��$��13(�6�   5/      50      51      