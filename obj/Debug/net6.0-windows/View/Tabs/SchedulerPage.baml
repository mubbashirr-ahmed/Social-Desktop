   M S B A M L   `   `   `  ����K  GSocial Publisher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Social_Publisher.View.TabsSocial_Publisher.View.Tabs  5      .    (Social_Publisher.View.Tabs.SchedulerPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Social_Publisher.View.Tabs     5        N� Title$  SchedulerPage��5	      .��5      � 
�������.+�5   
   �� $��2q�6   
���   �$
��Left=�69   $	��200��6T   $	��Top=�6a   $��	54,50,0,0q�6y   ._�5      ��  �� Orientation$ Vertical=�6   .+�5      �� $��Start Schedule��6   $��20, 10, 20, 10q�61   $
��Left=�6I   $��12(�6d   $
��Bold"�6r   $	��Top=�6�   (  "System.Windows.Controls.DatePicker  5      -   5        $��startDatePicker��5      $
��left=�64   $��20, 0, 20, 20q�6O   $	��Top=�6f      Text$ 
Start Date��6~   �� 5      $��End Schedule: ��6   $��20, 10, 20, 10q�61   $
��Left=�6I   $��12(�6d   $	��Top=�6r   $
��Bold"�6�     5      -   5          Name$ endDatePicker��5      $
��Left=�64   $��20, 0, 20, 20q�6O   $	��Top=�6f   $ 
Start Date��6~   5      5      �� 5   
   
���   �$��2q�6%   $
��Left=�69   $	��200��6T   $	��Top=�6a   $��
210,50,0,0q�6y   ._�5      �� $ Vertical=�6   .+�5      �� $��Post Order ��6   $
��Left=�6.   $��10q�6I   $��12(�6U   $	��Top=�6c   $
��Bold"�6{   �� 5      $
��Left=�6   $	��183��68   .+�5      	� $��Process in Order��6"   $��10, 5, 0, 0q�6=   	� 5      $��Process Randomly��6"   $��10, 5, 0, 0q�6=   5      �� 5      $��Time Ordering��6   $
��Left=�60   $��0, 10, 0, 0q�6K   $��12(�6`   $	��Top=�6n   $
��Bold"�6�   �� 5      $��	0,10,10,0q�6   $	��Top=�60   $
��Left=�6H   $	��307��6c   .+�5      	� $ ��Process at a specific time��6"   $��10, 5, 0, 0q�6G   	� 5       $*��$Process at a Random time between UTC��6"   $��10, 5, 0, 0q�6Q   	� 5!      $��Process at a Random time��6"   $��10, 5, 0, 0q�6E   5"      5#      5$      �� 5%   
   
���   �$��2q�6%   $
��Left=�69   $	��200��6T   $	��Top=�6a   $��540, 50, 0, 0q�6y   ._�5&      �� $ Vertical=�6   .+�5'      �� $��Post Destination��6   $��10, 10, 20, 10q�63   $
��Left=�6K   $��12(�6f   $
��Bold"�6t   $	��Top=�6�   �� 5(      -   5         ��Name$ 	cFacebook��5(   A   $��Facebook��6   $��	20, 0,0,0q�6.   6A   �� 5)      -   5        $ cTwitter��5)   A   $��Twitter��6   $��
20, 10,0,0q�6-   6A   5+      5,      �� 5-   
   -   5         ��Name$ bPublish��5-      $��Publish��6#   $��	0,0,30,30q�65   $��Right=�6H   $��89��6d   $��32��6o   $��Bottom=�6{   $��1��6�   6�   �� 5.   
   -   5         ��Name$ lMessage��5.      $
��Bold"�6"   $>��8Do not close this window un less all posts are published��64   $��232,310,196,114q�6w   �� 5/   
   -   5        $ 
progressFB��5/      $��Posting 0 out of 0��6$   $��293,346,257,78q�6A   $y�Hidden=�6Y   �� 50   
   -   5        $ 
progressTW��50      $��Posting 0 out of 0��6$   $��293,377,257,46q�6A   $y�Hidden=�6Y   52      53      