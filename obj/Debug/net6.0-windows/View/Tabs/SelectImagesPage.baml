   M S B A M L   `   `   `  ����K  GSocial Publisher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Social_Publisher.View.TabsSocial_Publisher.View.Tabs  5      1    +Social_Publisher.View.Tabs.SelectImagesPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Social_Publisher.View.Tabs     5        N� Title$  SelectImagesPage��5	      .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��500* �6   �� 5      $��95 �6   .+�5   
   � $��0��6   .+�5      �� -   5         ��Name$ imageListBox��5      $��
10,50,10,5q�6,   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� 
���   �$��2q�69   ._�5   "   �� $ 
Horizontal=�6-   .+�5   &   �� $��/Assets/facebook.png��6,   $��24��6J   $��24��6U   $��10q�6a   �� 5   &   $	��250��6K   $	��250��6W   $��10q�6d   $
��Fill=�6p   ��6,   �� *ImageSource+6p   �� 5   &   $��10q�6I   $	��250��6U   $	��300��6b   ��6.   �� *	fbContent+6b   5   #   5      �� 5      
���   �$��2q�69   $��5, 0,0,0q�6M   ._�5   "   �� $ 
Horizontal=�6-   .+�5    &   �� $��/Assets/twitter.png��6,   $��24��6I   $��24��6T   $��10q�6`   �� 5!   &   $	��250��6K   $	��250��6W   $��10q�6d   $
��Fill=�6p   ��6,   �� *ImageSource+6p   �� 5"   &   $��10q�6I   $	��250��6U   $	��300��6b   ��6.   �� *	twContent+6b   5#   #   5$      �� 5&      
���   �$��2q�69   $��5, 0,0,0q�6M   ._�5'   "   �� $ 
Horizontal=�6-   .+�5(   &   �� $��/Assets/pinterest.png��6,   $��24��6K   $��24��6V   $��10q�6b   �� 5)   &   $	��200��6K   $	��250��6W   $��10q�6d   $
��Fill=�6p   ��6,   �� *ImageSource+6p   �� 5*   &   $ Vertical=�61   .+�5+   *   �� $��	Add Title��60   $
��Left=�6D   $��10,5,0,0q�6_   $��10(�6q   �� 5,   *   $��	10,0,10,0q�63   $��20��6F   $	��200��6R   �� 5-   *   $+��%Tell Everyone what this post is about��60   $
��Left=�6`   $��10,5,0,0q�6{   $��10(�6�   �� 5.   *   $��	10,0,10,0q�63   $��20��6F   $	��200��6R   �� 5/   *   $��Add Alt text��60   $
��Left=�6G   $��10,5,0,0q�6b   $��10(�6t   �� 50   *   $��	10,0,10,0q�63   $��20��6F   $	��200��6R   �� 51   *   $��Add Destination Link��60   $
��Left=�6O   $��10,5,0,0q�6j   $��10(�6|   �� 52   *   $��	10,0,10,0q�63   $��20��6F   $	��200��6R   �� 53   *   $��Add Description��60   $
��Left=�6J   $��	10,10,0,0q�6e   $��10(�6x   �� 54   *   $��	10,0,10,0q�63   $��20��6F   $	��200��6R   55   '   56   #   57      5:      5;      5=      �� 5>      -   5        $��
Add Images��5>      $��Right=�6*   $��30��6F   $��80��6R   $	��Top=�6]   $��	0,10,10,0q�6u   6�   �� 5?      $��Select Media to Schedule��6   $��12(�67   $
��Bold"�6E   $
��Left=�6X   $��5,10,0,0q�6s   $	��Top=�6�   $	��160��6�   5@      � 5A   
   $��1��6   .+�5B      �� -   5         ��Name$ 	clearList��5B      $��/Assets/deleteall.png��6'   $��24��6F   $��24��6Q   5C      5D      5E      